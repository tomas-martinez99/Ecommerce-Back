using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; }   // Ruta relativa o URL absoluta
        public bool IsMain { get; set; }  // Para marcar la principal
        public int ProductId { get; set; }
        public Product Product
        {
            get; set;
        }
    }
}
