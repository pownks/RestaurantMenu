using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantMenu.Admin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string ADMIN_ID = Guid.NewGuid().ToString();
            var adminUser = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "GigiGagaRestAdmin@mail.com",
                NormalizedEmail = "GIGIGAGARESTADMIN@MAIL.COM",
                EmailConfirmed = true,
                UserName = "GigiGagaRestAdmin@mail.com",
                NormalizedUserName = "GIGIGAGARESTADMIN@MAIL.COM",

            };

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "supersecret123");
            builder.Entity<IdentityUser>().HasData(adminUser);
        }
    }
}