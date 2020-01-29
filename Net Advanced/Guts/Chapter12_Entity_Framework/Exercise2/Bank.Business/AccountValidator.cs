using System;
using System.Linq;
using Bank.Business.Interfaces;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.Business
{
    public class AccountValidator : IAccountValidator
    {
        private ICustomerRepository _customerRepository;
        public AccountValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ValidatorResult IsValid(Account account)
        {

            
            return account == null ?
                    ValidatorResult.Fail("Account doesn't exist!") :
                    !_customerRepository.GetAllWithAccounts().Any(c => c.Id == account.CustomerId) ?
                    ValidatorResult.Fail("Customer doesn't exist!") :
                    account.AccountNumber == "" || account.AccountNumber == null ?
                    ValidatorResult.Fail("accountNumber can't be empty!") :
                    account.Balance < 0 ?
                    ValidatorResult.Fail("Balance can't be lower than 0") :
                    account.AccountType < 0 ?
                    ValidatorResult.Fail("Wrong account type") :
                    ValidatorResult.Success();
        }
    }
}