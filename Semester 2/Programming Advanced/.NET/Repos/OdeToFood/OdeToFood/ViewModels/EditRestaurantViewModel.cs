using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Services;
using OdeToFood.Entities;
namespace OdeToFood.ViewModels
{
    public class EditRestaurantViewModel
    {
        [Required, MaxLength(30)]
        [Display(Name = "Naam Restaurant")]
        public string Name { get; set; }
        public CuisineType CuisineType { get; set; }

    }
}
