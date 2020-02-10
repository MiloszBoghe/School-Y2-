using InvoiceQueries.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace InvoiceQueries
{
    public partial class MainWindow : Window
    {
        private IList<Invoice> _allInvoices;
        private IList<Invoice> AllInvoices
        {
            get
            {
                if (_allInvoices == null)
                {
                    _allInvoices = InvoiceRepository.GetAll();
                }
                return _allInvoices;
            }
        }

        private IList<Vendor> _allVendors;
        private IList<Vendor> AllVendors
        {
            get
            {
                if (_allVendors == null)
                {
                    _allVendors = VendorRepository.GetAll();
                }
                return _allVendors;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowInvoicesSumButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Define the query expression
            //TODO: write Linq query
            var invoiceQuery = from invoice in AllInvoices
                                   //where invoice.InvoiceTotal > 1000
                               select invoice;

            //Execute the query
            decimal sum = 0;
            foreach (var invoice in invoiceQuery) //triggers query execution
            {
                sum += invoice.InvoiceTotal;
            }
            //OF decimal sum = AllInvoices.Sum(i => i.InvoiceTotal);
            MessageBox.Show(sum.ToString("C"), "Sum of invoices");
        }

        private void ShowBigInvoicesButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Define the query expression
            //var bigInvoiceQuery = //TODO: write Linq query
            //var bigInvoiceQuery = from invoice in AllInvoices
            //                      join vendor in AllVendors on invoice.VendorId equals vendor.VendorId
            //                      where invoice.InvoiceTotal > 20000
            //                      select new
            //                      {
            //                          VendorName = vendor.Name,
            //                          invoice.InvoiceNumber,
            //                          total = invoice.InvoiceTotal
            //                      };
            //Execute the query


            var bigInvoiceQuery = AllInvoices.Where(invoice => invoice.InvoiceTotal > 20000).Join(AllVendors, invoice => invoice.VendorId, vendor => vendor.VendorId, (invoice, vendor) => new
            {
                VendorName = vendor.Name,
                invoice.InvoiceNumber,
                invoice.InvoiceTotal
            });
            var resultBuilder = new StringBuilder("Invoice No.\tBalance due");
            resultBuilder.AppendLine();
            foreach (var invoice in bigInvoiceQuery) //triggers query execution
            {
                resultBuilder.AppendLine($"{invoice.VendorName}\t\t{invoice.InvoiceNumber:C}\t\t{invoice.InvoiceTotal}");
            }
            MessageBox.Show(resultBuilder.ToString());
        }

        private void ShowInvoicesDueButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Define the query expression
            //var invoicesDueQuery = //TODO write Linq query

            //Execute the query
            var resultBuilder = new StringBuilder("Invoice No.\tBalance due");
            resultBuilder.AppendLine();
            //foreach (var invoice in invoicesDueQuery) //triggers query execution
            //{
            //    resultBuilder.AppendLine($"{invoice.InvoiceNumber.PadRight(15)}\t{invoice.BalanceDue:C}");
            //}
            MessageBox.Show(resultBuilder.ToString());
        }

        private void ShowGroupedInvoicesButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Define the query expression
            //TODO write Linq query
            var groupedInvoicesQuery =
                from invoice in AllInvoices
                group invoice by invoice.VendorId into vendorGroup
                select new
                {
                    VendorId = vendorGroup.Key,
                    InvoiceCount = vendorGroup.Count(),
                    Invoices = vendorGroup
                };
            //Execute the query
            var resultBuilder = new StringBuilder("Vendor Id (#invoices)\t\tInvoice No.\tInvoice Total");
            resultBuilder.AppendLine();
            //foreach (var vendorInfo in groupedInvoicesQuery) //triggers query execution
            //{
            //    resultBuilder.AppendLine($"{vendorInfo.VendorId} ({vendorInfo.InvoiceCount})");
            //    foreach (var invoice in vendorInfo.Invoices)
            //    {
            //        resultBuilder.AppendLine($"\t\t\t{invoice.InvoiceNumber.PadRight(15)}\t{invoice.InvoiceTotal:C}");
            //    }
            //}
            MessageBox.Show(resultBuilder.ToString());
        }
    }
}
