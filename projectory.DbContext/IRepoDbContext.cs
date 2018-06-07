using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using projectory.Models.repository;

namespace projectory.DbContext
{
    public interface IRepoDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<Idea> Ideas { get; set; }
        IDbSet<UserAvatar> UserAvatars { get; set; }
        IDbSet<Comment> Comments { get; set; }
        IDbSet<Rating> Ratings { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        int SaveChanges();
    }
}
