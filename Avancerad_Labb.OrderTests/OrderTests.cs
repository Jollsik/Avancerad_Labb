using Avancerad_Labb.OrderAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Avancerad_Labb.OrderTests
{
    public class OrderTests
    {
        public Guid Id = Guid.NewGuid();
        [Fact]
        public async void PostOrder_Returns_Order()
        {
            using (var client = new TestClientProvider().Client)
            {
                Order order = new Order();
                order.Id =  Id;
                order.FirstName = "TestOrder";

                var jsonOrder = JsonConvert.SerializeObject(order);
                var orderContent = new StringContent(jsonOrder, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/order", orderContent);
                var orderResponse = await response.Content.ReadAsStringAsync();

                Assert.IsType<Order>(JsonConvert.DeserializeObject<Order>(orderResponse));
            }
        }
        [Fact]
        public async void GetOrderById_Returns_Order()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("api/order" + Id);
                var order = await response.Content.ReadAsStringAsync();

                Assert.IsType<Order>(JsonConvert.DeserializeObject<Order>(order));
            }
        }
        [Fact]
        public async void DeleteOrderById_Returns_204()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("api/order" + Id);
            }
        }
    }
}
