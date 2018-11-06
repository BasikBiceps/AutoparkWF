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
    public partial class AddAccounts : Form
    {
        Admin admin;

        public AddAccounts()
        {
            InitializeComponent();
        }

        public AddAccounts(AdminWindow f, Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            this.admin.AccManager.Added += Messenger;
        }

        private void Messenger(object sender, EventArgs e) {
            AccountManagerEventArgs temp = e as AccountManagerEventArgs;
            if (temp.Account != null)
            {
                MessageBox.Show("Account was added.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || maskedTextBox4.Text == "")
            {
                MessageBox.Show("Not all fields are filled!");
            }
            else
            {
                if (radioButton1.Checked)
                {
                    object obj = admin.AccManager.Add(new Admin(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text, admin.AccManager));

                    if (obj == null) {
                        MessageBox.Show("Account wasn't added.");
                    }
                }
                else if (radioButton2.Checked)
                {
                    object obj = admin.AccManager.Add(new User(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text));
                    if (obj == null)
                    {
                        MessageBox.Show("Account wasn't added.");
                    }
                }
            }
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
        }
    }
}
