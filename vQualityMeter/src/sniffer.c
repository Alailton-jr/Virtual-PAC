

#include "sniffer.h"
#include "mySocket.h"
#include "threadTask.h"
#include "util.h"

#include <stdio.h>
#include <stdint.h>
#include <sys/socket.h>
#include <time.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>



// Continuous Capture by pre-defined number of SV
// Timed Capture by specific SV, save the waveform
// Timed Capture to search for SV (Optional to save the waveform)


int snifferSocket(eth_t *eth, char ifname[], int txSize, int rxSize){
    eth->fanout_grp = 2;
    socketSetup(eth, txSize, rxSize);
    if (createSocket(eth, ifname, 1, 0, 1, 0, 1) != 0){
        printf("Error creating socket\n");
        return -1;
    }
    return 0;
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

double substract_times(struct timespec *start, struct timespec *end){
    double start_sec = (double)start->tv_sec + (double)start->tv_nsec / 1000000000.0;
    double end_sec = (double)end->tv_sec + (double)end->tv_nsec / 1000000000.0;
    return end_sec - start_sec;
}

#pragma region Continuous Capture

void* process_frame_continuous(void *threadInfo){

    // Extract the thread information
    processThread_t *processThread = (processThread_t*)threadInfo;
    uint8_t *frame = processThread->frame;
    uint32_t frameSize = processThread->frameSize;
    SampledValue_t *sv = processThread->sv;
    int num_sv = *processThread->num_sv;

    // -------- Process the frame --------
    int i = 0;
    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  
        i = 16;
    else 
        i = 12;

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) // Check if packet is SV
        return NULL; 

    int j=0, noAsdu, svIdx = -1;

    // Skip SV Header until seqAsdu
    if (frame[i+11] == 0x82){
        i += 17;
    }else if (frame[i+11] == 0x81){
        i += 16;
    }else{
        i += 15;
    }

    noAsdu = frame[i-1]; // Number of ASDUs

    // Skip until first asdu
    if (frame[i+1] == 0x82) {
        i += 4;
    } else if(frame[i+1] == 0x81){
        i += 3;
    } else {
        i += 2;
    }

    for (int k = 0; k < noAsdu; k++) { // Decode Each Asdu

        if (frame[i+1] == 0x82) 
            i += 4;
        else 
            i += 2;
        while (frame[i] != 0x87) { // skip all the fields until Sequence of Data
            /*
                * 0x80 -> svId
                * 0x81 -> datSet Name
                * 0x82 -> smpCnt
                * 0x83 -> confRev
                * 0x84 -> refrTm
                * 0x85 -> sympSync
                * 0x86 -> smpRate
                * 0x87 -> Sequence of Data
                * 0x88 -> SmpMod
                * 0x89 -> gmIdentity
            */
            if (frame[i] == 0x80){ // If tag is svId 0x80
                if (svIdx == -1){
                    for (int idx = 0; idx < num_sv; idx++){ // look for the sampled value
                        if (memcmp(sv[idx].info.svId, &frame[i+2], frame[i+1]) == 0){
                            svIdx = idx;
                            break;
                        }
                    }
                }
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
            if (i >= frameSize) {
                return NULL;
            }
        }
        j = 0;
        if (svIdx == -1) {
            return NULL;
        }
        pthread_mutex_lock(&sv[svIdx].process.mutex);
        if (frame[i+1] > sv[svIdx].info.noChannels * 8) {
            pthread_mutex_unlock(&sv[svIdx].process.mutex);
            return NULL;
        }
        while (j < frame[i+1]) // Decode the raw values
        {
            sv[svIdx].process.captArr[j/8][sv[svIdx].process.sniffer.idxBuffer][sv[svIdx].process.sniffer.idxCycle] = (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216));
            j += 8;
        }

        sv[svIdx].process.sniffer.idxCycle++;
        if (sv[svIdx].process.sniffer.idxCycle >= sv[svIdx].info.smpRate) {
            sv[svIdx].process.sniffer.idxCycle = 0;
            sv[svIdx].process.sniffer.idxBuffer++;
            pthread_cond_signal(&sv[svIdx].process.condition);
            if (sv[svIdx].process.sniffer.idxBuffer >= sv[svIdx].info.frequency) {
                sv[svIdx].process.sniffer.idxBuffer = 0;
            }
        }
        pthread_mutex_unlock(&sv[svIdx].process.mutex);
        i += frame[i+1] + 2;
    }
    return NULL;
}

