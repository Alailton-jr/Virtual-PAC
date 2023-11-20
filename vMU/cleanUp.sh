#!/bin/bash -x

find . -type d -name ".git" -prune -o -type f ! -name "*.*" -exec rm {} \;

