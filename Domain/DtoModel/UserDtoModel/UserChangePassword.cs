using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.DtoModel.UserDtoModel
{
    public class UserChangePassword
    {
        [Required]
        public long Id { get; set; }

        [Required(ErrorMessage = "Вкажіть поточний пароль")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Вкажіть новий пароль")]
        [RegularExpression(@"[0-9a-zA-Z]{6,20}", ErrorMessage = "пароль може містити тільки латинські літери або цифри")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "довжина повинна бути від 6 до 40 символів")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Підтвердіть новий пароль")]
        [Compare("Password", ErrorMessage = "паролі не збігаются")]
        public string PasswordConfirme { get; set; }
    }
}
