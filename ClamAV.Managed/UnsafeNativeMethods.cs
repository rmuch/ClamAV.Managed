/*
 * ClamAV.Managed - Managed bindings for ClamAV
 * Copyright (C) 2011, 2013-2014, 2016 Rupert Muchembled
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

/*
 * libclamav copyright notice from clamav.h
 * 
 * Copyright (C) 2015 Cisco Systems, Inc. and/or its affiliates. All rights reserved.
 * Copyright (C) 2007-2013 Sourcefire, Inc.
 *
 * Authors: Tomasz Kojm
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License version 2 as
 * published by the Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 */

using System;
using System.Runtime.InteropServices;

namespace ClamAV.Managed
{
    /// <summary>
    /// This class contains P/Invoke methods and type definitions corresponding to clamav.h in libclamav.
    /// </summary>
    internal static class UnsafeNativeMethods
    {
        // Name of the ClamAV library to be located within the library search path.
        internal const string libraryName = "libclamav";

        // For ease of access.
        internal const uint CL_SUCCESS = 0;

        /* return codes */
        internal enum cl_error_t
        {
            /* libclamav specific */
            CL_CLEAN = 0,
            CL_SUCCESS = 0,
            CL_VIRUS,
            CL_ENULLARG,
            CL_EARG,
            CL_EMALFDB,
            CL_ECVD,
            CL_EVERIFY,
            CL_EUNPACK,

            /* I/O and memory errors */
            CL_EOPEN,
            CL_ECREAT,
            CL_EUNLINK,
            CL_ESTAT,
            CL_EREAD,
            CL_ESEEK,
            CL_EWRITE,
            CL_EDUP,
            CL_EACCES,
            CL_ETMPFILE,
            CL_ETMPDIR,
            CL_EMAP,
            CL_EMEM,
            CL_ETIMEOUT,

            /* internal (not reported outside libclamav) */
            CL_BREAK,
            CL_EMAXREC,
            CL_EMAXSIZE,
            CL_EMAXFILES,
            CL_EFORMAT,
            CL_EBYTECODE,/* may be reported in testmode */
            CL_EBYTECODE_TESTFAIL, /* may be reported in testmode */

            /* c4w error codes */
            CL_ELOCK,
            CL_EBUSY,
            CL_ESTATE,

            /* no error codes below this line please */
            CL_ELAST_ERROR
        }

        /* db options */
        internal const uint CL_DB_PHISHING = 0x2;
        internal const uint CL_DB_PHISHING_URLS = 0x8;
        internal const uint CL_DB_PUA = 0x10;
        internal const uint CL_DB_CVDNOTMP = 0x20; /* obsolete */
        internal const uint CL_DB_OFFICIAL = 0x40; /* internal */
        internal const uint CL_DB_PUA_MODE = 0x80;
        internal const uint CL_DB_PUA_INCLUDE = 0x100;
        internal const uint CL_DB_PUA_EXCLUDE = 0x200;
        internal const uint CL_DB_COMPILED = 0x400; /* internal */
        internal const uint CL_DB_DIRECTORY = 0x800; /* internal */
        internal const uint CL_DB_OFFICIAL_ONLY = 0x1000;
        internal const uint CL_DB_BYTECODE = 0x2000;
        internal const uint CL_DB_SIGNED = 0x4000; /* internal */
        internal const uint CL_DB_BYTECODE_UNSIGNED = 0x8000;
        internal const uint CL_DB_UNSIGNED = 0x10000;
        internal const uint CL_DB_BYTECODE_STATS = 0x20000;
        internal const uint CL_DB_ENHANCED = 0x40000;
        internal const uint CL_DB_PCRE_STATS = 0x80000;
        internal const uint CL_DB_YARA_EXCLUDE = 0x100000;
        internal const uint CL_DB_YARA_ONLY = 0x200000;

        /* recommended db settings */
        internal const uint CL_DB_STDOPT = (CL_DB_PHISHING | CL_DB_PHISHING_URLS | CL_DB_BYTECODE);

