using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.Models
{
    public class Order
    {
        public Guid Id { get; set; } 
        public List<OrderRow> OrderRows { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class OrderRow
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
