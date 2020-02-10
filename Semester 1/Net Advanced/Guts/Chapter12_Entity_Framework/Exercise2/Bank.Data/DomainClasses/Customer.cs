using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Data.DomainClasses
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public int ZipCode { get; set; }
        public City City { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}