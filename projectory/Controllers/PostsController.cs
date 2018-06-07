using System.Web.Mvc;
using projectory.Common.StringConsts;
using projectory.DTO.Web.ViewDTO;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;

namespace projectory.Controllers
{
    [Authorize(Roles = UserRoles.AdminAndModeratorAndEditor)]
    public class PostsController : Controller
    {
        private readonly IIdeaService _postService;
        public PostsController(IIdeaService postService)
        {
            _postService = postService;
        }

        // GET: Administration/Posts
        public ActionResult Index()
        {
            var posts = _postService.GetAllPosts();
            return View(posts);
        }



        // GET: Administration/Posts/Create
        public ActionResult Create()
        {
            var viewModel = _postService.CreatePostModel();
            return View(viewModel);
        }

        // POST: Administration/Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CreateIdeaViewDto post)
        {
            if (!ModelState.IsValid) return View(post);
            _postService.AddCreatedPost(post);
            return RedirectToAction(DefaultActions.Index);
        }

        // GET: Administration/Posts/Edit
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            var post = _postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var postViewModel = _postService.MapPost(post);
            return View(postViewModel);
        }

        // POST: Administration/Posts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CreateIdeaViewDto post)
        {
            if (ModelState.IsValid)
            {
                _postService.UpdatePost(post);
                return RedirectToAction(DefaultActions.Index);
            }

            post = _postService.CreatePostModel();
            return View(post);
        }


        // GET: Administration/Posts/Delete/5
        public ActionResult Delete(int id)
        {
            Idea post = _postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _postService.Delete(id);
            return RedirectToAction(DefaultActions.Index);
        }
    }
}