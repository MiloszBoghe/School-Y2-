using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.UI
{
    public partial class TransferWindow : Window
    {
        private Account _fromAccount;
        private IList<Account> _accounts;
        private IAccountRepository _accountRepository;

        public TransferWindow(Account fromAccount,
            IEnumerable<Account> allAccountsOfCustomer,
            IAccountRepository accountRepository)
        {
            InitializeComponent();

            _fromAccount = fromAccount;
            _accounts = allAccountsOfCustomer.ToList();
            _accountRepository = accountRepository;

            FromAccountTextBlock.Text = _fromAccount.AccountNumber;
            ToAccountComboBox.ItemsSource = _accounts;

        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessageTextBlock.Text = "";
            Account toAccount = (Account)ToAccountComboBox.SelectedItem;
            decimal amount = Convert.ToDecimal(AmountTextBox.Text);
            int fromId = Convert.ToInt32(_fromAccount.Id);
            int toId = Convert.ToInt32(toAccount.Id);
            if (amount >= _fromAccount.Balance)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "The maximum amount is " + _fromAccount.Balance;
            }
            else if (amount <= 0)
            {
                ErrorMessageTextBlock.Text = "Amount must be between 0 and "+_fromAccount.Balance;
            }
            else
            {
                if (toAccount != null)
                {
                    _accountRepository.TransferMoney(fromId, toId, amount);
                }
            }
        }
    }
}
