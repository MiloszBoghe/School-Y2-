using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private BankContext _bankContext;
        public CustomerRepository(BankContext context)
        {
            _bankContext = context;
        }

        public IList<Customer> GetAllWithAccounts()
        {
            return _bankContext.Customers.Include(c=>c.Accounts).ToList();
        }

        public void Update(Customer existingCustomer)
        {
            if (!_bankContext.Customers.Contains(existingCustomer)) throw new ArgumentException();
            _bankContext.Update(existingCustomer);
            _bankContext.SaveChanges();
        }

        public void Add(Customer newCustomer)
        {
            if (_bankContext.Customers.Contains(newCustomer)) throw new ArgumentException();
            _bankContext.Entry(newCustomer).State = EntityState.Added;
            _bankContext.SaveChanges();
        }
    }
}