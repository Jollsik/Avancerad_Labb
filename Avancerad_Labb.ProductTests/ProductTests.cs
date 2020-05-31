using Avancerad_Labb.Product.API.Controllers;
using Avancerad_Labb.Product.API.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Avancerad_Labb.ProductTests
{
    public class ProductTests
    {
        [Fact]
        public async void GetProductById_Returns_Product()
        {

            using(var client = new TestClientProvider().Client)
            {
                Guid id = new Guid("503e1eb4-4cbe-40f9-08d8-08d800c97813");

                var response = await client.GetAsync("/api/product/" + id);
                var product = await response.Content.ReadAsStringAsync();

                Assert.IsType<Product.API.Models.Product>(JsonConvert.DeserializeObject<Product.API.Models.Product>(product));
            }

        }
        [Fact]
        public async void GetAllProducts_Returns_ProductList()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/product/");
                var products = await response.Content.ReadAsStringAsync();

                Assert.IsType<List<Product.API.Models.Product>>(JsonConvert.DeserializeObject<List<Product.API.Models.Product>>(products));
            }

        }
    }
}
