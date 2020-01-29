using System.Collections.Generic;

namespace Bank.DomainClasses
{
    public class Customer
    {
        //DONE: vul de klasse aanµ
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Cellphone { get; set; }
        public int ZipCode { get; set; }
        public List<Account> Accounts { get; set; }
        public City City { get; set; }
    }
}
