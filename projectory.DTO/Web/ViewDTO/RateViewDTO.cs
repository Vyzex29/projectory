using System.Diagnostics.CodeAnalysis;
using Projectory.enums;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class RateViewDto
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public RatingType Type { get; set; }
    }
}
