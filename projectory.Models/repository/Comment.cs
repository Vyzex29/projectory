using System;
using System.ComponentModel;

namespace projectory.Models.repository
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
        public virtual Idea Idea { get; set; }
        public DateTime CreateOn { get; set; }
        public int PostId { get; set; }
        [DefaultValue(false)]
        public bool IsBlocked { get; set; }
    }
}
