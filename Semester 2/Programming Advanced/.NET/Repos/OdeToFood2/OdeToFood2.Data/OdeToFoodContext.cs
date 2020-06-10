using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdeToFood2.Data.DomainClasses;

namespace OdeToFood2.Data
{
    public class OdeToFoodContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public OdeToFoodContext(DbContextOptions<OdeToFoodContext> options)
            : base(options)
        {
        }
    }
}
