#include <Python.h>
#include "sampledValue.h"

static PyObject* pyOpenSvSharedMemory(PyObject* self, PyObject* args){
    sampledValue_t *sv = openSampledValue(0);
    PyObject *capsule = PyCapsule_New(sv, "sampledValue_t", NULL);
    if (capsule == NULL){
        PyErr_SetString(PyExc_RuntimeError, "Failed to create capsule object");
        return NULL;
    }
    return capsule;
}

static PyMethodDef methods[] = {
    {"openSvMemory_c", pyOpenSvSharedMemory, METH_VARARGS, "Open a SV Shared Memory and return it's pointer"},
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