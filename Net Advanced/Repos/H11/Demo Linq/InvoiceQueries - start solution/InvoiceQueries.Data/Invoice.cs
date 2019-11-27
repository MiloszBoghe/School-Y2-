using System;

namespace InvoiceQueries.Data
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal InvoiceTotal { get; set; }

        public decimal PaymentTotal { get; set; }

        public decimal CreditTotal { get; set; }

        public DateTime DueDate { get; set; }

        public int VendorId { get; set; }

        public decimal BalanceDue => InvoiceTotal - PaymentTotal - CreditTotal;
    }
}
