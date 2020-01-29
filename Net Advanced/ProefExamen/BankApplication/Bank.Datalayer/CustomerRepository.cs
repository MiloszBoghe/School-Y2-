using Bank.DomainClasses;
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
            // TODO: voeg de code toe om alle klanten op te halen
            return null;
        }

        public void Add(Customer newCustomer)
        {
            // TODO: voeg de code toe om een customer aan de database toe te voegen
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
        }

        /// <summary>
        /// Updates an existing customer in the database
        /// </summary>
        /// <param name="customerId">The identifier of the existing customer</param>
        /// <param name="source">A (possibly untracked) customer object from which the properties will be copied to the existing customer</param>
        public void UpdateCustomer(int customerId, Customer source)
        {
            // TODO: voeg de code toe om een klant (met doorgegeven customerId) aan te passen
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
        }

        public void Commit()
        {
            // TODO: voeg code toe die de gedane aanpassingen doorvoert in de database
        }
    }
}