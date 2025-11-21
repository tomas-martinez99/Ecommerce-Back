using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Stock {  get; set; }
        public decimal Width { get; set; } //Ancho
        public decimal Height { get; set; } //Altura
        public decimal Length {  get; set; } //Largo
        public decimal Weight { get; set; } //Peso
        public Provider Provider { get; set; }
        public int? ProviderId { get; set; }
        public ICollection<OrderProduct> Orders { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();


    }
}
