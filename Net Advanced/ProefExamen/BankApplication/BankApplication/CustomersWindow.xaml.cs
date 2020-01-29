using Bank.Datalayer;
using Bank.DomainClasses;
using System.Windows;

namespace BankApplication
{
    /// <summary>
    /// Interaction logic for BankCustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        public CustomersWindow()
        {
            // TODO: vul deze code aan zodat alle Customers in het overzicht worden getoond.
            // Zorg ook dat alle Cities in de combobox geselecteerd kunnen worden
            InitializeComponent();

        }

        private void ShowAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Vul deze code aan zodat er naar het AccountsWindow genavigeerd wordt. 
        }

        private void SaveBankButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: vul deze code aan zodat de geselecteerde Customer in de database wordt toegevoegd / aangepast.
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: zorg dat je een nieuwe klant in de datagrid kan toevoegen (zie Tip in opgave)
        }
    }
}
