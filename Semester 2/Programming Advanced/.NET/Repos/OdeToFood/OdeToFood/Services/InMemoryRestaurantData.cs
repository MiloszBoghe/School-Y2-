using OdeToFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Entities;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            this._restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Pizza"},
                new Restaurant {Id = 2, Name = "KFC"},
                new Restaurant {Id = 3, Name = "Chicken"}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {

            return _restaurants;
        }

        public Restaurant GetById(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Restaurant restaurant)
        {
            var newId = _restaurants.Max(r => r.Id) + 1;
            restaurant.Id = newId;
            _restaurants.Add(restaurant);
        }

        public void Edit(Restaurant restaurant)
        {
            Restaurant updated = _restaurants.Find(r => r.Id == restaurant.Id);
            updated = restaurant;
        }
    }
}
