#!/bin/bash

# Specify the directory where your Python files are located
target_directory=src/Python_Code

current_directory=$(pwd)

# Set the new shebang

new_shebang="#!$current_directory/vEnv/bin/python3"

# Find and update shebangs in Python files
find "$target_directory" -type f -name "*.py" -exec sed -i "1s@.*@$new_shebang@" {} \;


