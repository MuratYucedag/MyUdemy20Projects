using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project10_PostgreSQLToDoListApp
{
    public partial class FrmToDoListApp : Form
    {
        public FrmToDoListApp()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localHost;port=5432;Database=DbProject10ToDoApp1;user ID=postgres;Password=1234";
        private void FrmToDoListApp_Load(object sender, EventArgs e)
        {
            // NpgsqlConnection connection = new NpgsqlConnection("");

           var connection=new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select * From mytable1";
            var command=new NpgsqlCommand(query, connection);
            var adapter=new NpgsqlDataAdapter (command);    
            DataTable dataSet = new DataTable();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet;
            connection.Close();
        }
    }
}
