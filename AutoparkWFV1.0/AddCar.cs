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
    public partial class AddCar : Form
    {
        CarManager cm;

        public AddCar()
        {
            InitializeComponent();
        }

        public AddCar(AdminWindow f, CarManager cm)
        {
            this.cm = cm;
            cm.Added += Messenger;
            InitializeComponent();
        }

        private void Messenger(object sender, EventArgs e)
        {
            CarManagerEventArgs temp = e as CarManagerEventArgs;
            if (temp == null)
            {
                return;
            }
            else
            {
                if (temp.Car != null)
                {
                    label4.Text = "Car was added.";
                }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "" || !maskedTextBox2.MaskFull || maskedTextBox3.Text == "")
            {
                maskedTextBox1.Clear();
                maskedTextBox2.Clear();
                maskedTextBox3.Clear();
                label4.Text = "Not all fields are filled!";
            }
            else
            {
                try
                {
                    object obj = cm.Add(new Car.Car(maskedTextBox2.Text.ToUpper(), maskedTextBox1.Text, Convert.ToInt32(maskedTextBox3.Text)));
                    if (obj == null)
                    {
                        label4.Text = "Car didn't add.";
                        return;
                    }
                }
                catch (CarAlreadyExistsException) {
                    label4.Text = "Car didn't add.";
                }
                finally
                {
                    maskedTextBox1.Clear();
                    maskedTextBox2.Clear();
                    maskedTextBox3.Clear();
                }
            }
        }
    }
}
