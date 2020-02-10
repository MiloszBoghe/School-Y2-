using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Data.DomainClasses;
using Bank.Data.Interfaces;

namespace Bank.Data
{
    public class CityRepository : ICityRepository
    {
        private BankContext _bankContext;
        public CityRepository(BankContext context)
        {
            _bankContext = context;
        }

        public IList<City> GetAll()
        {
           return _bankContext.Cities.ToList();
        }
    }
}