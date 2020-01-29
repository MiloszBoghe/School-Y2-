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
            // TODO: voeg de code toe om alle cities uit de database te lezen
            return null;
        }
    }
}

