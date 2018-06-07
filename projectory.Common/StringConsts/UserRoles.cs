using System.Diagnostics.CodeAnalysis;

namespace projectory.Common.StringConsts
{
    [ExcludeFromCodeCoverage]
    public static class UserRoles
    {
        public const string User = "user";
        public const string Editor = "editor";
        public const string Moderator = "moderator";
        public const string Admin = "admin";
        public const string AdminAndModeratorAndEditor = "admin,moderator,editor";
    }
}
