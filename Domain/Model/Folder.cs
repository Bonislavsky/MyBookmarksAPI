using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Model
{
    public class Folder : EntityBase
    {
        [MaxLength(20)]
        public string Name { get; set; }

        //Navigation Properties
        public long UserId { get; set; }
        public User User { get; set; }

        public List<Bookmark> BookMarks { get; set; }
    }
}