        /* scan options */
        internal const uint CL_SCAN_RAW = 0x0;
        internal const uint CL_SCAN_ARCHIVE = 0x1;
        internal const uint CL_SCAN_MAIL = 0x2;
        internal const uint CL_SCAN_OLE2 = 0x4;
        internal const uint CL_SCAN_BLOCKENCRYPTED = 0x8;
        internal const uint CL_SCAN_HTML = 0x10;
        internal const uint CL_SCAN_PE = 0x20;
        internal const uint CL_SCAN_BLOCKBROKEN = 0x40;
        internal const uint CL_SCAN_MAILURL = 0x80; /* ignored */
        internal const uint CL_SCAN_BLOCKMAX = 0x100; /* ignored */
        internal const uint CL_SCAN_ALGORITHMIC = 0x200;
        internal const uint CL_SCAN_PHISHING_BLOCKSSL = 0x800; /* ssl mismatches, not ssl by itself*/
        internal const uint CL_SCAN_PHISHING_BLOCKCLOAK = 0x1000;
        internal const uint CL_SCAN_ELF = 0x2000;
        internal const uint CL_SCAN_PDF = 0x4000;
        internal const uint CL_SCAN_STRUCTURED = 0x8000;
        internal const uint CL_SCAN_STRUCTURED_SSN_NORMAL = 0x10000;
        internal const uint CL_SCAN_STRUCTURED_SSN_STRIPPED = 0x20000;
        internal const uint CL_SCAN_PARTIAL_MESSAGE = 0x40000;
        internal const uint CL_SCAN_HEURISTIC_PRECEDENCE = 0x80000;
        internal const uint CL_SCAN_BLOCKMACROS = 0x100000;
        internal const uint CL_SCAN_ALLMATCHES = 0x200000;
        internal const uint CL_SCAN_SWF = 0x400000;
        internal const uint CL_SCAN_PARTITION_INTXN = 0x800000;
        internal const uint CL_SCAN_XMLDOCS = 0x1000000;
        internal const uint CL_SCAN_HWP3 = 0x2000000;
        internal const uint CL_SCAN_FILE_PROPERTIES = 0x10000000;
        // internal const uint UNUSED = 0x20000000;
        internal const uint CL_SCAN_PERFORMANCE_INFO = 0x40000000; /* collect performance timings */
        internal const uint CL_SCAN_INTERNAL_COLLECT_SHA = 0x80000000; /* Enables hash output in sha-collect builds - for internal use only */

        /* recommended scan settings */
        internal const uint CL_SCAN_STDOPT = (CL_SCAN_ARCHIVE | CL_SCAN_MAIL | CL_SCAN_OLE2 | CL_SCAN_PDF | CL_SCAN_HTML | CL_SCAN_PE | CL_SCAN_ALGORITHMIC | CL_SCAN_ELF);

        /* cl_countsigs options */
        internal const uint CL_COUNTSIGS_OFFICIAL = 0x1;
        internal const uint CL_COUNTSIGS_UNOFFICIAL = 0x2;
        internal const uint CL_COUNTSIGS_ALL = (CL_COUNTSIGS_OFFICIAL | CL_COUNTSIGS_UNOFFICIAL);

        /* For the new engine_options bit field in the engine */
        internal const uint ENGINE_OPTIONS_NONE = 0x0;
        internal const uint ENGINE_OPTIONS_DISABLE_CACHE = 0x1;
        internal const uint ENGINE_OPTIONS_FORCE_TO_DISK = 0x2;
        internal const uint ENGINE_OPTIONS_DISABLE_PE_STATS = 0x4;
        internal const uint ENGINE_OPTIONS_DISABLE_PE_CERTS = 0x8;
        internal const uint ENGINE_OPTIONS_PE_DUMPCERTS = 0x10;

        internal const uint CL_INIT_DEFAULT = 0x0;

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_init(uint initoptions);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_engine_new();

