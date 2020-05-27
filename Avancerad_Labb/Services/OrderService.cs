using Avancerad_Labb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Avancerad_Labb.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/api/order/{Id}");
            response.EnsureSuccessStatusCode();
            var order = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Order>(order);
        }

        public async Task<Order> PostOrder(Order order)
        {
            var JsonOrder = JsonConvert.SerializeObject(order);
            var orderContent = new StringContent(JsonOrder, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/order", orderContent);

            var orderResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Order>(orderResponse);
        }
    }
}
