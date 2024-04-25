using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace University_Events_Management_System
{
    public partial class dashboard : Form
    {

       

        public dashboard()
        {
            InitializeComponent();

           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myform = new dashboard();
            myform.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            var home = new report();
            this.Hide();
            home.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            var home = new AboutBox1();
            
            home.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            var home = new Form1();
            this.Hide();
            home.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            this.Hide();
            Event loginForm = new Event();
            loginForm.Show();
        }
    }
}
