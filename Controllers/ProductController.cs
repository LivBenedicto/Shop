using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            List<Product> products = await context.Products.Include(product => product.Category).AsNoTracking().ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            Product product = await context.Products.Include(product => product.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return product;
        }

        [HttpGet]
        [Route("categories/{id:int}")] // products/categories/1
        public async Task<ActionResult<List<Product>>> GetByCategory(
            int id,
            [FromServices] DataContext context
        )
        {
            List<Product> products = await context.Products
                .Include(product => product.Category)
                .AsNoTracking()
                .Where(product => product.CategoryId == id)
                .ToListAsync();
            
            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            int id,
            [FromServices] DataContext context,
            [FromBody] Product modelProduct
        )
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(modelProduct);
                await context.SaveChangesAsync();
                return Ok(modelProduct);
            }
            else
                return BadRequest(ModelState);
        }
    }
}