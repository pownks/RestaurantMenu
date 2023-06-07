using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Web.Models;

namespace RestaurantMenu.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHostEnvironment _env;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHostEnvironment env) : base(options)
        {
            _env = env;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            FileStream stream = new FileStream(Path.Combine(_env.ContentRootPath, "initial.jpg"), FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            string base64 = Convert.ToBase64String(buffer);

            modelBuilder.Entity<Dish>().HasData(new Dish
            {
                Id = 1,
                Name = "Пельмень",
                Price = 100,
                Special = true,
                SpecialPrice = 50,
                Consist = "м'ясо, тісто",
                Weight = 20,
                ImageBase64 = base64,
                DishCategoryId = 1
            });
            modelBuilder.Entity<DishCategory>().HasData(new DishCategory
            {
                Id = 1,
                Name = "Пельмені"
            });
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
    }
}
