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
    public partial class LogIn : Form
    {
        AccountManager accManager;

        public LogIn()
        {
            InitializeComponent();
        }

        public LogIn(StartPage f)
        {
            InitializeComponent();
            accManager = new AccountManager("AccountManager.dat");
            accManager.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Account.Account account = accManager.findAccount(maskedTextBox1.Text);
            if (account == null)
            {
                label3.Text = "There is no account with such login!";
                maskedTextBox1.Clear();
                maskedTextBox2.Clear();
                return;
            }
            if (account.Password.Equals(maskedTextBox2.Text))
            {
                if (account as Account.User != null)
                {
                    Account.User user = account as Account.User;
                    Form3 nF = new Form3(this, user);
                    this.Visible = false;
                    nF.ShowDialog();
                    this.Dispose();
                }
                if (account as Account.Admin != null)
                {
                    Account.Admin admin = account as Account.Admin;
                    admin.AccManager = accManager;
                    AdminWindow nF = new AdminWindow(this, admin);
                    this.Visible = false;
                    nF.ShowDialog();
                    this.Dispose();
                }
                accManager.SaveInformation();
            }
            else
            {
                label3.Text = "Incorrect password!";
                maskedTextBox1.Clear();
                maskedTextBox2.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
