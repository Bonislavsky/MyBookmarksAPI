using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Domain.DtoModel.FolderDto;
using MyBookmarksAPI.Domain.TDOModel;
using MyBookmarksAPI.Service.Implementation;
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
        //11 89 752

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
    }
}
