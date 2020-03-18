using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Entities;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly RestaurantDbContext _context;


        public SqlRestaurantData(RestaurantDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant GetById(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void Edit(Restaurant restaurant)
        {
            Restaurant existingRestaurant = GetById(restaurant.Id);
            _context.Entry(existingRestaurant).CurrentValues.SetValues(restaurant);
            _context.SaveChanges();
        }
    }
}
