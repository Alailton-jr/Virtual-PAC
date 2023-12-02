/*
    unfinished
*/

#include <stdio.h>
#include <stdint.h>
#include <json-c/json.h>
#include <string.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/if_ether.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <net/if.h>
#include <sys/ioctl.h>
#include <stdlib.h>
#include <linux/if_packet.h>


struct gooseFrame
{
    uint32_t length;
    uint8_t* data;
}typedef gooseFrame;


struct gooseHeader{
    uint16_t APPID;
    uint16_t length;
    uint16_t reserved1; 
    uint16_t reserved2;
}typedef gooseHeader;

struct gooseAsn{
    uint8_t tag;
    uint16_t length;
    uint8_t* value;
}typedef gooseAsn;

struct goosePduData{
    uint8_t tag;
    uint16_t length;
    uint8_t* value;
    struct goosePduData* next;
}typedef goosePduData;

struct goosePdu{
    gooseAsn goCbRef;
    gooseAsn timeAllowedtoLive;
    gooseAsn datSet;
    gooseAsn goID;
    gooseAsn t;
    gooseAsn stNum;
    gooseAsn sqNum;
    gooseAsn test;
    gooseAsn confRev;
    gooseAsn ndsCom;
    gooseAsn numDatSetEntries;
    goosePduData* allData;
}typedef goosePdu;

struct gooseEth{
    uint8_t macDst[6];
    uint8_t macSrc[6];
    uint8_t vLanTag[4];
    uint16_t etherType;
}typedef gooseEth;

struct gooseCtl{
    gooseHeader header;
    goosePdu pdu;
    gooseEth eth;
    char** monitor;
    uint8_t numMonitor;
    uint64_t minTime;
    uint64_t maxTime;
}typedef gooseCtl;

gooseCtl* CreateGooseCtl(uint16_t APPID){
    gooseCtl* goose = (gooseCtl*)malloc(sizeof(gooseCtl));
    goose->header.APPID = 0x0001;
    goose->header.length = 0x0000;
    goose->header.reserved1 = 0x0000;
    goose->header.reserved2 = 0x0000;
    return goose;
}

void DeleteGooseCtl(gooseCtl* goose){
    free(goose->pdu.goCbRef.value);
    free(goose->pdu.timeAllowedtoLive.value);
    free(goose->pdu.datSet.value);
    free(goose->pdu.goID.value);
    free(goose->pdu.t.value);
    free(goose->pdu.stNum.value);
    free(goose->pdu.sqNum.value);
    free(goose->pdu.test.value);
    free(goose->pdu.confRev.value);
    free(goose->pdu.ndsCom.value);
    free(goose->pdu.numDatSetEntries.value);
    goosePduData* temp = goose->pdu.allData;
    while(temp != NULL){
        free(temp->value);
        temp = temp->next;
    }
    free(goose->pdu.allData);
    // for (int i = 0; i < goose->numMonitor; i++) {
    //     free(goose->monitor[i]);
    // }
    free(goose);
}

void setAsnNum(gooseAsn* asn, uint64_t value, uint8_t tag){
    uint64_t tempValue = value;
    while(tempValue > 0){
        tempValue /=  256;
        asn->length++;
    }
    asn->value = malloc(asn->length * sizeof(uint8_t));
    for(int i = 0; i < asn->length; i++){
        asn->value[i] = value % 256;
        value /= 256;
    }
    for (int i = 0; i < asn->length; i++) {
        printf("%x\n", asn->value[i]);
    }
    asn->tag = tag;
}

void setAsnStr(gooseAsn* asn, const char* value, uint64_t size, uint8_t tag){
    asn->length = size;
    asn->value = malloc(asn->length * sizeof(uint8_t));
    for(int i = 0; i < asn->length; i++){
        asn->value[i] = (uint8_t)value[i];
    }
    asn->tag = tag;
}

