using Bank.Datalayer;
using Bank.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BankApplication
{
    /// <summary>
    /// Interaction logic for BankCustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        private BankContext _context;
        private CustomerRepository _customerRepository;
        private AccountRepository _accountRepository;
        private CityRepository _cityRepository;
        private List<Customer> _customerList;

        public CustomersWindow()
        {
            // DONE: vul deze code aan zodat alle Customers in het overzicht worden getoond.
            // Zorg ook dat alle Cities in de combobox geselecteerd kunnen worden
            _context = new BankContext();
            _customerRepository = new CustomerRepository(_context);
            _accountRepository = new AccountRepository(_context);
            _cityRepository = new CityRepository(_context);

            //de customers in een lijst doen voor performantieredenen. (maar 1x database aanspreken).
            _customerList = _customerRepository.GetAll().ToList();
            InitializeComponent();

            CustomersDataGrid.ItemsSource = _customerList;
            CityComboBox.ItemsSource = _cityRepository.GetAllCities();
        }

        private void ShowAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            // DONE: Vul deze code aan zodat er naar het AccountsWindow genavigeerd wordt. 
            Window accountWindow = new AccountsWindow((Customer)CustomersDataGrid.SelectedItem, _accountRepository);
            accountWindow.Show();
        }

        private void SaveBankButton_Click(object sender, RoutedEventArgs e)
        {
            // DONE: vul deze code aan zodat de geselecteerde Customer in de database wordt toegevoegd / aangepast.
            CustomersDataGrid.CanUserAddRows = false;
            Customer customer = ((Customer)CustomersDataGrid.SelectedItem);
            if (customer.CustomerId == 0)
            {
                _customerRepository.Add(customer);
            }
            else
            {
                _customerRepository.UpdateCustomer(customer.CustomerId, customer);
            }
            _customerRepository.Commit();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            //DONE: zorg dat je een nieuwe klant in de datagrid kan toevoegen (zie Tip in opgave)
            CustomersDataGrid.CanUserAddRows = true;

        }
    }
}