        internal enum cl_engine_field
        {
            CL_ENGINE_MAX_SCANSIZE,	        /* uint64_t */
            CL_ENGINE_MAX_FILESIZE,	        /* uint64_t */
            CL_ENGINE_MAX_RECURSION,	    /* uint32_t*/
            CL_ENGINE_MAX_FILES,	        /* uint32_t */
            CL_ENGINE_MIN_CC_COUNT,	        /* uint32_t */
            CL_ENGINE_MIN_SSN_COUNT,	    /* uint32_t */
            CL_ENGINE_PUA_CATEGORIES,	    /* (char *) */
            CL_ENGINE_DB_OPTIONS,	        /* uint32_t */
            CL_ENGINE_DB_VERSION,	        /* uint32_t */
            CL_ENGINE_DB_TIME,		        /* time_t */
            CL_ENGINE_AC_ONLY,		        /* uint32_t */
            CL_ENGINE_AC_MINDEPTH,	        /* uint32_t */
            CL_ENGINE_AC_MAXDEPTH,	        /* uint32_t */
            CL_ENGINE_TMPDIR,		        /* (char *) */
            CL_ENGINE_KEEPTMP,		        /* uint32_t */
            CL_ENGINE_BYTECODE_SECURITY,    /* uint32_t */
            CL_ENGINE_BYTECODE_TIMEOUT,     /* uint32_t */
            CL_ENGINE_BYTECODE_MODE,        /* uint32_t */
            CL_ENGINE_MAX_EMBEDDEDPE,       /* uint64_t */
            CL_ENGINE_MAX_HTMLNORMALIZE,    /* uint64_t */
            CL_ENGINE_MAX_HTMLNOTAGS,       /* uint64_t */
            CL_ENGINE_MAX_SCRIPTNORMALIZE,  /* uint64_t */
            CL_ENGINE_MAX_ZIPTYPERCG,       /* uint64_t */
            CL_ENGINE_FORCETODISK,          /* uint32_t */
            CL_ENGINE_DISABLE_CACHE,        /* uint32_t */
            CL_ENGINE_DISABLE_PE_STATS,     /* uint32_t */
            CL_ENGINE_STATS_TIMEOUT,        /* uint32_t */
            CL_ENGINE_MAX_PARTITIONS,       /* uint32_t */
            CL_ENGINE_MAX_ICONSPE,          /* uint32_t */
            CL_ENGINE_MAX_RECHWP3,          /* uint32_t */
            CL_ENGINE_TIME_LIMIT,           /* uint32_t */
            CL_ENGINE_PCRE_MATCH_LIMIT,     /* uint64_t */
            CL_ENGINE_PCRE_RECMATCH_LIMIT,  /* uint64_t */
            CL_ENGINE_PCRE_MAX_FILESIZE,    /* uint64_t */
            CL_ENGINE_DISABLE_PE_CERTS,     /* uint32_t */
            CL_ENGINE_PE_DUMPCERTS          /* uint32_t */
        };

        internal enum bytecode_security
        {
            CL_BYTECODE_TRUST_ALL = 0, /* obsolete */
            CL_BYTECODE_TRUST_SIGNED, /* default */
            CL_BYTECODE_TRUST_NOTHING /* paranoid setting */
        };

        internal enum bytecode_mode
        {
            CL_BYTECODE_MODE_AUTO = 0, /* JIT if possible, fallback to interpreter */
            CL_BYTECODE_MODE_JIT, /* force JIT */
            CL_BYTECODE_MODE_INTERPRETER, /* force interpreter */
            CL_BYTECODE_MODE_TEST, /* both JIT and interpreter, compare results,
                  all failures are fatal */
            CL_BYTECODE_MODE_OFF /* for query only, not settable */
        };

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_set_num(IntPtr engine, cl_engine_field field, long num);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern long cl_engine_get_num(IntPtr engine, cl_engine_field field, ref int err);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_set_str(IntPtr engine, cl_engine_field field, string str);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_engine_get_str(IntPtr engine, cl_engine_field field, ref int err);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_engine_settings_copy(IntPtr engine);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_settings_apply(IntPtr engine, IntPtr settings);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_settings_free(IntPtr settings);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_compile(IntPtr engine);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_addref(IntPtr engine);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_engine_free(IntPtr engine);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cli_cache_disable();

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cli_cache_enable(IntPtr engine);

