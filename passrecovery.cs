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
    public partial class passrecovery : Form
    {
        public passrecovery()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void passrecovery_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=sarahmae;database=collegedept";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT email, password FROM user WHERE email = @email";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Assuming textUser is a TextBox control where the username is entered
                    command.Parameters.AddWithValue("@email", textUser.Text);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming textPass is a TextBox control where the password will be displayed
                            textPass.Text = reader["password"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Username Not Available");
                            textPass.Text = "";
                        }
                    }
                }
            }
        }
    }
}
