using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using projectory.Interfaces.Facades;
using projectory.UserIdentity;

namespace projectory.Facades.Identity
{
    [ExcludeFromCodeCoverage]
    public class AplicationSignInManagerFacade : IApplicationSignInManagerFacade
    {
        private readonly IOwinContext _context;
        public AplicationSignInManagerFacade()
        {
            _context = HttpContext.Current.GetOwinContext();
        }
        public ApplicationSignInManager GetApplicationSignInManager()
        {
            return _context.Get<ApplicationSignInManager>();

        }
    }
}
