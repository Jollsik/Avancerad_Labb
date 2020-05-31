using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.Product.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string imageURL { get; set; }
    }
}
