using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.FolderDto;
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

        [HttpGet("userId:{userId}/sortBy:{sortParam}")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<FolderWithoutBmDto>> GetFolder(long id)
        {
            var folder = await _folderService.GetyById(id);
            if (folder == null)
            {
                return NotFound($"Folder with Id {id} not found");
            }

            return _mapper.Map<FolderWithoutBmDto>(folder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FolderWithoutBmDto>> EditFolder(long id, FolderUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest("UserID mismatch");
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

        [HttpPost]
        public async Task<ActionResult<FolderWithoutBmDto>> CreateFolder(FolderCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (await _folderService.EntityExists(model.UserId))
            {
                return BadRequest("User ID mismatch");
            }

            Folder folder = await _folderService.Create(_mapper.Map<Folder>(model));

            var folderDto = _mapper.Map<FolderWithoutBmDto>(folder);

            return CreatedAtAction("GetFolder", new { id = folderDto.Id }, folderDto);
        }

        [HttpDelete("{id}")]
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
