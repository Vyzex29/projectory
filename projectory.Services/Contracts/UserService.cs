using projectory.Interfaces.Repositories;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;

namespace projectory.Services.Contracts
{
    
    public class UserService : IUserService
    {
        private readonly IGenericRepository<ApplicationUser> _userRepo;
        public UserService(IGenericRepository<ApplicationUser> userRepo)
        {
            _userRepo = userRepo;
        }

        public ApplicationUser Find(string id)
        {
            var user=_userRepo.Find(id);
            return user;
        }
    }
}
