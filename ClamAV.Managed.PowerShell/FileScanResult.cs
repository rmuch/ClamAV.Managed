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

namespace ClamAV.Managed.PowerShell
{
    /// <summary>
    /// File scan result.
    /// </summary>
    public class FileScanResult
    {
        internal FileScanResult(string path, bool infected, string virusName)
        {
            Path = path;
            Infected = infected;
            VirusName = virusName;
        }

        /// <summary>
        /// Path to the file that has been scanned.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Whether a virus has been detected.
        /// </summary>
        public bool Infected { get; private set; }

        /// <summary>
        /// If a virus has been detected, VirusName will contain the name.
        /// </summary>
        public string VirusName { get; private set; }
    }
}
