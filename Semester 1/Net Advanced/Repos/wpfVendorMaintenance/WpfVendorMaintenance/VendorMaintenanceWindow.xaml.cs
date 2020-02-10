using System;
using System.Windows;
using PayablesData;

namespace WpfVendorMaintenance
{
    public partial class VendorMaintenanceWindow : Window
    {
        private Vendor _vendor;

        public VendorMaintenanceWindow()
        {
            InitializeComponent();
        }

        private void GetVendorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.HasValue(VendorIdTextBox) && Validator.IsInt32(VendorIdTextBox))
            {
                GetVendor(Convert.ToInt32(VendorIdTextBox.Text));
            }
            else
            {
                MessageBox.Show(" VendorID is a required field or must be an integer value.");
            }
        }

        private void GetVendor(int vendorId)
        {
            try
            {
                _vendor = VendorRepository.GetById(vendorId);
                if (_vendor == null)
                {
                    MessageBox.Show("No vendor found with this ID. " +
                                    "Please try again.", "Vendor Not Found");
                }
                else
                {
                    DisplayVendor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void DisplayVendor()
        {
            DataContext = _vendor;
            ModifyButton.IsEnabled = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _vendor = new Vendor();
            DataContext = _vendor;

            var addModifyWindow = new AddModifyVendorWindow(_vendor, true);

            addModifyWindow.ShowDialog();

            if (addModifyWindow.DialogResult.HasValue && addModifyWindow.DialogResult.Value == true)
            {
                DisplayVendor();
                MessageBox.Show("Vendor succesfully inserted...");
            }
            else
            {
                MessageBox.Show("User clicked Cancel");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            var addModifyWindow = new AddModifyVendorWindow(_vendor, false);

            addModifyWindow.ShowDialog();
            if (addModifyWindow.DialogResult.HasValue && addModifyWindow.DialogResult.Value)
            {
                //DisplayVendor();
                MessageBox.Show("Vendor succesfully updated ...");
            }
            else
            {
                ClearControls();
                GetVendor(_vendor.VendorId);
            }
        }

        private void ClearControls()
        {
            ModifyButton.IsEnabled = false;
            VendorAddress2TextBox.Text = "";
            VendorAddressTextBox.Text = "";
            VendorNameTextBox.Text = "";
            VendorStateTextBox.Text = "";
            VendorCityTextBox.Text = "";
        }
    }
}
