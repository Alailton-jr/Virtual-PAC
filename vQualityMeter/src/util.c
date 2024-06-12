#include "util.h"
#include <dirent.h>
#include <sys/types.h>
#include <errno.h>
#include <string.h>

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


void remove_dir(const char *name) {
    struct dirent *entry;
    DIR *dir = opendir(name);

    if (!dir) {
        perror("Could not open directory");
        return;
    }

    while ((entry = readdir(dir))!= NULL) {
        if (strcmp(entry->d_name, ".") == 0 || strcmp(entry->d_name, "..") == 0) continue;

        char path[1024];
        snprintf(path, sizeof(path), "%s/%s", name, entry->d_name);

        if (entry->d_type == DT_DIR) {
            remove_dir(path);
        } else {
            unlink(path);
        }
    }

    closedir(dir);
    rmdir(name);
}