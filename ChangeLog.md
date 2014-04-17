ClamAV.Managed Change Log
=========================

0.2.0.0 - Current
-----------------

ClamAV.Managed 0.2.0.0 makes the following changes:

 * Preliminary support for PowerShell, implementing a set of cmdlets around the
   ClamAV.Managed API.
 * Upgraded from ClamAV 0.97 to 0.98, adding support for new settings added in
   the latest version.
 * Functions which do not require an engine handle are now callable statically.
 * Improved error handling.
 * Asynchronous API using async/await.

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
