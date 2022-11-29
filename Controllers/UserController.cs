﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Helpers.ApiException.UserException;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return _mapper.Map<List<UserDto>>(await _userService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            var user = await _userService.GetyById(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        [HttpGet("{id}/WithFolders")]
        public async Task<ActionResult<UserAllDataDto>> GetAllDataUser(long id)
        {
            var user = await _userService.GetAllDataById(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }

            return _mapper.Map<UserAllDataDto>(user);
        }

        [HttpPut("{id}/ChangePassword")]
        public async Task<ActionResult> EditUserPassword(long id, UserChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest("User ID mismatch");
            }

            if (!await _userService.EntityExists(model.Id))
            {
                return NotFound($"User with Id {id} not found");
            }

            try
            {
                await _userService.ChangePassword(model);
                await _userService.Save();
            }
            catch (VerifyPasswordUserException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> EditUser(long id, UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest("User ID mismatch");
            }

            User user = await _userService.GetyById(id);

            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }

            _mapper.Map(model, user);

            _userService.Update(user);
            await _userService.Save();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto model)
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

            var userDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> VerifyUser(UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (!await _userService.EntityExists(model.Email))
            {
                return BadRequest();
            }

            try
            {
                return _mapper.Map<UserDto>(await _userService.LoginUser(model));
            }
            catch (VerifyPasswordUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (!await _userService.EntityExists(id))
            {
                return NotFound();
            }

            await _userService.Delete(id);
            await _userService.Save();

            return NoContent();
        }
    }
}