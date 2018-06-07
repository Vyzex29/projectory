using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class InfoPostViewDto
    {
        public IdeaViewDto IdeaDto { get; set; }
        public IEnumerable<CommentViewDto> Comments { get; set; }
        public CreateCommentViewDto CreateComment { get; set; }

    }
}
