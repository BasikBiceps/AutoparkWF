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
    public partial class IAACA : Form
    {
        Admin admin;

        public IAACA()
        {
            InitializeComponent();
        }

        public IAACA(AdminWindow f, Admin admin)
        {
            InitializeComponent();
            this.admin = admin;

            listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;
            listBox1.DataSource = admin.CarManager.CarList;
            listBox1.ValueMember = "Model";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Car.Car selectedCar = (Car.Car)listBox1.SelectedItem;
            if (selectedCar.IsFree)
            {
                MessageBox.Show("Model: " + selectedCar.Model + "\n" + "Number: " + selectedCar.Number + "\n" +
                "Price per one day: " + selectedCar.PricePerDay + "\n" + "Status: " + selectedCar.IsFree.ToString() +
                "\nBusy days: " + selectedCar.Days);
            }
            else
            {
                object obj = admin.AccManager.Find(selectedCar.Number);

                if (obj as Account.Account == null)
                {
                    MessageBox.Show("Model: " + selectedCar.Model + "\n" + "Number: " + selectedCar.Number + "\n" +
                    "Price per one day: " + selectedCar.PricePerDay + "\n" + "Status: " + selectedCar.IsFree.ToString() +
                    "\nBusy days: " + selectedCar.Days + "\nTenant login: error");
                }
                else
                {
                    Account.Account acc = (Account.Account)obj;
                    MessageBox.Show("Model: " + selectedCar.Model + "\n" + "Number: " + selectedCar.Number + "\n" +
                    "Price per one day: " + selectedCar.PricePerDay + "\n" + "Status: " + selectedCar.IsFree.ToString() +
                    "\nBusy days: " + selectedCar.Days + "\nTenant login: " + acc.Login);
                }
            }
        }
    }
}

