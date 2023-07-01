using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheShedBL;
using TheShedDB;

namespace TheShedDataEntry
{
    public partial class Form1 : Form
    {
        private Bikes B;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            int bikeId = -1;
            if(int.TryParse(txtBikeID.Text, out bikeId)&&bikeId>0) 
            {
                try
                {
                    B = BikeDB.GetBikeByID(bikeId);
                    if(B != null)
                    {
                        lblManufacturer.Text = B.Manufacturer;
                        lblModel.Text = B.Model;
                        lblType.Text = B.Type;
                        lblPrice.Text = B.Price.ToString();
                        pbBike.Image = Image.FromFile(B.BikeImage);
                    }
                }
                catch 
                {
                    MessageBox.Show("No Bike With That ID Was Found");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddModify frmAddModify = new frmAddModify();
            frmAddModify.bikes = null;
            frmAddModify.ShowDialog();

            if(frmAddModify.DialogResult == DialogResult.OK)
            {
                Bikes Bike = frmAddModify.bikes;
                txtBikeID.Text= Bike.id.ToString();
                lblManufacturer.Text = Bike.Manufacturer;
                lblModel.Text = Bike.Model;
                lblType.Text = Bike.Type;
                lblPrice.Text= Bike.Price.ToString();
                pbBike.Image = Image.FromFile(Bike.BikeImage);
            }
        }
    }
}
