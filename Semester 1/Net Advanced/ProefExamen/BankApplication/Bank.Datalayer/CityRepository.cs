using Bank.DomainClasses;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Datalayer
{
    public class CityRepository
    {
        private readonly BankContext _context;

        public CityRepository(BankContext context)
        {
            _context = context;
        }

        public IList<City> GetAllCities()
        {
            return _context.Cities.ToList();
        }
    }
}

