#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

def runBuild():
    from distutils.core import setup, Extension
    import numpy as np
    from os import path, listdir, getcwd, environ
    from shutil import move, rmtree
    #get python.h include location
    import sys
    include = sys.executable
    print(include)
    c_folder = path.abspath(path.join(path.dirname(__file__), '..', 'C_Code'))
    removeDir = path.abspath(path.join(path.dirname(__file__), '..', '..', 'build'))
    build_folder = path.abspath(path.join(path.dirname(__file__)))

    args = ['-Wno-unused-function', '-Wno-incompatible-pointer-types', '-Wno-unused-variable', '-Wno-implicit-function-declaration']

    exts = [
        Extension(
            'pyCFunctions',
            sources=[path.join(c_folder, 'pyFunctions.c')],
            include_dirs=[np.get_include()],
            extra_compile_args=args,
        )
    ]

    setup(
        name=" ",
        version="1.0.0",
        description=" ",
        author=" ",
        author_email=" ",
        ext_modules=exts,
        script_args=["build_ext", "-b", "build", "-if"]
    )

    files = listdir(getcwd())
    for _file in files:
        if _file.endswith('.so'):
            move(_file, path.join(build_folder, _file))
    if path.exists(removeDir):
        rmtree(removeDir)

runBuild()