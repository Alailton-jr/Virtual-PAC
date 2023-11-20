#include "mySocket.h"

#include <unistd.h>

eth_t eth;
eth_t *eth_p;

int main(){

    eth_p = &eth;
    socketSetup(eth_p, 4096, 4096);
    createSocket(eth_p, "eth0");

    uint8_t frame[4096] = {24};

    // for(int i = 0;i<200;i++)
    //     frame[i] = 160;

    int32_t tx_bytes, rx_bytes;
    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));
    msg_hdr.msg_name = &eth.bind_addr;
    msg_hdr.msg_namelen = eth.bind_addrSize;
    // msg_hdr.msg_control = NULL;
    // msg_hdr.msg_controllen = 0;
    iov.iov_base = eth_p->rx_buffer;
    iov.iov_len = eth_p->rx_size;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;
    memset(eth_p->rx_buffer, 0, eth_p->rx_size);
    int i = 0;
    while(i<4){
        // tx_bytes = sendmsg(eth_p->socket, &msg_hdr, 0);
        rx_bytes = recvmsg(eth_p->socket, &msg_hdr,0);
        if (rx_bytes == -1){
            perror("rcvmsg");
        }
        printf("Rx Bytes: %d\n", rx_bytes);
        if (rx_bytes>0){
            // for(int j =0;j<10;j++) printf("%x ",frame[j]);
            // for(int j =0;j<10;j++) printf("%x ",((uint8_t*)msg_hdr.msg_iov[0].iov_base)[j]);
            // for(int j =0;j<10;j++) printf("%x ",((uint8_t*)iov.iov_base)[j]);
            for(int j =0;j<400;j++) printf("%02x ",eth_p->rx_buffer[j]);
            printf("\n");
            i++;
        }
        rx_bytes = 0;
    }

    socketCleanup(eth_p);


    return 0;
}