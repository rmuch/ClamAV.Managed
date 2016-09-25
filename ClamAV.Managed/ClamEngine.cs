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

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace ClamAV.Managed
{
    /// <summary>
    /// Encapsulates an instance of the ClamAV engine.
    /// </summary>
    public class ClamEngine : IDisposable, IClamEngine
    {
        #region Global Initialization

        /// <summary>
        /// Whether the ClamAV library has been globally initialized.
        /// </summary>
        private static bool _initialized = false;

        /// <summary>
        /// Global initialization method to load the ClamAV library into the process.
        /// This method should be called once during the lifetime of a host process.
        /// </summary>
        private static void Initialize()
        {
            if (!_initialized)
            {
                int result = UnsafeNativeMethods.cl_init(UnsafeNativeMethods.CL_INIT_DEFAULT);

                if (result != UnsafeNativeMethods.CL_SUCCESS)
                    throw new ClamException(result, ErrorString(result));
            }

            _initialized = true;
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Returns the description string of a ClamAV error code.
        /// </summary>
        /// <param name="error">ClamAV error code.</param>
        /// <returns>Error description.</returns>
        internal static string ErrorString(int error)
        {
            IntPtr result = UnsafeNativeMethods.cl_strerror(error);

            return UnmarshalString(result);
        }

        /// <summary>
        /// Unmarshals a pointer to an ANSI string value.
        /// </summary>
        /// <param name="ptr">Pointer to a string to be unmarshalled.</param>
        /// <returns>An unmarshalled string.</returns>
        private static string UnmarshalString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal handle to the ClamAV engine.
        /// </summary>
        private IntPtr _engine;

        /// <summary>
        /// Whether Dispose has been called on this instance.
        /// </summary>
        private bool _disposed = false;

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
        private static string _clamVersion;

        /// <summary>
        /// ClamAV engine version string.
        /// </summary>
        public static string Version
        {
            get
            {
                if (_clamVersion == null)
                    _clamVersion = UnmarshalString(UnsafeNativeMethods.cl_retver());

                return _clamVersion;
            }
        }

        /// <summary>
        /// Cached database directory path.
        /// </summary>
        private string _databaseDirectory;

        /// <summary>
        /// ClamAV hard-coded default database directory.
        /// </summary>
        public string DatabaseDirectory
        {
            get
            {
                if (_databaseDirectory == null)
                    _databaseDirectory = UnmarshalString(UnsafeNativeMethods.cl_retdbdir());

                return _databaseDirectory;
            }
        }

        #endregion

        #region Engine Settings

        /// <summary>
        /// Get a numerical settings value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <returns>Setting value.</returns>
        internal long EngineGetNum(UnsafeNativeMethods.cl_engine_field setting)
        {
            int error = 0;
            long numValue = UnsafeNativeMethods.cl_engine_get_num(_engine, setting, ref error);

            if (error != UnsafeNativeMethods.CL_SUCCESS)
                throw new ClamException(error, ErrorString(error));

            return numValue;
        }

        /// <summary>
        /// Set a numerical setting value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <param name="value">Setting value.</param>
        internal void EngineSetNum(UnsafeNativeMethods.cl_engine_field setting, long value)
        {
            int error = UnsafeNativeMethods.cl_engine_set_num(_engine, setting, value);

            if (error != UnsafeNativeMethods.CL_SUCCESS)
                throw new ClamException(error, ErrorString(error));
        }

        /// <summary>
        /// Get a string setting value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <returns>Setting value.</returns>
        internal string EngineGetStr(UnsafeNativeMethods.cl_engine_field setting)
        {
            int error = 0;
            IntPtr strPtr = UnsafeNativeMethods.cl_engine_get_str(_engine, setting, ref error);

            if (error != UnsafeNativeMethods.CL_SUCCESS)
                throw new ClamException(error, ErrorString(error));

            return Marshal.PtrToStringAnsi(strPtr);
        }

        /// <summary>
        /// Set a string setting value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <param name="value">Setting value.</param>
        internal void EngineSetStr(UnsafeNativeMethods.cl_engine_field setting, string value)
        {
            int error = UnsafeNativeMethods.cl_engine_set_str(_engine, setting, value);

            if (error != UnsafeNativeMethods.CL_SUCCESS)
                throw new ClamException(error, ErrorString(error));
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
        /// Maximum size file to check for embedded PE.
        /// </summary>
        public ulong MaxEmbeddedPE
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_EMBEDDEDPE);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_EMBEDDEDPE, (long)value);
            }
        }

        /// <summary>
        /// Maximum size of HTML file to normalize.
        /// </summary>
        public ulong MaxHtmlNormalize
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNORMALIZE);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNORMALIZE, (long)value);
            }
        }

        /// <summary>
        /// Maximum size of normalized HTML file to scan.
        /// </summary>
        public ulong MaxHtmlNoTags
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNOTAGS);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNOTAGS, (long)value);
            }
        }

        /// <summary>
        /// Maximum size of script file to normalize.
        /// </summary>
        public ulong MaxScriptNormalize
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCRIPTNORMALIZE);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCRIPTNORMALIZE, (long)value);
            }
        }

        /// <summary>
        /// Maximum size zip to type reanalyze.
        /// </summary>
        public ulong MaxZipTypeRcg
        {
            get
            {
                return (ulong)EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_ZIPTYPERCG);
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_ZIPTYPERCG, (long)value);
            }
        }

