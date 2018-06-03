namespace WebServer.Application.Data
{
    using Microsoft.EntityFrameworkCore;
    using WebServer.Application.Data.Models;

    public class ByTheCakeDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.
                UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ByTheCake;Integrated Security=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
               .Entity<Order>()
                .HasOne(u => u.User)
                .WithMany(o=>o.Orders)
                .HasForeignKey(u => u.UserId);

            builder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId,op.ProductId });

            builder.Entity<OrderProduct>()
                .HasOne(o=>o.Order)
                .WithMany(op=>op.OrderProducts)
                .HasForeignKey(o => o.OrderId);

            builder.Entity<OrderProduct>()
                .HasOne(p=>p.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(p => p.ProductId);


        }
    }
}
