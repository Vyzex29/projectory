using Projectory.enums;

namespace projectory.Models.repository
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IdeaId { get; set; }
        public RatingType Type { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Idea Idea { get; set; }
    }
}
