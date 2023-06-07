using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Web.Data;
using RestaurantMenu.Web.Models;

namespace RestaurantMenu.Web.Controllers
{
    public class DishController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DishController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IEnumerable<DishCategory> GetDishCategories()
        {
            return _dbContext.DishCategories;
        }
        public IActionResult Index()
        {
            IEnumerable<Dish> dishes = _dbContext.Dishes
                .Where(d => d.Special == true).ToList();

            ViewData["Dishes"] = dishes;
            ViewData["Categories"] = GetDishCategories();

            return View();
        }
        //GET
        public IActionResult Category(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            IEnumerable<Dish> dishes = _dbContext.Dishes
                .Where(d => d.DishCategoryId == id);
            if (!dishes.Any())
                return NotFound();

            ViewData["Dishes"] = dishes;
            ViewData["Categories"] = GetDishCategories();

            return View();
        }

    }
}
