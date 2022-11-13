using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using MyBookmarksAPI.Service.Interface;

namespace MyBookmarksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            if (!await _userService.EntityExists(id))
            {
                return NotFound();
            }

            return await _userService.GetyById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(long id, UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest("User ID mismatch");
            }

            if (await _userService.EntityExists(id))
            {
                return NotFound($"User with Id {id} not found");
            }
            
            _userService.Update(model);
            _userService.Save();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (await _userService.EntityExists(model.Email))
            {
                return BadRequest();
            }

            User user = await _userService.Create(model);
            user.Folders = await _userService.CreateStartFolders(3, user.Id);
            _userService.Save();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (!await _userService.EntityExists(id))
            {
                return NotFound();
            }

            await _userService.Delete(id);
            _userService.Save();

            return NoContent();
        }
    }
}
