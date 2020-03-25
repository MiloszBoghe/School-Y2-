using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Services;
using OdeToFood.Entities;
namespace OdeToFood.ViewModels
{
    public class HomePageViewModel
    {
        public string CurrentMessageOfTheDay { get; set; }
        public IEnumerable<Restaurant> Restaurants;

    }
}