void *snifferThread(void *threadInfo) {

    snifferThread_t *snifferThread = (snifferThread_t*)threadInfo;
    snifferThread->running = 1;

    // Initialize the Ethernet Socket
    eth_t eth;
    if(snifferSocket(&eth, snifferThread->ifName, 0, 2048) != 0){
        printf("Error creating socket\n");
        return NULL;
    }

    // Initialize the Thread Pool
    ThreadPool pool;
    thread_pool_init(&pool, NUM_THREADS, NUM_TASK);
    
    // Initialize the frameCaptured
    processThread_t frameInfo[NUM_TASK + 2];
    uint8_t frames[NUM_TASK+2][MAX_RX_BUFFER];

    struct msghdr msg_hdr;
    struct iovec iov;
    configMsgHdr(&msg_hdr, &iov, &eth);
    int idxThreads = 0;
    ssize_t rx_bytes;

    // Sniffing loop
    while(!snifferThread->stop) {
        rx_bytes = recvmsg(eth.socket, &msg_hdr, 0);
        if ((rx_bytes > 0) && (rx_bytes < eth.rx_size)) {
            memcpy(frames[idxThreads], eth.rx_buffer, rx_bytes); // Copy the packet to the buffer
            frameInfo[idxThreads].frame = frames[idxThreads];
            frameInfo[idxThreads].frameSize = rx_bytes;
            frameInfo[idxThreads].sv = snifferThread->sv;
            frameInfo[idxThreads].num_sv = &snifferThread->num_sv;

            thread_pool_submit(&pool, (void (*)(void*))process_frame_continuous, &frameInfo[idxThreads]);
            idxThreads++;
            if (idxThreads == NUM_TASK+2) idxThreads = 0;
        }
    }
    
    int done = 1;
    while (!done){
        done = 0;
        for (int i = 0; i < NUM_TASK+2; i++){
            done += frameInfo[i].done;
        }
    }

    // Cleanup
    thread_pool_destroy(&pool);
    socketCleanup(&eth);
    snifferThread->running = 0;
    printf("Sniffer Thread Closed\n");
    return NULL;
}

#pragma endregion

#pragma region Timed Capture

int save_captured_sv_data(SampledValue_t *sv, int num_sv, char* data[]){
    
    // Estimate Size

    int buffer_size = 0;
    for (int i = 0; i < num_sv; i++) {
        int record_size = snprintf(NULL, 0, "- svID: %s\n  MeanTime: %lf\n  nPackets: %lu\n  nAsdu: %u\n  macSrc: %02x:%02x:%02x:%02x:%02x:%02x\n  macDst: %02x:%02x:%02x:%02x:%02x:%02x\n  vLanId: %d\n  vLanPriority: %d\n  appID: %d\n  nChannels: %d\n",
                                   sv[i].info.svId, sv[i].capture.meanTime, sv[i].capture.nPackets, sv[i].capture.nAsdu, 
                                   sv[i].capture.macSrc[0], sv[i].capture.macSrc[1], sv[i].capture.macSrc[2], sv[i].capture.macSrc[3], sv[i].capture.macSrc[4], sv[i].capture.macSrc[5],
                                   sv[i].capture.macDst[0], sv[i].capture.macDst[1], sv[i].capture.macDst[2], sv[i].capture.macDst[3], sv[i].capture.macDst[4], sv[i].capture.macDst[5],
                                   sv[i].capture.vLanId, sv[i].capture.vLanPriority, sv[i].capture.appID, sv[i].capture.nChannels);
        buffer_size += record_size + 2;
    }
    buffer_size += 13 + snprintf(NULL, 0, "%u", num_sv);

    *data = (char*)malloc(buffer_size + 1); // +1 for null terminator
    if (*data == NULL) {
        printf("Memory allocation failed.\n");
        return -1;
    }

    // Write data to the buffer
    int offset = 0;
    if (num_sv > 0) {
        offset += sprintf(*data + offset, "svData:\n");
        for (int i = 0; i < num_sv; i++) {
            sv[i].capture.meanTime /= sv[i].capture.nPackets;
            offset += sprintf(*data + offset, "- svID: %s\n  MeanTime: %lf\n  nPackets: %lu\n  nAsdu: %u\n  macSrc: %02x:%02x:%02x:%02x:%02x:%02x\n  macDst: %02x:%02x:%02x:%02x:%02x:%02x\n  vLanId: %d\n  vLanPriority: %d\n  appID: %d\n  nChannels: %d\n",
                              sv[i].info.svId, sv[i].capture.meanTime, sv[i].capture.nPackets, sv[i].capture.nAsdu, 
                              sv[i].capture.macSrc[0], sv[i].capture.macSrc[1], sv[i].capture.macSrc[2], sv[i].capture.macSrc[3], sv[i].capture.macSrc[4], sv[i].capture.macSrc[5],
                              sv[i].capture.macDst[0], sv[i].capture.macDst[1], sv[i].capture.macDst[2], sv[i].capture.macDst[3], sv[i].capture.macDst[4], sv[i].capture.macDst[5],
                              sv[i].capture.vLanId, sv[i].capture.vLanPriority, sv[i].capture.appID, sv[i].capture.nChannels);
        }
    } else {
        offset += sprintf(*data + offset, "svData: []\n");
    }
    offset += sprintf(*data + offset, "nSV: %u  \n", num_sv);
    (*data)[offset] = '\0';

    return offset;
}

