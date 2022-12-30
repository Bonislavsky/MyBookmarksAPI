using System.ComponentModel.DataAnnotations;

namespace MyBookmarksAPI.Domain.DtoModel.FolderDtoModel
{
    public class FolderUpdateDto
    {
        [Required(ErrorMessage = "Вкажіть Id")]
        public long Id { get; set; }


        [Required(ErrorMessage = "Вкажіть iм'я папки")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "довжина повинна бути від 1 до 25 символів")]
        public string Name { get; set; }
    }
}
