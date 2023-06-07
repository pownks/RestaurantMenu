using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RestaurantMenu.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMenu.Admin.ViewModels
{
    public class DishViewModel
    {
        public Dish Dish { get; set; }
        [Display(Name = "Зображення")]
        [ValidateNever]
        public IFormFile? Image { get; set; }
    }
}
