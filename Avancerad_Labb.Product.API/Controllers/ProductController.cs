using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.Product.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Avancerad_Labb.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products != null && products.Count() > 0)
            {
                return products;
            }
            return null;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Models.Product>>GetProductById(Guid Id)
        {
            Models.Product product = await _context.Products.FindAsync(Id);
            if(product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

    }
}