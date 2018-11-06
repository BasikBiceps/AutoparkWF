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
    public partial class IAYC : Form
    {
        public IAYC()
        {
            InitializeComponent();
        }

        public IAYC(Form3 f, Car.Car car)
        {
            InitializeComponent();
            label5.Text = car.Model;
            label6.Text = car.Number;
            label7.Text = car.IsFree.ToString();
            label8.Text = car.PricePerDay.ToString();
            label10.Text = car.Days.ToString();
        }
    }
}
