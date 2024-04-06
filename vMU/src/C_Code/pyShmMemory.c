
// sudo apt-get install python3-numpy

#define NPY_NO_DEPRECATED_API NPY_1_7_API_VERSION

#include <Python.h>
#include <numpy/arrayobject.h>
#include <stdio.h>
#include "shmMemory.h"
#include "transientReplay.h"

static PyObject* pyTransientReplay(PyObject* self, PyObject* args){
    PyObject *py_arr = NULL; //numpy array
    PyObject *py_smpCountPos, *py_asduLength, *py_allDataPos, *py_interGap, *py_maxSmpCount, *py_n_channels, *py_nasdu, *py_isLoop; // uint16_t
    PyObject *py_structShmName, *py_arrShmName, *py_frameShmName, *py_frame; // uint8_t[]

    // Parse the arguments
    if(!PyArg_ParseTuple(args, "OOOOOOOOOOOOO", &py_arr, &py_smpCountPos, &py_asduLength, &py_allDataPos, &py_interGap, &py_maxSmpCount, &py_n_channels, &py_nasdu, &py_isLoop, &py_structShmName, &py_arrShmName, &py_frameShmName, &py_frame)){
        return NULL;
    }

    // Get shm names
    const char *structShmName = PyUnicode_AsUTF8(py_structShmName);
    const char *arrShmName = PyUnicode_AsUTF8(py_arrShmName);
    const char *frameShmName = PyUnicode_AsUTF8(py_frameShmName);

    // Check if the shared memory is created
    shm_setup_s shm_data = openSharedMemory(structShmName, sizeof(replayData_t));
    if (shm_data.ptr != NULL){
        replayData_t *data = (replayData_t*)shm_data.ptr;
        shm_setup_s shm_arr = openSharedMemory((char*)data->arrShmName, 0);
        if (shm_arr.ptr != NULL){
            deleteSharedMemory(&shm_arr);
        }
        shm_setup_s shm_frame = openSharedMemory((char*)data->frameShmName, 0);
        if (shm_frame.ptr != NULL){
            deleteSharedMemory(&shm_frame);
        }
        deleteSharedMemory(&shm_data);
    }


    shm_data = createSharedMemory(structShmName, sizeof(replayData_t));
    if (shm_data.ptr == NULL){
        return NULL;
    }
    replayData_t *data = (replayData_t*)shm_data.ptr;
    memcpy(data->arrShmName, arrShmName, strlen(arrShmName));
    data->arrShmName[strlen(arrShmName)] = '\0';
    memcpy(data->frameShmName, frameShmName, strlen(frameShmName));
    data->frameShmName[strlen(frameShmName)] = '\0';

    // Get the numpy array as a C array
    PyArrayObject *_arr = (PyArrayObject*)PyArray_FROM_OTF(py_arr, NPY_INT32, NPY_ARRAY_IN_ARRAY);
    if(_arr == NULL) return NULL;
    int32_t *arr = (int32_t*)PyArray_DATA(_arr);
    npy_intp *dims = PyArray_DIMS(_arr);
    data->arrLength = dims[0] > dims[1] ? dims[0] : dims[1];
    shm_setup_s shm_arr =  createSharedMemory(arrShmName, data->arrLength * sizeof(int32_t));
    if (shm_arr.ptr == NULL) return NULL;
    memcpy(shm_arr.ptr, arr, data->arrLength * sizeof(int32_t));

    // Get the python bytearray(it's not numpy) as a C array
    uint8_t *frame = (uint8_t*)PyByteArray_AsString(py_frame);
    data->frameLength = PyByteArray_Size(py_frame);
    shm_setup_s shm_frame = createSharedMemory(frameShmName, data->frameLength);
    if (shm_frame.ptr == NULL) return NULL;
    memcpy(shm_frame.ptr, frame, data->frameLength);

    // Get the data from the other arguments inside the struct
    data->smpCountPos = (uint16_t)PyLong_AsLong(py_smpCountPos);
    data->asduLength = (uint16_t)PyLong_AsLong(py_asduLength);
    data->allDataPos = (uint16_t)PyLong_AsLong(py_allDataPos);
    data->interGap = (uint64_t)PyLong_AsLong(py_interGap);
    data->maxSmpCount = (uint32_t)PyLong_AsLong(py_maxSmpCount);
    data->n_channels = (uint16_t)PyLong_AsLong(py_n_channels);
    data->n_asdu = (uint16_t)PyLong_AsLong(py_nasdu);
    data->isLoop = (uint8_t)PyLong_AsLong(py_isLoop);

    printf("Memory %s Created\n", structShmName);
    printf("With: Intergap: %lu; Buffer Size: %lu; Packet Size: %u\n", data->interGap, data->arrLength, data->frameLength);

    // Clean up
    Py_DECREF(_arr);
    Py_RETURN_NONE;
}

static PyMethodDef methods[] = {
    {"shm_transientReplay", pyTransientReplay, METH_VARARGS, "Create shared memory for transient replay"},
    {NULL, NULL, 0, NULL}
};

static struct PyModuleDef module = {
    PyModuleDef_HEAD_INIT,
    "pyShmMemory",
    "Create shared memory for transient replay",
    -1,
    methods
};

PyMODINIT_FUNC PyInit_pyShmMemory(void){
    import_array();
    return PyModule_Create(&module);
}

int main(){
    return 0;
}