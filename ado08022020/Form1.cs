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
    public partial class Form1 : Form
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public bool showAdmin = true;
        public Form1()
        {
            InitializeComponent();
        }
        private void BtAdd_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void CbShowAdmin_CheckedChanged(object sender, EventArgs e)
        {
            showAdmin = cbShowAdmin.Checked;
            RefreshList();
        }
        public void AddEmployee(Employee tmp)
        {
            Employees.Add(tmp);
            RefreshList();
        }
        private void RefreshList()
        {
            listBox1.DataSource = null;
            if (showAdmin)
                listBox1.DataSource = Employees;
            else
                listBox1.DataSource = Array.FindAll<Employee>(Employees.ToArray(), x => x.IsAdmin == showAdmin);
        }
        private void BtDelete_Click(object sender, EventArgs e)
        {
            Employees.Remove(listBox1.SelectedItem as Employee);
            RefreshList();
        }
    }
    public class Employee
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        public Employee(string login, string pass, string address = "", string phone = "", bool isAdmin = false)
        {
            Login = login;
            Pass = pass;
            Address = address;
            Phone = phone;
            IsAdmin = isAdmin;
        }
        public override string ToString()
        {
            return $"{(IsAdmin ? "[adm] " : "")}{Login}";
        }
    }
}
