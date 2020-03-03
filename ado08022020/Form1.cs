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
            listBox1.DataSource = Array.FindAll<Employee>(Employees.ToArray(), x =>x.IsAdmin == showAdmin);
        }
        private void BtAdd_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }
    }
    public class Employee
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        public override string ToString()
        {
            return Login;
        }
    }
}
