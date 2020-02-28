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
    }
}
