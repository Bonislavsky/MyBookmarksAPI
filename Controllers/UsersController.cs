using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using MyBookmarksAPI.Service.Interface;

namespace MyBookmarksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyBookmarksDbContext _context;
        private readonly IUserService _userService;

        public UsersController(MyBookmarksDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }   

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateTDO model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            User user = await _userService.Create(model);
            _userService.CreateStartFolders(3, user.Id);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