void *process_frame_predefined_sv(void *threadInfo){

    // Extract the thread information
    processThread_t *processThread = (processThread_t*)threadInfo;
    uint8_t *frame = processThread->frame;
    ssize_t frameSize = processThread->frameSize;
    SampledValue_t *sv = processThread->sv;
    int num_sv = *processThread->num_sv;

    // Process the frame
    int i = 0, has_vLAN = 0;
    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  {
        i = 16;
        has_vLAN = 1;
    }
    else {
        i = 12;
        has_vLAN = 0;
    }

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) // Check if packet is SV
        return NULL; 

    int j=0, noAsdu, svIdx = -1;

    // Skip SV Header until seqAsdu
    if (frame[i+11] == 0x82){
        i += 17;
    }else if (frame[i+11] == 0x81){
        i += 16;
    }else{
        i += 15;
    }

    noAsdu = frame[i-1]; // Number of ASDUs

    // Skip until first asdu
    if (frame[i+1] == 0x82) {
        i += 4;
    } else if(frame[i+1] == 0x81){
        i += 3;
    } else {
        i += 2;
    }

    for (int k = 0; k < noAsdu; k++) { // Decode Each Asdu

        if (frame[i+1] == 0x82) 
            i += 4;
        else 
            i += 2;
        while (frame[i] != 0x87) { // skip all the fields until Sequence of Data
            /*
                * 0x80 -> svId
                * 0x81 -> datSet Name
                * 0x82 -> smpCnt
                * 0x83 -> confRev
                * 0x84 -> refrTm
                * 0x85 -> sympSync
                * 0x86 -> smpRate
                * 0x87 -> Sequence of Data
                * 0x88 -> SmpMod
                * 0x89 -> gmIdentity
            */
            if (frame[i] == 0x80){ // If tag is svId 0x80
                if(svIdx == -1){
                    for (int idx = 0; idx < num_sv; idx++){ // look for the sampled value
                        if (memcmp(sv[idx].info.svId, &frame[i+2], frame[i+1]) == 0){
                            svIdx = idx;
                            if (!sv[idx].capture.filled){
                                sv[idx].capture.filled = 1;
                                if (has_vLAN){
                                    sv[idx].capture.vLanPriority = (frame[14] & 0xe0) >> 5;
                                    sv[idx].capture.vLanId = frame[15] + (frame[14] & 0x0f)*256;
                                    sv[idx].capture.appID = (frame[18] << 8) | frame[19];
                                }else{
                                    sv[idx].capture.vLanPriority = -1;
                                    sv[idx].capture.vLanId = -1;
                                    sv[idx].capture.appID = (frame[14] << 8) | frame[15];
                                }
                                memcpy(sv[idx].capture.macDst, &frame[0], 6);
                                memcpy(sv[idx].capture.macSrc, &frame[6], 6);
                                
                                sv[idx].capture.nAsdu = noAsdu;
                                sv[idx].capture.meanTime = 0;
                                sv[idx].capture.nPackets = 0;
                                clock_gettime(0, &sv[idx].capture.t0);
                            }
                            else{
                                clock_gettime(0, &sv[idx].capture.t1);
                                sv[idx].capture.meanTime += substract_times(&sv[idx].capture.t0, &sv[idx].capture.t1);
                                sv[idx].capture.nPackets++;
                                sv[idx].capture.t0 = sv[idx].capture.t1;
                            }
                            break;
                        }
                    }   
                }
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
            if (i >= frameSize) { // Check if the frame is not corrupted
                return NULL;
            }
        }
        j = 0;
        if (svIdx == -1) { // Check if the sampled value is not found
            return NULL;
        }
        if (processThread->save_waveform){
            while (j < frame[i+1]) { // Decode the raw values
                fprintf(processThread->files[svIdx], "%d,", (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216)));
                j += 8;
            }
            fprintf(processThread->files[svIdx], "\n");
        }else{
            return NULL;
        }
        i += frame[i+1] + 2;
    }
    return NULL;
}

