using AutoMapper;
using projectory.DTO.Web.ViewDTO;
using projectory.Models.repository;

namespace projectory.Common.MapperConfig
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Idea, IdeaViewDto>();
            CreateMap<IdeaViewDto, Idea>();
            CreateMap<Idea, CreateIdeaViewDto>();
            CreateMap<CreateIdeaViewDto, Idea>();
            CreateMap<ApplicationUser, ApplicationUserViewDto>();
            CreateMap<ApplicationUserViewDto, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserRoleViewDto>();
            CreateMap<ApplicationUserRoleViewDto, ApplicationUser>();
            CreateMap<UserAvatar, AvatarViewDto>();
            CreateMap<AvatarViewDto, UserAvatar>();
            CreateMap<Comment, CreateCommentViewDto>();
            CreateMap<CreateCommentViewDto, Comment>();
            CreateMap<Comment, CommentViewDto>();
            CreateMap<CommentViewDto, Comment>();
            CreateMap<Rating, RateViewDto>();
            CreateMap<RateViewDto, Rating>();
        }
    }
}
