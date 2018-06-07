using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using projectory.Common.StringConsts;
using projectory.DTO.Web.ViewDTO;
using projectory.Interfaces.Facades;
using projectory.Interfaces.Repositories;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;
using Projectory.enums;

namespace projectory.Services.Contracts
{


    public class IdeaService : IIdeaService
    {
        private readonly IGenericRepository<Idea> _ideaRepo;
        private readonly IGenericRepository<Rating> _ratingRepo;
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly IGenericRepository<ApplicationUser> _userRepo;
        private readonly IUserIdentityFacade _identityFacade;

        public IdeaService(IGenericRepository<Rating> ratingRepo,
            IGenericRepository<Idea> ideaRepo,
            IGenericRepository<Comment> commentRepo,
            IGenericRepository<ApplicationUser> userRepo,
            IUserIdentityFacade identityFacade)
        {
            _ratingRepo = ratingRepo;
            _ideaRepo = ideaRepo;
            _commentRepo = commentRepo;
            _userRepo = userRepo;
            _identityFacade = identityFacade;
        }

        public IEnumerable<IdeaViewDto> GetAllPosts()
        {
            var dbPosts = GetAll();
            var postsViewModels = Mapper.Map<IEnumerable<Idea>, IEnumerable<IdeaViewDto>>(dbPosts.ToList());
            return postsViewModels;
        }

        public IEnumerable<IdeaViewDto> Search(string searchCriteria)
        {
            var dbPosts = GetAll().Where(t => t.Title.Contains(searchCriteria)).ToList();
            var postViewModels = Mapper.Map<IEnumerable<Idea>, IEnumerable<IdeaViewDto>>(dbPosts);
            return postViewModels;
        }

        public IndexViewDto GetIndexViewModel()
        {
            var indexViewModel = new IndexViewDto
            {
                AllPosts = GetAllPosts(),
                LastCommentedPosts = GetLastCommentedPosts(),
                LastPosts = GetLatestPosts(WidgetSettings.LatestPostsCount),
                TopRatedPosts = GetTopRankedPosts()
            };
            return indexViewModel;
        }

        public IndexViewDto SearchIndexViewDto(string search)
        {
            var indexViewModel = new IndexViewDto
            {
                AllPosts = Search(search),
                LastCommentedPosts = GetLastCommentedPosts(),
                LastPosts = GetLatestPosts(WidgetSettings.LatestPostsCount),
                TopRatedPosts = GetTopRankedPosts()
            };
            return indexViewModel;
        }

        public Idea Find(int id)
        {
            var idea=_ideaRepo.Find(id);
            return idea;
        }

        public void Delete(int id)
        {
            _ideaRepo.Delete(id);
        }

        public IEnumerable<IdeaViewDto> GetLatestPosts(int count)
        {
            var dbPosts = GetAll().Take(count);
            var postsViewModels = Mapper.Map<IEnumerable<Idea>, IEnumerable<IdeaViewDto>>(dbPosts.ToList());
            return postsViewModels;
        }

        public IEnumerable<IdeaViewDto> GetTopRankedPosts()
        {
            var dbPosts = GetAll().OrderByDescending(p => p.Rating).Take(WidgetSettings.TopRankedPostsCount);
            var postsViewModels = Mapper.Map<IEnumerable<Idea>, IEnumerable<IdeaViewDto>>(dbPosts.ToList());
            return postsViewModels;
        }
        public IEnumerable<IdeaViewDto> GetLastCommentedPosts()
        {
            var latestCommentsList = _commentRepo.All().
                OrderByDescending(p => p.CreateOn).Select(p => p.PostId).Distinct().
                Take(WidgetSettings.LastCommentedPostsCount).ToList();
            var dbPosts = LatestCommentPosts(latestCommentsList);
            var postsViewModels = Mapper.Map<IEnumerable<Idea>, IEnumerable<IdeaViewDto>>(dbPosts);
            return postsViewModels;
        }

        private IEnumerable<Idea> LatestCommentPosts(IEnumerable<int> latestCommentsPostIdList)
        {
            var dbPosts = new List<Idea>();
            foreach (var id in latestCommentsPostIdList)
            {
                var post = _ideaRepo.Find(id);
                if (dbPosts.Contains(post))
                {
                    continue;
                }
                dbPosts.Add(post);
            }

            return dbPosts;
        }

        public InfoPostViewDto SpecificPost(int id)
        {
            var comments =_commentRepo.All().Where(x => x.PostId == id && !x.IsBlocked).OrderByDescending(x=>x.CreateOn).ToList();
            var post = _ideaRepo.Find(id);
            var infoPost = new InfoPostViewDto
            {
                IdeaDto = Mapper.Map<IdeaViewDto>(post),
                Comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewDto>>(comments),
                CreateComment = new CreateCommentViewDto()
            };
            return infoPost;
        }

        public CreateIdeaViewDto CreatePostModel()
        {
            var createPostModel = new CreateIdeaViewDto();
            return createPostModel;
        }

        public void AddCreatedPost(CreateIdeaViewDto idea)
        {
            idea.AuthorId = _identityFacade.GetUserId();
            idea.CreateOn = DateTime.Now;
            var dbPost = Mapper.Map<Idea>(idea);
            _ideaRepo.Add(dbPost);
        }

        public void UpdatePost(CreateIdeaViewDto idea)
        {
            var dbPost = Mapper.Map<Idea>(idea);
            _ideaRepo.Update(dbPost);
        }



        public CreateIdeaViewDto MapPost(Idea post)
        {
            var postViewModel = Mapper.Map<CreateIdeaViewDto>(post);
            return postViewModel;
        }

        public void Rate(int postId, RatingType type)
        {
            var userId = _identityFacade.GetUserId();
            var post = _ideaRepo.Find(postId);
            var findRate = _ratingRepo.GetDbSet().SingleOrDefault(x => x.IdeaId == postId && x.UserId == userId);
            if (findRate == null)
            {
                AddRating(userId, postId, type, post);
            }
            else
            {
                UpdateRating(findRate, post, type);

            }

        }



        private void UpdateRating(Rating rate, Idea post, RatingType type)
        {
            if (rate.Type == type)
            {
                post.Rating = rate.Type == RatingType.Like ? --post.Rating : ++post.Rating;
                _ratingRepo.Delete(rate);
                _ideaRepo.Update(post);
            }
            else
            {
                post.Rating = rate.Type == RatingType.Like ? post.Rating - 2 : post.Rating + 2;
                rate.Type = type;
                _ratingRepo.Update(rate);
                _ideaRepo.Update(post);

            }
        }

        private void AddRating(string userId, int ideaId, RatingType type, Idea idea)
        {
            var ratingDb = new Rating
            {
                UserId = userId,
                IdeaId = ideaId,
                Idea = idea,
                User = _userRepo.Find(userId),
                Type = type
            };
            idea.Rating = type == RatingType.Like ? ++idea.Rating : --idea.Rating;
            _ratingRepo.Add(ratingDb);
            _ideaRepo.Update(idea);
        }


        public IQueryable<Idea> GetAll()
        {
            return _ideaRepo.All().OrderByDescending(p => p.CreatedOn);
        }

    }
}
