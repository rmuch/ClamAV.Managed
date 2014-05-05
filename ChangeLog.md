ClamAV.Managed Change Log
=========================

0.2.0.0 - 5 May 2014
--------------------

ClamAV.Managed 0.2.0.0 is the second versioned release of the project, adding
several new features and many small fixes and improvements, including:

 * Preliminary support for PowerShell, implementing a set of cmdlets around the
   ClamAV.Managed API.
 * PowerShell helper script for downloading definition databases from a
   selected HTTP mirror.
 * Upgraded from ClamAV 0.97 to 0.98, adding support for settings and functions
   new in the latest version.
 * Functions which do not require an engine handle are now callable statically.
 * Improved error condition handling.
 * Asynchronous API using async/await.
 * A GUI sample project using WPF and the new async/await API.

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
