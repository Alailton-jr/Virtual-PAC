
// apt install libyaml-dev

#include "yamlReader.h"
#include "sampledValue.h"

#include <yaml.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void getNextToken(yaml_token_t *token, yaml_parser_t *parser){
    yaml_parser_scan(parser, token);
    while (token->type != YAML_SCALAR_TOKEN && token->type != YAML_STREAM_END_TOKEN) {
        yaml_parser_scan(parser, token);
    }
}

void parseQualityEvent(QualityEventsYaml_t *event, yaml_token_t *token, yaml_parser_t *parser){
    while (token->type != YAML_STREAM_END_TOKEN){
        getNextToken(token, parser);
        if (strcmp("topThreshold",token->data.scalar.value) == 0){
            getNextToken(token, parser);
            event->topThreshold = atof((char *)token->data.scalar.value);
        }else if (strcmp("bottomThreshold",token->data.scalar.value) == 0){
            getNextToken(token, parser);
            event->bottomThreshold = atof((char *)token->data.scalar.value);
        }else if (strcmp("minDuration",token->data.scalar.value) == 0){
            getNextToken(token, parser);
            event->minDuration = atof((char *)token->data.scalar.value);
        }else if (strcmp("maxDuration",token->data.scalar.value) == 0){
            getNextToken(token, parser);
            event->maxDuration = atof((char *)token->data.scalar.value);
            return;
        }
    }
}

void printSampledValueYaml(SampledValuesYaml_t *sv, int numSv){
    for (int i=0 ; i< numSv; i++){
        printf("SVID: %s\n", sv[i].SVID);
        printf("macSrc: %s\n", sv[i].macSrc);
        printf("frequency: %d\n", sv[i].frequency);
        printf("smpRate: %d\n", sv[i].smpRate);
        printf("noAsdu: %d\n", sv[i].noAsdu);
        printf("noChannels: %d\n", sv[i].noChannels);
        printf("nominalVoltage: %d\n", sv[i].nominalVoltage);
        printf("nominalCurrent: %d\n", sv[i].nominalCurrent);
        printf("sag:\n");
        printf("\ttopThreshold: %f\n", sv[i].sag.topThreshold);
        printf("\tbottomThreshold: %f\n", sv[i].sag.bottomThreshold);
        printf("\tminDuration: %f\n", sv[i].sag.minDuration);
        printf("\tmaxDuration: %f\n", sv[i].sag.maxDuration);
        printf("swell:\n");
        printf("\ttopThreshold: %f\n", sv[i].swell.topThreshold);
        printf("\tbottomThreshold: %f\n", sv[i].swell.bottomThreshold);
        printf("\tminDuration: %f\n", sv[i].swell.minDuration);
        printf("\tmaxDuration: %f\n", sv[i].swell.maxDuration);
        printf("interruption:\n");
        printf("\ttopThreshold: %f\n", sv[i].interruption.topThreshold);
        printf("\tbottomThreshold: %f\n", sv[i].interruption.bottomThreshold);
        printf("\tminDuration: %f\n", sv[i].interruption.minDuration);
        printf("\tmaxDuration: %f\n", sv[i].interruption.maxDuration);
        printf("overVoltage:\n");
        printf("\ttopThreshold: %f\n", sv[i].overVoltage.topThreshold);
        printf("\tbottomThreshold: %f\n", sv[i].overVoltage.bottomThreshold);
        printf("\tminDuration: %f\n", sv[i].overVoltage.minDuration);
        printf("\tmaxDuration: %f\n", sv[i].overVoltage.maxDuration);
        printf("underVoltage:\n");
        printf("\ttopThreshold: %f\n", sv[i].underVoltage.topThreshold);
        printf("\tbottomThreshold: %f\n", sv[i].underVoltage.bottomThreshold);
        printf("\tminDuration: %f\n", sv[i].underVoltage.minDuration);
        printf("\tmaxDuration: %f\n", sv[i].underVoltage.maxDuration);
        printf("sustainedinterruption:\n");
        printf("\ttopThreshold: %f\n", sv[i].sustainedinterruption.topThreshold);
        printf("\tbottomThreshold: %f\n", sv[i].sustainedinterruption.bottomThreshold);
        printf("\tminDuration: %f\n", sv[i].sustainedinterruption.minDuration);
        printf("\tmaxDuration: %f\n", sv[i].sustainedinterruption.maxDuration);
    }
}

