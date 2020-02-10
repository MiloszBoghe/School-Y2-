using Bank.Business;
using Bank.Datalayer;
using Bank.DomainClasses;
using Bank.DomainClasses.Enums;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BankApplication
{
    /// <summary>
    /// Interaction logic for AccountsWindow.xaml
    /// </summary>
    public partial class AccountsWindow : Window
    {
        private AccountRepository _accountRepository;
        private Customer _customer;
        public AccountsWindow(Customer customer, AccountRepository accountRepository)
        {
            // DONE: vul de code aan zodat alle rekeningen van de 
            // geselecteerde klant in het overzicht worden getoond
            _accountRepository = accountRepository;
            _customer = customer;
            InitializeComponent();
            AccountsDataGrid.ItemsSource = customer.Accounts;
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            //DONE: zorg dat je een nieuwe rekening in de datagrid kan toevoegen (zie Tip in opgave)
            AccountsDataGrid.CanUserAddRows = true;
        }

        private void SaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            // DONE: vul deze code aan zodat de geselecteerde rekening
            // in de database wordt toegevoegd / aangepast.
            AccountsDataGrid.CanUserAddRows = false;
            foreach (Account acc in _customer.Accounts)
            {
                if (acc.Id <= 0)
                {
                    acc.Id = 0; 
                    _accountRepository.AddAccountForCustomer(acc, _customer.CustomerId);
                }
                else
                {
                    _accountRepository.UpdateAccount(acc.Id, acc);
                }
            }

            _accountRepository.Commit();

        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: vul deze code aan zodat naar het TransferWindow wordt genavigeerd. 
            // Wanneer het TransferWindow wordt gesloten, moet de transfer doorgevoerd worden in de database
            // Zorg er ook voor dat de aanpassingen aan de balansen ook automatisch worden aangepast in het AccountsWindow
            // (gebruik hiervoor de INotitfyPropertyChanged interface)
            int accId = ((Account)AccountsDataGrid.SelectedItem).Id;
            Window transferWindow = new TransferWindow(_customer.Accounts, accId);
            transferWindow.Show();

        }
    }
}
