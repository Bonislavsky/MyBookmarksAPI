using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Model
{
    public class Bookmark : EntityBase
    {
        public DateTime DateСreation { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        //Navigation Properties
        public long FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
