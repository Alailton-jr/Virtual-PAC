
// #include "QualityMeter_lib.h"
#include <Python.h>

// gcc -shared -o QualityMeter_lib.so -fPIC -I /usr/include/python3.11/ vQualityMeter/src/C_Code/sampledValue.c

void addSampledValue(int index, uint8_t* svId){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL){
        shm_setup_s svMemory = createSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    }
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;

    strcpy(sv[index].svId, "TRTC");
    sv[index].smpRate = 80;
    sv[index].freq = 60;
    sv[index].snifferValues = (int32_t **)malloc(sv[index].freq*sizeof(int32_t*));
    for (int i = 0; i < sv[index].freq; i++){
        sv[index].snifferValues[i] = (int32_t *)malloc(sv[index].smpRate*sizeof(int32_t));
    }
    sv[index].idxBuffer = 0;
    sv[index].idxCycle = 0;
    sv[index].idxProcessed = 0;
    sv[index].cycledCaptured = 0;

    printf("Added Sampled Value\n");
}

void deleteSampledValue(int index){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL) return;
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;
    free(sv[index].snifferValues);
    sv[index].snifferValues = NULL;
}

void deleteSampledValueMemory(){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    printf("%d", MAX_SAMPLED_VALUES);
    if (svMemory.ptr == NULL) return;
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;
    for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
        if (sv[i].snifferValues == NULL) continue;
        free(sv[i].snifferValues);
    }
    deleteSharedMemory(&svMemory);
}

int main(){
    return 0;
}


// Python Export
static PyObject* addSampledValue_py(PyObject* self, PyObject* args) {
    int index;
    uint8_t* svId;
    uint16_t smpRate;
    uint16_t freq;
    
    // Parse arguments from Python
    if (!PyArg_ParseTuple(args, "isH", &index, &svId, &freq, &smpRate)) {
        return NULL;
    }
    
    // Call C function
    addSampledValue(index, svId, freq, smpRate);
    
    // Return None
    return Py_None;
}

static PyObject* deleteSampledValue_py(PyObject* self, PyObject* args) {
    int index;
    
    // Parse arguments from Python
    if (!PyArg_ParseTuple(args, "i", &index)) {
        return NULL;
    }
    
    // Call C function
    deleteSampledValue(index);
    
    // Return None
    return Py_None;
}

static PyObject* deleteSampledValueMemory_py(PyObject* self, PyObject* args) {
    // Call C function
    deleteSampledValueMemory();
    
    // Return None
    return Py_None;
}

static PyMethodDef QualityMeter_libMethods[] = {
    {"addSampledValue", addSampledValue_py, METH_VARARGS, "Add a sampled value"},
    {"deleteSampledValue", deleteSampledValue_py, METH_VARARGS, "Delete a sampled value"},
    {"deleteSampledValueMemory", deleteSampledValueMemory_py, METH_NOARGS, "Delete all sampled values from memory"},
    {NULL, NULL, 0, NULL} // Sentinel
};
PyMODINIT_FUNC PyInit_QualityMeter_lib(void) {
    static struct PyModuleDef moduledef = {
        PyModuleDef_HEAD_INIT,
        "QualityMeter_lib",      // Name of the module
        "Documentation of the QualityMeter_lib module", // Module documentation
        -1,                      // Size of per-interpreter state of the module
        QualityMeter_libMethods,                    // Methods for the module (you may add module functions here)
        NULL,                    // Slot definition table for the module (if you define slots)
        NULL,                    // Optional traversal function for GC
        NULL,                    // Optional clear function for GC
        NULL                     // Optional module deallocation function
    };
    PyObject *module = PyModule_Create(&moduledef);
    if (module == NULL)
        return NULL;
    return module;
}