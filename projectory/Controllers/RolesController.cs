using System.Web.Mvc;
using projectory.Common.StringConsts;
using projectory.Interfaces.Services.Contracts;

namespace projectory.Controllers
{
    
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }


        public ActionResult ManageUsers()
        {
            var manageRolesViewModel = _rolesService.GetManageRoles();
            return View(manageRolesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string id, string roleName)
        {
            _rolesService.AddRoleToUser(id, roleName);
            return RedirectToAction(RolesActions.ManageUsers);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string id, string roleName)
        {
            _rolesService.DeleteRoleFromUser(id, roleName);
          return RedirectToAction(RolesActions.ManageUsers);
        }
    }
}