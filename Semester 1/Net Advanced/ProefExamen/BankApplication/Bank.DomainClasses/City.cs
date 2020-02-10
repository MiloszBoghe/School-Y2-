using System.Collections.Generic;

namespace Bank.DomainClasses
{
    public class City
    {
        // Done: vul deze klasse aan
        public int ZipCode { get; set; }
        public string Name { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
