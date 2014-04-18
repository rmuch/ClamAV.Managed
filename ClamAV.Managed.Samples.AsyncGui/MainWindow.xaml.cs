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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClamAV.Managed.Async;

namespace ClamAV.Managed.Samples.AsyncGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClamEngine _clamEngine;

        public MainWindow()
        {
            InitializeComponent();

            _clamEngine = new ClamEngine();

            Log("ClamAV version " + ClamEngine.Version);
        }

        private void Log(string message)
        {
            ResultBox.AppendText(message + "\r\n");
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DisableInteraction();

            Log("Loading databases...");

            try
            {
                await _clamEngine.LoadDatabaseAsync();
            }
            catch (ClamException ex)
            {
                Log("Failed to load databases:");
                Log(ex.ToString());

                return;
            }

            Log("Databases loaded");

            EnableInteraction();
        }

        private void EnableInteraction()
        {
            PathBox.IsEnabled = true;
            ScanButton.IsEnabled = true;
        }

        private void DisableInteraction()
        {
            PathBox.IsEnabled = false;
            ScanButton.IsEnabled = false;
        }

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            var scanPath = PathBox.Text;

            // Determine file or directory.
            var fileAttributes = File.GetAttributes(PathBox.Text);
            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                ScanDirectory(scanPath);
            else
                ScanFile(scanPath);
        }

        private async void ScanFile(string scanPath)
        {
            DisableInteraction();

            Log("Scanning file " + scanPath);

            var result = await _clamEngine.ScanFileAsync(scanPath);

            if (result.Infected)
                Log("File infected with " + result.VirusName);
            else
                Log("File clean");
            Log("==========");

            EnableInteraction();
        }

        private async void ScanDirectory(string scanPath)
        {
            DisableInteraction();

            Log("Scanning directory " + scanPath);

            var results = await _clamEngine.ScanDirectoryAsync(scanPath);

            var infected = results.Where(r => r.Infected);

            Log(scanPath + " scanned");
            Log(string.Format("{0} file(s) scanned, {1} infected", results.Count(), infected.Count()));
            infected.All(
                result =>
                {
                    Log(string.Format("{0} infected with {1}", result.Path, result.VirusName));
                    return true;
                });
            Log("==========");

            EnableInteraction();
        }
    }
}
