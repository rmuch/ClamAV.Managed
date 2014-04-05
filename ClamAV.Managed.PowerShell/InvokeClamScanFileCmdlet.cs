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

using System.Management.Automation;

namespace ClamAV.Managed.PowerShell
{
    /// <summary>
    /// Cmdlet wrapping the ClamEngine.ScanFile() method.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Invoke, "ClamScanFile")]
    public class InvokeClamScanFileCmdlet : Cmdlet
    {
        /// <summary>
        /// Parameter accepting a ClamEngine instance.
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "ClamEngine created by New-ClamEngine.")]
        public ClamEngine Engine { get; set; }

        /// <summary>
        /// Parameter accepting the path to the file to scan.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "Path to the file to scan.")]
        public string Path { get; set; }

        /// <summary>
        /// Scans a file for viruses.
        /// </summary>
        protected override void ProcessRecord()
        {
            string virusName;

            var scanResult = Engine.ScanFile(Path, out virusName);

            var fileScanResult = new FileScanResult(Path, scanResult == ScanResult.Virus, virusName);

            WriteObject(fileScanResult);
        }
    }
}
