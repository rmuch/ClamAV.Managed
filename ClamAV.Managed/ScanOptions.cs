/*
 * ClamAV.Managed - Managed bindings for ClamAV
 * Copyright (C) 2011, 2013 Rupert Muchembled
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClamAV.Managed
{
    /// <summary>
    /// Option flags for performing scans.
    /// </summary>
    [Flags]
    public enum ScanOptions
    {
        /// <summary>
        /// Alias for a recommended set of scan options.
        /// </summary>
        StandardOptions = 1,
        /// <summary>
        /// Disable support for special files.
        /// </summary>
        Raw = 2,
        /// <summary>
        /// Transparently scan various archive formats.
        /// </summary>
        Archive = 4,
        /// <summary>
        /// Detect encrypted archives as viruses.
        /// </summary>
        BlockEncryptedFiles = 8,
        /// <summary>
        /// Scan mail files.
        /// </summary>
        ScanMail = 16,
        /// <summary>
        /// Scan OLE2 containers, including Microsoft Office files and Windows Installer packages.
        /// </summary>
        OLE2 = 32,
        /// <summary>
        /// Scan Adobe PDF files.
        /// </summary>
        PDF = 64,
        /// <summary>
        /// Enable deep scanning and unpacking of Portable Executable files.
        /// </summary>
        PE = 128,
        /// <summary>
        /// Enable support for ELF executable files.
        /// </summary>
        ELF = 256,
        /// <summary>
        /// Try to detect and mark broken executables.
        /// </summary>
        BlockBroken = 512,
        /// <summary>
        /// Enable HTML normalisation (including ScrEnc decryption).
        /// </summary>
        HTML = 1024,
        /// <summary>
        /// Enable algorithmic virus detection.
        /// </summary>
        Algorithmic = 2048,
        /// <summary>
        /// Always block SSL mismatches in URLs.
        /// </summary>
        PhishingBlockSSL = 4096,
        /// <summary>
        /// Always block cloaked URLs.
        /// </summary>
        PhishingBlockCloak = 8192,
        /// <summary>
        /// Enable the DLP module to scan for credit card numbers and SSNs.
        /// </summary>
        Structured = 16384,
        /// <summary>
        /// Search for SSNs structured as xx-yy-zzzz.
        /// </summary>
        StructuredSSNNormal = 32768,
        /// <summary>
        /// Search for SSNs structured as xxyyzzzz.
        /// </summary>
        StructuredSSNStripped = 65536,
        /// <summary>
        /// Scan RFC1341 messages split over many emails.
        /// </summary>
        PartialMessage = 131072,
        /// <summary>
        /// Allow heuristic matches to take precedence.
        /// </summary>
        HeuristicPrecedence = 262144
    }
}
