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

namespace Logic
{
    public partial class Loginscreen : Form
    {

        public String username;
        public String password;
        public Loginscreen()
        {
            InitializeComponent();

            username = Properties.Settings.Default.Username;
            password = Properties.Settings.Default.Password;

            webBrowser1.Navigate("https://files.getdiscrete.cc/logic_organizer/login.php?username=" + username + "&password=" + password);
        }

        private void MetroProgressSpinner1_Click(object sender, EventArgs e)
        {

        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            String response = webBrowser1.DocumentText;

            if (response.Contains("p1"))
            //Open brackets
            {
                var hub = new MainForm();

                hub.Closed += (s, args) => this.Close();
                hub.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Your Username or Password is incorrect",
                    "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                var hub = new Logincs();

                hub.Closed += (s, args) => this.Close();
                hub.Show();
                this.Hide();
            }
        }
    }
}
