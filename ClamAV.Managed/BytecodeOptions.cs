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
    /// Bytecode security level.
    /// </summary>
    public enum BytecodeSecurity : uint
    {
        /// <summary>
        /// Trust all.
        /// </summary>
        TrustAll = 0,
        /// <summary>
        /// Only trust signed bytecode.
        /// </summary>
        TrustSigned,
        /// <summary>
        /// Don't trust any bytecode.
        /// </summary>
        TrustNothing
    }

    /// <summary>
    /// Bytecode mode.
    /// </summary>
    public enum BytecodeMode : uint
    {
        /// <summary>
        /// Use JIT compliation where possible, and fall back to the interpreter.
        /// </summary>
        Auto = 0,
        /// <summary>
        /// Use only JIT compilation.
        /// </summary>
        Jit,
        /// <summary>
        /// Use only the interpreter.
        /// </summary>
        Interpreter,
        /// <summary>
        /// Use both and compare results.
        /// </summary>
        Test,
        /// <summary>
        /// Off. Not settable.
        /// </summary>
        Off
    }
}
