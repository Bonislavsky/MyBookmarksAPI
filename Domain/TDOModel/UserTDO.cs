using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.TDOModel
{
    public class UserTDO
    {
        public long Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public string Email { get; set; }
        public List<FolderTDO> Folders { get; set; }
    }
}
