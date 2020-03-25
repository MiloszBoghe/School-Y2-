using Microsoft.AspNetCore.Mvc;
using OdeToFood.ViewModels;
using OdeToFood.Services;
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

        #region createMethods
        [HttpPost]
        public IActionResult Create(EditRestaurantViewModel restaurantViewModel)
        {
            if (!ModelState.IsValid) return View(nameof(Create));

            var restaurant = new Restaurant
            {
                CuisineType = restaurantViewModel.CuisineType,
                Name = restaurantViewModel.Name
            };
            _restaurantData.Add(restaurant);
            return RedirectToAction(nameof(Details), new { id = restaurant.Id });
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                CurrentMessageOfTheDay = _greeter.GetMessageOfTheDay(),
                Restaurants = _restaurantData.GetAll()
            };
            return View(model);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            var restaurant = _restaurantData.GetById(id);
            if (restaurant == null) return NotFound();
            return View(nameof(Details), restaurant);
        }
        #endregion

        #region Edit

        public IActionResult Edit(EditRestaurantViewModel restaurantViewModel, int id)
        {
            if (!ModelState.IsValid) return View(nameof(Edit));

            var restaurant = new Restaurant
            {
                Id = id,
                CuisineType = restaurantViewModel.CuisineType,
                Name = restaurantViewModel.Name
            };

            _restaurantData.Edit(restaurant);
            return RedirectToAction(nameof(Details), new { id = restaurant.Id });
            
        }

        #endregion

    }
}
