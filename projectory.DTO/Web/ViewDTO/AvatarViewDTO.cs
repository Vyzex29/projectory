using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class AvatarViewDto
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Avatar")]
        public byte[] Image { get; set; }

    }
}
