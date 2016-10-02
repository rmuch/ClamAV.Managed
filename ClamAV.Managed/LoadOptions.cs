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
    /// Option flags for loading definitions databases.
    /// </summary>
    [Flags]
    public enum LoadOptions : uint
    {
        /// <summary>
        /// Load a recommended set of scan options.
        /// </summary>
        StandardOptions = PhishingSignatures | PhishingURLs | Bytecode,
        /// <summary>
        /// Load phishing signatures.
        /// </summary>
        PhishingSignatures = 0x2,
        /// <summary>
        /// Initialize the phishing detection module and load .wdb and .pdb files.
        /// </summary>
        PhishingURLs = 0x8,
        /// <summary>
        /// Load signatures for potentially unwanted applications.
        /// </summary>
        PotentiallyUnwantedApplications = 0x10,
        /// <summary>
        /// Include potentially unwanted applications.
        /// </summary>
        IncludePotentiallyUnwantedApplications = 0x100,
        /// <summary>
        /// Exclude potentially unwanted applications.
        /// </summary>
        ExcludePotentiallyUnwantedApplications = 0x200,
        /// <summary>
        /// Only load official signatures from digitally signed databases.
        /// </summary>
        OfficialOnly = 0x1000,
        /// <summary>
        /// Load bytecode.
        /// </summary>
        Bytecode = 0x2000,
        /// <summary>
        /// Load unsigned bytecode.
        /// </summary>
        UnsignedBytecode = 0x8000,
        /// <summary>
        /// Collect and print bytecode performance statistics.
        /// </summary>
        BytecodeStats = 0x20000,
        /// <summary>
        /// 
        /// </summary>
        Enhanced = 0x40000,
        /// <summary>
        /// WHAT?
        /// </summary>
        PcreStats = 0x80000,
        /// <summary>
        /// WTF is YARA?
        /// </summary>
        YaraExclude = 0x100000,
        /// <summary>
        /// WTF is YARA?
        /// </summary>
        YaraOnly = 0x200000
    }
}
