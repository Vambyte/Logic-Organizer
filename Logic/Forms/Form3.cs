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
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Time_Click(object sender, EventArgs e)
        {

        }




        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "U s e r n a m e")
            {
                textBox1.Text = "";
                textBox1.ForeColor = SystemColors.WindowText;
            }

            panel1.BackColor = Color.FromArgb(255, 255, 255);
            textBox1.ForeColor = Color.FromArgb(255, 255, 255);

            panel2.BackColor = Color.WhiteSmoke;
            textBox2.ForeColor = Color.WhiteSmoke;

        }


        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "P a s s w o r d")
            {
                textBox2.Text = "";
                textBox2.ForeColor = SystemColors.WindowText;
                textBox2.UseSystemPasswordChar = false;
            }

            panel2.BackColor = Color.FromArgb(255, 255, 255);
            textBox2.ForeColor = Color.FromArgb(255, 255, 255);

            panel2.BackColor = Color.WhiteSmoke;
            textBox2.ForeColor = Color.WhiteSmoke;

            panel1.BackColor = Color.WhiteSmoke;
            textBox1.ForeColor = Color.WhiteSmoke;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;


            if (textBox1.Text == "123" && textBox2.Text == "123")
            {
                var hub = new Loading();


                hub.Closed += (s, args) => this.Close();
                hub.Show();
                this.Hide();
            }
            else
                MessageBox.Show("You suck");




        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
                button1.BackColor = Color.Transparent;
                button1.ForeColor = Color.Black;
        }

        private void button1_CursorChanged(object sender, EventArgs e)
        {
                button1.BackColor = Color.Transparent;

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;

        }

        private void button1_DragLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
        }

        private void button1_DragOver(object sender, DragEventArgs e)
        {
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
        }

        private void button1_DragEnter(object sender, DragEventArgs e)
        {
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "U s e r n a m e";
                textBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox2.UseSystemPasswordChar = true;
                textBox2.Text = "P a s s w o r d";
                textBox2.ForeColor = SystemColors.GrayText;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
