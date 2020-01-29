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
        public TransferWindow(IList<Account> allCustomerAccounts, int fromAccountId)
        {
            // TODO: Geef het rekeningnummer van de opdrachtgevende rekening weer.
            // Zorg ervoor dat alle andere rekeningen van de klant in de combobox worden getoond.
            InitializeComponent();
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Vul deze code aan zodat het bedrag van de ene rekening naar de andere wordt overgezet.
            // Maak hierbij gebruik van de AccountManager class.
            // Zorg dat bij een succesvolle transfer het AccountWindow op de hoogte gebracht wordt (Tip: DialogResult) 
            // zodat daar de transfer kan doorgevoerd worden in de database. 
        }
    }
}
