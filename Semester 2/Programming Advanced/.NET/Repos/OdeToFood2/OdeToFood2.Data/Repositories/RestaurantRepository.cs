using System.Collections.Generic;
using System.Linq;
using OdeToFood2.Data.DomainClasses;
using OdeToFood2.Data.IRepositories;

namespace OdeToFood2.Data.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly OdeToFoodContext _context;

        public RestaurantRepository(OdeToFoodContext context) :base(context)
        {
            _context = context;
        }
    }
}
