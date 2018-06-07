using System;
using System.ComponentModel;

namespace projectory.Models.repository
{
    public class Idea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        [DefaultValue(0)]
        public int Rating { get; set; }
    }
}
