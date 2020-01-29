using System;
using Bank.DomainClasses;
using Bank.DomainClasses.Enums;

namespace Bank.Business
{
    public class AccountManager
    {
        public void TransferMoney(Account fromAccount, Account toAccount, decimal amount)
        {
            //TODO: voeg de logica toe die nodig is om een bedrag over te schrijven
            if (fromAccount.Balance < amount) throw new InvalidTransferException("You poor!");
            if (fromAccount.AccountType == (AccountType)2 && amount >= 1000) throw new InvalidTransferException("you young!");
            toAccount.Balance += amount;
            fromAccount.Balance -= amount;
        }
    }
}
