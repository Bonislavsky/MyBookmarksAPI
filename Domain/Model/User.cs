using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Model
{
    public class User : EntityBase
    {
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(45)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public byte[] Password { get; set; }

        [DataType(DataType.Password)]
        public byte[] Salt { get; set; }


        //Navigation Properties
        public List<Folder> Folders { get; set; }
    }
}
