using projectory.DTO.Web.ViewDTO;
using projectory.Models.repository;

namespace projectory.Interfaces.Services.Contracts
{
    public interface ICommentService 
    {
        void AddCreatedComment(CreateCommentViewDto comment, int ideaId);
        CreateCommentViewDto MapComment(Comment comment);
        void EditComment(CreateCommentViewDto post);
        Comment Find(int id);
        void Update(Comment comment);
        void Delete(int id);
    }
}
