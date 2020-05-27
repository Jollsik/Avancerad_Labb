using Avancerad_Labb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.Services
{
    public interface IOrderService
    {
        public Task<Order> GetOrderById(Guid Id);
        public Task<Order> PostOrder(Order order);
    }
}
