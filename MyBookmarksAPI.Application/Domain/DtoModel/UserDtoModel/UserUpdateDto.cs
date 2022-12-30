using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.DtoModel.UserDtoModel
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Вкажіть Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Вкажіть електрону пошту")]
        [StringLength(45, MinimumLength = 5, ErrorMessage = "довжина повинна бути від 5 до 45 символів")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректна адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вкажіть Ім'я")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "довжина повинна бути від 2 до 25 символів")]
        public string Name { get; set; }
    }
}
