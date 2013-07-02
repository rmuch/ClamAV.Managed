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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ClamAV.Managed
{
    /// <summary>
    /// Encapsulates an instance of the ClamAV engine.
    /// </summary>
    public class ClamAV : IDisposable
    {
        /// <summary>
        /// Whether the ClamAV library has been globally initialized.
        /// </summary>
        private static bool initialized = false;

        /// <summary>
        /// Global initialization method to load the ClamAV library into the process.
        /// This method should be called once during the lifetime of a host process.
        /// </summary>
        private static void initialize()
        {
            if (!initialized)
            {
                int result = UnsafeNativeMethods.cl_init(UnsafeNativeMethods.CL_INIT_DEFAULT);

                if (result != UnsafeNativeMethods.CL_SUCCESS)
                    throw new ClamAVException(result, ErrorString(result));
            }

            initialized = true;
        }

        /// <summary>
        /// Returns the description string of a ClamAV error code.
        /// </summary>
        /// <param name="error">ClamAV error code.</param>
        /// <returns>Error description.</returns>
        protected static string ErrorString(int error)
        {
            IntPtr result = UnsafeNativeMethods.cl_strerror(error);

            return UnmarshalString(result);
        }

        /// <summary>
        /// Unmarshals a pointer to an ANSI string value.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        protected static string UnmarshalString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
        /// Internal handle to the ClamAV engine.
        /// </summary>
        private IntPtr _engine;

        /// <summary>
        /// Handle to the unmanaged ClamAV engine belonging to this instance.
        /// </summary>
        public IntPtr Handle
        {
            get { return _engine; }
        }

        /// <summary>
        /// Cached ClamAV version string.
        /// </summary>
        private string _clamVersion;

        /// <summary>
        /// ClamAV engine version string.
        /// </summary>
        public string Version
        {
            get
            {
                if (_clamVersion == null)
                    _clamVersion = UnmarshalString(UnsafeNativeMethods.cl_retver());

                return _clamVersion;
            }
        }

        /// <summary>
        /// Creates a new ClamAV engine instance.
        /// </summary>
        /// <param name="debug">Enable verbose ClamAV debug logging.</param>
        public ClamAV(bool debug = false)
        {
            // Enable debug mode.
            if (debug)
                UnsafeNativeMethods.cl_debug();
            
            // Initialize ClamAV library.
            initialize();

            // Create a new instance of the ClamAV engine.
            _engine = UnsafeNativeMethods.cl_engine_new();

            // Handle an error condition.
            if (_engine == IntPtr.Zero)
            {
                int error = _engine.ToInt32();

                throw new ClamAVException(error, ErrorString(error));
            }
        }

        ~ClamAV()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool b)
        {
            // Free ClamAV engine instance.
            int result = UnsafeNativeMethods.cl_engine_free(_engine);

            if (result != UnsafeNativeMethods.CL_SUCCESS)
            {
                // XXX: FxCop doesn't like throwing an exception on Dispose - 
                //      this should be removed or only thrown on very serious errors.
                throw new ClamAVException(result, ErrorString(result));
            }
        }

        /// <summary>
        /// Loads a database file or directory into the engine.
        /// </summary>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        /// <param name="options">Options with which to load the database.</param>
        public void LoadDatabase(string path = "", LoadOptions options = LoadOptions.StandardOptions)
        {
            uint signo = 0;
            uint optnum = 0;

            // Convert LoadOptions parameter.
            optnum = (uint)options;

            // If the path hasn't been specified, use ClamAV's default hardcoded path.
            if (string.IsNullOrEmpty(path))
                path = UnmarshalString(UnsafeNativeMethods.cl_retdbdir());

            // Invoke the native method to load the database.
            var loadResult = (UnsafeNativeMethods.cl_error_t)UnsafeNativeMethods.cl_load(path, _engine, ref signo, optnum);

            if (loadResult != UnsafeNativeMethods.cl_error_t.CL_SUCCESS)
            {
                throw new ClamAVException((int)loadResult, ErrorString((int)loadResult));
            }

            // Compile the database.
            var compileResult = (UnsafeNativeMethods.cl_error_t)UnsafeNativeMethods.cl_engine_compile(_engine);

            if (compileResult != UnsafeNativeMethods.cl_error_t.CL_SUCCESS)
            {
                throw new ClamAVException((int)compileResult, ErrorString((int)compileResult));
            }
        }

        /// <summary>
        /// Scans a file for viruses.
        /// </summary>
        /// <param name="filePath">Path to the file to be scanned.</param>
        /// <param name="virusName">Output variable for the virus name, if detected.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <returns></returns>
        public ScanResult ScanFile(string filePath, out string virusName, ScanOptions scanOptions = ScanOptions.StandardOptions)
        {
            IntPtr virusNamePtr = IntPtr.Zero;

            ulong scanned = 0;
            uint options = 0;

            // Convert ScanOptions parameter.
            options = (uint)scanOptions;

            // Perform scan
            var result = (UnsafeNativeMethods.cl_error_t)UnsafeNativeMethods.cl_scanfile(filePath, ref virusNamePtr, ref scanned, _engine, options);

            if (result == UnsafeNativeMethods.cl_error_t.CL_CLEAN)
            {
                // File is clean.
                virusName = string.Empty;

                return ScanResult.Clean;
            }
            else if (result == UnsafeNativeMethods.cl_error_t.CL_VIRUS)
            {
                // We've detected a virus.
                virusName = Marshal.PtrToStringAnsi(virusNamePtr);

                return ScanResult.Virus;
            }
            else
            {
                // Probably an error condition.
                throw new ClamAVException((int)result, ErrorString((int)result));
            }
        }
    }
}
