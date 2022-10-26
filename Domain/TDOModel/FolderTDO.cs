using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.TDOModel
{
    public class FolderTDO
    {
        public long Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public long UserId { get; set; }

        public List<BookmarkTDO> BookMarks { get; set; }
    }
}
