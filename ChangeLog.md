ClamAV.Managed Change Log
=========================

0.1.0.0 - 10 January 2014
-------------------------

ClamAV.Managed 0.1.0.0 is the first public release of the project. This release
supports the following feature set:

 * A managed code interface for libclamav, written in C# and implemented using
   P/Invoke to wrap the C library functions.
 * Support for most of the libclamav interface, except for callback functions.
 * Functionality for scanning directories as well as files.
 * A sample project implementing a simple GUI virus scanner, to help developers
   get started.
 * Support for Windows, working on Linux and Mac OS X with Mono.
