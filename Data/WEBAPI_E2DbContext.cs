
using Microsoft.EntityFrameworkCore;
using WEBAPI_E2.Models;

namespace WEBAPI_E2.Data
{
    public class WEBAPI_E2DbContext : DbContext
    {
        public WEBAPI_E2DbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<LocationModel> Location { get; set; }
        public DbSet<RegistrationModel> Registration { get; set; }
        public DbSet<ImageModel> Image { get; set; }
    }
}
