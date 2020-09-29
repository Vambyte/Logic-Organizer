using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Logic
{
    public partial class MainForm : Form
    {

        string fileOpt = "copy";

        string currentScanText = "Scanning";

        string localDriveChar = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);

        public MainForm()
        {
            InitializeComponent();

            SideBarColor(sidePanel_home);
            home_btn.BackColor = Color.FromArgb(54, 59, 68);

            welcomeLabel.Text += " " + Properties.Settings.Default.Username;

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;

            timer1.Start();
            Time.Text = DateTime.Now.ToLongTimeString();

            btnNewScan.Enabled = false;
            btnNewScan.Visible = false;
            btnToDest.Enabled = false;
            btnToDest.Visible = false;

            hasChecked = true;

            currentTab = home_btn;

            panelTest.Top = currentTab.Top;

            extensionListBox.DisplayMember = "Text";
            extensionListBox.ValueMember = "Value";

            string[] exts = { "PNG", "JPG", "GIF", "BMP", "TXT", "RAR", "ZIP", "PDF", "MP3", "MP4", "WAV", "M4V"};

            foreach (string s in exts)
            {
                extensionListBox.Items.Add(new { Text = s, Value = "*." + s.ToLower() });
            }

        }

        Button currentTab;

        private void PanelBackColor(Button b)
        {
            /*
            if (b.BackColor == Color.FromArgb(88, 97, 112)) // If it already has an overlay
                return;

            home_btn.BackColor = Color.FromArgb(54, 59, 68);
            account_btn.BackColor = Color.FromArgb(54, 59, 68);
            data_btn.BackColor = Color.FromArgb(54, 59, 68);
            scan_btn.BackColor = Color.FromArgb(54, 59, 68); // Resets all button overlays

            b.BackColor = Color.FromArgb(88, 97, 112); //Color.FromArgb(73, 81, 94); // Sets new overlay, for specified button
            */
        }


        private void SideBarColor(Panel p)
        {
            /*
            if (p.BackColor == Color.FromArgb(61, 57, 57)) 
                return;

            sidePanel_home.BackColor = Color.DarkGray;
            sidePanel_account.BackColor = Color.DarkGray;
            sidePanel_data.BackColor = Color.DarkGray;
            sidePanel_scan.BackColor = Color.DarkGray; 

            p.BackColor = Color.FromArgb(61, 57, 57);
             */
        }


        private void Account_btn_Click(object sender, EventArgs e)
        {
            //  SideBarColor(sidePanel_account);
            PanelBackColor(account_btn);

            accountPanel.BringToFront();

            currentTab = account_btn;
        }

        private void Data_btn_Click(object sender, EventArgs e)
        {
            //SideBarColor(sidePanel_data);
            PanelBackColor(data_btn);

            dataPanel.BringToFront();

            currentTab = data_btn;
        }

        private void Scan_btn_Click(object sender, EventArgs e)
        {
            // SideBarColor(sidePanel_scan);
            PanelBackColor(scan_btn);

            switch (currentOrgTab)
            {
                case "option":
                    organizePanel_Opt.BringToFront();
                    break;
                case "extension":
                    organizePanel_Ext.BringToFront();
                    break;
                case "directory":
                    organizePanel_Dir.BringToFront();
                    break;
                case "scan":
                    organizePanel_Scan.BringToFront();
                    break;
                case "status":
                    organizePanel_Status.BringToFront();
                    break;
            }

            currentTab = scan_btn;
        }

        private void Home_btn_Click(object sender, EventArgs e)
        {
            //  SideBarColor(sidePanel_home);
            PanelBackColor(home_btn);

            homePanel.BringToFront();

            currentTab = home_btn;
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // OTHER //

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        // Extension Tab

        private bool hasChecked = false;

        private void ChangeExtension()
        {
            //  orgFiles = Directory.GetFiles(fbdSource.SelectedPath, extension, SearchOption.AllDirectories).ToList();
            changeExt = true;
            organizePanel_Ext.BringToFront();
            currentOrgTab = "extension";
        }

        private void otherExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
            if (otherExtension.Checked)
            {
                otherExtensionTxtbox.WaterMark = "Enter extension without dot...";
                otherExtensionTxtbox.Enabled = true;
            }

            if (!otherExtension.Checked)
            {
                otherExtensionTxtbox.WaterMark = "";
                otherExtensionTxtbox.Text = "";
                otherExtensionTxtbox.Enabled = false;
            }
        }

        private void btnToDir_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hey1");
            if (hasChecked)
            {
                Console.WriteLine("hey");
                if (allExtension.Checked)
                    extension = "*.*";
                if (pngExtension.Checked)
                    extension = "*.png";
                if (jpgExtension.Checked)
                    extension = "*.jpg";
                if (mp3Extension.Checked)
                    extension = "*.mp3";
                if (bmpExtension.Checked)
                    extension = "*.bmp";
                if (rarExtension.Checked)
                    extension = "*.rar";
                if (zipExtension.Checked)
                    extension = "*.zip";
                if (mp4Extension.Checked)
                    extension = "*.mp4";
                if (wavExtension.Checked)
                    extension = "*.wav";
                if (txtExtension.Checked)
                    extension = "*.txt";
                if (gifExtension.Checked)
                    extension = "*.gif";
                if (pdfExtension.Checked)
                    extension = "*.pdf";
                if (m4vExtension.Checked)
                    extension = "*.mv4";
                if (otherExtension.Checked)
                    extension = "*" + "." + otherExtensionTxtbox.Text.ToLower();

                if (changeExt)
                {
                    orgFiles = Directory.GetFiles(fbdSource.SelectedPath, extension, SearchOption.AllDirectories).ToList();
                    changeExt = false;
                }

                organizePanel_Dir.BringToFront();
                currentOrgTab = "directory";
            }
            else
            {
                // They haven't checked anything
            }
        }

        private void allExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void pngExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void txtExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void mp3Extension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void jpgExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void zipExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void wavExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void bmpExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void rarExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void mp4Extension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void gifExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void pdfExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        private void m4vExtension_CheckedChanged(object sender, EventArgs e)
        {
            hasChecked = true;
        }

        // Directory Tab

        private string currentOrgTab = "option";
        List<string> orgFiles = new List<string>();
        private string extension = "*.*";
        BackgroundWorker worker = new BackgroundWorker();

        public void GetExtension(string ext)
        {
            extension = ext;
        }

        void CopyFile(string source, string dest, DoWorkEventArgs e)
        {
            var thread = new Thread(() =>
            {
                try
                {

                    string file = Path.GetFileName(source);

                    string fileExt = Path.GetFileNameWithoutExtension(dest);


                    if (!File.Exists(dest))
                    {
                        if (fileOpt == "copy")
                            File.Copy(source, dest);
                        if (fileOpt == "move")
                            File.Move(source, dest);

                    }
                    else // If file already exits in destination directory
                    {
                        int i = 0;
                        while (File.Exists(dest))
                        {
                            if (i == 0)
                                dest = dest.Replace(fileExt, "(" + ++i + ")" + fileExt); // Change name by adding .(i)
                            else
                                dest = dest.Replace("(" + i + ")" + fileExt, "(" + ++i + ")" + fileExt); // Change name by adding .(i)
                        }

                        if (fileOpt == "copy")
                            File.Copy(source, dest); // Copy file to new path
                        if (fileOpt == "move")
                            File.Move(source, dest); // Move file to new path

                    }
                }
                catch (FileNotFoundException ex) // Doesn't find the file in the list.
                {
                    Console.WriteLine(ex.Message);
                }
            });
            thread.Start();

            thread.Join();

            copiedFiles++;

            if (copiedFiles == orgFiles.Count + 1)
                worker.CancelAsync();

            if (worker.CancellationPending)
                e.Cancel = true;
            else
                worker.ReportProgress((int)(copiedFiles * 100 / orgFiles.Count));
        }

        int copiedFiles = 0;
        DoWorkEventArgs dwea;
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (extension != "*.*")
            {
                if (!Directory.Exists(txtboxTarget.Text + "\\" + Path.GetExtension(orgFiles[0]).ToUpper()))
                    Directory.CreateDirectory(txtboxTarget.Text + "\\" + Path.GetExtension(orgFiles[0]).ToUpper());

                Console.WriteLine(Path.GetFileNameWithoutExtension(orgFiles[0]).ToUpper());
            }

            int i = 0;

            foreach (string file in orgFiles)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    string destPath = "";
                    if (extension != "*.*")
                    {
                        destPath = Path.Combine(txtboxTarget.Text + "\\" + Path.GetExtension(orgFiles[0]).ToUpper(), Path.GetFileName(file));
                    }
                    else
                    {
                        if (!Directory.Exists(txtboxTarget.Text + "\\" + Path.GetExtension(orgFiles[i]).ToUpper()))
                            Directory.CreateDirectory(txtboxTarget.Text + "\\" + Path.GetExtension(orgFiles[i]).ToUpper());

                        destPath = Path.Combine(txtboxTarget.Text + "\\" + Path.GetExtension(orgFiles[i]).ToUpper(), Path.GetFileName(file));
                    }

                    CopyFile(file, destPath, e);
                }

                i++;
            }

            worker.CancelAsync();

            dwea = e;

        }

        private bool scanDone = false;
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage);
            progressBar.Value = e.ProgressPercentage;
            txtPercentage.Text = "Processing... " + progressBar.Value.ToString() + "%";


            if (txtPercentage.Text == "Processing... 100%") // It is done with the processing
            {
                worker.CancelAsync();
                dwea.Cancel = true;
                txtPercentage.Text = "Done!";

                t.Stop();
                s = 0;
                m = 0;
                h = 0;

                txtFileAmount_Status.Text = "Files transferred: " + orgFiles.Count;
                scanDone = true;

                btnNewScan.Enabled = true;
                btnNewScan.Visible = true;

                btnToDest.Enabled = true;
                btnToDest.Visible = true;

                numOfScans++;
            }

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                progressBar.Value = 0;
            }
        }




        FolderBrowserDialog fbdSource = new FolderBrowserDialog();
        private bool changeExt = false;
        private int numOfScans = 0;

        private void btnSource_Click_1(object sender, EventArgs e)
        {
            if (fbdSource.ShowDialog() == DialogResult.OK)
            {
                txtboxSource.Text = fbdSource.SelectedPath;
            }
        }

        private void btnTarget_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtboxTarget.Text = fbd.SelectedPath;
            }
        }

        System.Timers.Timer t;
        int h, m, s;

        long filesSizeCalculated = 0;

        private void ExceptionScanUI()
        {
            ResetScanUI();

            scanningTextTimer.Stop();

            scanLabelTag.Text = "Scan failed";

            scanSpinner.Visible = false;
        }

        private void ResetScanUI()
        {
            amntFilesScan.Visible = false;

            availSpace_scan.Visible = false;
            fileSizeInfo.Visible = false;
            reqSpace_scan.Visible = false;

            btnStartOperation.Visible = false;
            btnCancelOperation.Visible = false;

            notEnoughSpaceWarning.Visible = false;

            scanSpinner.Visible = true;

            currentScanText = "Scanning";
            scanLabelTag.Text = currentScanText;
        }

        private void AfterScan()
        {
            if (exceptionScanBool) // If we got an exeption
            {
                String tempExtension = extension; // Gets temporary extension
                ResetScan();
                extension = tempExtension; // Sets the extension back to what the user first chose

                organizePanel_Dir.BringToFront();
                currentOrgTab = "directory";

                ExceptionScanUI();
            }
            else
            {
                if (orgFiles.Any())
                {
                    var thread = new Thread(() =>
                    {

                        long totalFilesSize = 0;

                        foreach (string s in orgFiles)
                        {

                            filesSizeCalculated += 1;

                            totalFilesSize += GetFileSizeOnDisk(s); // 1.0 × 10(-9)

                            // Action textAction = () => scanSpinner.Text = ((filesSizeCalculated / orgFiles.Count) * 100) + "%";
                            // this.BeginInvoke(textAction);
                        }

                        Action action = new Action(() =>
                        {
                            scanningTextTimer.Stop();

                            scanLabelTag.Text = "Scan Complete";

                            long totalBytesAvailSize = GetTotalFreeSpace(localDriveChar);

                            //totalFilesSize = 100216395500;  // Test the space check
                            //totalBytesAvailSize = 12312689;

                            string reqFileSize = FormatBytes(totalFilesSize);
                            string availFileSize = FormatBytes(totalBytesAvailSize);

                            reqSpace_scan.Text = "Required space: " + reqFileSize;
                            availSpace_scan.Text = "Available space: " + availFileSize;

                            amntFilesScan.Text = "Amount of files: " + orgFiles.Count;
                            amntFilesScan.Visible = true;

                            scanSpinner.Visible = false;


                            if (fileOpt == "copy")
                            {
                                reqSpace_scan.Visible = true;
                                fileSizeInfo.Visible = true;
                            }                            
                            else // If selected "move" start operation directly
                            {
                                organizePanel_Status.BringToFront();
                                currentOrgTab = "status";

                                worker.RunWorkerAsync();

                                t = new System.Timers.Timer();
                                t.Interval = 1000;
                                t.Elapsed += OntimeEvent;
                                t.Start();
                            }

                            availSpace_scan.Visible = true;
                            
                            if (totalFilesSize < totalBytesAvailSize) // If the user has enough disk space for the required files
                            {
                                btnStartOperation.Visible = true;
                                btnCancelOperation.Visible = true;

                                availSpace_scan.ForeColor = Color.Lime;
                            }
                            else
                            {
                                notEnoughSpaceWarning.Visible = true;

                                btnCancelOperation.Top = btnStartOperation.Top;
                                btnCancelOperation.Visible = true;

                                availSpace_scan.ForeColor = Color.Red;
                            }

                        });
                        this.BeginInvoke(action);
                    });
                    thread.Start();


                }
                else
                {
                    ExceptionScanUI();

                    DialogResult dialogResult = MessageBox.Show("We could not locate any files with your selected extension \n \nDo you want to select a new extension?",
                        "Extension Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Console.WriteLine("Change extension.");

                        ChangeExtension();
                    }
                    if (dialogResult == DialogResult.No)
                    {
                        Console.WriteLine("Don't change extension.");

                        organizePanel_Dir.BringToFront();
                        currentOrgTab = "directory";
                    }
                }
            }
        }

        bool exceptionScanBool = false;

        private void btnCopy_Click_1(object sender, EventArgs e)
        {
            if (txtboxSource.Text != "" && txtboxTarget.Text != "")
            {
                organizePanel_Scan.BringToFront();
                currentOrgTab = "scan";

                exceptionScanBool = false;

                scanningTextTimer.Start();

                scanSpinner.Visible = true;
                var thread = new Thread(() =>
                {
                    try
                    {
                        orgFiles = Directory.GetFiles(fbdSource.SelectedPath, extension, SearchOption.AllDirectories).ToList();
                    }
                    catch (UnauthorizedAccessException ex) // Couldn't access certain directory
                    {
                        exceptionScanBool = true;

                        MessageBox.Show(ex.Message,
                        "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    Action action = new Action(AfterScan);
                    this.BeginInvoke(action);
                });
                thread.Start();
            }
            else
            {
                MessageBox.Show("You have not selected the path for the source or/and the destination",
                            "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetScan()
        {
            orgFiles.Clear();
            extension = "";
            txtTime_Status.Text = "00:00:00";

            txtFileAmount_Status.Text = "";
            txtPercentage.Text = "Processing... 0%";
            btnNewScan.Enabled = false;
            btnNewScan.Visible = false;
            btnToDest.Enabled = false;
            btnToDest.Visible = false;

            copiedFiles = 0;

            progressBar.Value = 0;


            ResetScanUI();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ResetScan();
            organizePanel_Ext.BringToFront();
            currentOrgTab = "extension";
        }

        private void HomePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }
        private void PanelTimer_Tick(object sender, EventArgs e)
        {

            int diff = panelTest.Top - currentTab.Top;

            var val = 3;

            if (diff < 0)
            {

                if ((diff / val) <= 0 && (diff / val) >= -1)
                {
                    diff = 0;
                }
                else
                {
                    panelTest.Top -= (int)(diff / val);
                }
            }
            else if (diff == 0)
            {
                return;
            }
            else
            {
                if ((diff / val) <= 0 && (diff / val) >= -1)
                {
                    diff = 0;
                }
                else
                {
                    panelTest.Top -= (int)(diff / val);
                }
            }

            if (diff <= 2 && diff >= -2)
            {
                panelTest.Top = currentTab.Top;
                panelTest.Height = currentTab.Height;
            }
        }

        private void OtherExtensionTxtbox_Click(object sender, EventArgs e)
        {

        }

        private void BtnBackToExt_Click(object sender, EventArgs e)
        {
            organizePanel_Ext.BringToFront();
            currentOrgTab = "extension";
        }

        private void btnOptCpy_Click(object sender, EventArgs e)
        {
            fileOpt = "copy";

            organizePanel_Ext.BringToFront();
            currentOrgTab = "extension";
        }

        private void BtnOptMov_Click(object sender, EventArgs e)
        {
            fileOpt = "move";

            organizePanel_Ext.BringToFront();
            currentOrgTab = "extension";
        }

        private void BtnBackToOpt_Click(object sender, EventArgs e)
        {
            organizePanel_Opt.BringToFront();
            currentOrgTab = "option";
        }

        private void btnNewScan_Click(object sender, EventArgs e)
        {
            ResetScan();

            organizePanel_Opt.BringToFront();
            currentOrgTab = "option";
        }

        private void BtnStartOperation_Click(object sender, EventArgs e)
        {
            organizePanel_Status.BringToFront();
            currentOrgTab = "status";

            worker.RunWorkerAsync();

            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OntimeEvent;
            t.Start();
        }

        private void BtnCancelOperation_Click(object sender, EventArgs e)
        {
            ResetScan();

            organizePanel_Opt.BringToFront();
            currentOrgTab = "option";
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtboxTarget.Text);
        }

        // Status Tab

        private void OntimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                s += 1;
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;
                }
                txtTime_Status.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));

            }));
        }

        // API //
        public static long GetFileSizeOnDisk(string file)
        {
            FileInfo info = new FileInfo(file);
            uint dummy, sectorsPerCluster, bytesPerSector;
            int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
            if (result == 0) throw new Win32Exception();
            uint clusterSize = sectorsPerCluster * bytesPerSector;
            uint hosize;
            uint losize = GetCompressedFileSizeW(file, out hosize);
            long size;
            size = (long)hosize << 32 | losize;
            return ((size + clusterSize - 1) / clusterSize) * clusterSize;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);

        private void AfterFilesScan_Tick(object sender, EventArgs e)
        {

        }

        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private void AmntFilesTimer_Tick(object sender, EventArgs e)
        {
            amntFilesScan.Text = "Amount of files: " + GenerateRandomNumber(10, 1000);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChecked = true;
            extension = (extensionListBox.SelectedItem as dynamic).Value;

            Console.WriteLine((extensionListBox.SelectedItem as dynamic).Value);
        }

        private void ScanningTextTimer_Tick(object sender, EventArgs e)
        {
            if (scanLabelTag.Text != currentScanText + "...")
                scanLabelTag.Text += ".";
            else
                scanLabelTag.Text = currentScanText;
            
        }

        private void ExtensionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChecked = true;
            extension = (extensionListBox.SelectedItem as dynamic).Value;

            allExtension.Checked = false;

            Console.WriteLine((extensionListBox.SelectedItem as dynamic).Value);
        }

        private long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        private int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // // // // //
    }
}

