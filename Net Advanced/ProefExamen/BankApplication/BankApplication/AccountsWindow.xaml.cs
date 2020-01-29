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
        public AccountsWindow(Customer customer)
        {
            // TODO: vul de code aan zodat alle rekeningen van de 
            // geselecteerde klant in het overzicht worden getoond
            InitializeComponent();
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: zorg dat je een nieuwe rekening in de datagrid kan toevoegen (zie Tip in opgave)        
        }

        private void SaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: vul deze code aan zodat de geselecteerde rekening
            // in de database wordt toegevoegd / aangepast.
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: vul deze code aan zodat naar het TransferWindow wordt genavigeerd. 
            // Wanneer het TransferWindow wordt gesloten wordt gesloten, moet de transfer doorgevoerd worden in de database
            // Zorg er ook voor dat de aanpassingen aan de balansen ook automatisch worden aangepast in het AccountsWindow
            // (gebruik hiervoor de INotitfyPropertyChanged interface)
        }
    }
}
