using System;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class IdeaViewDto 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        
        public ApplicationUserViewDto Author { get; set; }

        public DateTime CreateOn { get; set; }

        public int Rating { get; set; }

    }
}