void parseYamlEvent2SV(QualityEventsYaml_t* yamlEvent, VTCD_Info_t* event, int32_t nomVal){
    event->topThreshold = (int32_t)(yamlEvent->topThreshold * nomVal);
    event->bottomThreshold = (int32_t)(yamlEvent->bottomThreshold * nomVal);
    event->minDuration = yamlEvent->minDuration;
    event->maxDuration = yamlEvent->maxDuration;
}

int parseYaml2Sv(SampledValuesYaml_t *svYaml, int numSv, SampledValue_t *svArr){
    for (int i=0; i<numSv; i++){
        strcpy(svArr[i].info.svId, svYaml[i].SVID);
        strcpy(svArr[i].info.macSrc, svYaml[i].macSrc);
        svArr[i].info.frequency = svYaml[i].frequency;
        svArr[i].info.smpRate = svYaml[i].smpRate;
        svArr[i].info.noAsdu = svYaml[i].noAsdu;
        svArr[i].info.noChannels = svYaml[i].noChannels;
        svArr[i].info.nomVoltage = svYaml[i].nominalVoltage;
        svArr[i].info.nomCurrent = svYaml[i].nominalCurrent;
        parseYamlEvent2SV(&svYaml[i].sag, &svArr[i].quality.sag, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&svYaml[i].swell, &svArr[i].quality.swell, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&svYaml[i].interruption, &svArr[i].quality.interruption, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&svYaml[i].overVoltage, &svArr[i].quality.overVoltage, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&svYaml[i].underVoltage, &svArr[i].quality.underVoltage, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&svYaml[i].sustainedinterruption, &svArr[i].quality.sustainedinterruption, svArr[i].info.nomVoltage);
    }
    return 0;
}

int parse_yaml(FILE *file, int *nSV, SampledValuesYaml_t* svYaml) {
    yaml_parser_t parser;
    yaml_token_t token;
    SampledValuesYaml_t* curSv = svYaml;
    int done = 0;
    int step = 0;

    int debug;

    yaml_parser_initialize(&parser);
    yaml_parser_set_input_file(&parser, file);

    while(!done){
        getNextToken(&token, &parser);

        switch (token.type) {
            case YAML_STREAM_END_TOKEN:
                done = 1;
                break;
            case YAML_SCALAR_TOKEN:
                debug = strcmp("SVID",token.data.scalar.value);
                if (strcmp("SVID",token.data.scalar.value) == 0){
                    curSv = &svYaml[nSV[0]];
                    nSV[0] = nSV[0] + 1;
                    getNextToken(&token, &parser);
                    strcpy(curSv->SVID, (char *)token.data.scalar.value);
                }else if (strcmp("macSrc",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    strcpy(curSv->macSrc, (char *)token.data.scalar.value);
                }else if (strcmp("frequency",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    curSv->frequency = atoi((char *)token.data.scalar.value);
                }else if (strcmp("smpRate",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    curSv->smpRate = atoi((char *)token.data.scalar.value);
                }else if (strcmp("noAsdu",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    curSv->noAsdu = atoi((char *)token.data.scalar.value);
                }else if (strcmp("noChannels",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    curSv->noChannels = atoi((char *)token.data.scalar.value);
                }else if (strcmp("nominalVoltage",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    curSv->nominalVoltage = atoi((char *)token.data.scalar.value);
                }else if (strcmp("nominalCurrent",token.data.scalar.value) == 0){
                    getNextToken(&token, &parser);
                    curSv->nominalCurrent = atoi((char *)token.data.scalar.value);
                }else if (strcmp("sag",token.data.scalar.value) == 0){
                    parseQualityEvent(&curSv->sag, &token, &parser);
                }else if (strcmp("swell",token.data.scalar.value) == 0){
                    parseQualityEvent(&curSv->swell, &token, &parser);
                }else if (strcmp("interruption",token.data.scalar.value) == 0){
                    parseQualityEvent(&curSv->interruption, &token, &parser);
                }else if (strcmp("overVoltage",token.data.scalar.value) == 0){
                    parseQualityEvent(&curSv->overVoltage, &token, &parser);
                }else if (strcmp("underVoltage",token.data.scalar.value) == 0){
                    parseQualityEvent(&curSv->underVoltage, &token, &parser);
                }else if (strcmp("sustainedinterruption",token.data.scalar.value) == 0){
                    parseQualityEvent(&curSv->sustainedinterruption, &token, &parser);
                }
                break;
            default:
                break;
        }
    }
    yaml_parser_delete(&parser);
    return 0;
}