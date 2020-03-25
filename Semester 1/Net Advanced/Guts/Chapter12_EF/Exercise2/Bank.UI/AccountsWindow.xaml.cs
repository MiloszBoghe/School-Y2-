using System;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;
using System.Windows;
using Bank.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Bank.Business;

namespace Bank.UI
{
    public partial class AccountsWindow : Window
    {
        private Customer _customer;
        private IAccountRepository _accountRepository;
        private IAccountValidator _accountValidator;
        private IWindowDialogService _windowDialogService;
        private IList<Account> _accounts;

        public AccountsWindow(Customer customer,
            IAccountRepository accountRepository,
            IAccountValidator accountValidator,
            IWindowDialogService windowDialogService)
        {
            InitializeComponent();
            _customer = customer;
            _accounts = customer.Accounts.ToList();
            _accountRepository = accountRepository;
            _accountValidator = accountValidator;
            _windowDialogService = windowDialogService;

            AccountsDataGrid.ItemsSource = _accounts;
            Title = customer.FirstName + " " + customer.Name;
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = (Account)AccountsDataGrid.SelectedItem;
            _accountRepository.Add(account);
        }

        private void SaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            Account account = (Account)AccountsDataGrid.SelectedItem;
            ValidatorResult result = _accountValidator.IsValid(account);
            if (result.IsValid)
            {
                if (_accounts.Contains(account))
                {
                    _accountRepository.Update(account);
                }
                else
                {
                    if (account != null)
                    {
                        _accountRepository.Add(account);
                        AccountsDataGrid.CanUserAddRows = false;
                    }
                    else
                    {
                        ErrorTextBlock.Text = "No account selected!";
                    }
                }
            }
            else
            {
                ErrorTextBlock.Text = result.Message;
            }
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = (Account)AccountsDataGrid.SelectedItem;
            if (account != null) _windowDialogService.ShowTransferDialog(account,_accounts);
        }
    }
}
