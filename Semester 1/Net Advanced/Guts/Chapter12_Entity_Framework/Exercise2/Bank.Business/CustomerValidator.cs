using System;
using System.Linq;
using Bank.Business.Interfaces;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.Business
{
    public class CustomerValidator : ICustomerValidator
    {
        private ICityRepository _cityRepository;
        public CustomerValidator(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public ValidatorResult IsValid(Customer customer)
        {
            return customer == null ?
                   ValidatorResult.Fail("Customer doesn't exist!") :
                   customer.FirstName == "" || customer.FirstName == null ?
                   ValidatorResult.Fail("First name is invalid!") :
                   customer.Name == "" || customer.Name == null ?
                   ValidatorResult.Fail("Name is invalid!") :
                   !_cityRepository.GetAll().Any(c => c.ZipCode == customer.ZipCode) ?
                   ValidatorResult.Fail("City doesn't exist") :
                   ValidatorResult.Success();
        }
    }
}
