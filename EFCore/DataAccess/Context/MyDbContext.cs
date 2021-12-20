using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DataAccess.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=RAIDER\SQLEXPRESS;Initial Catalog=HomeWorkEntityFrameWorkCore;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
