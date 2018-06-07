using System.Linq;
using projectory.Interfaces.Repositories;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;

namespace projectory.Services.Contracts
{
    public class AvatarService : IAvatarService
    {
        private readonly IGenericRepository<UserAvatar> _avatarRepo;
        public AvatarService(IGenericRepository<UserAvatar> avatarRepo)
        {
            _avatarRepo = avatarRepo;
        }

        public void Add(UserAvatar entity)
        {
            var avatar = _avatarRepo.GetDbSet().SingleOrDefault(x => x.UserId == entity.UserId);
            if (avatar == null)
            {
                _avatarRepo.Add(entity);
            }
            else
            {
                avatar.Image = entity.Image;
                _avatarRepo.Update(avatar);
            }
            
        }
    }
}
