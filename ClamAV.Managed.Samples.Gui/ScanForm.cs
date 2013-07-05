using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClamAV.Managed.Samples.Gui
{
    public partial class ScanForm : Form
    {
        private ClamEngine _clamAV;

        public ScanForm()
        {
            // Windows Forms setup.
            InitializeComponent();

            this.Icon = SystemIcons.Shield;

            // Instantiate ClamAV engine.
            _clamAV = new ClamEngine();

            logTextBox.AppendText("ClamAV Version: " + _clamAV.Version +"\r\n");

            logTextBox.AppendText("Loading signatures from default location...\r\n");

            // Disable user input while loading.
            scanPathTextBox.Enabled = false;
            browseButton.Enabled = false;
            scanButton.Enabled = false;

            // Load signatures on a new thread.
            Thread setupThread = new Thread(() =>
                {
                    try
                    {
                        // Load database from the default location.
                        _clamAV.LoadDatabase();

                        // Signal database load is complete.
                        this.Invoke(new Action(() =>
                        {
                            logTextBox.AppendText("Signatures loaded.\r\n");

                            scanPathTextBox.Enabled = true;
                            browseButton.Enabled = true;
                            scanButton.Enabled = true;
                        }));
                    }
                    catch (Exception ex)
                    {
                        // Report exception to user.
                        this.Invoke(new Action(() =>
                        {
                            logTextBox.AppendText("Failed to load signatures:\r\n");
                            logTextBox.AppendText(ex.ToString());
                        }));
                    }
                });

            // Begin work.
            setupThread.Start();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // Show context menu strip to select between a file and directory to scan.
            fileOrDirectoryContextMenuStrip.Show(browseButton.PointToScreen(new Point(1, browseButton.Height)));
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Browse for a file to scan.

            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                scanPathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Browse for a folder to scan.

            var dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                scanPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            string scanPath = scanPathTextBox.Text;
            FileAttributes attribs = File.GetAttributes(scanPath);

            if ((attribs & FileAttributes.Directory) == FileAttributes.Directory)
            {
                // Perform directory scan.
                Thread scanThread = new Thread(() =>
                    {
                        var scannedFiles = new List<Tuple<string, ScanResult, string>>();

                        this.Invoke(new Action(() =>
                        {
                            logTextBox.AppendText("==========\r\n");
                            logTextBox.AppendText("\r\n");
                            logTextBox.AppendText("Scanning directory " + scanPath + "\r\n");
                        }));

                        _clamAV.ScanDirectory(scanPath,
                            (file, result, virus) =>
                            {
                                scannedFiles.Add(Tuple.Create(file, result, virus));

                                this.Invoke(new Action(() =>
                                {
                                    logTextBox.AppendText("Scanning: " + file + "\r\n");
                                }));
                            });

                        // Analyse results.
                        var infectedFiles = scannedFiles.Where(f => f.Item2 == ScanResult.Virus);

                        this.Invoke(new Action(() =>
                        {
                            logTextBox.AppendText("==========\r\n");
                            logTextBox.AppendText("\r\n");
                            logTextBox.AppendText(scanPath + "scanned\r\n");
                            logTextBox.AppendText("\r\n");
                            logTextBox.AppendText(string.Format("{0} file(s) scanned, {1} infected\r\n",
                                scannedFiles.Count, infectedFiles.Count()));
                            logTextBox.AppendText("\r\n");

                            foreach (var file in infectedFiles)
                            {
                                logTextBox.AppendText(string.Format("{0} infected with {1}\r\n", file.Item1, file.Item3));
                            }

                            logTextBox.AppendText("\r\n");
                        }));
                    });

                scanThread.Start();
            }
            else
            {
                // Perform file scan.
                Thread scanThread = new Thread(() =>
                {
                    // Signal start of scan on UI thread.
                    this.Invoke(new Action(() =>
                    {
                        statusLabel.Text = "Scanning...";
                        scanProgressBar.Value = 10;
                    }));

                    string virusName = "";

                    ScanResult scanResult = _clamAV.ScanFile(scanPath, ScanOptions.StandardOptions, out virusName);

                    // Report scan completion.
                    this.Invoke(new Action(() =>
                    {
                        statusLabel.Text = "Scan complete.";
                        scanProgressBar.Value = 100;

                        logTextBox.AppendText("==========\r\n");
                        logTextBox.AppendText("\r\n");
                        logTextBox.AppendText("Scanning file " + scanPath + " complete.\r\n");
                        if (scanResult == ScanResult.Clean)
                            logTextBox.AppendText("File is clean.\r\n");
                        else
                            logTextBox.AppendText("File is infected with " + virusName + ".\r\n");
                        logTextBox.AppendText("\r\n");
                    }));
                });

                // Begin scanning.
                scanThread.Start();
            }
        }
    }
}
