using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get ([FromServices] DataContext context)
        {
            List<User> users = await context.Users.AsNoTracking().ToListAsync();

            return users;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model
        )
        {
            // data validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Isn't possible to create the user" });
            }
        }
    
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model
        )
        {
            User user = await context.Users.AsNoTracking()
                .Where(user => user.Username == model.Username && user.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Invalid username or password"});

            var token = TokenService.GenerateToken(user);

            return new { user = user, token = token };
        }
    
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(
            int id,
            [FromServices] DataContext context,
            [FromBody] User model
        )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(id != model.Id)
                return NotFound(new { message = "User not found" });;

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Isn't possible to create the user"});
            }
        }
    
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> Delete (
            int id,
            [FromServices] DataContext context
        )
        {
            User user = await context.Users.FirstOrDefaultAsync(
                user => user.Id == id
            );

            if (user == null)
                return NotFound(new { message = "User not found."});

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(new { message = "User successfully removed"});
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Isn't possible to remove this user."});
            }
        }
    }
}