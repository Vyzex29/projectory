using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace projectory.DTO.Web.ViewDTO
{
    [ExcludeFromCodeCoverage]
    public class ApplicationUserRoleViewDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
