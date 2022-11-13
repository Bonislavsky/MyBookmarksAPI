using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.TDOModel
{
    public class FolderDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }

        public List<BookmarkDto> BookMarks { get; set; }
    }
}