char* jsonFile(){
    const char *file_name = "gooseConf.json"; // Replace with your file name
    FILE *file = fopen(file_name, "r");
    if (!file) {
        fprintf(stderr, "Failed to open JSON file\n");
        return NULL;
    }
    char buffer[1024];
    size_t nread;
    char *json_string = NULL;
    size_t json_length = 0;

    while ((nread = fread(buffer, 1, sizeof(buffer), file)) > 0) {
        json_string = realloc(json_string, json_length + nread + 1);
        if (json_string == NULL) {
            fprintf(stderr, "Memory allocation failed\n");
            fclose(file);
            return 1;
        }
        memcpy(json_string + json_length, buffer, nread);
        json_length += nread;
    }

    json_string[json_length] = '\0'; // Null-terminate the JSON string

    return json_string;
    fclose(file);
}

int parseMacAddress(const char *macStr, uint8_t macArray[6]) {
    char *token;
    char *endptr;
    int i = 0;

    token = strtok((char *)macStr, ":-");

    while (token != NULL && i < 6) {
        macArray[i] = (uint8_t)strtoul(token, &endptr, 16);

        // Check if conversion failed
        if (*endptr != '\0') {
            return 0; // Parsing error
        }

        token = strtok(NULL, ":-");
        i++;
    }
    if (i != 6 || token != NULL) {
        return 0; 
    }
    return 1; 
}

void setGooseEth(gooseCtl* goose, const char* macDst, const char* macSrc, uint16_t etherType, uint16_t vLanId, uint8_t vLanPriority){
    parseMacAddress(macDst, goose->eth.macDst);
    parseMacAddress(macSrc, goose->eth.macSrc);
    goose->eth.etherType = etherType;
    goose->eth.vLanTag[0] = 0x81;
    goose->eth.vLanTag[1] = 0x00;
    goose->eth.vLanTag[2] = ((vLanPriority & 0x0F) << 5) | (vLanId >> 8);
    goose->eth.vLanTag[3] = (vLanId);
}

void loadGoose(gooseCtl* ctl, char* jsonString){

    struct json_object *json_obj = json_tokener_parse(jsonString);

    uint64_t appId, confRef, maxTime, minTime, vLanId, vLanPriority;

    appId = json_object_get_uint64(json_object_object_get(json_obj, "appId"));
    confRef = json_object_get_uint64(json_object_object_get(json_obj, "confRef"));
    maxTime = json_object_get_uint64(json_object_object_get(json_obj, "maxTime"));
    minTime = json_object_get_uint64(json_object_object_get(json_obj, "minTime"));
    vLanId = json_object_get_uint64(json_object_object_get(json_obj, "vLanId"));
    vLanPriority = json_object_get_uint64(json_object_object_get(json_obj, "vLanPriority"));

    const char *cbRef, *dataSetName, *goId, *macDst, *macSrc;
    cbRef = json_object_get_string(json_object_object_get(json_obj, "cbRef"));
    dataSetName = json_object_get_string(json_object_object_get(json_obj, "dataSetName"));
    goId = json_object_get_string(json_object_object_get(json_obj, "goId"));
    macDst = json_object_get_string(json_object_object_get(json_obj, "macDst"));
    macSrc = json_object_get_string(json_object_object_get(json_obj, "macSrc"));   

    struct json_object *dataSetArray;
    json_object_object_get_ex(json_obj, "dataSet", &dataSetArray);

    int dataSetArrayLength = json_object_array_length(dataSetArray);
    ctl->monitor = malloc(dataSetArrayLength * sizeof(char*));
    ctl->numMonitor = dataSetArrayLength;
    for (int i = 0; i < dataSetArrayLength; i++) {
        struct json_object *dataSetItem = json_object_array_get_idx(dataSetArray, i);
        const char *dataSetValue = json_object_get_string(dataSetItem);
        ctl->monitor[i] = malloc(sizeof(char) * strlen(dataSetValue));
        strcpy(ctl->monitor[i], dataSetValue);
    }

    ctl->header.APPID = appId;
    ctl->minTime = minTime;
    ctl->maxTime = maxTime;
    setAsnStr(&ctl->pdu.goCbRef, cbRef, sizeof(cbRef)+1, 0x80);
    setAsnNum(&ctl->pdu.timeAllowedtoLive, 2*maxTime, 0x81);
    setAsnStr(&ctl->pdu.datSet, dataSetName, sizeof(dataSetName)+1, 0x82);
    setAsnStr(&ctl->pdu.goID, goId, sizeof(goId)+1, 0x83);
    setAsnStr(&ctl->pdu.t, "00000000000000000000", sizeof(uint8_t)*8, 0x84);
    setAsnNum(&ctl->pdu.stNum, 0, 0x85);
    setAsnNum(&ctl->pdu.sqNum, 0, 0x86);
    setAsnNum(&ctl->pdu.test, 0, 0x87);
    setAsnNum(&ctl->pdu.confRev, confRef, 0x88);
    setAsnNum(&ctl->pdu.ndsCom, 0, 0x89);
    setAsnNum(&ctl->pdu.numDatSetEntries, dataSetArrayLength, 0x8a);
    setGooseEth(ctl, macDst, macSrc, 0x88b8, vLanId, vLanPriority);


    goosePduData* temp = ctl->pdu.allData;
    for (int i = 0; i < ctl->numMonitor; i++) {
        temp = malloc(sizeof(goosePduData));
        temp->tag = 0x83;
        temp->length = 1;
        temp->value = malloc(sizeof(uint8_t));
        temp->value[0] = 0x00;
        temp = temp->next;
    }

    free(jsonString);
    json_object_put(json_obj);
}

