/*
 * ClamAV.Managed.PowerShell - Managed bindings for ClamAV - PowerShell cmdlets
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
using System.Management.Automation;
using System.Reflection;
using System.Text;

namespace ClamAV.Managed.PowerShell
{
    /// <summary>
    /// Cmdlet wrapping the ClamEngine.LoadDatabase() method.
    /// </summary>
    [Cmdlet("Load", "Database")]
    public class LoadDatabaseCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "ClamEngine created by New-ClamEngine.")]
        public ClamEngine Engine { get; set; }

        [Parameter(Mandatory = false, Position = 0, HelpMessage = "Path to the database to load.")]
        public string DatabasePath { get; set; }

        protected override void ProcessRecord()
        {
            string expectedDbPath = Path.Combine(Assembly.GetExecutingAssembly().Location, "db");

            if (String.IsNullOrEmpty(DatabasePath))
                Engine.LoadDatabase(expectedDbPath);
            else
                Engine.LoadDatabase(DatabasePath);
        }
    }
}
