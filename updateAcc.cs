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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace University_Events_Management_System
{
    public partial class updateAcc : Form
    {
        MySqlConnection conn;
        string myConnectionString;

        public updateAcc()
        {
            InitializeComponent();

            // Initialize connection string
            myConnectionString = "server=127.0.0.1;uid=root;pwd=sarahmae;database=collegedept";

            // Initialize MySqlConnection
            conn = new MySqlConnection(myConnectionString);
        }
      
       




        private void updateAcc_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Get input values from textboxes
            string username = txtname.Text;
            string password = txtpass.Text;
            string email = txtemail.Text;

            // Ensure all fields are filled
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            try
            {
                // Open connection
                conn.Open();

                // Prepare SQL query
                string query = "UPDATE user SET password = @password, email = @email WHERE username = @username";

                // Create MySqlCommand
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add parameters to the command
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                // Execute the command
                int rowsAffected = cmd.ExecuteNonQuery();

                // Check if any rows were affected
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Profile Updated Successfully");
                }
                else
                {
                    MessageBox.Show("No data updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                conn.Close();
            }

        }

        private void UpdateRecordToDatabase(string name, string pass, string email)
        {
    
        }

       

        private void cancelAccBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
        
        }


    }
}