        /* CALLBACKS - WARNING: unstable API - WIP */

        internal delegate cl_error_t clcb_pre_scan(int fd, string type, IntPtr context);

        /* PRE-SCAN
        Called for each NEW file (inner and outer) before the scanning takes place. This is
        roughly the the same as clcb_before_cache, but it is affected by clean file caching.
        This means that it won't be called if a clean cached file (inner or outer) is
        scanned a second time.

        Input:
        fd = File descriptor which is about to be scanned
        type = File type detected via magic - i.e. NOT on the fly - (e.g. "CL_TYPE_MSEXE")
        context = Opaque application provided data

        Output:
        CL_CLEAN = File is scanned
        CL_BREAK = Whitelisted by callback - file is skipped and marked as clean
        CL_VIRUS = Blacklisted by callback - file is skipped and marked as infected
        */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_engine_set_clcb_pre_scan(IntPtr engine, clcb_pre_scan callback);

        internal delegate cl_error_t clcb_post_scan(int fd, int result, string virname, IntPtr context);
        /* POST-SCAN
        Called for each processed file (inner and outer), after the scanning is complete.

        Input:
        fd = File descriptor which is was scanned
        result = The scan result for the file
        virname = Virus name if infected
        context = Opaque application provided data

        Output:
        CL_CLEAN = Scan result is not overridden
        CL_BREAK = Whitelisted by callback - scan result is set to CL_CLEAN
        CL_VIRUS = Blacklisted by callback - scan result is set to CL_VIRUS
        */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_engine_set_clcb_post_scan(IntPtr engine, clcb_post_scan callback);

        internal delegate void clcb_virus_found(int fd, string virname, IntPtr context);
        //typedef void (*clcb_virus_found)(int fd, const char* virname, void* context);
        /* VIRUS FOUND
           Called for each virus found.
        Input:
        fd      = File descriptor which is was scanned
        virname = Virus name 
        context = Opaque application provided data
        Output:
        none
        */
        //extern void cl_engine_set_clcb_virus_found(struct cl_engine *engine, clcb_virus_found callback);
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_engine_set_clcb_virus_found(IntPtr engine, clcb_virus_found callback);

        internal delegate int clcb_sigload(IntPtr type, IntPtr name, IntPtr context);
        /* SIGNATURE LOAD
        Input:
        type = The signature type (e.g. "db", "ndb", "mdb", etc.)
        name = The virus name
        custom = The signature is official (custom == 0) or custom (custom != 0)
        context = Opaque application provided data

        Output:
        0 = Load the current signature
        Non 0 = Skip the current signature

        WARNING: Some signatures (notably ldb, cbc) can be dependent upon other signatures.
        Failure to preserve dependency chains will result in database loading failure.
        It is the implementor's responsibility to guarantee consistency.
        */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_engine_set_clcb_sigload(IntPtr engine, clcb_sigload callback, IntPtr context);

        /* LibClamAV messages callback
         * The specified callback will be called instead of logging to stderr.
         * Messages of lower severity than specified are logged as usual.
         *
         * Just like with cl_debug() this must be called before going multithreaded.
         * Callable before cl_init, if you want to log messages from cl_init() itself.
         *
         * You can use context of cl_scandesc_callback to convey more information to the callback (such as the filename!)
         * Note: setting a 2nd callbacks overwrites previous, multiple callbacks are not
         * supported
         */
        internal enum cl_msg
        {
            /* leave room for more message levels in the future */
            CL_MSG_INFO_VERBOSE = 32, /* verbose */
            CL_MSG_WARN = 64, /* LibClamAV WARNING: */
            CL_MSG_ERROR = 128/* LibClamAV ERROR: */
        };
        internal delegate void clcb_msg(cl_msg severity, string fullmsg, string msg, IntPtr context);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_set_clcb_msg(clcb_msg callback);

        /* LibClamAV hash stats callback */
        internal delegate void clcb_hash(int fd, ulong size, string md5, string virname, IntPtr context);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_engine_set_clcb_hash(IntPtr engine, clcb_hash callback);

