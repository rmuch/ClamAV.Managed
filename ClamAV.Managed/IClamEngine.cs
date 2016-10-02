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

namespace ClamAV.Managed
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClamEngine : IClamEngineSettings, IDisposable
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
        void ScanDirectory(string directoryPath, FileScannedCallback fileScannedCallback);

        /// <summary>
        /// Scan a directory for viruses with custom scan options, recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        void ScanDirectory(string directoryPath, ScanOptions scanOptions, FileScannedCallback fileScannedCallback);

        /// <summary>
        /// Scan a directory for viruses, optionally recursing into subdirectories.
        /// </summary>
        /// <param name="directoryPath">Path to scan.</param>
        /// <param name="scanOptions">Scan options.</param>
        /// <param name="fileScannedCallback">Called after a file has been scanned.</param>
        /// <param name="recurse">Enter subdirectories.</param>
        /// <param name="maxDepth">Maximum depth to scan, or zero for unlimited.</param>
        void ScanDirectory(string directoryPath, ScanOptions scanOptions, FileScannedCallback fileScannedCallback, bool recurse, int maxDepth);
    }
}