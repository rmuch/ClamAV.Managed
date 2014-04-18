/*
 * ClamAV.Managed.Async - Managed bindings for ClamAV - Asynchronous extensions
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClamAV.Managed.Async
{
    /// <summary>
    /// Provides methods for asynchronously performing virus scans.
    /// </summary>
    public static class ClamEngineExtensions
    {
        /// <summary>
        /// Asynchronously load databases from the default hardcoded path using standard options.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task LoadDatabaseAsync(this ClamEngine engine)
        {
            await Task.Factory.StartNew(engine.LoadDatabase);
        }

        /// <summary>
        /// Asynchronously load databases from the default hardcoded path using custom options.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="options">Options with which to load the database.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task LoadDatabaseAsync(this ClamEngine engine, LoadOptions options)
        {
            await Task.Factory.StartNew(() => engine.LoadDatabase(options));
        }

        /// <summary>
        /// Asynchronously load databases from a custom path using standard options.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task LoadDatabaseAsync(this ClamEngine engine, string path)
        {
            await Task.Factory.StartNew(() => engine.LoadDatabase(path));
        }

        /// <summary>
        /// Asynchronously loads a database file or directory into the engine.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to the database file or a directory containing database files.</param>
        /// <param name="options">Options with which to load the database.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task LoadDatabaseAsync(this ClamEngine engine, string path, LoadOptions options)
        {
            await Task.Factory.StartNew(() => engine.LoadDatabase(path, options));
        }

        /// <summary>
        /// Asynchronously scans a file for viruses with the default scan options.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to the file to be scanned.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task returns a scan result.</returns>
        public static async Task<FileScanResult> ScanFileAsync(this ClamEngine engine, string path)
        {
            return await engine.ScanFileAsync(path, ScanOptions.StandardOptions);
        }

        /// <summary>
        /// Asynchronously scans a file for viruses.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to the file to be scanned.</param>
        /// <param name="options">Scan options.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task returns a scan result.</returns>
        public static async Task<FileScanResult> ScanFileAsync(this ClamEngine engine, string path, ScanOptions options)
        {
            var virusName = string.Empty;
            var scanResult = await Task.Factory.StartNew(() => engine.ScanFile(path, options, out virusName));

            return new FileScanResult(path, scanResult == ScanResult.Virus, virusName);
        }

        /// <summary>
        /// Asynchronously scans a directory for viruses with the default scan options, recursing into subdirectories.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to scan.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task returns the scan results.</returns>
        public static async Task<IEnumerable<FileScanResult>> ScanDirectoryAsync(this ClamEngine engine, string path)
        {
            return await engine.ScanDirectoryAsync(path, ScanOptions.StandardOptions, true, 0);
        }

        /// <summary>
        /// Asynchronously scans a directory for viruses, recursing into subdirectories.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to scan.</param>
        /// <param name="options">Scan options.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task returns the scan results.</returns>
        public static async Task<IEnumerable<FileScanResult>> ScanDirectoryAsync(this ClamEngine engine, string path, ScanOptions options)
        {
            return await engine.ScanDirectoryAsync(path, options, true, 0);
        }

        /// <summary>
        /// Asynchronously scans a directory for viruses, optionally recursing into subdirectories.
        /// </summary>
        /// <param name="engine">ClamAV engine instance.</param>
        /// <param name="path">Path to scan.</param>
        /// <param name="options">Scan options.</param>
        /// <param name="recurse">Whether to enter subdirectories.</param>
        /// <param name="maxDepth">Maximum depth to scan, or zero for unlimited.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task returns the scan results.</returns>
        public static async Task<IEnumerable<FileScanResult>> ScanDirectoryAsync(this ClamEngine engine, string path, ScanOptions options, bool recurse, int maxDepth)
        {
            var scanQueue = new Queue<string>();
            
            var pathStack = new Stack<Tuple<string /* path */, int /* depth */>>();

            // Push the starting directory onto the stack.
            pathStack.Push(Tuple.Create(path, 1));

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
                // If this is a file, enqueue it for scanning.
                else
                {
                    scanQueue.Enqueue(currentPath);
                }
            }

            var scanTasks = scanQueue.Select(engine.ScanFileAsync);
            var scanResults = await Task.WhenAll(scanTasks);

            return scanResults;
        }
    }
}
