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
    public partial class Event : Form
    {
        private bool isLoggedIn = false; // Flag to track login status
        private MySqlConnection conn;
        private string connectionString = "server=127.0.0.1;uid=root;pwd=sarahmae;database=collegedept";



        public Event()
        {
            InitializeComponent();

            conn = new MySqlConnection(connectionString);
            CheckLoginStatus();
        }

        // Method to update UI based on login status
        private void UpdateUI()
        {
            if (isLoggedIn)
            {
                // Enable controls for logged-in state
                btnlogin.Enabled = true;

            }
            else
            {
                // Disable controls for logged-out state
                btnlogin.Enabled = false;

            }
        }



        // Method to check login status from the database
        private void CheckLoginStatus()
        {
           

        }

      




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            isLoggedIn = true;
            UpdateUI();

            string username = this.txt_username.Text;
            string password = this.txt_password.Text;
            /* if ((username == "admin") && (password == "12345"))
             {
                 this.Hide();
                 var myform = new Form1();
                 myform.Show();
             }
             else 
             {
                 MessageBox.Show("Invalid Username or Password", "Invalid",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
             }*/

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=sarahmae;database=collegedept";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "SELECT COUNT(*) from user where username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());


                if (count > 0)
                {
                    //MessageBox.Show("You are now logged in");
                    var home = new Form1();
                    this.Hide();
                    home.Show();

                }
                else
                {
                    MessageBox.Show("Invalid Username and Password, please try again");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Reset_Click(object sender, EventArgs e)
        {
            txt_username.Text = "";
            txt_password.Text = "";
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var myform = new passrecovery();
            myform.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var home = new signUp();
            this.Hide();
            home.Show();
        }
    }
}
 



