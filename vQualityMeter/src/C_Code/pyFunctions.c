
#define NPY_NO_DEPRECATED_API NPY_1_7_API_VERSION

#include <Python.h>
#include <numpy/arrayobject.h>
#include "sampledValue.h"



static PyObject* pyOpenSvSharedMemory(PyObject* self, PyObject* args){

    sampledValue_t *sv = openSampledValue(0);
    PyObject *capsule = PyCapsule_New(sv, "sampledValue_t", NULL);
    if (capsule == NULL){
        PyErr_SetString(PyExc_RuntimeError, "Failed to create capsule object");
        return NULL;
    }
    long int ptr = (long int)sv;
    // return both in tuple
    return Py_BuildValue("Ol", capsule, ptr);
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

    // Debug
    QualityAnalyse_t *analyse = &(newSv->analyseData);

    QualityEvent_t *event = &(analyse->sag);
    printf("%d\n", event->topThreshold);
    printf("%d\n", event->bottomThreshold);


    // Extract the index from the Python object
    int svIdx = PyLong_AsLong(py_svIdx);

    // Call your C function passing the sampledValue_t structure and the index
    addSampledValue(svIdx, newSv);

    Py_RETURN_NONE;
}



static PyMethodDef methods[] = {
    {"openSvMemory_c", pyOpenSvSharedMemory, METH_VARARGS, "Open a SV Shared Memory and return it's pointer"},
    {"addSampledValue_c", pyAddSampledValue, METH_VARARGS, "Add a sampled value to the shared memory"},
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