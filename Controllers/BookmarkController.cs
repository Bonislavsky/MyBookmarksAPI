using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get a list of all bookmark
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return List bookmarks</response>
        /// <response code="404">If bookmarks by folderid not found</response>
        /// <response code="400">Sort parameter not found</response> 
        [HttpGet("folderId:{folderId}/sortBy:{sortParam}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Get a Bookmark by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return bookmark</response>
        /// <response code="404">If bookmarks by id not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookmarkDto>> GetBookmark(long id)
        {
            var bookmark = await _bookmarkService.GetyById(id);
            if (bookmark == null)
            {
                return NotFound($"Bookmark with Id {id} not found");
            }

            return Ok(_mapper.Map<BookmarkDto>(bookmark));
        }

        /// <summary>
        /// Changing the bookmark's data
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     id = 1
        ///     Put
        ///     {
        ///           "Id": 1,
        ///           "name": "nameexemple"
        ///           "url": "https://www.google.com/"
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

        /// <summary>
        /// Creates a bookmark
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post
        ///     {
        ///           "name": "harry potter",
        ///           "url": "https://www.google.com/",
        ///           "folderId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns> A new created Bookmark</returns>
        /// <response code="201">Returns the new created bookmark</response>
        /// <response code="404">If folder by folderId not found</response>
        /// <response code="400">If url incorrect</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookmarkDto>> CreateFolder(BookmarkCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (await _bookmarkService.EntityExists(model.FolderId))
            {
                return NotFound("Folder ID mismatch");
            }

            if (!Uri.IsWellFormedUriString(model.Url, UriKind.Absolute))
            {
                return BadRequest($"incorrect url: {model.Url}");
            }

            Bookmark bookmark = await _bookmarkService.Create(_mapper.Map<Bookmark>(model));

            BookmarkDto bookmarkDto = _mapper.Map<BookmarkDto>(bookmark);

            return CreatedAtAction("GetBookmark", new { id = bookmarkDto.Id }, bookmarkDto);
        }

        /// <summary>
        /// Delete bookmark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>       
        /// <response code="404">If bookmark by id not found</response>
        /// <response code="204">bookmark deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
