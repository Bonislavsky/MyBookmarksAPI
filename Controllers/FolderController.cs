using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.FolderDtoModel;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public FolderController(IFolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all folders
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return List folder`s</response>
        /// <response code="404">If folder by userid not found</response>
        /// <response code="400">Sort parameter not found</response> 
        [HttpGet("userId:{userId}/sortBy:{sortParam}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FolderWithoutBmDto>>> GetFolders(long userId, string sortParam = "Id", bool isDec = false)
        {
            if (!await _folderService.UserByIdExists(userId))
            {
                return NotFound($"User with Id {userId} not found");
            }

            if (typeof(FolderWithoutBmDto).GetProperty(sortParam) is null)
            {
                return BadRequest($"\"{sortParam}\" parametr is not found");
            }

            return Ok(_mapper.Map<List<FolderWithoutBmDto>>(await _folderService.GetListByUserId(userId, sortParam, isDec ? "DESC" : "ASC")));
        }

        /// <summary>
        /// Get a Folder by Id with bookmark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return folder</response>
        /// <response code="404">If folder by id not found</response>
        [HttpGet("WithBookmark/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FolderWithBmDto>> GetFolderWithBookmark(long id)
        {
            var folder = await _folderService.GetAllDataById(id);
            if (folder == null)
            {
                return NotFound($"Folder with Id {id} not found");
            }

            return Ok(_mapper.Map<FolderWithBmDto>(folder));
        }

        /// <summary>
        /// Get a Folder by Id without bookmark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If folder by id not found</response>
        [HttpGet("WithoutBookmark/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FolderWithoutBmDto>> GetFolderWithoutBookmark(long id)
        {
            var folder = await _folderService.GetyById(id);
            if (folder == null)
            {
                return NotFound($"Folder with Id {id} not found");
            }

            return Ok(_mapper.Map<FolderWithoutBmDto>(folder));
        }

        /// <summary>
        /// Changing the folder's data
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     id = 1
        ///     Put
        ///     {
        ///           "Id": 1,
        ///           "name": "nameexemple"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Folder data updated</response>
        /// <response code="404">If folder by id not found</response>
        /// <response code="400">input error</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FolderWithoutBmDto>> EditFolder(long id, FolderUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest("Folder ID mismatch");
            }

            Folder folder = await _folderService.GetyById(id);

            if (folder == null)
            {
                return NotFound($"Folder with Id {id} not found");
            }

            _mapper.Map(model, folder);

            _folderService.Update(folder);
            await _folderService.Save();

            return Ok(_mapper.Map<FolderWithoutBmDto>(folder));
        }

        /// <summary>
        /// Creates a Folder
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post
        ///     {
        ///           "userId": "1,
        ///           "name": "Movie"
        ///     }
        ///
        /// </remarks>
        /// <returns> A new created User</returns>
        /// <response code="201">Returns the new created Folder</response>
        /// <response code="400">If userId was not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FolderWithoutBmDto>> CreateFolder(FolderCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (await _folderService.EntityExists(model.UserId))
            {
                return BadRequest($"User with this id:{model.UserId} was not found");
            }

            Folder folder = await _folderService.Create(_mapper.Map<Folder>(model));

            FolderWithoutBmDto folderDto = _mapper.Map<FolderWithoutBmDto>(folder);

            return CreatedAtAction("GetFolderWithoutBookmark", new { id = folderDto.Id }, folderDto);
        }

        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>       
        /// <response code="404">If folder by id not found</response>
        /// <response code="204">Folder deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFolder(long id)
        {
            if (!await _folderService.EntityExists(id))
            {
                return NotFound();
            }

            await _folderService.Delete(id);
            await _folderService.Save();

            return NoContent();
        }
    }
}