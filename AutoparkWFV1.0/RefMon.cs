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
    public partial class RefMon : Form
    {
        User user;

        public RefMon()
        {
            InitializeComponent();
        }

        public RefMon(Form3 f, User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!maskedTextBox1.MaskFull)
            {
                maskedTextBox1.Clear();
                maskedTextBox2.Clear();
                MessageBox.Show("Incorrect number of card!");
                return;
            }
            if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("Incorrect amount of money.");
                return;
            }
            user.Money += Convert.ToInt32(maskedTextBox2.Text);
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            this.Dispose();
        }
    }
}
