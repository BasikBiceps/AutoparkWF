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
    public partial class Registration : Form
    {
        AccountManager accManager;

        public Registration()
        {
            InitializeComponent();
        }

        public Registration(StartPage f)
        {
            InitializeComponent();
            accManager = new AccountManager("AccountManager.dat");
            accManager.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length >= 25 || maskedTextBox2.Text.Length >= 25 || maskedTextBox3.Text.Length >= 15 || maskedTextBox4.Text.Length >= 15)
            {
                label5.Text = "Incorrect length.";
                return;
            }

            if (!maskedTextBox1.Text.All<char>(Account.Account.isCorrectSymbol) || !maskedTextBox2.Text.All<char>(Account.Account.isCorrectSymbol))
            {
                label5.Text = "Incorrect name.";
                return;
            }

            if (maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || maskedTextBox4.Text == "")
            {
                label5.Text = "Not all fields are filled";
                return;
            }

            if (maskedTextBox3.Text.Contains(" ") || maskedTextBox4.Text.Contains(" "))
            {
                label5.Text = "Incorrect login.";
                return;
            }

            if (accManager.findAccount(maskedTextBox3.Text) != null)
            {
                label5.Text = "Account with this login already exists";
                return;
            }
            object obj = accManager.Add(new User(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, maskedTextBox4.Text));
            if (obj == null) {
                label5.Text = "Account wasn't create.";
                return;
            }

            User tempUser = (User)obj;

            Form3 newForm = new Form3(this, tempUser);
            this.Visible = false;
            newForm.ShowDialog();
            this.Close();
            accManager.SaveInformation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
