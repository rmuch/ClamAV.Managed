/*
 * ClamAV.Managed.Samples.AsyncGui - Managed bindings for ClamAV - asynchronous GUI sample project
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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ClamAV.Managed.Async;
using ClamAV.Managed.Samples.AsyncGui.Helpers;

namespace ClamAV.Managed.Samples.AsyncGui.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly ClamEngine _clamEngine;

        private string _scanPath;
        private string _logView;
        private bool _busy;

        public string ScanPath
        {
            get { return _scanPath; }
            set
            {
                if (value == _scanPath)
                    return;

                _scanPath = value;

                OnPropertyChanged();
            }
        }

        public string Log
        {
            get { return _logView; }
            set
            {
                if (value == _logView)
                    return;

                _logView = value;

                OnPropertyChanged();
            }
        }

        public bool Busy
        {
            get { return _busy; }
            set
            {
                if (value.Equals(_busy))
                    return;

                _busy = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ScanCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        public MainWindowViewModel()
        {
            ScanCommand = new RelayCommand(OnScan, CanScan);
            LoadCommand = new RelayCommand(OnLoad);

            try
            {
                Busy = true;

                _clamEngine = new ClamEngine();
                WriteLogLine("ClamAV version " + ClamEngine.Version);
            }
            catch (Exception ex)
            {
                WriteLogLine("Failed to load engine:");
                WriteLogLine(ex.ToString());
            }
            finally
            {
                Busy = false;
            }
        }

        private void WriteLogLine(string message)
        {
            Log += message + "\r\n";
        }

        private async void OnLoad(object obj)
        {
            WriteLogLine("Loading databases...");

            try
            {
                Busy = true;

                await _clamEngine.LoadDatabaseAsync();
            }
            catch (ClamException ex)
            {
                WriteLogLine("Failed to load databases:");
                WriteLogLine(ex.ToString());

                return;
            }
            finally
            {
                Busy = false;
            }

            WriteLogLine("Databases loaded");
        }

        private async void OnScan(object obj)
        {
            var scanPath = ScanPath;

            try
            {
                Busy = true;

                // Determine file or directory.
                var fileAttributes = File.GetAttributes(ScanPath);
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    await ScanDirectory(scanPath);
                }
                else
                {
                    await ScanFile(scanPath);
                }
            }
            catch (Exception ex)
            {
                WriteLogLine("Failed to scan file or directory:");
                WriteLogLine(ex.ToString());
            }
            finally
            {
                Busy = false;
            }
        }

        private bool CanScan(object obj)
        {
            return !string.IsNullOrWhiteSpace(ScanPath) && !Busy;
        }

        private async Task ScanFile(string scanPath)
        {
            WriteLogLine("Scanning file " + scanPath);

            var result = await _clamEngine.ScanFileAsync(scanPath);

            if (result.Infected)
            {
                WriteLogLine("File infected with " + result.VirusName);
            }
            else
            {
                WriteLogLine("File clean");
            }

            WriteLogLine("==========");
        }

        private async Task ScanDirectory(string scanPath)
        {
            WriteLogLine("Scanning directory " + scanPath);

            var results = await _clamEngine.ScanDirectoryAsync(scanPath);

            var resultList = results.ToList();
            var infectedList = resultList.Where(r => r.Infected).ToList();

            WriteLogLine(scanPath + " scanned");
            WriteLogLine(string.Format("{0} file(s) scanned, {1} infected", resultList.Count, infectedList.Count));

            foreach (var infected in infectedList)
            {
                WriteLogLine(string.Format("{0} infected with {1}", infected.Path, infected.VirusName));
            }

            WriteLogLine("==========");
        }

        public void Dispose()
        {
            if (_clamEngine != null)
            {
                _clamEngine.Dispose();
            }
        }
    }
}
