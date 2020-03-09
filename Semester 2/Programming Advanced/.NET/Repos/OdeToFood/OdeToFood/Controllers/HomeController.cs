using Microsoft.AspNetCore.Mvc;
using OdeToFood.ViewModels;
using OdeToFood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Entities;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;
        private IRestaurantData _restaurantData;
        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }
        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                CurrentMessageOfTheDay = _greeter.GetMessageOfTheDay(),
                Restaurants = _restaurantData.GetAll()
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var restaurant = _restaurantData.GetById(id);
            if (restaurant == null) return NotFound();
            return View(restaurant);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Restaurant restaurant)
        {
            _restaurantData.Add(restaurant);
            return RedirectToAction(nameof(Details), new { id = restaurant.Id });
        }
    }
}
