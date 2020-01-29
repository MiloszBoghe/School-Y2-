using Bank.Business;
using Bank.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BankApplication
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        Account _fromAccount;
        public TransferWindow(IList<Account> allCustomerAccounts, int fromAccountId)
        {
            // DONE: Geef het rekeningnummer van de opdrachtgevende rekening weer.
            // Zorg ervoor dat alle andere rekeningen van de klant in de combobox worden getoond.
            InitializeComponent();
            _fromAccount = allCustomerAccounts.Where(a => a.Id == fromAccountId).FirstOrDefault();
            FromAccountTextBlock.DataContext = _fromAccount;
            ToAccountComboBox.ItemsSource = allCustomerAccounts.Where(a => a.Id != fromAccountId);
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Vul deze code aan zodat het bedrag van de ene rekening naar de andere wordt overgezet.
            // Maak hierbij gebruik van de AccountManager class.
            // Zorg dat bij een succesvolle transfer het AccountWindow op de hoogte gebracht wordt (Tip: DialogResult) 
            // zodat daar de transfer kan doorgevoerd worden in de database. 
            Account toAccount = (Account)ToAccountComboBox.SelectedItem;
            decimal amount = Convert.ToDecimal(AmountTextBox.Text);

            AccountManager accountManager = new AccountManager();
            accountManager.TransferMoney(_fromAccount, toAccount, amount);

        }
    }
}
