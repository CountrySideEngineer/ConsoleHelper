# OutputRecorder

[![LICENSE](https://img.shields.io/badge/License-MIT-brightfreen.svg)](https://spdx.org/licenses/MIT)  
[![APP_VER](https://img.shields.io/badge/Application-1.0.1-brightfreen?logo=.NET)](https://github.com/CountrySideEngineer/ConsoleHelper/releases/tag/Release_1.0.1)  
[![LIB_VER](https://img.shields.io/badge/library-0.3.1-brightfreen?logo=.NET)](https://github.com/CountrySideEngineer/ConsoleHelper/releases/tag/Release_1.0.1)  

ConsoleHelper is an application that records the contents of the standard output of a specified application.

## Features  

When outputting the contents of standard output to a file, many users employ redirection. However, the method does not allow user to check the contents of the standard output during execution. This application simultaneously observes the standard output of a process and outputs it to a file.

## How to use.

To use this application, input the command into command interface.
```
ConsoleHelper.exe path/to/file/to/run
```

The content of application's standard output are output to a file in the same folder of the application. The file name is **"ApplicationName.log"**.
