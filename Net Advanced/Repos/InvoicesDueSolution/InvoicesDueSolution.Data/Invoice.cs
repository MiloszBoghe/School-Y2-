using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesDueSolution.Data
{
    public class Invoice
    {
        public Invoice()
        {

        }

        public int MyNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal InvoiceTotal { get; set; }

        public decimal PaymentTotal { get; set; }

        public decimal CreditTotal { get; set; }

        public DateTime DueDate { get; set; }

        public decimal BalanceDue => InvoiceTotal - PaymentTotal - CreditTotal;
    }
}
