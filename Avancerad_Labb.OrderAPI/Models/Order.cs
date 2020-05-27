using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.OrderAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderRow> OrderRows { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
