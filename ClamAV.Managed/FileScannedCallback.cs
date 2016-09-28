/*
 * ClamAV.Managed - Managed bindings for ClamAV
 * Copyright (C) 2011, 2013-2016 Rupert Muchembled
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

namespace ClamAV.Managed
{
    /// <summary>
    /// Delegate for file scan completion callback.
    /// </summary>
    /// <param name="path">Path to file which has been scanned.</param>
    /// <param name="result">Scan parameters.</param>
    /// <param name="virusName">Signature name if file is infected.</param>
    public delegate void FileScannedCallback(string path, ScanResult result, string virusName);
}