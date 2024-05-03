
#define NPY_NO_DEPRECATED_API NPY_1_7_API_VERSION

#include <Python.h>
#include <numpy/arrayobject.h>
#include "sampledValue.h"



static PyObject* pyOpenSvSharedMemory(PyObject* self, PyObject* args){

    sampledValue_t *sv = openSampledValue(2);
    long int ptr = (long int)sv;
    return Py_BuildValue("l", ptr);
}

static PyObject* pyAddSampledValue(PyObject* self, PyObject* args){
    PyObject *py_newSvData, *py_svIdx;
    
    // Parse the arguments
    if (!PyArg_ParseTuple(args, "OO", &py_newSvData, &py_svIdx)) {
        PyErr_SetString(PyExc_RuntimeError, "Failed to parse arguments");
        return NULL;
    }

    long newSvPtr = PyLong_AsLong(py_newSvData);

    // Extract the sampledValue_t structure from the Python object
    sampledValue_t *newSv = (sampledValue_t *) newSvPtr;

    // Extract the index from the Python object
    int svIdx = PyLong_AsLong(py_svIdx);

    // Call your C function passing the sampledValue_t structure and the index
    addSampledValue(svIdx, newSv);

    Py_RETURN_NONE;
}

static PyObject* pyGetQualityAnalyseData(PyObject* self, PyObject* args){
    PyObject *py_svPtr;
    int svIdx;

    if (!PyArg_ParseTuple(args, "Oi", &py_svPtr, &svIdx)) {
        PyErr_SetString(PyExc_RuntimeError, "Failed to parse arguments");
        return NULL;
    }
    printf("svIdx: %d\n", svIdx);

    long int svPtr = PyLong_AsLong(py_svPtr);

    printf("svPtr: %ld\n", svPtr); 

    sampledValue_t *sv = (sampledValue_t *) svPtr;

    printf("svId: %s\n", sv[svIdx].svId);
    
    QualityAnalyse_t *analyse = &(sv[svIdx].analyseData);
    
    return Py_BuildValue("l", (long int) analyse);
}

static PyObject* pyDeleteSvShm(PyObject* self, PyObject* args){
    deleteSampledValueMemory();
    Py_RETURN_NONE;
}

static PyMethodDef methods[] = {
    {"openSvMemory_c", pyOpenSvSharedMemory, METH_VARARGS, "Open a SV Shared Memory and return it's pointer"},
    {"addSampledValue_c", pyAddSampledValue, METH_VARARGS, "Add a sampled value to the shared memory"},
    {"getQualityAnalyseData_c", pyGetQualityAnalyseData, METH_VARARGS, "Get the Quality Analyse Data"},
    {"deleteSvShm_c", pyDeleteSvShm, METH_VARARGS, "Delete all sampled values"},
    {NULL, NULL, 0, NULL}
};

static struct PyModuleDef module = {
    PyModuleDef_HEAD_INIT,
    "pyCFunctions",
    "Module to call C functions from Python",
    -1,
    methods
};


PyMODINIT_FUNC PyInit_pyCFunctions(void){
    import_array();
    return PyModule_Create(&module);
}