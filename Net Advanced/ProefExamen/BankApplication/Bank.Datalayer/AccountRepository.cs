using Bank.DomainClasses;
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
            // TODO: voeg de code toe om een nieuwe account toe te voegen (voor een bestaande klant)
            // Let op: de aanpassing mag nog niet doorgevoerd worden in de database
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
        }

        public void Commit()
        {
           // TODO: voeg code toe die de gedane aanpassingen doorvoert in de database
        }

        public Account GetAccountById(int id)
        {
            // TODO: voeg de code toe om een rekening op te halen met een doorgegeven id
            return null; 
        }

        public IList<Account> GetAllAccountsOfCustomer(int customerId)
        {
            // TODO: voeg de code toe om alle rekeningen van een klant met doorgegeven customerId op te halen
            return null;
        }
    }
}
