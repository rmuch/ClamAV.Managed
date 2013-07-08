using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClamAV.Managed.Tests
{
    /// <summary>
    /// Test getting and setting ClamEngine property values.
    /// </summary>
    [TestFixture]
    public class ClamEnginePropertyTests
    {
        private ClamEngine _clamEngine;

        [SetUp]
        public void SetUpClamEngine()
        {
            _clamEngine = new ClamEngine();
        }

        [TearDown]
        public void TearDownClamEngine()
        {
            _clamEngine.Dispose();
            _clamEngine = null;
        }

        [Test]
        public void VersionPropertyIsNotNullOrEmpty()
        {
            var version = _clamEngine.Version;

            Assert.IsFalse(string.IsNullOrEmpty(version));
        }

        [Test]
        public void DatabaseDirectoryPropertyIsNotNullOrEmpty()
        {
            var databaseDirectory = _clamEngine.DatabaseDirectory;

            Assert.IsFalse(string.IsNullOrEmpty(databaseDirectory));
        }

        [Test]
        public void MaxScanSizeIsReadWritable()
        {
            ulong value = 1024 * 123;

            _clamEngine.MaxScanSize = value;

            Assert.AreEqual(value, _clamEngine.MaxScanSize);
        }

        [Test]
        public void MaxFileSizeIsReadWritable()
        {
            ulong value = 1024 * 124;

            _clamEngine.MaxFileSize = value;

            Assert.AreEqual(value, _clamEngine.MaxFileSize);
        }

        [Test]
        public void MaxRecursionIsReadWritable()
        {
            uint value = 125;

            _clamEngine.MaxRecursion = value;

            Assert.AreEqual(value, _clamEngine.MaxRecursion);
        }

        [Test]
        public void MaxFilesIsReadWritableIsReadWritable()
        {
            uint value = 126;

            _clamEngine.MaxFiles = value;

            Assert.AreEqual(value, _clamEngine.MaxFiles);
        }

        [Test]
        public void MinCCCountIsReadWritableIsReadWritable()
        {
            uint value = 3;

            _clamEngine.MinCCCount = value;

            Assert.AreEqual(value, _clamEngine.MinCCCount);
        }

        [Test]
        public void MinSSNCountIsReadWritableIsReadWritable()
        {
            uint value = 4;

            _clamEngine.MinSSNCount = value;

            Assert.AreEqual(value, _clamEngine.MinSSNCount);
        }

        [Test]
        public void PuaCategoriesIsReadWritable()
        {
            string value = "xyz";

            _clamEngine.PuaCategories = value;

            Assert.AreEqual(value, _clamEngine.PuaCategories);
        }

        [Test]
        public void ACOnlyIsReadWritable()
        {
            uint value = 1;

            _clamEngine.ACOnly = value;

            Assert.AreEqual(value, _clamEngine.ACOnly);
        }

        [Test]
        public void ACMinDepthIsReadWritable()
        {
            uint value = 4;

            _clamEngine.ACMinDepth = value;

            Assert.AreEqual(value, _clamEngine.ACMinDepth);
        }

        [Test]
        public void ACMaxDepthIsReadWritable()
        {
            uint value = 5;

            _clamEngine.ACMaxDepth = value;

            Assert.AreEqual(value, _clamEngine.ACMaxDepth);
        }

        [Test]
        public void TempDirIsReadWritable()
        {
            string value = @"E:\Temp";

            _clamEngine.TempDir = value;

            Assert.AreEqual(value, _clamEngine.TempDir);
        }

        [Test]
        public void KeepTempFilesIsReadWritable()
        {
            uint value = 0;

            _clamEngine.KeepTempFiles = value;

            Assert.AreEqual(value, _clamEngine.KeepTempFiles);
        }

        [Test]
        public void BytecodeSecurityIsReadWritable()
        {
            var value = BytecodeSecurity.TrustAll;

            _clamEngine.BytecodeSecurity = value;

            Assert.AreEqual(value, _clamEngine.BytecodeSecurity);
        }

        [Test]
        public void BytecodeTimeoutIsReadWritable()
        {
            uint value = 3600;

            _clamEngine.BytecodeTimeout = value;

            Assert.AreEqual(value, _clamEngine.BytecodeTimeout);
        }

        [Test]
        public void BytecodeModeIsReadWritable()
        {
            var value = BytecodeMode.Interpreter;

            _clamEngine.BytecodeMode = value;

            Assert.AreEqual(value, _clamEngine.BytecodeMode);
        }
    }
}
