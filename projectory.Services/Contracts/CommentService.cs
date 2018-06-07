using System;
using AutoMapper;
using projectory.DTO.Web.ViewDTO;
using projectory.Interfaces.Facades;
using projectory.Interfaces.Repositories;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;

namespace projectory.Services.Contracts
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly IUserIdentityFacade _identityFacade;
        public CommentService(IGenericRepository<Comment> commentRepo, IUserIdentityFacade identityFacade)
        {
            _commentRepo = commentRepo;
            _identityFacade = identityFacade;
        }

        public void AddCreatedComment(CreateCommentViewDto comment, int ideaId)
        {
            comment.CreateOn = DateTime.Now;
            comment.AuthorId = _identityFacade.GetUserId();
            comment.IdeaId = ideaId;
            comment.IsBlocked = false;
            var dbPost = Mapper.Map<Comment>(comment);
            _commentRepo.Add(dbPost);
        }

        public void EditComment(CreateCommentViewDto post)
        {
            var dbPost = Mapper.Map<Comment>(post);
            _commentRepo.Update(dbPost);
        }

        public Comment Find(int id)
        {
            var comment = _commentRepo.Find(id);
            return comment;
        }

        public void Update(Comment comment)
        {
            _commentRepo.Update(comment);
        }

        public void Delete(int id)
        {
            _commentRepo.Delete(id);
        }

        public CreateCommentViewDto MapComment(Comment comment)
        {
            var commentViewModel = Mapper.Map<CreateCommentViewDto>(comment);

            return commentViewModel;
        }



    }
}
