using Bank.DomainClasses.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bank.DomainClasses
{
    public class Account
    {
        // DONE: vul deze klasse aan
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
