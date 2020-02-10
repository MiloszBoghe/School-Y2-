using System.Collections.Generic;

namespace Bank.Data.DomainClasses
{
    public class City
    {
        public int ZipCode { get; set; }
        public string Name { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}