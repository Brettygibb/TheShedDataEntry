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
using static System.Net.Mime.MediaTypeNames;

namespace TheShedDataEntry
{
    public partial class frmAddModify : Form
    {
        public Bikes bikes;
        public bool addBike;
        public frmAddModify()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (addBike)
            {
                bikes = new Bikes();
            }
            bikes.Manufacturer= txtManufacturer.Text;
            bikes.Model= txtModel.Text;
            bikes.Type= txtType.Text;
            bikes.Price= Convert.ToDecimal(txtPrice.Text);
            bikes.BikeImage = txtPictureurl.Text;
            try
            {
                if(addBike)
                {
                    bikes.id = BikeDB.AddBike(bikes);
                    MessageBox.Show("Bike Successfully Added");
                }
                else
                {
                    int numRows = BikeDB.UpdateBikes(bikes);

                    if (numRows > 0)
                    {
                        MessageBox.Show(bikes.id + "Was successfully updated");
                    }
                    else
                    {
                        MessageBox.Show("Unable to Update Current Bike");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.DialogResult= DialogResult.OK;
        }

        private void frmAddModify_Load(object sender, EventArgs e)
        {
            if (bikes == null)
            {
                addBike = true;
            }
            else
            {
                addBike = false;
                txtBikeID.Text = bikes.id.ToString();
                txtManufacturer.Text = bikes.Manufacturer;
                txtModel.Text = bikes.Model;
                txtType.Text = bikes.Type;
                txtPrice.Text = bikes.Price.ToString();
                txtBikeID.ReadOnly = true;
                txtPictureurl.Text = bikes.BikeImage;
            }
        }
    }
}
