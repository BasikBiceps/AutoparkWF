using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Account;

namespace AutoparkWFV1._0
{
    public partial class AdminWindow : Form
    {
        private Admin admin;

        public AdminWindow()
        {
            InitializeComponent();
        }

        public AdminWindow(LogIn f, Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            label3.Text = admin.FirstName;
            label4.Text = admin.SecondName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DelAccount newForm = new DelAccount(this, admin);
            newForm.ShowDialog();
            newForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DelCar newForm = new DelCar(this, admin);
            newForm.ShowDialog();
            newForm.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IAACA newform = new IAACA(this, admin);
            newform.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddCar newform = new AddCar(this, admin.CarManager);
            newform.ShowDialog();
            newform.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllAccounts newform = new AllAccounts(this, admin);
            newform.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddAccounts newform = new AddAccounts(this, admin);
            newform.ShowDialog();
            newform.Close();
        }
    }
}
