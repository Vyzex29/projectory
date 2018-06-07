using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class ManageRolesViewDto
    {
        public IEnumerable<ApplicationUserRoleViewDto> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
