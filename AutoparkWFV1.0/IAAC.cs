using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car;
using Account;

namespace AutoparkWFV1._0
{
    public partial class IAFC : Form
    {
        CarManager cm;

        public IAFC()
        {
            InitializeComponent();
        }

        public IAFC(AdminWindow f, CarManager cm)
        {
            InitializeComponent();
            this.cm = cm;

            listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;
            listBox1.DataSource = cm.CarList;
            listBox1.ValueMember = "Model";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        public IAFC(Form3 f, CarManager cm)
        {
            InitializeComponent();
            this.cm = cm;

            listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;
            listBox1.DataSource = cm.CarList;
            listBox1.ValueMember = "Model";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Car.Car selectedCar = (Car.Car)listBox1.SelectedItem;
            MessageBox.Show("Model: " + selectedCar.Model + "\n" + "Number: " + selectedCar.Number + "\n" +
                "Price per one day: " + selectedCar.PricePerDay + "\n" + "Status: " + selectedCar.IsFree.ToString() +
                "\nBusy days: " + selectedCar.Days);
        }
    }
}
