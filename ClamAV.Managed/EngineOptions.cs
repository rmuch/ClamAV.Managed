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
    public enum EngineOptions : uint
    {
         
        None = 0x0,
        
        DisableCache = 0x1,
        
        ForceToDisk = 0x2,
        
        DisablePEStats = 0x4,
        
        DisablePECerts = 0x8,
        
        PEDumpCerts =  0x10

    }

}
