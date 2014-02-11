# Automatic creation of ClamAV.Managed test directory
# This file is path of the ClamAV.Managed project
#
#   Copyright (c) 2014 Rupert Muchembled
#   ClamAV.Managed is licensed under the GNU GPL v2 or any later version.

Write-Host "Automatic creation of ClamAV.Managed test directory"
Write-Host "  Part of the ClamAV.Managed project"
Write-Host "  Copyright (c) 2014 Rupert Muchembled"
Write-Host "  ClamAV.Managed is licensed under the GNU GPL v2 or any later version."

New-Item -ItemType Directory "../TestFiles"
if (!(Test-Path -PathType Container "../TestFiles"))
{
    Write-Host "Failed to create directory ../TestFiles and folder does not already exist"
    exit 1
}

.\FetchDatabases.ps1 "../TestFiles/db" "http://clamav.oucs.ox.ac.uk"
