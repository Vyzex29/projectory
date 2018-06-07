using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class CreateCommentViewDto
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUserViewDto Author { get; set; }

        public DateTime CreateOn { get; set; }

        public int IdeaId { get; set; }
        public bool IsBlocked { get; set; }
    }
}