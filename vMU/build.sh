#!/bin/bash -x

defaultTag=""
path=$(pwd)/C_Code

gcc -o replay $path/replay.c -lpthread $defaultTag
gcc -o replaySeq $path/replaySeq.c -lpthread $defaultTag
gcc -o sniffer $path/sniffer.c -lpthread $defaultTag