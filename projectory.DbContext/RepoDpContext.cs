using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using projectory.Common.StringConsts;
using projectory.Models.repository;

namespace projectory.DbContext
{
    public class RepoDbContext : IdentityDbContext, IRepoDbContext
    {
        public RepoDbContext()
            : base(DbConnections.Default)
        {
        }

        public IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<Idea> Ideas { get; set; }
        public IDbSet<UserAvatar> UserAvatars { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Rating> Ratings { get; set; }
        public override IDbSet<IdentityRole> Roles { get; set; }
        public static RepoDbContext Create()
        {
            return new RepoDbContext();
        }


    }
}
