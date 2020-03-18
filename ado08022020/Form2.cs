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
        Employee employee = null;
        public Form2(Form1 p, Employee employee = null)
        {
            InitializeComponent();
            parent = p;
            this.employee = employee;
            if (employee != null)
            {
                btAdd.Text = "Save";
                tbLogin.Text = employee.Login;
                tbPass.Text = employee.Pass;
                tbPassConf.Text = employee.Pass;
                tbAddress.Text = employee.Address;
                tbPhone.Text = employee.Phone;
                cbIsAdmin.Checked = employee.IsAdmin;
            }
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
            else if (String.IsNullOrWhiteSpace(tbPass.Text))
            {
                tbPass.Focus();
                lbError.Text = "Enter password";
                return;
            }
            else if (String.IsNullOrWhiteSpace(tbPassConf.Text))
            {
                tbPassConf.Focus();
                lbError.Text = "Confirm password";
                return;
            }
            else if (tbPass.Text != tbPassConf.Text)
            {
                MessageBox.Show("Password field is not equal to confirg password field");
                tbPass.Focus();
                return;
            }
            Employee tmp = new Employee(tbLogin.Text, tbPass.Text.GetHashCode().ToString(),
                                              tbAddress.Text, tbPhone.Text, cbIsAdmin.Checked);
            bool isContains = parent.Employees.Find(x => x.Login == tmp.Login) != null;

            if (!isContains && (employee == null))
            {
                parent.AddEmployee(tmp);
                Close();
            }
            else if (!isContains || (employee != null && isContains && tbLogin.Text == employee.Login))
            {
                DataRow row = parent.FindRow(employee);
                row[1] = tmp.Login;
                row[2] = tmp.Pass;
                row[3] = tmp.Address;
                row[4] = tmp.Phone;
                row[5] = tmp.IsAdmin;
                row.AcceptChanges();
                parent.Employees[parent.listBox1.SelectedIndex] = new Employee(
                    tbLogin.Text, 
                    (tbPass.Text == employee.Pass? tbPass.Text: tbPass.Text.GetHashCode().ToString()),
                    tbAddress.Text, 
                    tbPhone.Text, 
                    cbIsAdmin.Checked);
                parent.RefreshList();
                Close();
            }
            else
                lbError.Text = "Employee already exists";

        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
