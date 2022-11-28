using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Model
{
    public class EntityBase 
    {
        [Key]
        public long Id { get; set; }
    }
}
