using System.Collections.Generic;
using projectory.DTO.Web.ViewDTO;

namespace projectory.Interfaces.Services.Contracts
{
    public interface IRolesService 
    {
        ManageRolesViewDto GetManageRoles();
        void AddRoleToUser(string userId, string roleName);
        IList<string> GetUserRoles(string userId);
        void DeleteRoleFromUser(string userId, string roleName);
    }
}
