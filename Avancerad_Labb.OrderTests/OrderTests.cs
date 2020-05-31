using Avancerad_Labb.OrderAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Avancerad_Labb.OrderTests
{
    public class OrderTests : IClassFixture<OrderFixture>
    {
        OrderFixture _fixture;

        public OrderTests(OrderFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void PostOrder_Returns_Created_Order()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = new Order { Id = new Guid("03e781e4-4a28-402b-a7f3-25340d1f6f18"), FirstName = "testorder" };

                var jsonOrder = JsonConvert.SerializeObject(order);
                var orderContent = new StringContent(jsonOrder, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/order", orderContent);
                var orderResponse = await response.Content.ReadAsStringAsync();
                var createdOrder = JsonConvert.DeserializeObject<Order>(orderResponse);

                Assert.Equal(order.Id, createdOrder.Id);

                var deleteResponse = await client.DeleteAsync("api/order/" + order.Id);
                deleteResponse.EnsureSuccessStatusCode();
            }
        }
        [Fact]
        public async void GetOrderById_Returns_Order()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("api/order/" + _fixture.order.Id);
                var orderResponse = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(orderResponse);

                Assert.Equal(_fixture.order.Id, order.Id);
            }
        }
        [Fact]
        public async void DeleteOrderById_Returns_204()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = new Order { Id = new Guid("03e781e4-4a28-402b-a7f3-25340d1f6f18"), FirstName = "testorder" };

                var jsonOrder = JsonConvert.SerializeObject(order);
                var orderContent = new StringContent(jsonOrder, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/order", orderContent);
                var orderResponse = await response.Content.ReadAsStringAsync();
                var createdOrder = JsonConvert.DeserializeObject<Order>(orderResponse);

                var deleteResponse = await client.DeleteAsync("api/order/" + order.Id);

                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }
    }
}
