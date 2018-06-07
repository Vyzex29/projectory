using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using projectory.DbContext;
using projectory.DTO.Web.ViewDTO;
using projectory.Interfaces.Repositories;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;
using projectory.UserIdentity;

namespace projectory.Services.Contracts
{
    public class RolesService : IRolesService
    {
        private readonly IGenericRepository<ApplicationUser> _users;
        private readonly IRepoDbContext _context;
        private readonly IGenericRepository<IdentityRole> _roleRepository;

        public RolesService(IGenericRepository<IdentityRole> roleRepository,
            IGenericRepository<ApplicationUser> users,
            IRepoDbContext context
            )
        {
            _users = users;
            _context = context;
            _roleRepository = roleRepository;
        }

        public ManageRolesViewDto GetManageRoles()
        {
            var viewModel = new ManageRolesViewDto();
            var users = _users.GetDbSet();
            viewModel.Users = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserRoleViewDto>>(users.ToList());
            foreach (var user in viewModel.Users)
            {
                user.Roles = GetUserRoles(user.Id);
            }
            viewModel.Roles = _roleRepository.All().OrderBy(r => r.Name).ToList().Select(rr =>
                new SelectListItem
                {
                    Value = rr.Name.ToString(),
                    Text = rr.Name
                }).ToList();

            return viewModel;
        }

        public void AddRoleToUser(string userId, string roleName)
        {

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>((System.Data.Entity.DbContext)_context));
            manager.AddToRole(userId, roleName);
        }

        public IList<string> GetUserRoles(string userId)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>((System.Data.Entity.DbContext)_context));
            var rolesForThisUser = manager.GetRoles(userId);
           return rolesForThisUser;
        }
        public void DeleteRoleFromUser(string userId, string roleName)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>((System.Data.Entity.DbContext)_context));
            if (manager.IsInRole(userId, roleName))
            {
                manager.RemoveFromRole(userId, roleName);
            }
        }

    }
}
