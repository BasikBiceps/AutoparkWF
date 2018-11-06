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
    public partial class Form3 : Form
    {
        Account.User user;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(LogIn f, Account.User user)
        {
            InitializeComponent();
            label1.Text = user.FirstName;
            label2.Text = user.SecondName;
            label3.Text = user.Money.ToString();
            this.user = user;
        }

        public Form3(Registration f, Account.User user)
        {
            InitializeComponent();
            label1.Text = user.FirstName;
            label2.Text = user.SecondName;
            label3.Text = user.Money.ToString();
            this.user = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IAYC newForm = new IAYC(this, user.Car);
            newForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (user.Car.Number == "nonumber")
            {
                MessageBox.Show("You didn't have a car now.");
                return;
            }
            user.Car.IsFree = true;
            user.Car.Days = 0;
            user.Car = new Car.Car();
            MessageBox.Show("You passed a car.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IAFC newForm = new IAFC(this, user.CarManager);
            newForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrderCar newForm = new OrderCar(this, user);
            newForm.ShowDialog();
            label3.Text = user.Money.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefMon newForm = new RefMon(this, user);
            newForm.ShowDialog();
            label3.Text = user.Money.ToString();
        }
    }
}