void *process_frame_any_sv(void *threadInfo){
    // Extract the thread information
    processThread_t *processThread = (processThread_t*)threadInfo;
    uint8_t *frame = processThread->frame;
    ssize_t frameSize = processThread->frameSize;
    SampledValue_t *sv = processThread->sv;
    int *num_sv = processThread->num_sv;

    // Process the frame
    int i = 0, has_vLAN = 0;
    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  {
        i = 16;
        has_vLAN = 1;
    }
    else {
        i = 12;
        has_vLAN = 0;
    }
        

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) {// Check if packet is SV
        processThread->done = 1;
        return NULL; 
    }

    int j=0, noAsdu, svIdx = -1;

    // Skip SV Header until seqAsdu
    if (frame[i+11] == 0x82){
        i += 17;
    }else if (frame[i+11] == 0x81){
        i += 16;
    }else{
        i += 15;
    }

    noAsdu = frame[i-1]; // Number of ASDUs

    // Skip until first asdu
    if (frame[i+1] == 0x82) {
        i += 4;
    } else if(frame[i+1] == 0x81){
        i += 3;
    } else {
        i += 2;
    }

    for (int k = 0; k < noAsdu; k++) { // Decode Each Asdu

        if (frame[i+1] == 0x82) 
            i += 4;
        else 
            i += 2;
        while (frame[i] != 0x87) { // skip all the fields until Sequence of Data
            /*
                * 0x80 -> svId
                * 0x81 -> datSet Name
                * 0x82 -> smpCnt
                * 0x83 -> confRev
                * 0x84 -> refrTm
                * 0x85 -> sympSync
                * 0x86 -> smpRate
                * 0x87 -> Sequence of Data
                * 0x88 -> SmpMod
                * 0x89 -> gmIdentity
            */
            if (frame[i] == 0x80){ // If tag is svId 0x80
                if (svIdx == -1){
                    uint8_t found = 0;
                    for (int idx = 0; idx < *num_sv; idx++){ // look for the sampled value
                        if (memcmp(sv[idx].info.svId, &frame[i+2], frame[i+1]) == 0){
                            found = 1;
                            svIdx = idx;
                            if (!sv[idx].capture.filled){
                                sv[idx].capture.filled = 1;
                                if (has_vLAN){
                                    sv[idx].capture.vLanPriority = (frame[14] & 0xe0) >> 5;
                                    sv[idx].capture.vLanId = frame[15] + (frame[14] & 0x0f)*256;
                                    sv[idx].capture.appID = (frame[18] << 8) | frame[19];
                                }else{
                                    sv[idx].capture.vLanPriority = -1;
                                    sv[idx].capture.vLanId = -1;
                                    sv[idx].capture.appID = (frame[14] << 8) | frame[15];
                                }
                                memcpy(sv[idx].capture.macDst, &frame[0], 6);
                                memcpy(sv[idx].capture.macSrc, &frame[6], 6);
                                
                                sv[idx].capture.nAsdu = noAsdu;
                                sv[idx].capture.meanTime = 0;
                                sv[idx].capture.nPackets = 0;
                                clock_gettime(0, &sv[idx].capture.t0);
                            }
                            else{
                                clock_gettime(0, &sv[idx].capture.t1);
                                sv[idx].capture.meanTime += substract_times(&sv[idx].capture.t0, &sv[idx].capture.t1);
                                sv[idx].capture.nPackets++;
                                sv[idx].capture.t0 = sv[idx].capture.t1;
                            }
                            break;
                        }
                    }
                    if (!found){
                        svIdx = *num_sv;
                        *num_sv = *num_sv + 1;
                        memcpy(sv[svIdx].info.svId, &frame[i+2], frame[i+1]);
                        sv[svIdx].info.svId[frame[i+1]] = '\0';
                        sv[svIdx].capture.filled = 1;
                        if (has_vLAN){
                            sv[svIdx].capture.vLanPriority = (frame[14] & 0xe0) >> 5;
                            sv[svIdx].capture.vLanId = frame[15] + (frame[14] & 0x0f)*256;
                            sv[svIdx].capture.appID = (frame[18] << 8) | frame[19];
                        }else{
                            sv[svIdx].capture.vLanPriority = -1;
                            sv[svIdx].capture.vLanId = -1;
                            sv[svIdx].capture.appID = (frame[14] << 8) | frame[15];
                        }
                        memcpy(sv[svIdx].capture.macDst, &frame[0], 6);
                        memcpy(sv[svIdx].capture.macSrc, &frame[6], 6);
                        
                        sv[svIdx].capture.nAsdu = noAsdu;
                        sv[svIdx].capture.meanTime = 0;
                        sv[svIdx].capture.nPackets = 0;
                        clock_gettime(0, &sv[svIdx].capture.t0);
                    }
                }
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
            if (i >= frameSize) { // Check if the frame is not corrupted
                processThread->done = 1;
                return NULL;
            }
        }
        j = 0;
        if (svIdx == -1) { // Check if the sampled value is not found
            processThread->done = 1;
            return NULL;
        }
        sv[svIdx].capture.nChannels = frame[i+1]/8;
        if (processThread->save_waveform){
            while (j < frame[i+1]) { // Decode the raw values
                fprintf(processThread->files[svIdx], "%d,", (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216)));
                j += 8;
            }
            fprintf(processThread->files[svIdx], "\n");
        }else{
            processThread->done = 1;
            return NULL;
        }
        i += frame[i+1] + 2;
    }
    processThread->done = 1;
    return NULL;
}

