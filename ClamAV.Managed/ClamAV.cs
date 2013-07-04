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
        /// Get a numerical settings value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <returns>Setting value.</returns>
        internal long EngineGetNum(UnsafeNativeMethods.cl_engine_field setting)
        {
            return UnsafeNativeMethods.cl_engine_get_num(_engine, setting, IntPtr.Zero);
        }

        /// <summary>
        /// Set a numerical setting value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <param name="value">Setting value.</param>
        internal void EngineSetNum(UnsafeNativeMethods.cl_engine_field setting, long value)
        {
            int i = UnsafeNativeMethods.cl_engine_set_num(_engine, setting, value);
        }

        /// <summary>
        /// Get a string setting value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <returns>Setting value.</returns>
        internal string EngineGetStr(UnsafeNativeMethods.cl_engine_field setting)
        {
            return Marshal.PtrToStringAnsi(UnsafeNativeMethods.cl_engine_get_str(_engine, setting, IntPtr.Zero));
        }

        /// <summary>
        /// Set a string setting value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <param name="value">Setting value.</param>
        internal void EngineSetStr(UnsafeNativeMethods.cl_engine_field setting, string value)
        {
            int i = UnsafeNativeMethods.cl_engine_set_str(_engine, setting, value);
        }

        /// <summary>
        /// Maximum amount of data in a file to be scanned.
        /// </summary>
        public ulong MaxScanSize
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCANSIZE);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCANSIZE, (long)value);
            }
        }

        /// <summary>
        /// Maximum file size to be scanned.
        /// </summary>
        public ulong MaxFileSize
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILESIZE);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILESIZE, (long)value);
            }
        }

        /// <summary>
        /// Maximum recursion depth.
        /// </summary>
        public uint MaxRecursion
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_RECURSION);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_RECURSION, (long)value);
            }
        }

        /// <summary>
        /// Maximum number of files to scan inside an archive or container.
        /// </summary>
        public uint MaxFiles
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILES);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILES, (long)value);
            }
        }

        /// <summary>
        /// Minimum count of credit card numbers to trigger a detection.
        /// </summary>
        public uint MinCCCount
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_CC_COUNT);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_CC_COUNT, (long)value);
            }
        }

        /// <summary>
        /// Minimum count of SSNs to trigger a detection.
        /// </summary>
        public uint MinSSNCount
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_SSN_COUNT);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_SSN_COUNT, (long)value);
            }
        }

        /// <summary>
        /// Potentially unwanted application categories.
        /// </summary>
        public string PuaCategories
        {
            get
            {
                return EngineGetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PUA_CATEGORIES);
            }
            set
            {
                EngineSetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PUA_CATEGORIES, value);
            }
        }

        /// <summary>
        /// Database options.
        /// </summary>
        public LoadOptions DatabaseOptions
        {
            get
            {
                return (LoadOptions)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_OPTIONS);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_OPTIONS, (long)value);
            }
        }

        /// <summary>
        /// Database version.
        /// </summary>
        public uint DatabaseVersion
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_VERSION);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_VERSION, (long)value);
            }
        }

        /// <summary>
        /// Database time as a UNIX timestamp.
        /// </summary>
        public uint DatabaseTime
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_TIME);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_TIME, (long)value);
            }
        }

        /// <summary>
        /// Only use Aho-Corasick pattern matcher.
        /// </summary>
        public uint ACOnly
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_ONLY);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_ONLY, (long)value);
            }
        }

        /// <summary>
        /// Minimum trie depth for AC algorithm.
        /// </summary>
        public uint ACMinDepth
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MINDEPTH);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MINDEPTH, (long)value);
            }
        }

        /// <summary>
        /// Maximum trie depth for AC algorithm.
        /// </summary>
        public uint ACMaxDepth
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MAXDEPTH);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MAXDEPTH, (long)value);
            }
        }

        /// <summary>
        /// Path to temporary directory.
        /// </summary>
        public string TempDir
        {
            get
            {
                return EngineGetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_TMPDIR);
            }
            set
            {
                EngineSetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_TMPDIR, value);
            }
        }

        /// <summary>
        /// Automatically delete temporary files.
        /// </summary>
        public uint KeepTempFiles
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_KEEPTMP);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_KEEPTMP, (long)value);
            }
        }

        /// <summary>
        /// Bytecode security mode.
        /// </summary>
        public BytecodeSecurity BytecodeSecurity
        {
            get
            {
                return (BytecodeSecurity)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_SECURITY);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_SECURITY, (long)value);
            }
        }

        /// <summary>
        /// Bytecode timeout.
        /// </summary>
        public uint BytecodeTimeout
        {
            get
            {
                return (uint)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_TIMEOUT);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_TIMEOUT, (long)value);
            }
        }

        /// <summary>
        /// Bytecode mode.
        /// </summary>
        public BytecodeMode BytecodeMode
        {
            get
            {
                return (BytecodeMode)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_MODE);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_MODE, (long)value);
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

        public delegate void FileScannedCallback(string path, ScanResult result, string virusName);

        /// <summary>
        /// Scan a directory for viruses, optionally recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="fileScannedCallback">Callback function.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="recurse">Enter subdirectories.</param>
        /// <param name="maxDepth">Maximum depth to scan, or zero for unlimited.</param>
        public void ScanDirectory(string directoryPath, FileScannedCallback fileScannedCallback,
            ScanOptions scanOptions = ScanOptions.StandardOptions, bool recurse = true, int maxDepth = 9)
        {
            var pathStack = new Stack<Tuple<string /* path */, int /* depth */>>();

            // Push the starting directory onto the stack.
            pathStack.Push(Tuple.Create(directoryPath, 1));

            while (pathStack.Count > 0)
            {
                var stackState = pathStack.Pop();

                var currentPath = stackState.Item1;
                var currentDepth = stackState.Item2;

                var attributes = File.GetAttributes(currentPath);

                // If we're in a directory, push all files and subdirectories to the stack.
                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // Check if we're not about to go too deep.
                    if (maxDepth == 0 || currentDepth < maxDepth)
                    {
                        var subFiles = Directory.GetFiles(currentPath);
                        foreach (var file in subFiles)
                        {
                            pathStack.Push(Tuple.Create(file, currentDepth + 1));
                        }

                        var subDirectories = Directory.GetDirectories(currentPath);
                        foreach (var directory in subDirectories)
                        {
                            pathStack.Push(Tuple.Create(directory, currentDepth + 1));
                        }
                    }
                }
                // If this is a file, scan it.
                else
                {
                    var virusName = string.Empty;
                    var scanResult = ScanFile(currentPath, out virusName, ScanOptions.StandardOptions);

                    fileScannedCallback(currentPath, scanResult, virusName);
                }
            }
        }
    }
}
