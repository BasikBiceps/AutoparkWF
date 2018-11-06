using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoparkWFV1._0
{
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogIn newForm = new LogIn(this);
            this.Visible = false;
            newForm.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration newForm = new Registration(this);
            this.Visible = false;
            newForm.ShowDialog();
            this.Visible = true;
        }
    }
}
