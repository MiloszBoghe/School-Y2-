using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public class RestaurantData : IRestaurantData
    {
        public IEnumerable<Restaurant> GetAll()
        {
            IList<Restaurant> _restaurants = new List<Restaurant>()
            {
                new Restaurant(1,"Pizza"),
                new Restaurant(2,"KFC"),
                new Restaurant(3,"Chicken")
            };
            return _restaurants;
        }
    }
}
