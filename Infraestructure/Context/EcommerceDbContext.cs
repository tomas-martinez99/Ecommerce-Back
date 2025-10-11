using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Context
{
    public class EcommerceDbContext: DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) 
            : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many relationship: Order ↔ Product (with intermediate entity OrderProduct)

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(op => op.ProductId);

            //One-to-many relationship: Provider → Product

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Provider)
                .WithMany(pr => pr.Products)
                .HasForeignKey(p => p.ProviderId);


            //One-to-many relationship: Role → User

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);;

            modelBuilder.Entity<Order>(builder =>
            {
                // 1) Conversión del enum Status a string en la DB y marcarlo como NOT NULL
                builder.Property(o => o.Status)
                       .HasConversion<string>()
                       .IsRequired();

                // 2) Relación User → Orders (un User tiene muchas Orders)
                builder.HasOne(o => o.User)
                       .WithMany(u => u.Orders)     // navegación inversa en User.Orders
                       .HasForeignKey(o => o.UserId)
                       .OnDelete(DeleteBehavior.Restrict);
                //Relacion Empleado asignado → Order
                builder.HasOne(o => o.Employed)
                        .WithMany(u => u.AssignedOrders)
                        .HasForeignKey(o => o.EmployedId)
                        .OnDelete(DeleteBehavior.SetNull);

                // 3) Relación Order → OrderHistory (una Order tiene muchas OrderHistory)
                builder.HasMany(o => o.History)  // colección en Order
                       .WithOne(h => h.Order)           // navegación inversa en OrderHistory.Order
                       .HasForeignKey(h => h.OrderId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
