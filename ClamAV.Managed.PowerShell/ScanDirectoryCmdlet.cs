/*
 * ClamAV.Managed.PowerShell - Managed bindings for ClamAV - PowerShell cmdlets
 * Copyright (C) 2011, 2013-2014 Rupert Muchembled
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System.Collections.Generic;
using System.Management.Automation;

namespace ClamAV.Managed.PowerShell
{
    /// <summary>
    /// Cmdlet wrapping the ClamEngine.ScanDirectory() method.
    /// </summary>
    [Cmdlet("Scan", "Directory")]
    public class ScanDirectoryCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "ClamEngine created by New-ClamEngine.")]
        public ClamEngine Engine { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Whether clean files should be included in the results.")]
        public SwitchParameter IncludeCleanFiles { get; set; }

        [Parameter(Mandatory = true, Position = 0, HelpMessage = "Path to the directory to scan.")]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            var infectedFiles = new List<FileScanResult>();

            Engine.ScanDirectory(Path, (path, result, name) =>
            {
                if (IncludeCleanFiles.IsPresent || result == ScanResult.Virus)
                    infectedFiles.Add(new FileScanResult
                    {
                        Path = path,
                        Infected = result == ScanResult.Virus,
                        VirusName = name
                    });
            });

            WriteObject(infectedFiles, true);
        }
    }
}
