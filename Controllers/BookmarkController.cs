using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel;
using MyBookmarksAPI.Domain.DtoModel.FolderDtoModel;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IMapper _mapper;

        public BookmarkController(IBookmarkService bookmarkService, IMapper mapper)
        {
            _bookmarkService = bookmarkService;
            _mapper = mapper;
        }

        [HttpGet("folderId:{folderId}/sortBy:{sortParam}")]
        public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetBookmarks(long folderId, string sortParam = "Id", bool isDec = false)
        {
            if (!await _bookmarkService.FolderExists(folderId))
            {
                return NotFound($"Folder with Id {folderId} not found");
            }

            if (typeof(BookmarkDto).GetProperty(sortParam) is null)
            {
                return BadRequest($"\"{sortParam}\" parametr is not found");
            }

            return Ok(_mapper.Map<List<BookmarkDto>>(await _bookmarkService.GetBookmarkList(folderId, sortParam, isDec ? "DESC" : "ASC")));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkDto>> GetBookmark(long id)
        {
            var bookmark = await _bookmarkService.GetyById(id);
            if (bookmark == null)
            {
                return NotFound($"Bookmark with Id {id} not found");
            }

            return Ok(_mapper.Map<BookmarkDto>(bookmark));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FolderWithoutBmDto>> EditBookmark(long id, BookmarkUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest("Bookmark ID mismatch");
            }

            Bookmark bookmark = await _bookmarkService.GetyById(id);

            if (bookmark == null)
            {
                return NotFound($"Bookmark with Id {id} not found");
            }

            _mapper.Map(model, bookmark);

            _bookmarkService.Update(bookmark);
            await _bookmarkService.Save();

            return Ok(_mapper.Map<FolderWithoutBmDto>(bookmark));
        }
    }
}
