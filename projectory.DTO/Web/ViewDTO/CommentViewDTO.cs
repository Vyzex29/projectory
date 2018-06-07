using System;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class CommentViewDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ApplicationUserViewDto Author { get; set; }

        public DateTime CreateOn { get; set; }
        public bool IsBlocked { get; set; }
    }
}
