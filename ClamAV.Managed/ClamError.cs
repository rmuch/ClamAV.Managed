/*
 * ClamAV.Managed - Managed bindings for ClamAV
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

namespace ClamAV.Managed
{
    /// <summary>
    /// Error status corresponding to ClamAV error codes.
    /// </summary>
    public enum ClamError : uint
    {
        /// <summary>
        /// File is clean.
        /// </summary>
        Clean = 0,
        /// <summary>
        /// Operation completed successfully.
        /// </summary>
        Success = 0,
        /// <summary>
        /// Virus was detected.
        /// </summary>
        Virus,
        /// <summary>
        /// A null argument was provided.
        /// </summary>
        NullArgError,
        /// <summary>
        /// An incorrect argument was provided.
        /// </summary>
        ArgError,
        /// <summary>
        /// Malformed database.
        /// </summary>
        MalformedDatabaseError,
        /// <summary>
        /// 
        /// </summary>
        CvdError,
        /// <summary>
        /// 
        /// </summary>
        VerificationError,
        /// <summary>
        /// 
        /// </summary>
        UnpackError,

        /// <summary>
        /// 
        /// </summary>
        OpenError,
        /// <summary>
        /// 
        /// </summary>
        CreateError,
        /// <summary>
        /// 
        /// </summary>
        UnlinkError,
        /// <summary>
        /// 
        /// </summary>
        StatError,
        /// <summary>
        /// 
        /// </summary>
        ReadError,
        /// <summary>
        /// 
        /// </summary>
        SeekError,
        /// <summary>
        /// 
        /// </summary>
        WriteError,
        /// <summary>
        /// 
        /// </summary>
        DupError,
        /// <summary>
        /// 
        /// </summary>
        AccessError,
        /// <summary>
        /// 
        /// </summary>
        TempFileError,
        /// <summary>
        /// 
        /// </summary>
        TempDirError,
        /// <summary>
        /// 
        /// </summary>
        MapError,
        /// <summary>
        /// 
        /// </summary>
        MemError,
        /// <summary>
        /// 
        /// </summary>
        TimeoutError,

        /// <summary>
        /// 
        /// </summary>
        BreakError,
        /// <summary>
        /// 
        /// </summary>
        MaxRecursionError,
        /// <summary>
        /// 
        /// </summary>
        MaxSizeError,
        /// <summary>
        /// 
        /// </summary>
        MaxFilesError,
        /// <summary>
        /// 
        /// </summary>
        FormatError,
        /// <summary>
        /// 
        /// </summary>
        BytecodeError,
        /// <summary>
        /// 
        /// </summary>
        BytecodeTestFailError,

        /// <summary>
        /// 
        /// </summary>
        LockError,
        /// <summary>
        /// 
        /// </summary>
        BusyError,
        /// <summary>
        /// 
        /// </summary>
        StateError,

        /// <summary>
        /// 
        /// </summary>
        LastError
    }
}
