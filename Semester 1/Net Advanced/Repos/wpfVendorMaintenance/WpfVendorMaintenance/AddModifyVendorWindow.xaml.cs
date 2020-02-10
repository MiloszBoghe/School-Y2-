using System;
using System.Collections.Generic;
using System.Windows;
using PayablesData;

namespace WpfVendorMaintenance
{
    public partial class AddModifyVendorWindow : Window
    {
        private readonly bool _isNewVendor;
        private IList<State> _allStates;
        private readonly Vendor _vendor;

        public AddModifyVendorWindow(Vendor vendor, bool isNewVendor)
        {
            InitializeComponent();

            _vendor = vendor;
            _isNewVendor = isNewVendor;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBoxes();

            if (_isNewVendor)
            {
                VendorTermsComboBox.SelectedIndex = -1;
                VendorAccountComboBox.SelectedIndex = -1;
                VendorStateComboBox.SelectedIndex = -1;
            }
            DataContext = _vendor;
        }

        private void LoadComboBoxes()
        {
            try
            {
                IList<GLAccount> allAccounts = GLAccountRepository.GetAll();
                IList<Terms> allTerms = TermsRepository.GetAll();
                _allStates = StateRepository.GetAll();

                VendorTermsComboBox.ItemsSource = allTerms;
                VendorAccountComboBox.ItemsSource = allAccounts;
                VendorStateComboBox.ItemsSource = _allStates;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                if (_isNewVendor)
                {
                    AddNewVendor();
                }
                else
                {
                    UpdateExistingVendor();
                }
            }
            else
            {
                MessageBox.Show("You have entered invalid data ...");
            }
        }

        private void AddNewVendor()
        {
            try
            {
                _vendor.VendorId = VendorRepository.Add(_vendor);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void UpdateExistingVendor()
        {
            try
            {
                if (!VendorRepository.Update(_vendor))
                {
                    MessageBox.Show("Another user has updated or deleted that vendor.", "DataBase Error");
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool IsValidData()
        {
            if (!Validator.HasValue(VendorNameTextBox)) return false;
            if (!Validator.HasValue(VendorAddressTextBox)) return false;
            if (!Validator.HasValue(VendorCityTextBox)) return false;
            if (!Validator.HasValue(VendorZipTextBox)) return false;
            if (!Validator.IsInt32(VendorZipTextBox)) return false;
            if (!Validator.HasValue(VendorStateComboBox)) return false;
            if (!Validator.HasValue(VendorTermsComboBox)) return false;
            if (!Validator.HasValue(VendorAccountComboBox)) return false;

            //Zipcode must be within range of selected state
            int firstZip = _allStates[VendorStateComboBox.SelectedIndex].FirstZipCode;
            int lastZip = _allStates[VendorStateComboBox.SelectedIndex].LastZipCode;
            if (!Validator.IsStateZipCode(VendorZipTextBox, firstZip, lastZip))
            {
                MessageBox.Show("Zipcode must be within this range: " + firstZip + " to " + lastZip + ".");
                return false;
            }

            //phone must be valid if filled in
            return VendorPhoneTextBox.Text == "" || Validator.IsPhoneNumber(VendorPhoneTextBox);
        }
    }
}
