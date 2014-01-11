# PowerShell utility for retrieving ClamAV definitions
# This file is path of the ClamAV.Managed project
#
#   Copyright (c) 2014 Rupert Muchembled
#   ClamAV.Managed is licensed under the GNU GPL v2 or any later version.

param ([string]$path = "", [string]$mirror = "http://database.clamav.net")

Write-Host "PowerShell script for fetching ClamAV databases"
Write-Host "Usage: > .\FetchDatabases.ps1 [save-path] [database-mirror]"
Write-Host "  Part of the ClamAV.Managed project"
Write-Host "  Copyright (c) 2014 Rupert Muchembled"
Write-Host "  ClamAV.Managed is licensed under the GNU GPL v2 or any later version."
Write-Host ""
Write-Host "Using mirror $mirror"

if ($mirror -eq "http://database.clamav.net")
{
    Write-Host "You are using the default mirror! It will be slow."
    Write-Host "Please specify another mirror from http://www.clamav.net/mirrors.html."
}

if ($path -eq "")
{
    Write-Host "Downloading ClamAV definitions to current directory..."
}
else
{
    Write-Host "Downloading ClamAV definitions to $path..."
    $path += "\"
    New-Item -ItemType Directory -Force -Path $path | Out-Null

    if (!(Test-Path -PathType Container $path))
    {
        Write-Host "Failed to create directory $path and folder does not already exist"
        exit 1
    }
}

$webClient = New-Object System.Net.WebClient

Write-Host "Fetching main database..."
$webClient.DownloadFile(($mirror + "/main.cvd"), ($path + "main.cvd"))

Write-Host "Fetching daily database..."
$webClient.DownloadFile(($mirror + "/daily.cvd"), ($path + "daily.cvd"))

Write-Host "Fetching bytecode database..."
$webClient.DownloadFile(($mirror + "/bytecode.cvd"), ($path + "bytecode.cvd"))

Write-Host "Fetching safe browsing database..."
$webClient.DownloadFile(($mirror + "/safebrowsing.cvd"), ($path + "safebrowsing.cvd"))

Write-Host "Script complete."
