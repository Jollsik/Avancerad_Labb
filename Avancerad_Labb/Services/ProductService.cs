using Avancerad_Labb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Avancerad_Labb.Services
{
    public class ProductService : IProductService
    {
        public List<Product> ProductList = new List<Product>();
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("product/");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Product>>(products);
        }
        public async Task<Product> GetProductById(Guid Id)
        {
            foreach(var product in ProductList)
            {
                if (product.ID == Id)
                {
                    return product;
                }
            }
            return null;
        }
    }
}