#if PRERELEASE
        /// <summary>
        /// This option causes memory or nested map scans to dump the content to disk.
        /// </summary>
        public bool ForceToDisk
        {
            get
            {
                return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_FORCETODISK) != 0;
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_FORCETODISK, value ? 1 : 0);
            }
        }

        /// <summary>
        /// This option allows you to disable the caching feature of the engine. By
        /// default, the engine will store an MD5 in a cache of any files that are
        /// not flagged as virus or that hit limits checks. Disabling the cache will
        /// have a negative performance impact on large scans.
        /// </summary>
        public bool DisableCache
        {
            get
            {
                return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_CACHE) != 0;
            }
            set
            {
                EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_CACHE, value ? 1 : 0);
            }
        }
#endif

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Creates a new ClamAV engine instance.
        /// </summary>
        public ClamEngine() : this(false) { }

        /// <summary>
        /// Creates a new ClamAV engine instance.
        /// </summary>
        /// <param name="debug">Enable verbose ClamAV debug logging.</param>
        public ClamEngine(bool debug)
        {
            // Enable debug mode.
            if (debug)
                UnsafeNativeMethods.cl_debug();

            // Initialize ClamAV library.
            Initialize();

            // Create a new instance of the ClamAV engine.
            _engine = UnsafeNativeMethods.cl_engine_new();

            // Handle an error condition.
            if (_engine == IntPtr.Zero)
            {
                int error = _engine.ToInt32();

                throw new ClamException(error, ErrorString(error));
            }
        }

        ~ClamEngine()
        {
            Dispose(false);
        }

        /// <summary>
        /// Free the unmanaged resource associated with this ClamAV engine instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Free the unmanaged resource associated with this ClamAV engine instance.
        /// </summary>
        /// <param name="b">Whether the dispose method has been called from the finalizer.</param>
        protected virtual void Dispose(bool b)
        {
            if (!_disposed && _engine.ToInt64() != 0)
            {
                // Free ClamAV engine instance.
                int result = UnsafeNativeMethods.cl_engine_free(_engine);

                _disposed = true;
            }
        }

        #endregion

        #region ClamAV Methods

        /// <summary>
        /// Load databases from the default hardcoded path using standard options.
        /// </summary>
        public void LoadDatabase()
        {
            LoadDatabase(DatabaseDirectory, LoadOptions.StandardOptions);
        }

        /// <summary>
        /// Load databases from the default hardcoded path using custom options.
        /// </summary>
        /// <param name="options">Options with which to load the database.</param>
        public void LoadDatabase(LoadOptions options)
        {
            LoadDatabase(DatabaseDirectory, options);
        }

        /// <summary>
        /// Load databases from a custom path using standard options.
        /// </summary>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        public void LoadDatabase(string path)
        {
            LoadDatabase(path, LoadOptions.StandardOptions);
        }

        /// <summary>
        /// Loads a database file or directory into the engine.
        /// </summary>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        /// <param name="options">Options with which to load the database.</param>
        public void LoadDatabase(string path, LoadOptions options)
        {
            // Validate arguments.
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            uint signo = 0;
            uint optnum = 0;

            // Convert LoadOptions parameter.
            optnum = (uint)options;

            // Invoke the native method to load the database.
            var loadResult = (UnsafeNativeMethods.cl_error_t)UnsafeNativeMethods.cl_load(path, _engine, ref signo, optnum);

            if (loadResult != UnsafeNativeMethods.cl_error_t.CL_SUCCESS)
            {
                throw new ClamException((int)loadResult, ErrorString((int)loadResult));
            }

            // Compile the database.
            var compileResult = (UnsafeNativeMethods.cl_error_t)UnsafeNativeMethods.cl_engine_compile(_engine);

            if (compileResult != UnsafeNativeMethods.cl_error_t.CL_SUCCESS)
            {
                throw new ClamException((int)compileResult, ErrorString((int)compileResult));
            }
        }

        /// <summary>
        /// Scans a file for viruses with the default scan options.
        /// </summary>
        /// <param name="filePath">Path to the file to be scanned.</param>
        /// <param name="virusName">Output variable for the virus name, if detected.</param>
        /// <returns>File status.</returns>
        public ScanResult ScanFile(string filePath, out string virusName)
        {
            return ScanFile(filePath, ScanOptions.StandardOptions, out virusName);
        }

        /// <summary>
        /// Scans a file for viruses.
        /// </summary>
        /// <param name="filePath">Path to the file to be scanned.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="virusName">Output variable for the virus name, if detected.</param>
        /// <returns>File status.</returns>
        public ScanResult ScanFile(string filePath, ScanOptions scanOptions, out string virusName)
        {
            // Validate arguments.
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

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
                throw new ClamException((int)result, ErrorString((int)result));
            }
        }

        /// <summary>
        /// Delegate for file scan completion callback.
        /// </summary>
        /// <param name="path">Path to file which has been scanned.</param>
        /// <param name="result">Scan parameters.</param>
        /// <param name="virusName">Signature name if file is infected.</param>
        public delegate void FileScannedCallback(string path, ScanResult result, string virusName);

        /// <summary>
        /// Scan a directory for viruses with default scan options, recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        public void ScanDirectory(string directoryPath, FileScannedCallback fileScannedCallback)
        {
            ScanDirectory(directoryPath, ScanOptions.StandardOptions, fileScannedCallback, true, 0);
        }

        /// <summary>
        /// Scan a directory for viruses with custom scan options, recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        public void ScanDirectory(string directoryPath, ScanOptions scanOptions, FileScannedCallback fileScannedCallback)
        {
            ScanDirectory(directoryPath, ScanOptions.StandardOptions, fileScannedCallback, true, 0);
        }

        /// <summary>
        /// Scan a directory for viruses, optionally recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        /// <param name="recurse">Enter subdirectories.</param>
        /// <param name="maxDepth">Maximum depth to scan, or zero for unlimited.</param>
        public void ScanDirectory(string directoryPath, ScanOptions scanOptions, FileScannedCallback fileScannedCallback, bool recurse, int maxDepth)
        {
            // Validate arguments.
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath));

            if (fileScannedCallback == null)
                throw new ArgumentNullException(nameof(fileScannedCallback));

            if (maxDepth < 0)
                throw new ArgumentException("maxDepth must be 0 or greater.");

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
                    var scanResult = ScanFile(currentPath, scanOptions, out virusName);

                    fileScannedCallback(currentPath, scanResult, virusName);
                }
            }
        }

        #endregion
    }
}
