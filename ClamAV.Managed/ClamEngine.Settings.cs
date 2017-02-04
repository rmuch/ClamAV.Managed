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
using System.Runtime.InteropServices;

namespace ClamAV.Managed
{
    public partial class ClamEngine
    {
        #region Engine Settings

        /// <summary>
        /// Get a numerical settings value.
        /// </summary>
        /// <param name="setting">Setting key.</param>
        /// <returns>Setting value.</returns>
        private long EngineGetNum(UnsafeNativeMethods.cl_engine_field setting)
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
        private void EngineSetNum(UnsafeNativeMethods.cl_engine_field setting, long value)
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
        private string EngineGetStr(UnsafeNativeMethods.cl_engine_field setting)
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
        private void EngineSetStr(UnsafeNativeMethods.cl_engine_field setting, string value)
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
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCANSIZE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCANSIZE, (long) value); }
        }

        /// <summary>
        /// Maximum file size to be scanned.
        /// </summary>
        public ulong MaxFileSize
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILESIZE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILESIZE, (long) value); }
        }

        /// <summary>
        /// Maximum recursion depth.
        /// </summary>
        public uint MaxRecursion
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_RECURSION); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_RECURSION, (long) value); }
        }

        /// <summary>
        /// Maximum number of files to scan inside an archive or container.
        /// </summary>
        public uint MaxFiles
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILES); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILES, (long) value); }
        }

        /// <summary>
        /// Minimum count of credit card numbers to trigger a detection.
        /// </summary>
        public uint MinCCCount
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_CC_COUNT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_CC_COUNT, (long) value); }
        }

        /// <summary>
        /// Minimum count of SSNs to trigger a detection.
        /// </summary>
        public uint MinSSNCount
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_SSN_COUNT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MIN_SSN_COUNT, (long) value); }
        }

        /// <summary>
        /// Potentially unwanted application categories.
        /// </summary>
        public string PuaCategories
        {
            get { return EngineGetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PUA_CATEGORIES); }
            set { EngineSetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PUA_CATEGORIES, value); }
        }

        /// <summary>
        /// Database options.
        /// </summary>
        public LoadOptions DatabaseOptions
        {
            get { return (LoadOptions) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_OPTIONS); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_OPTIONS, (long) value); }
        }

        /// <summary>
        /// Database version.
        /// </summary>
        public uint DatabaseVersion
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_VERSION); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_VERSION, (long) value); }
        }

        /// <summary>
        /// Database time as a UNIX timestamp.
        /// </summary>
        public uint DatabaseTime
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_TIME); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DB_TIME, (long) value); }
        }

        /// <summary>
        /// Only use Aho-Corasick pattern matcher.
        /// </summary>
        public uint ACOnly
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_ONLY); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_ONLY, (long) value); }
        }

        /// <summary>
        /// Minimum trie depth for AC algorithm.
        /// </summary>
        public uint ACMinDepth
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MINDEPTH); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MINDEPTH, (long) value); }
        }

        /// <summary>
        /// Maximum trie depth for AC algorithm.
        /// </summary>
        public uint ACMaxDepth
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MAXDEPTH); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_AC_MAXDEPTH, (long) value); }
        }

        /// <summary>
        /// Path to temporary directory.
        /// </summary>
        public string TempDir
        {
            get { return EngineGetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_TMPDIR); }
            set { EngineSetStr(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_TMPDIR, value); }
        }

        /// <summary>
        /// Automatically delete temporary files.
        /// </summary>
        public uint KeepTempFiles
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_KEEPTMP); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_KEEPTMP, (long) value); }
        }

        /// <summary>
        /// Bytecode security mode.
        /// </summary>
        public BytecodeSecurity BytecodeSecurity
        {
            get
            {
                return (BytecodeSecurity) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_SECURITY);
            }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_SECURITY, (long) value); }
        }

        /// <summary>
        /// Bytecode timeout.
        /// </summary>
        public uint BytecodeTimeout
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_TIMEOUT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_TIMEOUT, (long) value); }
        }

        /// <summary>
        /// Bytecode mode.
        /// </summary>
        public BytecodeMode BytecodeMode
        {
            get { return (BytecodeMode) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_MODE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_BYTECODE_MODE, (long) value); }
        }

        /// <summary>
        /// Maximum size file to check for embedded PE.
        /// </summary>
        public ulong MaxEmbeddedPE
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_EMBEDDEDPE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_EMBEDDEDPE, (long) value); }
        }

        /// <summary>
        /// Maximum size of HTML file to normalize.
        /// </summary>
        public ulong MaxHtmlNormalize
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNORMALIZE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNORMALIZE, (long) value); }
        }

        /// <summary>
        /// Maximum size of normalized HTML file to scan.
        /// </summary>
        public ulong MaxHtmlNoTags
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNOTAGS); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_HTMLNOTAGS, (long) value); }
        }

        /// <summary>
        /// Maximum size of script file to normalize.
        /// </summary>
        public ulong MaxScriptNormalize
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCRIPTNORMALIZE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_SCRIPTNORMALIZE, (long) value); }
        }

        /// <summary>
        /// Maximum size zip to type reanalyze.
        /// </summary>
        public ulong MaxZipTypeRcg
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_ZIPTYPERCG); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_ZIPTYPERCG, (long) value); }
        }

        /// <summary>
        /// This option causes memory or nested map scans to dump the content to disk.
        /// </summary>
        public bool ForceToDisk
        {
            get { return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_FORCETODISK) != 0; }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_FORCETODISK, value ? 1 : 0); }
        }

        /// <summary>
        /// This option allows you to disable the caching feature of the engine. By
        /// default, the engine will store an MD5 in a cache of any files that are
        /// not flagged as virus or that hit limits checks. Disabling the cache will
        /// have a negative performance impact on large scans.
        /// </summary>
        public bool DisableCache
        {
            get { return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_CACHE) != 0; }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_CACHE, value ? 1 : 0); }
        }

        /// <summary>
        /// Disable submission of PE section statistical data.
        /// </summary>
        public bool DisablePeStats
        {
            get { return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_PE_STATS) != 0; }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_PE_STATS, value ? 1 : 0); }
        }

        /// <summary>
        /// Timeout in seconds to timeout communication with the stats server.
        /// </summary>
        public uint StatsTimeout
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_STATS_TIMEOUT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_STATS_TIMEOUT, (long) value); }
        }

        /// <summary>
        /// This option sets the maximum number of partitions of a raw disk image to be scanned.
        /// Raw disk images with more partitions than this value will have up to the value number partitions scanned.
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may result in severe damage or impact performance.
        /// </summary>
        public uint MaxPartitions
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_PARTITIONS); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_PARTITIONS, (long) value); }
        }

        /// <summary>
        /// This option sets the maximum number of icons within a PE to be scanned.
        /// PE files with more icons than this value will have up to the value number icons scanned.
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may result in severe damage or impact performance.
        /// </summary>
        public uint MaxIconSpe
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_ICONSPE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_ICONSPE, value); }
        }

        /// <summary>
        /// This option sets the maximum recursive calls to HWP3 parsing function.
        /// HWP3 files using more than this limit will be terminated and alert the user.
        /// Scans will be unable to scan any HWP3 attachments if the recursive limit is reached.
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may result in severe damage or impact performance.
        /// </summary>
        public uint MaxRecHwp3
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_RECHWP3); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_RECHWP3, value); }
        }

        /// <summary>
        /// This clamscan option is currently for testing only. It sets the engine parameter CL_ENGINE_TIME_LIMIT. The value is in milliseconds.     
        /// </summary>
        public uint TimeLimit
        {
            get { return (uint) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_TIME_LIMIT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_TIME_LIMIT, value); }
        }

        /// <summary>
        /// This option sets the maximum calls to the PCRE match function during an instance of regex matching.
        /// Instances using more than this limit will be terminated and alert the user but the scan will continue.
        /// For more information on match_limit, see the PCRE documentation.
        /// 
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may severely impact performance.
        /// </summary>
        public ulong PcreMatchLimit
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PCRE_MATCH_LIMIT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PCRE_MATCH_LIMIT, (long) value); }
        }

        /// <summary>
        /// This option sets the maximum recursive calls to the PCRE match function during an instance of regex matching.
        /// Instances using more than this limit will be terminated and alert the user but the scan will continue.
        /// For more information on match_limit_recursion, see the PCRE documentation.
        /// Negative values are not allowed and values > PCREMatchLimit are superfluous.
        /// 
        /// WARNING: setting this limit too high may severely impact performance.
        /// </summary>
        public ulong PcreRecMatchLimit
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PCRE_RECMATCH_LIMIT); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PCRE_RECMATCH_LIMIT, (long) value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong PcreMaxFilesize
        {
            get { return (ulong) EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PCRE_MAX_FILESIZE); }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_MAX_FILESIZE, (long) value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool DisablePeCerts
        {
            get { return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_PE_CERTS) != 0; }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_DISABLE_PE_CERTS, value ? 1 : 0); }
        }

        /// <summary>
        /// Dump PE certificates.
        /// </summary>
        public bool PeDumpCerts
        {
            get { return EngineGetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PE_DUMPCERTS) != 0; }
            set { EngineSetNum(UnsafeNativeMethods.cl_engine_field.CL_ENGINE_PE_DUMPCERTS, value ? 1 : 0); }
        }

        #endregion
    }
}