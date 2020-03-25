using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class AccountRepository : IAccountRepository
    {
        private BankContext _bankContext;
        public AccountRepository(BankContext context)
        {
            _bankContext = context;
        }

        public void Add(Account newAccount)
        {
            if (_bankContext.Accounts.Contains(newAccount)) throw new ArgumentException();
            _bankContext.Entry(newAccount).State = EntityState.Added;
            _bankContext.SaveChanges();
        }

        public void Update(Account existingAccount)
        {
            if (!_bankContext.Accounts.Contains(existingAccount)) throw new ArgumentException();

            if (_bankContext.Entry(existingAccount).Property(a => a.Balance).IsModified) throw new InvalidOperationException();
            _bankContext.Update(existingAccount);
            _bankContext.SaveChanges();
        }



        public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
        {
            _bankContext.Accounts.Find(fromAccountId).Balance -= amount;
            _bankContext.Accounts.Find(toAccountId).Balance += amount;
            _bankContext.SaveChanges();
        }
    }
}
