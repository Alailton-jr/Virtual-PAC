
// apt install libyaml-dev

#include "yamlReader.h"
#include "sampledValue.h"

#include <yaml.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void deleteSampledValuesYaml(SampledValuesYaml_t *sv){
    SampledValuesYaml_t *curSv = sv;
    SampledValuesYaml_t *nextSv;
    while (curSv != NULL){
        nextSv = curSv->next;
        free(curSv);
        curSv = nextSv;
    }
}

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

void printSampledValueYaml(SampledValuesYaml_t *sv){
    SampledValuesYaml_t *curSv = sv;
    while (curSv != NULL){
        printf("SVID: %s\n", curSv->SVID);
        printf("macSrc: %s\n", curSv->macSrc);
        printf("frequency: %d\n", curSv->frequency);
        printf("smpRate: %d\n", curSv->smpRate);
        printf("noAsdu: %d\n", curSv->noAsdu);
        printf("noChannels: %d\n", curSv->noChannels);
        printf("nominalVoltage: %d\n", curSv->nominalVoltage);
        printf("nominalCurrent: %d\n", curSv->nominalCurrent);
        printf("sag:\n");
        printf("\ttopThreshold: %f\n", curSv->sag.topThreshold);
        printf("\tbottomThreshold: %f\n", curSv->sag.bottomThreshold);
        printf("\tminDuration: %f\n", curSv->sag.minDuration);
        printf("\tmaxDuration: %f\n", curSv->sag.maxDuration);
        printf("swell:\n");
        printf("\ttopThreshold: %f\n", curSv->swell.topThreshold);
        printf("\tbottomThreshold: %f\n", curSv->swell.bottomThreshold);
        printf("\tminDuration: %f\n", curSv->swell.minDuration);
        printf("\tmaxDuration: %f\n", curSv->swell.maxDuration);
        printf("interruption:\n");
        printf("\ttopThreshold: %f\n", curSv->interruption.topThreshold);
        printf("\tbottomThreshold: %f\n", curSv->interruption.bottomThreshold);
        printf("\tminDuration: %f\n", curSv->interruption.minDuration);
        printf("\tmaxDuration: %f\n", curSv->interruption.maxDuration);
        printf("overVoltage:\n");
        printf("\ttopThreshold: %f\n", curSv->overVoltage.topThreshold);
        printf("\tbottomThreshold: %f\n", curSv->overVoltage.bottomThreshold);
        printf("\tminDuration: %f\n", curSv->overVoltage.minDuration);
        printf("\tmaxDuration: %f\n", curSv->overVoltage.maxDuration);
        printf("underVoltage:\n");
        printf("\ttopThreshold: %f\n", curSv->underVoltage.topThreshold);
        printf("\tbottomThreshold: %f\n", curSv->underVoltage.bottomThreshold);
        printf("\tminDuration: %f\n", curSv->underVoltage.minDuration);
        printf("\tmaxDuration: %f\n", curSv->underVoltage.maxDuration);
        printf("sustainedinterruption:\n");
        printf("\ttopThreshold: %f\n", curSv->sustainedinterruption.topThreshold);
        printf("\tbottomThreshold: %f\n", curSv->sustainedinterruption.bottomThreshold);
        printf("\tminDuration: %f\n", curSv->sustainedinterruption.minDuration);
        printf("\tmaxDuration: %f\n", curSv->sustainedinterruption.maxDuration);
        curSv = curSv->next;
    }
}

void parseYamlEvent2SV(QualityEventsYaml_t* yamlEvent, VTCD_Info_t* event, int32_t nomVal){
    event->topThreshold = (int32_t)(yamlEvent->topThreshold * nomVal);
    event->bottomThreshold = (int32_t)(yamlEvent->bottomThreshold * nomVal);
    event->minDuration = yamlEvent->minDuration;
    event->maxDuration = yamlEvent->maxDuration;
}

SampledValue_t* parseYaml2Sv(SampledValuesYaml_t *sv, int numSv){
    SampledValuesYaml_t *curYamlSv = sv;
    SampledValue_t *svArr = (SampledValue_t *)malloc(numSv * sizeof(SampledValue_t));
    for (int i=0; i<numSv; i++){
        strcpy(svArr[i].info.svId, curYamlSv->SVID);
        strcpy(svArr[i].info.macSrc, curYamlSv->macSrc);
        svArr[i].info.frequency = curYamlSv->frequency;
        svArr[i].info.smpRate = curYamlSv->smpRate;
        svArr[i].info.noAsdu = curYamlSv->noAsdu;
        svArr[i].info.noChannels = curYamlSv->noChannels;
        svArr[i].info.nomVoltage = curYamlSv->nominalVoltage;
        svArr[i].info.nomCurrent = curYamlSv->nominalCurrent;
        parseYamlEvent2SV(&curYamlSv->sag, &svArr[i].quality.sag, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&curYamlSv->swell, &svArr[i].quality.swell, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&curYamlSv->interruption, &svArr[i].quality.interruption, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&curYamlSv->overVoltage, &svArr[i].quality.overVoltage, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&curYamlSv->underVoltage, &svArr[i].quality.underVoltage, svArr[i].info.nomVoltage);
        parseYamlEvent2SV(&curYamlSv->sustainedinterruption, &svArr[i].quality.sustainedinterruption, svArr[i].info.nomVoltage);
        curYamlSv = curYamlSv->next;
    }
    return svArr;
}

SampledValuesYaml_t* parse_yaml(FILE *file, int *nSV) {
    yaml_parser_t parser;
    yaml_token_t token;
    SampledValuesYaml_t sv;
    SampledValuesYaml_t *curSv = &sv;
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
                    curSv->next = (SampledValuesYaml_t*)malloc(sizeof(SampledValuesYaml_t));
                    nSV[0] = nSV[0] + 1;
                    curSv = curSv->next;
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
    return sv.next;
}