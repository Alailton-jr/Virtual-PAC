
#include "mySocket.h"

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
#include <time.h>

/*
    * Socket setup
    * @param eth: Ethernet socket structure
    * @param txSize: Tx buffer size
    * @param rxSize: Rx buffer size
*/
void socketSetup(eth_t* eth, uint32_t txSize, uint32_t rxSize){

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
void socketCleanup(eth_t* eth){
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
int32_t createSocket(eth_t *eth, char* ifName, uint8_t pushFrames2driver, uint8_t txRing, uint8_t timestamping, uint8_t fanout, uint8_t rxTimeout) {

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

    if (pushFrames2driver){
        // Bypass the kernel qdisc layer and push frames directly to the driver
        static const int32_t sock_qdisc_bypass = 1;
        if (setsockopt(eth->socket, SOL_PACKET, PACKET_QDISC_BYPASS, &sock_qdisc_bypass, sizeof(sock_qdisc_bypass)) == -1) {
            printf("Can't enable QDISC bypass on socket");
            return EXIT_FAILURE;
        }
    }
    
    if(txRing){
        // Enable Tx ring to skip over malformed frames
        static const int32_t sock_discard = 1;
        if (setsockopt(eth->socket, SOL_PACKET, PACKET_LOSS, &sock_discard, sizeof(sock_discard)) == -1) {
            printf("Can't enable PACKET_LOSS on socket");
            return EXIT_FAILURE;
        }
    }
    
    if (timestamping){
        // Set the socket Rx timestamping settings
        static int32_t timesource = 0;
        timesource |= SOF_TIMESTAMPING_RX_HARDWARE;    // Set Rx timestamps to hardware
        timesource |= SOF_TIMESTAMPING_RAW_HARDWARE;   // Use hardware time stamps for reporting
        if (setsockopt(eth->socket, SOL_PACKET, PACKET_TIMESTAMP, &timesource, sizeof(timesource)) == -1) {
            printf("Can't set socket Rx timestamp source");
        }
    }

    if (fanout){
        // Set the socket fanout group settings
        static const uint16_t fanout_type = PACKET_FANOUT_CPU;
        static uint32_t fanout_arg;
        fanout_arg = (eth->fanout_grp | (fanout_type << 16));
        if (setsockopt(eth->socket, SOL_PACKET, PACKET_FANOUT, &fanout_arg, sizeof(fanout_arg)) < 0) {
            printf("Can't configure socket fanout");
            return EXIT_FAILURE;
        }
    }
    
    if (rxTimeout){
        // Set the socket receive timeout
        struct timeval tv;
        tv.tv_sec = 4;
        tv.tv_usec = 0;
        if (setsockopt(eth->socket, SOL_SOCKET, SO_RCVTIMEO, &tv, sizeof(tv)) < 0) {
            perror("setsockopt failed");
            return EXIT_FAILURE;
        }
    }

    return EXIT_SUCCESS;
}

void configMsgHdr(struct msghdr *msg_hdr, struct iovec* iov, eth_t* eth){
    memset(msg_hdr, 0, sizeof(msg_hdr));
    memset(iov, 0, sizeof(iov));

    msg_hdr->msg_name = &eth->bind_addr;
    msg_hdr->msg_namelen = eth->bind_addrSize;

    iov->iov_base = eth->rx_buffer;
    iov->iov_len = eth->rx_size;
    
    // msg_hdr->msg_name = NULL;
    msg_hdr->msg_iov = iov;
    msg_hdr->msg_iovlen = 1;
    msg_hdr->msg_control = NULL;
    msg_hdr->msg_controllen = 0;
}



