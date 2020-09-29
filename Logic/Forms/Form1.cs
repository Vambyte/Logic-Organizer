using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Diagnostics;

namespace Logic
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public Point mouseLocation;
        public Form1()
        {
            InitializeComponent();


            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;

            timer1.Start();
            Time.Text = DateTime.Now.ToLongTimeString();

            //Label Home
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;

            pitem1.BackColor = Color.FromArgb(255, 255, 255);
            pitem2.BackColor = Color.FromArgb(14, 14, 14);
            pitem3.BackColor = Color.FromArgb(14, 14, 14);
            pitem4.BackColor = Color.FromArgb(14, 14, 14);
            this.CenterToScreen();

            btnNewScan.Enabled = false;
            btnNewScan.Visible = false;
            btnToDest.Enabled = false;
            btnToDest.Visible = false;
        }

        private void btnSource_Click(object sender, EventArgs e)
        {

        }
        private void btnTarget_Click(object sender, EventArgs e)
        {
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
        }


        public void StartForm()
        {
            Application.Run(new Loading());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }



        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Discrete_Click(object sender, EventArgs e)
        {

        }

        //Sidemenu

        private void label2_Click(object sender, EventArgs e) // HOME
        {
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;

            Logout.ForeColor = Color.White;

            pitem1.BackColor = Color.FromArgb(255, 255, 255);
            pitem2.BackColor = Color.FromArgb(14, 14, 14);
            pitem3.BackColor = Color.FromArgb(14, 14, 14);
            pitem4.BackColor = Color.FromArgb(14, 14, 14);

            homePanel.BringToFront();
        }

        private void label4_Click(object sender, EventArgs e) // DATA
        {
            label4.ForeColor = Color.Black;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label5.ForeColor = Color.White;

            Logout.ForeColor = Color.White;

            pitem2.BackColor = Color.FromArgb(255, 255, 255);
            pitem1.BackColor = Color.FromArgb(14, 14, 14);
            pitem3.BackColor = Color.FromArgb(14, 14, 14);
            pitem4.BackColor = Color.FromArgb(14, 14, 14);

            dataPanel.BringToFront();
        }

        private void label3_Click(object sender, EventArgs e) // ORGANIZE
        {
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label5.ForeColor = Color.White;

            Logout.ForeColor = Color.White;

            pitem3.BackColor = Color.FromArgb(255, 255, 255);
            pitem2.BackColor = Color.FromArgb(14, 14, 14);
            pitem1.BackColor = Color.FromArgb(14, 14, 14);
            pitem4.BackColor = Color.FromArgb(14, 14, 14);

           /* if (scanDone)
            {
                organizePanel_Ext.BringToFront();
                scanDone = false;
            }
            else
            {  */

            switch(currentOrgTab)
            {
                case "extension":
                    organizePanel_Ext.BringToFront();
                    break;
                case "directory":
                    organizePanel_Dir.BringToFront();
                    break;
                case "status":
                    organizePanel_Status.BringToFront();
                    break;
            }

            //}
        }

        private void label5_Click(object sender, EventArgs e) // ACCOUNT
        {
            label5.ForeColor = Color.Black;
            label4.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label2.ForeColor = Color.White;

            Logout.ForeColor = Color.White;

            pitem4.BackColor = Color.FromArgb(255, 255, 255);
            pitem3.BackColor = Color.FromArgb(14, 14, 14);
            pitem2.BackColor = Color.FromArgb(14, 14, 14);
            pitem1.BackColor = Color.FromArgb(14, 14, 14);

            accountPanel.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label2.ForeColor = Color.White;

            Logout.ForeColor = Color.Silver;

            pitem4.BackColor = Color.FromArgb(14, 14, 14);
            pitem3.BackColor = Color.FromArgb(14, 14, 14);
            pitem2.BackColor = Color.FromArgb(14, 14, 14);
            pitem1.BackColor = Color.FromArgb(14, 14, 14);


        }

        private void pitem1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }

        private void txtPercentage_Click(object sender, EventArgs e)
        {

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
                otherExtensionTxtbox.WaterMark = "Enter extension...";
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
            if (hasChecked)
            {

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

        private string currentOrgTab = "extension";
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
                        File.Copy(source, dest);

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

                        File.Copy(source, dest); // Copy file to new path

                    }
                } catch (FileNotFoundException ex) // Doesn't find the file in the list.
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
            Console.WriteLine("BEFORE");

            if (e.Cancelled)
            {
                progressBar.Value = 0;
                Console.WriteLine("AFTER");
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

        private void btnCopy_Click_1(object sender, EventArgs e)
        {
            organizePanel_Status.BringToFront();
            currentOrgTab = "status";

            txtPercentage.Text = "Scanning...";

            bool exeptionBool = false;
            var thread = new Thread(() =>
            {
                try
                {
                    orgFiles = Directory.GetFiles(fbdSource.SelectedPath, extension, SearchOption.AllDirectories).ToList();

                } catch(UnauthorizedAccessException ex) // Couldn't access certain directory
                {
                    exeptionBool = true;

                    MessageBox.Show(ex.Message,
                    "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } 
            });
            thread.Start();
            thread.Join(); // Wait for program to scan directory

            if (exeptionBool) // If we got an exeption
            {
                Console.WriteLine("Change directory");

                String tempExtension = extension; // Gets temporary extension
                ResetScan();
                extension = tempExtension; // Sets the extension back to what the user first chose

                organizePanel_Dir.BringToFront();
                currentOrgTab = "directory";
            }
            else
            {
                if (orgFiles.Any())
                {
                    worker.RunWorkerAsync();

                    t = new System.Timers.Timer();
                    t.Interval = 1000;
                    t.Elapsed += OntimeEvent;
                    t.Start();
                }
                else
                {
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
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ResetScan();
            organizePanel_Ext.BringToFront();
            currentOrgTab = "extension";
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtboxTarget.Text);
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

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
    }
}
