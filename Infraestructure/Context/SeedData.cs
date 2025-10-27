using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Context
{
    public static class SeedData
    {
        public static void Initialize(EcommerceDbContext context)
        {

            //Roles
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                new Role { Id = 1, RoleName = "User" },
                new Role { Id = 2, RoleName = "SYSAdmin" },
                new Role { Id = 3, RoleName = "Admin" }
            );
                context.SaveChanges();
            }

            //Providers
            if (!context.Providers.Any())
            {
                context.Providers.AddRange(
                new Provider { Id = 1, ProviderName = "Distribuidora Oeste" },
                new Provider { Id = 2, ProviderName = "Monsa" },
                new Provider { Id = 3, ProviderName = "Cordoba Motos" }
            );
                context.SaveChanges();
            }

            //Users

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                new User { Id = 1, UserName = "Tomas", Email = "User@gmail.com", Password = "user1", RoleId = 1 },
                new User { Id = 2, UserName = "Thiago", Email = "SYSAdmin@gmail.com", Password = "SYSAdmin1", RoleId = 2 },
                new User { Id = 3, UserName = "Admin", Email = "Admin@gmail.com", Password = "Admin1", RoleId = 3 }
             );
                context.SaveChanges();
            }



            //Products
            if (!context.Products.Any())
            {
                var products = new List<Product>
            {
                new Product { Id = 1, SKU = "1081N", ProductName = "Kit de plastico Samsh 110 Negro",Description = "Kit de plastico VC Compatible con: Gilera smash", Price = 25000, Cost = 18500, Stock = 1, ProviderId = 1, Brand = "PlasticaVC", FamilyGroup = "Plasticos" },
                new Product { Id = 2, SKU = "FV015NP", ProductName = "Funda antideslizante Motomel 110", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 2, ProviderId = 1, Brand = "TSL", FamilyGroup = "Accesorios" },
                new Product { Id = 3, SKU = "70766", ProductName = "Kit trnasmision Titan 150", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 3, ProviderId = 1, Brand = "Riffel", FamilyGroup = "Transmision" },
                new Product { Id = 4, SKU = "FV015N", ProductName = "Funda antideslizante Universal 110 Negra", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 4, ProviderId = 1, Brand = "TSL", FamilyGroup = "Accesorios" },
                new Product { Id = 5, SKU = "FV015A", ProductName = "Funda antideslizante Universal 110 Azul", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 5, ProviderId = 2, Brand = "TSL", FamilyGroup = "Accesorios" },
                new Product { Id = 6, SKU = "BXgg200", ProductName = "Kit Transmision Honda biz 125 ", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 6, ProviderId = 2, Brand = "GianGiu", FamilyGroup = "Transmision" },
                new Product { Id = 7, SKU = "1081ABT", ProductName = "Kit de plastico Smash 110 Azul Bitono", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 7, ProviderId = 3, Brand = "PlasticaVC", FamilyGroup = "Plasticos" },
                new Product { Id = 8, SKU = "6323", ProductName = "Casco Influence Negro Mate y Amarillo S", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 8, ProviderId = 3, Brand = "Vertigo", FamilyGroup = "Cascos" },
                new Product { Id = 9, SKU = "6322", ProductName = "Casco Influence Negro Mate y Amarillo M", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 9, ProviderId = 1, Brand = "Vertigo", FamilyGroup = "Cascos" },
                new Product { Id = 10, SKU = "6321", ProductName = "Casco Influence Negro Mate y Amarillo L", Description = "Funda antideslizante Para motomel blitz 110", Price = 25000, Cost = 18500, Stock = 10, ProviderId = 1, Brand = "Vertigo", FamilyGroup = "Cascos" }
                };
                context.Products.AddRange(products);
                context.SaveChanges();
                var productImages = new List<ProductImage>
            {
                new ProductImage { ProductId = 1, Url = "/images/products/1081N.jpg", IsMain = true },
                new ProductImage { ProductId = 2, Url = "/images/products/FV015NP.jpg", IsMain = true },
                new ProductImage { ProductId = 3, Url = "/images/products/70766.jpg", IsMain = true },
                new ProductImage { ProductId = 4, Url = "/images/products/FV015N.jpg", IsMain = true },
                new ProductImage { ProductId = 5, Url = "/images/products/FV015A.jpg", IsMain = true },
                new ProductImage { ProductId = 6, Url = "/images/products/BXgg200.jpg", IsMain = true },
                new ProductImage { ProductId = 7, Url = "/images/products/1081ABT.jpg", IsMain = true },
                new ProductImage { ProductId = 8, Url = "/images/products/6323.jpg", IsMain = true },
                new ProductImage { ProductId = 9, Url = "/images/products/6322.jpg", IsMain = true },
                new ProductImage { ProductId = 10, Url = "/images/products/6321.jpg", IsMain = true }             
            };
                context.ProductImages.AddRange(productImages);
                context.SaveChanges();
            }
            if (!context.Orders.Any())
            {
                var userIds = context.Users.Where(u => u.RoleId == 1).Select(u => u.Id).ToList(); // Usuarios normales
                var employeeIds = context.Users.Where(u => u.RoleId != 1).Select(u => u.Id).ToList(); // Empleados/Admins
                var productIds = context.Products.Select(p => p.Id).ToList();

                if (!userIds.Any() || !employeeIds.Any() || !productIds.Any())
                {
                    return; // No hay datos suficientes; salir sin crear órdenes
                }

                var random = new Random();
                var orders = new List<Order>();
                var statusValues = Enum.GetValues(typeof(OrderStatus));


                for (int i = 0; i < 10; i++)
                {
                    var userId = userIds[random.Next(userIds.Count)];
                    var employeeId = employeeIds[random.Next(employeeIds.Count)];
                    var createdDate = DateTime.UtcNow.AddDays(-random.Next(1, 30));
                    var status = (OrderStatus)random.Next(Enum.GetValues(typeof(OrderStatus)).Length);

                    var order = new Order
                    {
                        Created = createdDate,
                        UserId = userId,
                        EmployedId = employeeId,
                        Status = status,
                        Products = new List<OrderProduct>(),
                        History = new List<OrderHistory>()
                    };

                    // Agregar entre 1 y 3 productos por orden
                    var productCount = random.Next(1, 4);
                    var selectedProducts = productIds.OrderBy(x => Guid.NewGuid()).Take(productCount).ToList();

                    foreach (var pid in selectedProducts)
                    {
                        var product = context.Products.First(p => p.Id == pid);
                        order.Products.Add(new OrderProduct
                        {
                            ProductId = pid,
                            UnitPrice = product.Price,
                            Quantity = random.Next(1, 5)
                        });
                    }

                    // Agregar historial de estado
                    order.History.Add(new OrderHistory
                    {
                        OldStatus = OrderStatus.EnProseso,
                        NewStatus = status,
                        ChangedByEmployeeId = employeeId,
                        ChangedAt = createdDate.AddHours(1)
                    });

                    orders.Add(order);
                }

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
           
       
        }
    }
}
