using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using projectory.Interfaces.Facades;
using projectory.UserIdentity;


namespace projectory.Facades.Identity
{
    [ExcludeFromCodeCoverage]
   public class ApplicationUserManagerFacade: IApplicationUserManagerFacade
    {
        private readonly IOwinContext _context;
        public ApplicationUserManagerFacade()
        {
            _context = HttpContext.Current.GetOwinContext();
        }
      
        public ApplicationUserManager GetApplicationUserManager()
        {
            return _context.GetUserManager<ApplicationUserManager>();
        }
    }
}
