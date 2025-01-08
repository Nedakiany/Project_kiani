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


namespace WindowsFormsApp19
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string query = "INSERT INTO Users (Username , Password )" + " VALUES ('" + username + "' , '" + password + "')";
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\WindowsFormsApp19\WindowsFormsApp19\Database1.mdf;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                int i = command.ExecuteNonQuery();
                if (i >= 0)
                {
                    MessageBox.Show("ورود موفقیت آمیز بود");
                    textBox1.Text = textBox2.Text = "";
                }
                else
                    MessageBox.Show(" نام کاربری یا رمز عبور اشتباه است");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }
    }
}
