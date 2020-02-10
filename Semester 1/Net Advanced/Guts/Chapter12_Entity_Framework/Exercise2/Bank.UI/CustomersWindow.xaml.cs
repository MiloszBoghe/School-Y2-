using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Bank.Business;
using Bank.Business.Interfaces;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.UI
{
    public partial class CustomersWindow : Window
    {
        private ICustomerRepository _customerRepository;
        private ICityRepository _cityRepository;
        private ICustomerValidator _customerValidator;
        private IWindowDialogService _windowDialogService;
        private IList<Customer> _customers;

        public CustomersWindow(ICustomerRepository customerRepository,
            ICustomerValidator customerValidator,
            ICityRepository cityRepository,
            IWindowDialogService windowDialogService)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _cityRepository = cityRepository;
            _customerValidator = customerValidator;
            _windowDialogService = windowDialogService;
            _customers = customerRepository.GetAllWithAccounts();


            CustomersDataGrid.ItemsSource = _customers;
            CityComboBoxColumn.ItemsSource = _cityRepository.GetAll();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)CustomersDataGrid.SelectedItem;
            _customerRepository.Add(customer);
        }

        private void SaveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            Customer customer = (Customer)CustomersDataGrid.SelectedItem;
            ValidatorResult result = _customerValidator.IsValid(customer);
            if (result.IsValid)
            {
                if (_customers.Contains(customer))
                {
                    _customerRepository.Update(customer);
                }
                else
                {
                    if (customer != null)
                    {
                        _customerRepository.Add(customer);
                        CustomersDataGrid.CanUserAddRows = false;
                    }
                    else
                    {
                        ErrorTextBlock.Text = "No customer selected!";
                    }
                }
            }
            else
            {
                ErrorTextBlock.Text = result.Message;
            }
        }

        private void ShowAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)CustomersDataGrid.SelectedItem;
            if (customer != null) _windowDialogService.ShowAccountDialogForCustomer(customer);
        }
    }
}
