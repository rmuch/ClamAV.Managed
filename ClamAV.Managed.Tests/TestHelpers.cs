/*
 * ClamAV.Managed.Tests - Managed bindings for ClamAV - unit test suite
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
using System.Text;
using System.Threading.Tasks;

namespace ClamAV.Managed.Tests
{
    /// <summary>
    /// A set of static test helper functions.
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Relative or absolute path to the directory containing files required to
        /// run test fixtures.
        /// </summary>
        public static string TestFilesDirectory
        {
            get { return "../../../TestFiles"; }
        }

        /// <summary>
        /// Checks whether the directory containing files required for database
        /// loading and file scanning test fixtures exists.
        /// </summary>
        /// <returns>A boolean variable indicating whether the directory exists.</returns>
        public static bool TestFilesDirectoryExists()
        {
            return Directory.Exists(TestFilesDirectory);
        }

        /// <summary>
        /// Checks whether libclamav.dll is present and available to the test suite.
        /// </summary>
        /// <returns>A boolean variable indicating whether the library exists.</returns>
        public static bool NativeLibraryExists()
        {
            return File.Exists("libclamav.dll") || File.Exists("libclamav.so") || File.Exists("libclamav");
        }
    }
}
