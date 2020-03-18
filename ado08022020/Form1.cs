using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ado08022020
{
    public partial class Form1 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
        string dataProvider = ConfigurationManager.AppSettings["provider"];
        DataSet dataSet;
        DbDataAdapter adapter;
        DbCommandBuilder builder;
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public bool showAdmin = true;
        public Form1()
        {
            InitializeComponent();
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);
            adapter = factory.CreateDataAdapter();
            dataSet = new DataSet();
            DbCommand command = factory.CreateCommand();
            DbConnection dbConnection = factory.CreateConnection();
            command.Connection = dbConnection;
            command.Connection.ConnectionString = connectionString;
            command.CommandText = "Select * from Emp";
            builder = factory.CreateCommandBuilder();
            adapter.SelectCommand = command;
            builder.DataAdapter = adapter;
            adapter.Fill(dataSet);
            foreach (DataRow item in dataSet.Tables[0].Rows)
            {
                Employees.Add(ConvertToEmployee(item));
            }
            RefreshList();
            Debug.WriteLine(builder.GetDeleteCommand().CommandText);
            Debug.WriteLine(builder.GetInsertCommand().CommandText);
            Debug.WriteLine(builder.GetUpdateCommand().CommandText);
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
            dataSet.Tables[0].Rows.Add(ConvertToRow(tmp));
            RefreshList();
        }
        public void RefreshList()
        {
            listBox1.DataSource = null;
            if (showAdmin)
                listBox1.DataSource = Employees;
            else
                listBox1.DataSource = Array.FindAll<Employee>(Employees.ToArray(), x => x.IsAdmin == showAdmin);
            if (listBox1.SelectedIndex == -1)
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void BtDelete_Click(object sender, EventArgs e)
        {
            Employee tmp = listBox1.SelectedItem as Employee;
            if (tmp != null && FindRow(tmp) != null)
                FindRow(tmp).Delete();
            Employees.Remove(tmp);
            dataSet.AcceptChanges();
            RefreshList();
        }
        public DataRow FindRow(Employee tmp)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (row[1].ToString() == tmp.Login)
                    return row;
            }
            return null;
        }
        public DataRow ConvertToRow(Employee tmp)
        {
            DataTable dt = dataSet.Tables[0];
            DataRow row = dt.NewRow();
            row[1] = tmp.Login;
            row[2] = tmp.Pass;
            row[3] = tmp.Address;
            row[4] = tmp.Phone;
            row[5] = tmp.IsAdmin;
            return row;
        }
        private Employee ConvertToEmployee(DataRow item)
        {
            return new Employee(
                    item["Login"].ToString(),
                    item["Password"].ToString(),
                    item["Address"].ToString(),
                    item["Phone"].ToString(),
                    (bool)item["isAdmin"]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataSet.AcceptChanges();
            adapter.Update(dataSet);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataSet.AcceptChanges();
            adapter.Update(dataSet);
        }

        private void ListBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            Form2 form2 = new Form2(this, listBox1.SelectedItem as Employee);
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
