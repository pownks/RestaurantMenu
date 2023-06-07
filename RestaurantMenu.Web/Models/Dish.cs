using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMenu.Web.Models
{
    public class Dish
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Назва є обов'язковим")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Ціна є обов'язковою")]
        public decimal Price { get; set; }
        [Display(Name = "Акційний")]
        public bool Special { get; set; }
        [Display(Name = "Акційна ціна")]
        [Required(ErrorMessage = "Акційну ціну необхідно заповнити")]
        public decimal SpecialPrice { get; set; }
        [Display(Name = "Склад")]
        public string? Consist { get; set; }
        [Display(Name = "Вага")]
        [Required(ErrorMessage = "Вага є обов'язковою")]
        public int Weight { get; set; }
        [ValidateNever]
        [Display(Name = "Зображення")]
        public string? ImageBase64 { get; set; }
        [Display(Name = "Категорія")]
        [Required]
        public int DishCategoryId { get; set; }

        [ForeignKey("DishCategoryId")]
        [Display(Name = "Категорія")]
        [ValidateNever]
        public DishCategory DishCategory { get; set; }
    }
}