gooseFrame getFrame(gooseCtl* goose){
    uint16_t length = 0;
    length += sizeof(goose->pdu.goCbRef.tag);
    length += sizeof(goose->pdu.goCbRef.length);
    length += goose->pdu.goCbRef.length;
    length += sizeof(goose->pdu.timeAllowedtoLive.tag);
    length += sizeof(goose->pdu.timeAllowedtoLive.length);
    length += goose->pdu.timeAllowedtoLive.length;
    length += sizeof(goose->pdu.datSet.tag);
    length += sizeof(goose->pdu.datSet.length);
    length += goose->pdu.datSet.length;
    length += sizeof(goose->pdu.goID.tag);
    length += sizeof(goose->pdu.goID.length);
    length += goose->pdu.goID.length;
    length += sizeof(goose->pdu.t.tag);
    length += sizeof(goose->pdu.t.length);
    length += goose->pdu.t.length;
    length += sizeof(goose->pdu.stNum.tag);
    length += sizeof(goose->pdu.stNum.length);
    length += goose->pdu.stNum.length;
    length += sizeof(goose->pdu.sqNum.tag);
    length += sizeof(goose->pdu.sqNum.length);
    length += goose->pdu.sqNum.length;
    length += sizeof(goose->pdu.test.tag);
    length += sizeof(goose->pdu.test.length);
    length += goose->pdu.test.length;
    length += sizeof(goose->pdu.confRev.tag);
    length += sizeof(goose->pdu.confRev.length);
    length += goose->pdu.confRev.length;
    length += sizeof(goose->pdu.ndsCom.tag);
    length += sizeof(goose->pdu.ndsCom.length);
    length += goose->pdu.ndsCom.length;
    length += sizeof(goose->pdu.numDatSetEntries.tag);
    length += sizeof(goose->pdu.numDatSetEntries.length);
    length += goose->pdu.numDatSetEntries.length;
    goosePduData* temp = goose->pdu.allData;
    while(temp != NULL){
        length += sizeof(temp->tag);
        length += sizeof(temp->length);
        length += temp->length;
        temp = temp->next;
    }
    uint16_t pduLength = length;
    length += sizeof(goose->eth);
    length += sizeof(goose->header);
    goose->header.length = pduLength+10;
    uint8_t* frame = malloc(length * sizeof(uint8_t));
    uint16_t index = 0;
    memcpy(frame+index, &goose->eth, sizeof(goose->eth));
    index += sizeof(goose->eth);
    memcpy(frame+index, &goose->header, sizeof(goose->header));
    index += sizeof(goose->header);
    frame[index] = 0x61;
    if (pduLength >= 0x80){
        frame[index+1] = 0x80;
        frame[index+2] = pduLength % 256;
        pduLength /= 256;
        if (pduLength > 0){
            frame[index+3] = pduLength;
            index+=4;
        }
        else
        index+=3;
    }
    else{
        frame[index+1] = pduLength;
        index+=2;
    }
    memcpy(frame+index, &goose->pdu.goCbRef, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.goCbRef.length; i++) {
        frame[index+2+i] = goose->pdu.goCbRef.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.goCbRef.length;
    memcpy(frame+index, &goose->pdu.timeAllowedtoLive, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.timeAllowedtoLive.length; i++) {
        frame[index+2+i] = goose->pdu.timeAllowedtoLive.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.timeAllowedtoLive.length;
    memcpy(frame+index, &goose->pdu.datSet, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.datSet.length; i++) {
        frame[index+2+i] = goose->pdu.datSet.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.datSet.length;
    memcpy(frame+index, &goose->pdu.goID, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.goID.length; i++) {
        frame[index+2+i] = goose->pdu.goID.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.goID.length;
    memcpy(frame+index, &goose->pdu.t, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.t.length; i++) {
        frame[index+2+i] = goose->pdu.t.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.t.length;
    memcpy(frame+index, &goose->pdu.stNum, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.stNum.length; i++) {
        frame[index+2+i] = goose->pdu.stNum.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.stNum.length;
    memcpy(frame+index, &goose->pdu.sqNum, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.sqNum.length; i++) {
        frame[index+2+i] = goose->pdu.sqNum.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.sqNum.length;
    memcpy(frame+index, &goose->pdu.test, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.test.length; i++) {
        frame[index+2+i] = goose->pdu.test.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.test.length;
    memcpy(frame+index, &goose->pdu.confRev, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.confRev.length; i++) {
        frame[index+2+i] = goose->pdu.confRev.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.confRev.length;
    memcpy(frame+index, &goose->pdu.ndsCom, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.ndsCom.length; i++) {
        frame[index+2+i] = goose->pdu.ndsCom.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.ndsCom.length;
    memcpy(frame+index, &goose->pdu.numDatSetEntries, sizeof(uint8_t)*2);
    for (int i = 0; i < goose->pdu.numDatSetEntries.length; i++) {
        frame[index+2+i] = goose->pdu.numDatSetEntries.value[i];
    }
    index += sizeof(uint8_t)*2 + goose->pdu.numDatSetEntries.length;
    temp = goose->pdu.allData;
    while(temp != NULL){
        memcpy(frame+index, &temp->tag, sizeof(uint8_t));
        index += sizeof(uint8_t);
        memcpy(frame+index, &temp->length, sizeof(uint8_t));
        index += sizeof(uint8_t);
        for (int i = 0; i < temp->length; i++) {
            frame[index+i] = temp->value[i];
        }
        index += temp->length;
        temp = temp->next;
    }
    gooseFrame goFrame;
    goFrame.length = length;
    goFrame.data = frame;
    return goFrame;

}

void sendFrame(gooseFrame goFrame){
    int sockfd = socket(AF_PACKET, SOCK_RAW, htons(ETH_P_ALL));

    if (sockfd == -1) {
        perror("Socket creation failed");
        exit(EXIT_FAILURE);
    }

    struct sockaddr_ll sa;
    memset(&sa, 0, sizeof(sa));
    sa.sll_family = AF_PACKET;
    sa.sll_protocol = htons(ETH_P_ALL); 
    sa.sll_ifindex = if_nametoindex("eth0");

    for(int i = 0; i < 1000; i++){
        if (sendto(sockfd, goFrame.data, goFrame.length, 0, (struct sockaddr *)&sa, sizeof(sa)) == -1) {
            perror("Sendto failed");
            close(sockfd);
            exit(EXIT_FAILURE);
        }
    }
    printf("Raw Ethernet frame sent successfully\n");

    close(sockfd);
}

int main(int argc, char *argv[])
{
    gooseCtl* goose = CreateGooseCtl(0x4000);
    char* jsonString = jsonFile();
    loadGoose(goose, jsonString);

    gooseFrame gooseFrame = getFrame(goose);
    for (int i = 0; i < gooseFrame.length; i++) {
        printf("0x%x ", gooseFrame.data[i]);
    }
    sendFrame(gooseFrame);

    DeleteGooseCtl(goose);
    return 0;
}