#include <Python.h>

static PyObject* pyOpenSvSharedMemory(PyObject* self, PyObject* args){
    PyObject* shmName;

    if (!PyArg_ParseTuple)

}

static PyMethodDef methods[] = {
    {"openSvMemory_c", pyOpenSvSharedMemory, METH_VARARGS, "Open a SV Shared Memory and return it's pointer"},
    {NULL, NULL, 0, NULL}
};

static struct PyModuleDef module = {
    PyModuleDef_HEAD_INIT,
    "pyShmMemory",
    "Create shared memory for transient replay",
    -1,
    methods
};


PyMODINIT_FUNC PyInit_pyCFunctions(void){
    import_array();
    return PyModule_Create(&module);
}