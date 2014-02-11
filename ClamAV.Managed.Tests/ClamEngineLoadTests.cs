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

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClamAV.Managed.Tests
{
    [TestFixture]
    public class ClamEngineLoadTests
    {
        private ClamEngine _clamEngine;

        [SetUp]
        public void SetUp()
        {
            if (!TestHelpers.TestFilesDirectoryExists())
            {
                Assert.Ignore("TestFiles directory is missing.");
                return;
            }

            _clamEngine = new ClamEngine();
        }

        [TearDown]
        public void TearDown()
        {
            if (_clamEngine != null)
            {
                _clamEngine.Dispose();
                _clamEngine = null;
            }
        }

        [Test]
        public void TestLoadDatabasesFromCustomPathIsSuccessful()
        {
            _clamEngine.LoadDatabase(Path.Combine(TestHelpers.TestFilesDirectory, "db"));

            Assert.That(_clamEngine.DatabaseOptions, Is.Not.EqualTo(0));
            Assert.That(_clamEngine.DatabaseTime, Is.Not.EqualTo(0));
            Assert.That(_clamEngine.DatabaseVersion, Is.Not.EqualTo(0));
        }
    }
}
