using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic
{
    public partial class Loading : MetroFramework.Forms.MetroForm
    {
        public Loading()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void metroProgressSpinner2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void toHome_Tick(object sender, EventArgs e)
        {
            var hub = new Form1();


            hub.Closed += (s, args) => this.Close();
            hub.Show();
            this.Hide();

            toHome.Stop();
        }
    }
}
