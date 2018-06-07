using System;
using System.Linq;
using projectory.Common.StringConsts;
using projectory.DbContext;

namespace projectory.Services.Extensions
{
    public class ImageProvider
    {
        public static string Avatar(string id)
        {
            string imgsrc;
            var dbContext = new RepoDbContext();
            var avatar = dbContext.UserAvatars.SingleOrDefault(x => x.UserId == id);
            if (avatar == null)
            {
                imgsrc = FilePaths.Avatarpath;
            }
            else
            {
                var base64 = Convert.ToBase64String(avatar.Image);
                imgsrc = string.Format(StringFormats.ImageFormat, base64);
            }
            return imgsrc;
        }
    }
}
