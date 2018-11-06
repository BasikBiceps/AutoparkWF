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
    public partial class DelCar : Form
    {
        Admin admin;

        public DelCar()
        {
            InitializeComponent();
        }

        public DelCar(AdminWindow f, Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            this.admin.CarManager.Removed += Messenger;
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
                label2.Text = temp.Messege;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object obj = admin.AccManager.Find(maskedTextBox1.Text.ToUpper());

            if (obj != null)
            {
                User user = (User)obj;
                user.Car = new Car.Car();
            }
            try
            {
                admin.removeCar(maskedTextBox1.Text.ToUpper());
            }
            catch (CarIsNotFoundException) { }
            finally
            {
                maskedTextBox1.Clear();
            }
        }
    }
}
