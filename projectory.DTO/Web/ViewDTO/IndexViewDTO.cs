using System.Collections.Generic;

namespace projectory.DTO.Web.ViewDTO
{
    public class IndexViewDto
    {
        public IEnumerable<IdeaViewDto> AllPosts;
        public IEnumerable<IdeaViewDto> LastPosts;
        public IEnumerable<IdeaViewDto> TopRatedPosts;
        public IEnumerable<IdeaViewDto> LastCommentedPosts;
    }
}
