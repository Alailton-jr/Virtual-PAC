#ifndef MY_SOCKET_H  // If not defined
#define MY_SOCKET_H  // Define it

#include "stdint.h"
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
typedef struct eth{
    int32_t socket; // Socket Id
    struct sockaddr_ll bind_addr; // Socket address
    int32_t bind_addrSize; // Socket address size
    int32_t  if_index; // Interface index
    uint8_t  if_name[128]; // Interface name
    uint32_t fanout_grp; // Fanout group
    uint8_t  *tx_buffer; // Tx buffer
    uint32_t  tx_size; // Tx buffer size
    uint8_t  *rx_buffer; // Rx buffer
    uint32_t rx_size; // Rx buffer size
    uint32_t msgvec_vlen; // msg Vector length
}eth_t;

/*
    * Socket setup
    * @param eth: Ethernet socket structure
    * @param txSize: Tx buffer size
    * @param rxSize: Rx buffer size
*/
void socketSetup(eth_t* eth, uint32_t txSize, uint32_t rxSize); 

/*
    * Socket cleanup
    * @param eth: Ethernet socket structure
*/
void socketCleanup(eth_t* eth);

/*
    * Create socket
    * @param eth: Ethernet socket structure
    * @param ifName: Interface name
*/
int32_t createSocket(eth_t *eth, char* ifName, uint8_t pushFrames2driver, uint8_t txRing, uint8_t timestamping, uint8_t fanout, uint8_t rxTimeout);

void configMsgHdr(struct msghdr *msg_hdr, struct iovec* iov, eth_t* eth);

#endif //MY_SOCKET_H