using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace University_Events_Management_System
{
    public partial class report : Form
    {
        MySqlConnection conn;
        string myConnectionString;

        public report()
        {
            InitializeComponent();
            // Populate ComboBox with table choices
            comboBoxTables.Items.Add("Budget");
            comboBoxTables.Items.Add("Participants");
            comboBoxTables.Items.Add("Speaker");

            // Initialize connection string
            myConnectionString = "server=127.0.0.1;uid=root;pwd=sarahmae;database=collegedept";

            // Initialize MySqlConnection
            conn = new MySqlConnection(myConnectionString);
        }

        private void report_Load(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                conn.Open();

                // Prepare SQL query to select all records from the user table
                string query = "SELECT * FROM event";

                // Create MySqlCommand
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Create a DataTable to store the results
                DataTable dataTable = new DataTable();

                // Create a MySqlDataAdapter to fill the DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill the DataTable with the results from the query
                adapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView to display the results
                dataGridView1.DataSource = dataTable;
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

        private void label7_Click(object sender, EventArgs e)
        {
            var home = new Form1();
            this.Hide();
            home.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var home = new dashboard();
            this.Hide();
            home.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            var home = new report();
            this.Hide();
            home.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var home = new AboutBox1();
            home.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var home = new updateAcc();
            home.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string searchQuery = txtsearchr.Text.Trim();

            // Ensure search query is not empty
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a search query");
                return;
            }

            try
            {
                // Open connection
                conn.Open();

                // Prepare SQL query
                string query = "SELECT * FROM event WHERE eventname LIKE @searchQuery OR deptname LIKE @searchQuery";



                // Create MySqlCommand
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add parameter to the command
                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                // Create a DataTable to store the search results
                DataTable dataTable = new DataTable();

                // Create a MySqlDataAdapter to fill the DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill the DataTable with the search results
                adapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView to display the search results
                dataGridView1.DataSource = dataTable;
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


        private void ExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new Excel application
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;

                // Open the Excel template file (replace Template.xlsx with your template's file name)
                Excel.Workbook workbook = excelApp.Workbooks.Open(@"C:\Users\user\Documents\Template.xlsx");
                Excel.Worksheet dataSheet = workbook.Sheets[1]; // Assuming Sheet1 for data
                Excel.Worksheet graphSheet = workbook.Sheets[2]; // Assuming Sheet2 for graph

                int startRow = 19; // Adjust this according to your template
                int startColumn = 3; // Adjust this according to your template
                                    

                // Get the DataGridView data
                DataGridViewRowCollection rows = dataGridView1.Rows;
                DataGridViewColumnCollection columns = dataGridView1.Columns;

                // Write DataGridView data to Excel dataSheet
                for (int i = 0; i < rows.Count; i++)
                {
                    for (int j = 0; j < columns.Count; j++)
                    {
                        dataSheet.Cells[startRow + i, startColumn + j] = rows[i].Cells[j].Value?.ToString();
                    }
                }


              

                // Define the range for the data
                Excel.Range dataRange = dataSheet.Range[dataSheet.Cells[startRow, startColumn], dataSheet.Cells[startRow + rows.Count - 1, startColumn + columns.Count - 1]];

    

                // Define the range for the cell where you want to anchor the chart
                Excel.Range anchorRange = graphSheet.Cells[25, 25]; 


                // Add a chart
                Excel.ChartObjects chartObjects = (Excel.ChartObjects)graphSheet.ChartObjects();
                Excel.ChartObject chartObject = chartObjects.Add(100, 100, 300, 200); // Adjust position and size as needed
                Excel.Chart chart = chartObject.Chart;

                // Set chart type
                chart.ChartType = Excel.XlChartType.xlColumnClustered; // Change this to the desired chart type

                // Set chart data
                chart.SetSourceData(dataRange);

                // Set chart title
                chart.HasTitle = true;
                chart.ChartTitle.Text = "Event Management Chart";

                // Save the Excel file with a new name
                workbook.SaveAs(@"C:\Users\user\Documents\Report.xlsx");

                

                // Release COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(chart);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObject);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObjects);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(graphSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Excel file has been created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void comboBoxTables_SelectedIndexChanged_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = comboBoxTables.SelectedItem.ToString();

            switch (selectedTable)
            {
                case "Budget":
                    DisplaybudgetTable();
                    break;
                case "Participants":
                    DisplayParticipantsTable();
                    break;
                case "Speaker":
                    DisplaySpeakerTable();
                    break;
                default:
                    break;
            }
        }

        // Method to display data from the User table
        private void DisplaybudgetTable()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM budget_per_dept";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Similar methods for displaying data from Event and Budget tables
        private void DisplayParticipantsTable()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM participants";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void DisplaySpeakerTable()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM event_speaker";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



       

    }
}

