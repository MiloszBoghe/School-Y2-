using Bank.DomainClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Datalayer
{
    public class CustomerRepository
    {
        private readonly BankContext _context;

        public CustomerRepository(BankContext context)
        {
            _context = context;
        }

        public ICollection<Customer> GetAll()
        {
            // DONE: voeg de code toe om alle klanten op te halen
            return _context.Customers.Include(c => c.Accounts).ToList();
        }

        public void Add(Customer newCustomer)
        {
            // DONE: voeg de code toe om een customer aan de database toe te voegen
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
            if (_context.Customers.Contains(newCustomer)) throw new ArgumentException();
            _context.Entry(newCustomer).State = EntityState.Added;
        }

        /// <summary>
        /// Updates an existing customer in the database
        /// </summary>
        /// <param name="customerId">The identifier of the existing customer</param>
        /// <param name="source">A (possibly untracked) customer object from which the properties will be copied to the existing customer</param>
        public void UpdateCustomer(int customerId, Customer source)
        {
            // DONE: voeg de code toe om een klant (met doorgegeven customerId) aan te passen
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
            Customer customer = _context.Customers.Find(customerId);
            if (customer == null) throw new ArgumentException();
            _context.Entry(customer).CurrentValues.SetValues(source);
        }

        public void Commit()
        {
            // DONE: voeg code toe die de gedane aanpassingen doorvoert in de database
            _context.SaveChanges();
        }
    }
}