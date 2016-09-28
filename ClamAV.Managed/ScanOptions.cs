/*
 * ClamAV.Managed - Managed bindings for ClamAV
 * Copyright (C) 2011, 2013-2014, 2016 Rupert Muchembled
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

namespace ClamAV.Managed
{
    /// <summary>
    /// Option flags for performing scans.
    /// </summary>
    [Flags]
    public enum ScanOptions : uint
    {
        /// <summary>
        /// Alias for a recommended set of scan options.
        /// </summary>
        StandardOptions = Archive | ScanMail | OLE2 | PDF | HTML | PE | Algorithmic | ELF,
        /// <summary>
        /// Disable support for special files.
        /// </summary>
        Raw = 0x0,
        /// <summary>
        /// Transparently scan various archive formats.
        /// </summary>
        Archive = 0x1,
        /// <summary>
        /// Detect encrypted archives as viruses.
        /// </summary>
        BlockEncryptedFiles = 0x8,
        /// <summary>
        /// Scan mail files.
        /// </summary>
        ScanMail = 0x02,
        /// <summary>
        /// Scan OLE2 containers, including Microsoft Office files and Windows Installer packages.
        /// </summary>
        OLE2 = 0x4,
        /// <summary>
        /// Scan Adobe PDF files.
        /// </summary>
        PDF = 0x4000,
        /// <summary>
        /// Enable deep scanning and unpacking of Portable Executable files.
        /// </summary>
        PE = 0x20,
        /// <summary>
        /// Enable support for ELF executable files.
        /// </summary>
        ELF = 0x2000,
        /// <summary>
        /// Try to detect and mark broken executables.
        /// </summary>
        BlockBroken = 0x40,
        /// <summary>
        /// Enable HTML normalisation (including ScrEnc decryption).
        /// </summary>
        HTML = 0x10,
        /// <summary>
        /// Enable algorithmic virus detection.
        /// </summary>
        Algorithmic = 0x200,
        /// <summary>
        /// Always block SSL mismatches in URLs.
        /// </summary>
        PhishingBlockSSL = 0x800,
        /// <summary>
        /// Always block cloaked URLs.
        /// </summary>
        PhishingBlockCloak = 0x1000,
        /// <summary>
        /// Enable the DLP module to scan for credit card numbers and SSNs.
        /// </summary>
        Structured = 0x8000,
        /// <summary>
        /// Search for SSNs structured as xx-yy-zzzz.
        /// </summary>
        StructuredSSNNormal = 0x10000,
        /// <summary>
        /// Search for SSNs structured as xxyyzzzz.
        /// </summary>
        StructuredSSNStripped = 0x20000,
        /// <summary>
        /// Scan RFC1341 messages split over many emails.
        /// </summary>
        PartialMessage = 0x40000,
        /// <summary>
        /// Allow heuristic matches to take precedence.
        /// </summary>
        HeuristicPrecedence = 0x80000,
        /// <summary>
        /// OLE2 containers containing VBA macros will be marked as infected.
        /// </summary>
        BlockMacros = 0x100000,
        /// <summary>
        /// Return all matches in a scan result.
        /// </summary>
        AllMatches = 0x200000,
        /// <summary>
        /// Enable scanning of SWF files.
        /// </summary>
        SWF = 0x400000,
        /// <summary>
        /// Detect partition intersections in raw DMGs using heuristics.
        /// </summary>
        PartitionIntersections = 0x800000,
        /// <summary>
        /// Scan XML-based document files. If this option is turned off, the files will still be scanned, but without additional processing.
        /// </summary>
        XmlDocs = 0x1000000,
        /// <summary>
        /// Scan HPW3 document files. If this option is turned off, the files will still be scanned, but without additional processing.
        /// </summary>
        Hwp3 = 0x2000000,
        /// <summary>
        /// Collect performance timings.
        /// </summary>
        ScanPerformanceInfo = 0x40000000
    }
}
