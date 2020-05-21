// test - initial data

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("version1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get ([FromServices] DataContext context)
        {
            User employee = new User { Id = 1, Username = "employeeTest", Password = "employeeTest", Role = "Employee"};
            User manager = new User { Id = 2 , Username = "managerTest", Password = "managerTest", Role = "manager"};
            Category category = new Category { Id = 1, Title = "IT" };
            Product product = new Product { Id = 1, Category = category, Title = "Mouse", Price = 299, Description = "Mouse gamer" };

            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(new { message = "Configured data" });
        }
    }
}