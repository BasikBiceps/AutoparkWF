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
    public partial class OrderCar : Form
    {
        User user;

        public OrderCar()
        {
            InitializeComponent();
        }

        public OrderCar(Form3 f, User user )
        {
            InitializeComponent();
            this.user = user;

            listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;
            listBox1.DataSource = user.CarManager.CarList;
            listBox1.ValueMember = "Model";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Car.Car selectedCar = (Car.Car)listBox1.SelectedItem;
            ConfirmationOrder newForm = new ConfirmationOrder(this, user, selectedCar);
            newForm.ShowDialog();
        }
    }
}
