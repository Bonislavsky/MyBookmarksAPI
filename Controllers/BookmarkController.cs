using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System;
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
        public async Task<ActionResult<BookmarkDto>> EditBookmark(long id, BookmarkUpdateDto model)
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

            return Ok(_mapper.Map<BookmarkDto>(bookmark));
        }

        [HttpPost]
        public async Task<ActionResult<BookmarkDto>> CreateFolder(BookmarkCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (await _bookmarkService.EntityExists(model.FolderId))
            {
                return BadRequest("Folder ID mismatch");
            }

            if (!Uri.IsWellFormedUriString(model.Url, UriKind.Absolute))
            {
                return BadRequest($"incorrect url: {model.Url}");
            }

            Bookmark bookmark = await _bookmarkService.Create(_mapper.Map<Bookmark>(model));

            BookmarkDto bookmarkDto = _mapper.Map<BookmarkDto>(bookmark);

            return CreatedAtAction("GetBookmark", new { id = bookmarkDto.Id }, bookmarkDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(long id)
        {
            if (!await _bookmarkService.EntityExists(id))
            {
                return NotFound();
            }

            await _bookmarkService.Delete(id);
            await _bookmarkService.Save();

            return NoContent();
        }
    }
}
