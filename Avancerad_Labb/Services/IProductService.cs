using Avancerad_Labb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.Services
{
    public interface IProductService
    {
        public Task <IEnumerable<Product>> GetAllProducts();
        public Task <Product> GetProductById(Guid Id);
    }
}
