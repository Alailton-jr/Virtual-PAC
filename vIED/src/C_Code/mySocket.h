#ifndef MY_SOCKET_H  // If not defined
#define MY_SOCKET_H  // Define it

#include <sys/types.h>
#include <sys/socket.h>
#include <net/ethernet.h>
#include <linux/if_packet.h> 
#include <linux/net_tstamp.h>
#include <net/if.h>
#include <ifaddrs.h>          
#include <arpa/inet.h>        
#include <linux/sockios.h>
#include <sys/ioctl.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

typedef void (*socket_handler)(uint8_t* frame, uint32_t len);

/*
    * Ethernet socket structure
    * @param socket: Socket Id
    * @param bind_addr: Socket address
*/
struct eth_t
{
    int32_t socket; // Socket Id
    struct sockaddr_ll bind_addr; // Socket address
    int32_t bind_addrSize; // Socket address size
    int32_t  if_index; // Interface index
    uint8_t  if_name[IF_NAMESIZE]; // Interface name
    uint32_t fanout_grp; // Fanout group
    uint8_t  *tx_buffer; // Tx buffer
    uint32_t  tx_size; // Tx buffer size
    uint8_t  *rx_buffer; // Rx buffer
    uint32_t rx_size; // Rx buffer size
    uint32_t msgvec_vlen; // msg Vector length
}typedef eth_t;

/*
    * Socket setup
    * @param eth: Ethernet socket structure
    * @param txSize: Tx buffer size
    * @param rxSize: Rx buffer size
*/
void socketSetup(struct eth_t* eth, uint32_t txSize, uint32_t rxSize){

    if (txSize > 0){
        eth->tx_buffer = (uint8_t*) malloc(txSize * sizeof(uint8_t));
        memset(eth->tx_buffer, 0, txSize);
    }
    if (rxSize > 0){
        eth->rx_buffer = (uint8_t*) malloc(rxSize * sizeof(uint8_t));
        memset(eth->rx_buffer, 0, rxSize);
    }
    eth->tx_size = txSize;
    eth->rx_size = rxSize;
}

void socketCleanup(struct eth_t* eth){
    if (eth->tx_size){
        free(eth->tx_buffer);
    }
    if (eth->rx_size){
        free(eth->rx_buffer);
    }
    close(eth->socket);
}

int32_t createSocket(struct eth_t *eth, char* ifName) {

    // Create a raw socket
    eth->socket = socket(AF_PACKET, SOCK_RAW, htons(ETH_P_ALL));
   
    if (eth->socket == -1) {
        printf("Can't create AF_PACKET socket");
        return EXIT_FAILURE;
    }

    eth->if_index = if_nametoindex(ifName);

    // Bind socket to interface
    eth->bind_addrSize = sizeof(eth->bind_addr);
    memset(&eth->bind_addr, 0, sizeof(eth->bind_addr));
    eth->bind_addr.sll_family   = AF_PACKET;
    eth->bind_addr.sll_protocol = htons(ETH_P_ALL);
    eth->bind_addr.sll_ifindex  = eth->if_index;

    // Comented for tests
    // // Bypass the kernel qdisc layer and push frames directly to the driver
    // static const int32_t sock_qdisc_bypass = 1;
    // if (setsockopt(eth->socket, SOL_PACKET, PACKET_QDISC_BYPASS, &sock_qdisc_bypass, sizeof(sock_qdisc_bypass)) == -1) {
    //     printf("Can't enable QDISC bypass on socket");
    //     return EXIT_FAILURE;
    // }

    // Enable Tx ring to skip over malformed frames
    static const int32_t sock_discard = 1;
    if (setsockopt(eth->socket, SOL_PACKET, PACKET_LOSS, &sock_discard, sizeof(sock_discard)) == -1) {
        printf("Can't enable PACKET_LOSS on socket");
        return EXIT_FAILURE;
    }


    // Set the socket Rx timestamping settings
    static int32_t timesource = 0;
    timesource |= SOF_TIMESTAMPING_RX_HARDWARE;    // Set Rx timestamps to hardware
    timesource |= SOF_TIMESTAMPING_RAW_HARDWARE;   // Use hardware time stamps for reporting
    if (setsockopt(eth->socket, SOL_PACKET, PACKET_TIMESTAMP, &timesource, sizeof(timesource)) == -1) {
        printf("Can't set socket Rx timestamp source");
    }

    static const uint16_t fanout_type = PACKET_FANOUT_CPU;
    static uint32_t fanout_arg;
    fanout_arg = (eth->fanout_grp | (fanout_type << 16));
    if (setsockopt(eth->socket, SOL_PACKET, PACKET_FANOUT, &fanout_arg, sizeof(fanout_arg)) < 0) {
        printf("Can't configure socket fanout");
        return EXIT_FAILURE;
    }

    return EXIT_SUCCESS;
}

void send_tx(struct eth_t *eth) {

    int32_t tx_bytes;

    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));

    msg_hdr.msg_name = &eth->bind_addr;
    msg_hdr.msg_namelen = eth->bind_addrSize;

    iov.iov_base = eth->tx_buffer;
    iov.iov_len = eth->tx_size;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;
    int i = 0;
    
    while (1) {
        tx_bytes = sendmsg(eth->socket, &msg_hdr, 0);
        i++;
        if (i == 100000) {
            printf("Sent %d packets\n", i);
            i = 0;
            break;
        }
    }

}

void receive_rx(struct eth_t *eth, socket_handler handler) {

    int32_t rx_bytes = 0;

    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));

    msg_hdr.msg_name = &eth->bind_addr;
    msg_hdr.msg_namelen = eth->bind_addrSize;

    iov.iov_base = eth->rx_buffer;
    iov.iov_len = eth->rx_size;

    msg_hdr.msg_name = NULL;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;
    msg_hdr.msg_control = NULL;
    msg_hdr.msg_controllen = 0;

    int i =0;
    while(1) {

        rx_bytes = recvmsg(eth->socket, &msg_hdr, 0);
        if (rx_bytes) {
            handler(eth->rx_buffer, rx_bytes);
            i++;
            if (i == 2) {
                printf("Reveived %d packets\n", i);
                i = 0;
                break;
            }
        }
    }

}

void hexStream2Buffer(uint8_t* buffer)
{
    char x[] = "010ccd040000f3a9f988fded8100800088ba4000006600000000605c800101a2573055800454525443820208c283040000000185010087400000000b00000000000000f200000000000000080000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
    int debug = sizeof(x);
    for (int i=0; i < sizeof(x); i += 2)
    {
        sscanf(&x[i], "%02hhx", buffer + i/2);
    }
}

void process(uint8_t* frame, uint32_t len)
{
    printf("Received %d bytes\n", len);
    for (int i = 0; i < len; i++) {
        printf("0x%x ", frame[i]);
    }
    printf("\n");
}

#endif //MY_SOCKET_H