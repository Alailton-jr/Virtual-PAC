#include "util.h"


void get_script_dir(char *path, size_t size) {
    char buf[1024];
    memset(buf, 0, sizeof(buf));
    readlink("/proc/self/exe", buf, sizeof(buf));
    char *last_slash = strrchr(buf, '/');
    if (last_slash != NULL) {
        *last_slash = '\0';
    }
    snprintf(path, size, "%s", buf);
}
