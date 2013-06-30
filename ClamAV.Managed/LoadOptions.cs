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
    /// Option flags for loading definitions databases.
    /// </summary>
    [Flags]
    public enum LoadOptions
    {
        /// <summary>
        /// Load a recommended set of scan options.
        /// </summary>
        StandardOptions = 1,
        /// <summary>
        /// Load phishing signatures.
        /// </summary>
        PhishingSignatures = 2,
        /// <summary>
        /// Initialize the phishing detection module and load .wdb and .pdb files.
        /// </summary>
        PhishingURLs = 4,
        /// <summary>
        /// Load signatures for potentially unwanted applications.
        /// </summary>
        PotentiallyUnwantedApplications = 8,
        /// <summary>
        /// Only load official signatures from digitally signed databases.
        /// </summary>
        OfficialOnly = 16,
        /// <summary>
        /// Load bytecode.
        /// </summary>
        Bytecode = 32
    }
}
