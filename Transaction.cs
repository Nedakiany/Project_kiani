using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace WindowsFormsApp19
{
    public partial class Transaction : Form
    {
        private string connectionString = @" Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\WindowsFormsApp19\WindowsFormsApp19\Database1.mdf;Integrated Security=True";
        public Transaction()
        {
            InitializeComponent();
            LoadTransactions();
        }
        private void AddTransaction(decimal amount, string description, DateTime transactionDate, string transactiontype)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Transactions (Amount, Description, TransactionDate, TransactionType)VALUES (@Amount, @Description, @TransactionDate, @TransactionType)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@TransactionDate", transactionDate);
                        command.Parameters.AddWithValue("@TransactionType", transactiontype);
                        connection.Open();
                        int i = command.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Delete successful");
                        }
                        else
                        {
                            MessageBox.Show("No rows affected");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message }");
            }
        }
        private void UpdateTransaction(int transactionID, decimal amount, string description, DateTime transactionDate, string transactionType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Transactions SET Amount = @Amount, Description= @Description, TransactionDate=@TransactionDate, TransactionType WHERE TransactionID =  @TransactionID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionID", transactionID);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@TransactionDate", transactionDate);
                        command.Parameters.AddWithValue("@TransactionType", transactionType);
                        connection.Open();
                        int i = command.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Delete successful");
                        }
                        else
                        {
                            MessageBox.Show("No rows affected");
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message }");
            }
        }
        private void DeleteTransaction(int transactionID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Transacations WHERE TransactionID = @TransactionID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionID", transactionID);
                        connection.Open();
                        int i = command.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Delete successful");
                        }
                        else
                        {
                            MessageBox.Show("No rows affected");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message }");
            }
        }
            private void button1_Click(object sender, EventArgs e)
            {
                if (decimal.TryParse(textBox1.Text, out decimal amount) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                {
                    DateTime transactionDate = dateTimePicker1.Value;
                    string description = textBox2.Text;
                    string transactionType = comboBox1.Text;

                    AddTransaction(amount, description, transactionDate, transactionType);
                    LoadTransactions();
                }
                else
                {
                    MessageBox.Show("Please fil in all fields correctly");
                }

            }

            private void button2_Click(object sender, EventArgs e)
            {
                if (dataGridView1.SelectedRows.Count > 0 && decimal.TryParse(textBox1.Text, out decimal amount) && !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                {
                    int transactionID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TransactionID"].Value);
                    DateTime transactionDate = dateTimePicker1.Value;
                    string description = textBox2.Text;
                    string transactionType = comboBox1.Text;
                    UpdateTransaction(transactionID, amount, description, transactionDate, transactionType);
                    LoadTransactions();
                }
                else
                {
                    MessageBox.Show("Please select a transaction and fil in all fields correctly");
                }
            }

            private void button3_Click(object sender, EventArgs e)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int transactionID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TransactionID"].Value);
                    DeleteTransaction(transactionID);
                    LoadTransactions();
                }
                else
                {
                    MessageBox.Show("Please select a transaction to delete");
                }
            }
            private DataTable GetTransactions()
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Transactions";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable transactionsTable = new DataTable();
                        adapter.Fill(transactionsTable);
                        return transactionsTable;
                    }
                }
            }
            private void LoadTransactions()
            {
                DataTable transactionsTable = GetTransactions();
                dataGridView1.DataSource = transactionsTable;
            }

            private void button4_Click(object sender, EventArgs e)
            {
                LoadTransactions();
            }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();
        }
    }

    
}
