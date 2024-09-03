using Project12_JwtToken.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project12_JwtToken
{
    public partial class FrmTokenGenerator : Form
    {
        public FrmTokenGenerator()
        {
            InitializeComponent();
        }

        private void btnCreateToken_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string email = txtEmail.Text;
            string name = txtName.Text;
            string surname = txtSurname.Text;
            TokenGenerator tokenGenerator = new TokenGenerator();
            string token = tokenGenerator.GenerateJwtToken(username, email,name,surname);
            richTextBox1.Text = token;
        }
    }
}
