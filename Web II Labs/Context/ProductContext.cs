using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_II_Labs.Models;


namespace Web_II_Labs.Context
{
    public class ProductContext:IdentityDbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options):base(options)
        {
           

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;Database=EFProduct;
        //        Trusted_Connection=True; TrustServerCertificate=True");
        //}
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGallery> ProductGallerys { get; set; }

    }
}