        /* Archive member metadata callback. Return CL_VIRUS to blacklist, CL_CLEAN to
         * continue scanning */
        internal delegate cl_error_t clcb_meta(string container_type, ulong fsize_container, string filename,
            ulong fsize_real, int is_encrypted, uint filepos_container, IntPtr context);
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_engine_set_clcb_meta(IntPtr engine, clcb_meta callback);

        [StructLayout(LayoutKind.Sequential)]
        internal struct cl_stat
        {
            string dir;
            IntPtr stattab;
            IntPtr statdname;
            uint entries;
        };

        [StructLayout(LayoutKind.Sequential)]
        internal struct cl_cvd
        {		    /* field no. */
            string time;		    /* 2 */
            uint version;   /* 3 */
            uint sigs;	    /* 4 */
            uint fl;	    /* 5 */
            /* padding */
            string md5;		    /* 6 */
            string dsig;		    /* 7 */
            string builder;	    /* 8 */
            uint stime;	    /* 9 */
        };

        /* file scanning */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_scandesc(int desc, IntPtr virname, IntPtr scanned, IntPtr engine, uint scanoptions);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_scandesc_callback(int desc, IntPtr virname, IntPtr scanned, IntPtr engine, uint scanoptions, IntPtr context);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_scanfile([MarshalAs(UnmanagedType.LPStr)]string filename, ref IntPtr virname, ref ulong scanned, IntPtr engine, uint scanoptions);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_scanfile_callback(string filename, ref string virname, ref ulong scanned, IntPtr engine, uint scanoptions, IntPtr context);

        /* database handling */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_load(string path, IntPtr engine, ref uint signo, uint dboptions);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_retdbdir();

        /* engine handling */

        /* CVD */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern cl_cvd cl_cvdhead(IntPtr file);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern cl_cvd cl_cvdparse(IntPtr head);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_cvdverify(IntPtr file);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_cvdfree(IntPtr cvd);

        /* db dir stat functions */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_statinidir(IntPtr dirname, IntPtr dbstat);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_statchkdir(IntPtr dbstat);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_statfree(IntPtr dbstat);

        /* count signatures */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_countsigs(string path, uint countoptions, ref int sigs);

        /* enable debug messages */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_debug();

        /* software versions */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint cl_retflevel();

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_retver();

        /* others */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_strerror(int clerror);

        /* custom data scanning */

        /* handle - the handle passed to cl_fmap_open_handle, its meaning is up to the
         * callback's implementation
         * buf, count, offset - read 'count' bytes starting at 'offset' into the buffer 'buf'
         * Thread safety: it is guaranteed that only one callback is executing for a specific handle at
         * any time, but there might be multiple callbacks executing for different
         * handle at the same time.
        */
        internal delegate IntPtr clcb_pread(IntPtr handle, IntPtr buf, UIntPtr count, IntPtr offset);

        /* Open a map for scanning custom data accessed by a handle and pread (lseek +
         * read)-like interface. For example a WIN32 HANDLE.
         * By default fmap will use aging to discard old data, unless you tell it not
         * to.
         * The handle will be passed to the callback each time.
        */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_fmap_open_handle(IntPtr handle, UIntPtr offset, UIntPtr len, clcb_pread pread, int use_aging);

        /* Open a map for scanning custom data, where the data is already in memory,
         * either in the form of a buffer, a memory mapped file, etc.
         * Note that the memory [start, start+len) must be the _entire_ file,
         * you can't give it parts of a file and expect detection to work.
         */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr cl_fmap_open_memory(IntPtr start, UIntPtr len);

        /* Releases resources associated with the map, you should release any resources
         * you hold only after (handles, maps) calling this function */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void cl_fmap_close(IntPtr fmap);

        /* Scan custom data */
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int cl_scanmap_callback(IntPtr map, ref string virname, ref ulong scanned, IntPtr engine, uint scanoptions, IntPtr context);


        // Crypto API
        // Since 2 Jul 2014, the Crypto API has been moved to clamav.h
        // TODO: Implement in managed code
    }
}
