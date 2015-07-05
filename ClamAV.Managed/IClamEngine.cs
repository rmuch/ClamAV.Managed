using System;

namespace ClamAV.Managed
{
    public interface IClamEngine
    {
        /// <summary>
        /// Handle to the unmanaged ClamAV engine belonging to this instance.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>
        /// ClamAV hard-coded default database directory.
        /// </summary>
        string DatabaseDirectory { get; }

        /// <summary>
        /// Maximum amount of data in a file to be scanned.
        /// </summary>
        ulong MaxScanSize { get; set; }

        /// <summary>
        /// Maximum file size to be scanned.
        /// </summary>
        ulong MaxFileSize { get; set; }

        /// <summary>
        /// Maximum recursion depth.
        /// </summary>
        uint MaxRecursion { get; set; }

        /// <summary>
        /// Maximum number of files to scan inside an archive or container.
        /// </summary>
        uint MaxFiles { get; set; }

        /// <summary>
        /// Minimum count of credit card numbers to trigger a detection.
        /// </summary>
        uint MinCCCount { get; set; }

        /// <summary>
        /// Minimum count of SSNs to trigger a detection.
        /// </summary>
        uint MinSSNCount { get; set; }

        /// <summary>
        /// Potentially unwanted application categories.
        /// </summary>
        string PuaCategories { get; set; }

        /// <summary>
        /// Database options.
        /// </summary>
        LoadOptions DatabaseOptions { get; set; }

        /// <summary>
        /// Database version.
        /// </summary>
        uint DatabaseVersion { get; set; }

        /// <summary>
        /// Database time as a UNIX timestamp.
        /// </summary>
        uint DatabaseTime { get; set; }

        /// <summary>
        /// Only use Aho-Corasick pattern matcher.
        /// </summary>
        uint ACOnly { get; set; }

        /// <summary>
        /// Minimum trie depth for AC algorithm.
        /// </summary>
        uint ACMinDepth { get; set; }

        /// <summary>
        /// Maximum trie depth for AC algorithm.
        /// </summary>
        uint ACMaxDepth { get; set; }

        /// <summary>
        /// Path to temporary directory.
        /// </summary>
        string TempDir { get; set; }

        /// <summary>
        /// Automatically delete temporary files.
        /// </summary>
        uint KeepTempFiles { get; set; }

        /// <summary>
        /// Bytecode security mode.
        /// </summary>
        BytecodeSecurity BytecodeSecurity { get; set; }

        /// <summary>
        /// Bytecode timeout.
        /// </summary>
        uint BytecodeTimeout { get; set; }

        /// <summary>
        /// Bytecode mode.
        /// </summary>
        BytecodeMode BytecodeMode { get; set; }

        /// <summary>
        /// Maximum size file to check for embedded PE.
        /// </summary>
        ulong MaxEmbeddedPE { get; set; }

        /// <summary>
        /// Maximum size of HTML file to normalize.
        /// </summary>
        ulong MaxHtmlNormalize { get; set; }

        /// <summary>
        /// Maximum size of normalized HTML file to scan.
        /// </summary>
        ulong MaxHtmlNoTags { get; set; }

        /// <summary>
        /// Maximum size of script file to normalize.
        /// </summary>
        ulong MaxScriptNormalize { get; set; }

        /// <summary>
        /// Maximum size zip to type reanalyze.
        /// </summary>
        ulong MaxZipTypeRcg { get; set; }

        /// <summary>
        /// Free the unmanaged resource associated with this ClamAV engine instance.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Load databases from the default hardcoded path using standard options.
        /// </summary>
        void LoadDatabase();

        /// <summary>
        /// Load databases from the default hardcoded path using custom options.
        /// </summary>
        /// <param name="options">Options with which to load the database.</param>
        void LoadDatabase(LoadOptions options);

        /// <summary>
        /// Load databases from a custom path using standard options.
        /// </summary>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        void LoadDatabase(string path);

        /// <summary>
        /// Loads a database file or directory into the engine.
        /// </summary>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        /// <param name="options">Options with which to load the database.</param>
        void LoadDatabase(string path, LoadOptions options);

        /// <summary>
        /// Scans a file for viruses with the default scan options.
        /// </summary>
        /// <param name="filePath">Path to the file to be scanned.</param>
        /// <param name="virusName">Output variable for the virus name, if detected.</param>
        /// <returns>File status.</returns>
        ScanResult ScanFile(string filePath, out string virusName);

        /// <summary>
        /// Scans a file for viruses.
        /// </summary>
        /// <param name="filePath">Path to the file to be scanned.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="virusName">Output variable for the virus name, if detected.</param>
        /// <returns>File status.</returns>
        ScanResult ScanFile(string filePath, ScanOptions scanOptions, out string virusName);

        /// <summary>
        /// Scan a directory for viruses with default scan options, recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        void ScanDirectory(string directoryPath, ClamEngine.FileScannedCallback fileScannedCallback);

        /// <summary>
        /// Scan a directory for viruses with custom scan options, recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        void ScanDirectory(string directoryPath, ScanOptions scanOptions, ClamEngine.FileScannedCallback fileScannedCallback);

        /// <summary>
        /// Scan a directory for viruses, optionally recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        /// <param name="recurse">Enter subdirectories.</param>
        /// <param name="maxDepth">Maximum depth to scan, or zero for unlimited.</param>
        void ScanDirectory(string directoryPath, ScanOptions scanOptions, ClamEngine.FileScannedCallback fileScannedCallback, bool recurse, int maxDepth);
    }
}