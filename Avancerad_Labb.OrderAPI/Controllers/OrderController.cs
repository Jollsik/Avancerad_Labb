using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.OrderAPI.Data;
using Avancerad_Labb.OrderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Avancerad_Labb.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderDbContext _context;
        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(PostOrder),
                new { Id = order.Id },
                order);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid Id)
        {
            Order order = await _context.Orders.FindAsync(Id);
            if (order != null)
            {
                return order;
            }
            else
            {
                return null;
            }
        }
    }
}