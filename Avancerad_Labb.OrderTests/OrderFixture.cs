using Avancerad_Labb.OrderAPI.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Avancerad_Labb.OrderTests
{
    public class OrderFixture : IDisposable
    {
        public Order order { get; private set; }

        public OrderFixture()
        {
            order = Initialize().Result;
        }

        private async Task<Order> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = new Order
                {
                    FirstName = "Testorder",
                    TotalPrice = 1337,
                    LastName = "Felldin"
                };
                var serializedOrder = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/order", serializedOrder);
                var responseOrder = await response.Content.ReadAsStringAsync();

                var createdOrder = JsonConvert.DeserializeObject<Order>(responseOrder);
                return createdOrder;
            }
        }

        public async void Dispose()
        {
            using(var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync("api/order/" + order.Id);
                deleteResponse.EnsureSuccessStatusCode();
            }
        }
    }
}
