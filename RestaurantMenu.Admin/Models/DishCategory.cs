using System.ComponentModel.DataAnnotations;

namespace RestaurantMenu.Admin.Models
{
    public class DishCategory
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Назва є обов'язковою")]
        public string Name { get; set; }
    }
}
