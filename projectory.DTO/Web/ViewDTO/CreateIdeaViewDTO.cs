using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class CreateIdeaViewDto
    {
        public int Id { get; set; }

        [Display(Name = "Title content")]
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public ApplicationUserViewDto Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime? CreateOn { get; set; }

        public bool IsDeleted { get; set; }

        public int Rating { get; set; }
        
    }
}