using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Admin.Models;

namespace RestaurantMenu.Admin.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
            
        }
        public DbSet<Dish> Dishes {  get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
    }
}
