using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_AdonetCustomer
{
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-R7AR1ND;initial catalog=DbCustomer;integrated security=true");
        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select Count(*) From TblCity", sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                lblCityCount.Text = reader[0].ToString();
            }
            sqlConnection.Close();



            sqlConnection.Open();
            SqlCommand command2 = new SqlCommand("Select Count(*) From TblCustomer", sqlConnection);
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                lblCustomerCount.Text = reader2[0].ToString();
            }
            sqlConnection.Close();


            sqlConnection.Open();
            SqlCommand command3 = new SqlCommand("Select Sum(CustomerBalance) From TblCustomer", sqlConnection);
            SqlDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                lblCustomerBalance.Text = reader3[0].ToString() + " ₺";
            }
            sqlConnection.Close();
        }
    }
}
