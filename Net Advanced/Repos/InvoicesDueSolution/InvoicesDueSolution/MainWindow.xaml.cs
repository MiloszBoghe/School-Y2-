using InvoicesDueSolution.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoicesDueSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                IList<Invoice> invoiceList = new List<Invoice>();
                invoiceList = InvoiceRepository.GetInvoiceDue();
                if (invoiceList.Count > 0)
                {
                    InvoicesListView.ItemsSource = invoiceList;
                }
                else
                {
                    MessageBox.Show("All invoices are paid in full", "No Balance Due");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
    }
}
