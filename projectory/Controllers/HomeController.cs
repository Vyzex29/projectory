using System.Web.Mvc;
using projectory.Common.StringConsts;
using projectory.DTO.Web.ViewDTO;
using projectory.Interfaces.Services.Contracts;
using Projectory.enums;

namespace projectory.Controllers
{



    public class HomeController : Controller
    {
        private readonly IIdeaService _ideaService;
        private readonly ICommentService _commentService;


        public HomeController(IIdeaService ideaService,
            ICommentService commentService
         )
        {
            _ideaService = ideaService;
            _commentService = commentService;
        }

        public ActionResult Index()
        {
            var postsViewModels = _ideaService.GetIndexViewModel();
            return View(postsViewModels);
        }

        public ActionResult Search(string search)
        {
            var postsViewModels = _ideaService.SearchIndexViewDto(search);
            return View(DefaultActions.Index, postsViewModels);
        }

        public ActionResult Info(int id)
        {
            var post = _ideaService.SpecificPost(id);
            return View(post);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(CreateCommentViewDto createComment, int postId)
        {
            if (!ModelState.IsValid) return RedirectToAction(HomeActions.Info, new { id = postId });
            _commentService.AddCreatedComment(createComment, postId);
            return RedirectToAction(HomeActions.Info, new { id = postId });

        }


        public ActionResult DeleteComment(int id)
        {
            var comment = _commentService.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int id, int postId)
        {
            _commentService.Delete(id);
            return RedirectToAction(HomeActions.Info, new { id = postId });
        }


        public ActionResult EditComment(int id)
        {
            var comment = _commentService.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            var commentViewModel = _commentService.MapComment(comment);
            return View(commentViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditComment(CreateCommentViewDto comment, int postId)
        {
            if (!ModelState.IsValid) return View(comment);
            _commentService.EditComment(comment);
            return RedirectToAction(HomeActions.Info, new { id = postId });

        }


        public ActionResult Rate(int ideaId, RatingType type)
        {
            _ideaService.Rate(ideaId, type);
            return RedirectToAction(HomeActions.Info, new { id = ideaId });
        }

    }
}