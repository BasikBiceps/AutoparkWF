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
using Car;

namespace AutoparkWFV1._0
{
    public partial class AllAccounts : Form
    {
        Admin admin;

        public AllAccounts()
        {
            InitializeComponent();
        }

        public AllAccounts(AdminWindow f, Admin admin)
        {
            InitializeComponent();
            this.admin = admin;

            listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;
            listBox1.DataSource = admin.AccManager.AccountList;
            listBox1.ValueMember = "Login";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem as User != null)
            {
                User selectedUser = (User)listBox1.SelectedItem;

                MessageBox.Show("Firstname: " + selectedUser.FirstName + "\n" + "Lastname: " + selectedUser.SecondName + "\n" +
                "Money: " + selectedUser.Money + "\n" + "Number of car: " + selectedUser.Car.Number + "\nBusy days: " + selectedUser.Car.Days);
            }
            else if (listBox1.SelectedItem as Admin != null)
            {
                Admin selectedUser = (Admin)listBox1.SelectedItem;

                MessageBox.Show("Firstname: " + selectedUser.FirstName + "\n" + "Lastname: " + selectedUser.SecondName);
            }
        }          
    }
}
