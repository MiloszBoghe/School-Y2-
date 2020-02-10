using Bank.DomainClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Datalayer
{
    public class AccountRepository
    {
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public void AddAccountForCustomer(Account newAccount, int existingCustomerId)
        {
            // DONE: voeg de code toe om een nieuwe account toe te voegen (voor een bestaande klant)
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
            newAccount.CustomerId = existingCustomerId;
            if (_context.Accounts.Contains(newAccount)) throw new ArgumentException();
            _context.Accounts.Add(newAccount);
        }

        /// <summary>
        /// Updates an existing account in the database
        /// </summary>
        /// <param name="accountId">The identifier of the existing account</param>
        /// <param name="source">A (possibly untracked) customer object from which the properties will be copied to the existing customer</param>
        public void UpdateAccount(int accountId, Account source)
        {
            // TODO: voeg de code toe om een account (met doorgegeven accountId) aan te passen
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
            Account account = _context.Accounts.Find(accountId);
            if (account == null) throw new ArgumentException();
            _context.Entry(account).CurrentValues.SetValues(source);
        }

        public void Commit()
        {
            // DONE: voeg code toe die de gedane aanpassingen doorvoert in de database
            _context.SaveChanges();
        }

        public Account GetAccountById(int id)
        {
            // DONE: voeg de code toe om een rekening op te halen met een doorgegeven id
            return _context.Accounts.Find(id);
        }

        public IList<Account> GetAllAccountsOfCustomer(int customerId)
        {
            // DONE: voeg de code toe om alle rekeningen van een klant met doorgegeven customerId op te halen
            return _context.Customers.Find(customerId).Accounts.ToList();
        }
    }
}
