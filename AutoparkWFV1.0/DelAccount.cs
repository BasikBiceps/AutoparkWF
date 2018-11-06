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
    public partial class DelAccount : Form
    {
        Admin admin;

        public DelAccount()
        {
            InitializeComponent();
        }

        public DelAccount(AdminWindow f, Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            this.admin.AccManager.Removed += Messenger;
        }

        private void Messenger(object sender, EventArgs e)
        {
            AccountManagerEventArgs temp = e as AccountManagerEventArgs;
            if (temp == null)
            {
                return;
            }
            else
            {
                label2.Text = temp.Messege;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (admin.AccManager.findAccount(maskedTextBox1.Text) as User != null)
            {
                User tempuser = (User)admin.AccManager.findAccount(maskedTextBox1.Text);

                tempuser.Car.Days = 0;
                tempuser.Car.IsFree = true;
            }
            admin.removeAccount(maskedTextBox1.Text);
            maskedTextBox1.Clear();
        }
    }
}
