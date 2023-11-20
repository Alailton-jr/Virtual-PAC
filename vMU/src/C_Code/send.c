#include <stdio.h>
#include <pthread.h>
#include "timers.h"
#include "send.h"

#define EXIT_SUCCESS 0


void send_now(struct eth_t *eth) {

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
        if (tx_bytes == -1) {
            perror("sendmsg");
            return;
        }
        i++;
        if (i == 100) {
            printf("Sent %d packets\n", i);
            i = 0;
            break;
        }
    }

}

int main(int argc, char *argv[])
{   
    struct eth_t eth;

    socketSetup(&eth, 120, 2028);

    if (createSocket(&eth, "eno1") != 0){
        printf("Error Creating Socket");
        socketCleanup(&eth);
        return -1;
    }

    hexStream2Buffer(eth.tx_buffer);

    // for (int i = 0; i< 10000;i++)
    send_now(&eth);
    // receive_rx(&eth, process);
    socketCleanup(&eth);

    return 0;
}