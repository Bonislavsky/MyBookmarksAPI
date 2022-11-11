using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.TDOModel
{
    public class BookmarkTDO
    {
        public int Id { get; set; }
        public DateTime DateСreation { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public string Url { get; set; }

        public long FolderId { get; set; }
    }
}