void *timed_snifferThread(void *threadInfo){
    

    char fileDir[128];
    // getcwd(fileDir, sizeof(fileDir));
    get_script_dir(fileDir, sizeof(fileDir));

    snifferThread_t *snifferThread = (snifferThread_t*)threadInfo;

    //Checks
    if (!(snifferThread->captureTime > 0)){
        printf("Capture Time must be greater than 0\n");
        return NULL;
    }

    // Initialize the Ethernet Socket
    eth_t eth;
    if(snifferSocket(&eth, snifferThread->ifName, 0, 2048) != 0){
        printf("Error creating socket\n");
        return NULL;
    }

    
    void (*process_frame)(void*) = NULL;
    FILE *files[MAX_SAMPLED_VALUES];

    if (snifferThread->num_sv == 0){ // Search for any SV
        snifferThread->captureType = ANY_SV;
        process_frame = (void (*)(void*))process_frame_any_sv;
        if (snifferThread->save_waveform){
            for (int i = 0; i < snifferThread->max_sv; i++){
                memset(&snifferThread->sv[i].info, 0, sizeof(SV_Info_t));
                memset(&snifferThread->sv[i].capture, 0, sizeof(Capture_Info_t));
                char filename[180];
                sprintf(filename, "%s/captFiles/sv_%d", fileDir, i);
                files[i] = fopen(filename, "w");
            }
        }
    }else{ // Search for especifics SVs
        snifferThread->captureType = PREDEFINED_SV;
        process_frame = (void (*)(void*))process_frame_predefined_sv;
        if (snifferThread->save_waveform){
            for (int i = 0; i < snifferThread->num_sv; i++){
                memset(&snifferThread->sv[i].capture, 0, sizeof(Capture_Info_t));
                char filename[180];
                sprintf(filename, "%s/captFiles/%s", fileDir, snifferThread->sv[i].info.svId);
                files[i] = fopen(filename, "w");
                printf("%s\n", filename);
                printf("%s\n", snifferThread->sv[i].info.svId);
            }
        }
    }

    snifferThread->running = 1;

    // Initialize the Thread Pool
    ThreadPool pool;
    thread_pool_init(&pool, NUM_THREADS, NUM_TASK);
    
    // Initialize the frameCaptured
    // processThread_t *frameInfo = (processThread_t*)malloc((NUM_TASK+2)*sizeof(processThread_t));
    processThread_t frameInfo[NUM_TASK+2];
    uint8_t frames[NUM_TASK+2][MAX_RX_BUFFER];
    // uint8_t **frames = (uint8_t**)malloc((NUM_TASK+2)*sizeof(uint8_t*));
    // for (int i = 0; i < (snifferThread->nTask+2); i++){
    //     frames[i] = (uint8_t*)malloc(snifferThread->maxFrameSize*sizeof(uint8_t));
    // }

    struct msghdr msg_hdr;
    struct iovec iov;
    configMsgHdr(&msg_hdr, &iov, &eth);
    int idxThreads = 0;
    ssize_t rx_bytes;

    struct timespec start, end;
    clock_gettime(CLOCK_MONOTONIC, &start);

    // Sniffing loop
    while(!snifferThread->stop) {
        clock_gettime(CLOCK_MONOTONIC, &end);
        snifferThread->curTime = substract_times(&start, &end);
        if (snifferThread->curTime >= snifferThread->captureTime){
            snifferThread->curTime = snifferThread->captureTime;
            break;
        }
        rx_bytes = recvmsg(eth.socket, &msg_hdr, 0);
        if (rx_bytes > 0 && rx_bytes < eth.rx_size) {
            memcpy(frames[idxThreads], eth.rx_buffer, rx_bytes); // Copy the packet to the buffer
            frameInfo[idxThreads].frame = frames[idxThreads];
            frameInfo[idxThreads].frameSize = rx_bytes;
            frameInfo[idxThreads].sv = snifferThread->sv;
            frameInfo[idxThreads].num_sv = &snifferThread->num_sv;
            frameInfo[idxThreads].save_waveform = snifferThread->save_waveform;
            frameInfo[idxThreads].files = files;
            frameInfo[idxThreads].done = 0;

            thread_pool_submit(&pool, process_frame, &frameInfo[idxThreads]);
            // process_frame((void*) &frameInfo[idxThreads]);
            idxThreads++;
            if (idxThreads == NUM_TASK+2) idxThreads = 0;
        }
    }

    // Wait until all the threads are done
    int done = 1;
    while (!done){
        done = 0;
        for (int i = 0; i < NUM_TASK+2; i++){
            done += frameInfo[i].done;
        }
    }
    
    thread_pool_destroy(&pool);
    // Cleanup
    if (snifferThread->save_waveform){
        if (snifferThread->captureType == ANY_SV){
            char curFileName[180], realFileName[240];
            for (int i = 0; i < snifferThread->max_sv; i++){
                fclose(files[i]);
                sprintf(curFileName, "%s/captFiles/sv_%d", fileDir, i);
                if (i < snifferThread->num_sv){
                    sprintf(realFileName, "%s/captFiles/%s", fileDir, snifferThread->sv[i].info.svId);
                    rename(curFileName, realFileName);
                }else{
                    remove(curFileName);
                }
            }
            
            for (int i=0;i<snifferThread->num_sv;i++){
                sprintf(curFileName, "%s/captFiles/sv_%d", fileDir, i);
                sprintf(realFileName, "%s/captFiles/%s", fileDir, snifferThread->sv[i].info.svId);
                rename(curFileName, realFileName);
            }
        }else{
            for (int i = 0; i < snifferThread->num_sv; i++){
                fclose(files[i]);
            }
        }
    }
    socketCleanup(&eth);
    snifferThread->running = 0;
    printf("Sniffer Thread Closed\n");
    return NULL;
}

#pragma endregion








