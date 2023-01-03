using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Helpers.ApiException.UserException;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return List user`s</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_mapper.Map<List<UserDto>>(await _userService.GetAll()));
        }

        /// <summary>
        /// Get a User by Id BUT not folders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If user by id not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userService.GetyById(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Get a User by Id with folders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If user by id not found</response>
        [HttpGet("{id}/WithFolders")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDataUser(long id)
        {
            var user = await _userService.GetAllDataById(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }

            return Ok(_mapper.Map<UserAllDataDto>(user));
        }

        /// <summary>
        /// Changing the user's password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     id = 1
        ///     Put
        ///     {
        ///           "Id": 1
        ///           "CurrentPassword": "exemple123",
        ///           "password": "newexemple",
        ///           "passwordConfirme": "newexemple"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="404">If user by id not found</response>
        /// <response code="400">input error</response> 
        [HttpPut("{id}/ChangePassword")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUserPassword(long id, UserChangePassword model)
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

        /// <summary>
        /// Changing the user's data
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     id = 1
        ///     Put
        ///     {
        ///           "Id": 1
        ///           "email": "exemple@gmaol.com",
        ///           "name": "nameexemple",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">User data updated</response>
        /// <response code="404">If user by id not found</response>
        /// <response code="400">input error</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post
        ///     {
        ///           "email": "Exemple@gmail.com",
        ///           "password": "exemple123",
        ///           "passwordConfirme": "exemple123",
        ///           "name": "Tania"
        ///     }
        ///
        /// </remarks>
        /// <returns> A new created User</returns>
        /// <response code="201">Returns the new created User</response>
        /// <response code="400">If email already registered</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(UserCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (await _userService.EntityExists(model.Email))
            {
                return BadRequest($"This email:{model.Email} already registered");
            }

            User user = await _userService.Create(model);
            user.Folders = await _userService.CreateStartFolders(3, user.Id);

            var userDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }

        /// <summary>
        /// Login user into a system
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get
        ///     {
        ///           "email": "Exemple@gmail.com",
        ///           "password": "exemple123",
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid password supplied</response>
        /// <response code="404">If user by id not found</response>
        [HttpGet("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyUser(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (!await _userService.EntityExists(email))
            {
                return NotFound($"User with email {email} not found");
            }

            try
            {
                return Ok(_mapper.Map<UserDto>(await _userService.LoginUser(email, password)));
            }
            catch (VerifyPasswordUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>       
        /// <response code="404">If user by id not found</response>
        /// <response code="204">User deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (!await _userService.EntityExists(id))
            {
                return NotFound($"User with Id {id} not found");
            }

            await _userService.Delete(id);
            await _userService.Save();

            return NoContent();
        }
    }
}