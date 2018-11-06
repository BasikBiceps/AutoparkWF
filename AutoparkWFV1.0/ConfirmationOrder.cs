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
    public partial class ConfirmationOrder : Form
    {
        User user;
        Car.Car car;

        public ConfirmationOrder()
        {
            InitializeComponent();
        }

        public ConfirmationOrder(OrderCar f, User user, Car.Car car)
        {
            InitializeComponent();
            this.user = user;
            this.car = car;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "") {
                MessageBox.Show("Incorrect number of days.");
                return;
            }

            if (!car.IsFree) {
                MessageBox.Show("Car isn't free.");
                return;
            }

            int price = car.PricePerDay * Convert.ToInt32(maskedTextBox1.Text);

            if (price > user.Money) {
                MessageBox.Show("Not enough money.");
                return;
            }

            user.Money = user.Money - price;
            car.Days = Convert.ToInt32(maskedTextBox1.Text);

            user.Car.Days = 0;
            user.Car.IsFree = true;

            user.Car = car;
            MessageBox.Show("You ordered a car. Thanks you!");
            this.Close();
        }
    }
}
