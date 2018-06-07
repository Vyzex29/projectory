using projectory.Models.repository;

namespace projectory.Interfaces.Services.Contracts
{
    public interface IUserService
    {
        ApplicationUser Find(string id);
    }
}
