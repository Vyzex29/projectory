using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.AspNet.Identity;
using projectory.Interfaces.Facades;

namespace projectory.Facades.Identity
{
    [ExcludeFromCodeCoverage]
    public class UserIdentityFacade : IUserIdentityFacade
    {
        private readonly HttpContext _context;
        public UserIdentityFacade()
        {
            _context=HttpContext.Current;
            
        }
        public string GetUserId()
        {
            return _context.User.Identity.GetUserId();

        }
    }
}
