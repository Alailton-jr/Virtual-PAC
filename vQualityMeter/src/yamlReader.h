#ifndef YAML_READER_H
#define YAML_READER_H

#define MAX_STR_LEN 100

typedef struct QualityEventsYaml{
    double topThreshold;
    double bottomThreshold;
    double minDuration;
    double maxDuration;
} QualityEventsYaml_t;

typedef struct SampledValuesYaml{
    char SVID[MAX_STR_LEN];
    char macSrc[MAX_STR_LEN];
    int frequency;
    int smpRate;
    int noAsdu;
    int noChannels;
    int nominalVoltage;
    int nominalCurrent;
    QualityEventsYaml_t sag;
    QualityEventsYaml_t swell;
    QualityEventsYaml_t interruption;
    QualityEventsYaml_t overVoltage;
    QualityEventsYaml_t underVoltage;
    QualityEventsYaml_t sustainedinterruption;
} SampledValuesYaml_t;

#include "sampledValue.h"
// extern SampledValue_t;

typedef struct _IO_FILE FILE;

int parse_yaml(FILE *file, int *nSV, SampledValuesYaml_t* svYaml);
void printSampledValueYaml(SampledValuesYaml_t *sv, int numSv);
int parseYaml2Sv(SampledValuesYaml_t *sv, int numSv, SampledValue_t *svArr);


#endif //YAML_READER_H