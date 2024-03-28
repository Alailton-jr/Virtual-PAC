#ifndef MY_SOCKET_H  // If not defined
#define MY_SOCKET_H  // Define it

#include "QualityMeter_lib.h"

typedef void (*socket_handler)(uint8_t* frame, uint32_t len);

/*
    * Ethernet socket structure
    * @param socket: Socket Id
    * @param bind_addr: Socket address
    * @param bind_addrSize: Socket address size
    * @param if_index: Interface index
    * @param if_name: Interface name
    * @param fanout_grp: Fanout group
    * @param tx_buffer: Tx buffer
    * @param tx_size: Tx buffer size
    * @param rx_buffer: Rx buffer
    * @param rx_size: Rx buffer size
    * @param msgvec_vlen: msg Vector length
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

/*
    * Socket cleanup
    * @param eth: Ethernet socket structure
*/
void socketCleanup(struct eth_t* eth){
    if (eth->tx_size){
        free(eth->tx_buffer);
    }
    if (eth->rx_size){
        free(eth->rx_buffer);
    }
    close(eth->socket);
}

/*
    * Create socket
    * @param eth: Ethernet socket structure
    * @param ifName: Interface name
*/
int32_t createSocket(struct eth_t *eth, char* ifName) {

    // Create a raw socket
    eth->socket = socket(AF_PACKET, SOCK_RAW, htons(ETH_P_ALL));
   
    if (eth->socket == -1) {
        printf("Can't create AF_PACKET socket");
        return EXIT_FAILURE;
    }

    eth->if_index = if_nametoindex(ifName); // Get the interface index
    eth->bind_addrSize = sizeof(eth->bind_addr); // Set the bind address size
    memset(&eth->bind_addr, 0, sizeof(eth->bind_addr)); // Clear the bind address
    eth->bind_addr.sll_family   = AF_PACKET; // Set the bind address family to AF_PACKET -> Ethernet
    eth->bind_addr.sll_protocol = htons(ETH_P_ALL); // Set the bind address protocol to ETH_P_ALL -> not promiscuous
    eth->bind_addr.sll_ifindex  = eth->if_index; // Set the bind address interface index

    // Bypass the kernel qdisc layer and push frames directly to the driver
    static const int32_t sock_qdisc_bypass = 1;
    if (setsockopt(eth->socket, SOL_PACKET, PACKET_QDISC_BYPASS, &sock_qdisc_bypass, sizeof(sock_qdisc_bypass)) == -1) {
        printf("Can't enable QDISC bypass on socket");
        return EXIT_FAILURE;
    }

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

    // Set the socket fanout group settings
    static const uint16_t fanout_type = PACKET_FANOUT_CPU;
    static uint32_t fanout_arg;
    fanout_arg = (eth->fanout_grp | (fanout_type << 16));
    if (setsockopt(eth->socket, SOL_PACKET, PACKET_FANOUT, &fanout_arg, sizeof(fanout_arg)) < 0) {
        printf("Can't configure socket fanout");
        return EXIT_FAILURE;
    }

    return EXIT_SUCCESS;
}

#endif //MY_SOCKET_H