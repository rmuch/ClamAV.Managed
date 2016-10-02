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

namespace ClamAV.Managed
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClamEngineSettings
    {
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
        /// This option causes memory or nested map scans to dump the content to disk.
        /// </summary>
        bool ForceToDisk { get; set; }

        /// <summary>
        /// This option allows you to disable the caching feature of the engine. By
        /// default, the engine will store an MD5 in a cache of any files that are
        /// not flagged as virus or that hit limits checks. Disabling the cache will
        /// have a negative performance impact on large scans.
        /// </summary>
        bool DisableCache { get; set; }

        /// <summary>
        /// Disable submission of PE section statistical data.
        /// </summary>
        bool DisablePeStats { get; set; }

        /// <summary>
        /// Timeout in seconds to timeout communication with the stats server.
        /// </summary>
        uint StatsTimeout { get; set; }

        /// <summary>
        /// This option sets the maximum number of partitions of a raw disk image to be scanned.
        /// Raw disk images with more partitions than this value will have up to the value number partitions scanned.
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may result in severe damage or impact performance.
        /// </summary>
        uint MaxPartitions { get; set; }

        /// <summary>
        /// This option sets the maximum number of icons within a PE to be scanned.
        /// PE files with more icons than this value will have up to the value number icons scanned.
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may result in severe damage or impact performance.
        /// </summary>
        uint MaxIconSpe { get; set; }

        /// <summary>
        /// This option sets the maximum recursive calls to HWP3 parsing function.
        /// HWP3 files using more than this limit will be terminated and alert the user.
        /// Scans will be unable to scan any HWP3 attachments if the recursive limit is reached.
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may result in severe damage or impact performance.
        /// </summary>
        uint MaxRecHwp3 { get; set; }

        /// <summary>
        /// This clamscan option is currently for testing only. It sets the engine parameter CL_ENGINE_TIME_LIMIT. The value is in milliseconds.     
        /// </summary>
        uint TimeLimit { get; set; }

        /// <summary>
        /// This option sets the maximum calls to the PCRE match function during an instance of regex matching.
        /// Instances using more than this limit will be terminated and alert the user but the scan will continue.
        /// For more information on match_limit, see the PCRE documentation.
        /// 
        /// Negative values are not allowed.
        /// 
        /// WARNING: setting this limit too high may severely impact performance.
        /// </summary>
        ulong PcreMatchLimit { get; set; }

        /// <summary>
        /// This option sets the maximum recursive calls to the PCRE match function during an instance of regex matching.
        /// Instances using more than this limit will be terminated and alert the user but the scan will continue.
        /// For more information on match_limit_recursion, see the PCRE documentation.
        /// Negative values are not allowed and values > PCREMatchLimit are superfluous.
        /// 
        /// WARNING: setting this limit too high may severely impact performance.
        /// </summary>
        ulong PcreRecMatchLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ulong PcreMaxFilesize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool DisablePeCerts { get; set; }

        /// <summary>
        /// Dump PE certificates.
        /// </summary>
        bool PeDumpCerts { get; set; }
    }
}