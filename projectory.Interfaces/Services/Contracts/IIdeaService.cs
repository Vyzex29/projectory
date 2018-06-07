using System.Collections.Generic;
using projectory.DTO.Web.ViewDTO;
using projectory.Models.repository;
using Projectory.enums;

namespace projectory.Interfaces.Services.Contracts
{
    public interface IIdeaService 
    {
        
        InfoPostViewDto SpecificPost(int id);
        CreateIdeaViewDto CreatePostModel();
        void AddCreatedPost(CreateIdeaViewDto idea);
        void UpdatePost(CreateIdeaViewDto idea);
        CreateIdeaViewDto MapPost(Idea post);
        void Rate(int postId,RatingType type);
        IEnumerable<IdeaViewDto> GetLatestPosts(int count);
        IEnumerable<IdeaViewDto> GetLastCommentedPosts();
        IEnumerable<IdeaViewDto> GetTopRankedPosts();
        IEnumerable<IdeaViewDto> GetAllPosts();
        IEnumerable<IdeaViewDto> Search(string searchCriteria);
        IndexViewDto GetIndexViewModel();
        IndexViewDto SearchIndexViewDto(string search);
        Idea Find(int id);
        void Delete(int id);

    }
}
