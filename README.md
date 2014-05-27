# Managed Bindings for ClamAV

## Introduction

ClamAV.Managed is a library written in C# for the .NET Framework and Mono,
providing managed bindings for libclamav. It includes
ClamAV.Managed.PowerShell, a set of PowerShell cmdlets for ClamAV scanning.
It also comes with GUI sample code for performing virus scanning.

## Prerequisites

To use ClamAV.Managed, P/Invoke must be able to locate libclamav, either in 
your binary's execution directory or at some location in your system's library
search path.

You can obtain ClamAV binaries compiled for Windows at
http://oss.netfarm.it/clamav/. It is recommended to use a version of libclamav
compiled with Microsoft Visual C++.

ClamAV.Managed is designed to be platform-agnostic with the intention of
supporting Mono on Linux and Mac OS X. Although this is currently not a widely
used combination, users are encouraged to try the software on these platforms
to help find any possible issues that may arise.

For file scanning to work, you need to download ClamAV databases from 
http://www.clamav.net. Databases can be loaded from an arbitrary directory
by specifying a path in the ClamEngine.LoadDatabase method, or from a folder
in your application's working directory.
This folder may be named `db` or `database`, depending on which build of
libclamav you are running.

## Getting Started

### ClamAV.Managed Library

To get started using ClamAV.Managed in your project, you need to add a 
reference to ClamAV.Managed.dll.

libclamav must be either in your application's binary directory, or some other 
system path where it may be discovered. It is recommended that you bundle 
libclamav with your application, taking care to comply with the terms of the
GNU General Public License, version 2.

You need to obtain ClamAV databases. By default, ClamAV looks for databases in 
a folder called `db` in the current working directory. Depending on the version
of ClamAV, this directory may be called `database` instead. You can specify an 
arbitrary location from which to load virus databases as an argument to the 
ClamEngine.LoadDatabase method.

It is recommended to use the `freshclam` utility to obtain the latest ClamAV
databases, but you can also try the `FetchDatabases.ps1` PowerShell script in
the scripts directory and adapt it to your own purposes, remembering to select
which databases you would like to obtain and changing the download URL to a
local mirror.

You can find a list of ClamAV database mirrors at
http://www.clamav.net/mirrors.html. Databases will be named main.cvd, daily.cvd,
bytecode.cvd, safebrowsing.cvd in the root directory of the mirror. More
information about obtaining ClamAV databases can be found on the ClamAV website.

### ClamAV.Managed.Async Task-based Asynchronous Scanning

ClamAV.Managed provides a task-based asynchronous API using the new async/await
feature for .NET 4.5.1 in the ClamAV.Managed.Async project.

These extensions are still experimental, and you may encounter issues while
performing parallel scans, particularly with a large quantity of long-running
scan tasks.

ClamAV.Managed.Async is implemented as extension methods and can be used by adding
a reference to the library from your project.

### ClamAV.Managed.PowerShell Cmdlets

To use the cmdlets provided by ClamAV.Managed.PowerShell, use the `New-ClamEngine`
cmdlet.

1. Use `Import-Module` to load the ClamAV.Managed.PowerShell library. Assume
   we have created a directory C:\ClamAV-Managed\, containing ClamAV.Managed.dll,
   ClamAV.Managed.PowerShell.dll, ClamAV.Managed.PowerShell.psd1, libclamav.dll
   and a subdirectory containing definitions datases called `db`.
   We're now ready to proceed.
   ```Import-Module C:\ClamAV-Managed\ClamAV.Managed.PowerShell.psd1```
2. Create an instance of the ClamAV engine.
   ```$eng = New-ClamEngine -WithDatabase C:\ClamAV-Managed\db```
3. If you didn't provide a `-WithDatabase` parameter to `New-ClamEngine`, use
   `Import-ClamDatabase` to load a definitions database.
   ```Import-ClamDatabase -Engine $eng -Path C:\ClamAV-Managed\db```
4. To scan a file:
   ```Invoke-ClamScanFile -Engine $eng -Path C:\File.exe```
5. To scan a directory:
   ```Invoke-ClamScanDirectory -Engine $eng -Path C:\Directory\```

`Import-ClamDatabase` will, by default, look for databases in libclamav's
default subdirectory, probably `db` or perhaps `database`, within the
directory containing the ClamAV.Managed.PowerShell dynamic library.
However, you may specify a path manually with the `-DatabasePath` parameter.

## Samples

Refer to the project ClamAV.Managed.Samples.Gui to see an example of
ClamAV.Managed in action. To run the sample, you need to obtain libclamav, as 
described above, and download virus databases to a folder named `db` in the 
binary directory.

ClamAV.Managed.Samples.AsyncGui provides a sample with a user interface
implemented using Windows Presentation Foundation (WPF).

## License Information

This project is licensed under the terms of the GNU General Public License,
version 2. You can find a full copy of the license text in GPLv2.txt.

## Disclaimer

This program is distributed in the hope that it will be useful, but WITHOUT 
ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
FOR A PARTICULAR PURPOSE.

While the public version of this library is still in early development, the
type names and class interfaces are subject to change.
