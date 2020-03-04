using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ado08022020
{
    public partial class Form2 : Form
    {
        Form1 parent = null;
        public Form2(Form1 p)
        {
            InitializeComponent();
            parent = p;
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (String.IsNullOrWhiteSpace(tbLogin.Text))
            {
                tbLogin.Focus();
                lbError.Text = "Enter login";
                return;
            }
            else if(String.IsNullOrWhiteSpace(tbPass.Text))
            {
                tbPass.Focus();
                lbError.Text = "Enter password";
                return;
            }
            else if(String.IsNullOrWhiteSpace(tbPassConf.Text))
            {
                tbPassConf.Focus();
                lbError.Text = "Confirm password";
                return;
            }
            else if(tbPass.Text != tbPassConf.Text)
            {
                MessageBox.Show("Password field is not equal to confirg password field");
                tbPass.Focus();
                return;
            }
            parent.AddEmployee(new Employee(tbLogin.Text, tbPass.Text.GetHashCode().ToString(),
                                            tbAddress.Text, tbPhone.Text, cbIsAdmin.Checked));
            Close();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
