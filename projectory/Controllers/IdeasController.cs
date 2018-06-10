using System.Web.Mvc;
using projectory.Common.StringConsts;
using projectory.DTO.Web.ViewDTO;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;

namespace projectory.Controllers
{
    [Authorize]
    public class IdeasController : Controller
    {
        private readonly IIdeaService _ideaService;
        public IdeasController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        // GET: Administration/Ideas
        public ActionResult Index()
        {
            var posts = _ideaService.GetAllPosts();
            return View(posts);
        }



        // GET: Administration/Ideas/Create
        public ActionResult Create()
        {
            var viewModel = _ideaService.CreatePostModel();
            return View(viewModel);
        }

        // POST: Administration/Ideas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CreateIdeaViewDto idea)
        {
            if (!ModelState.IsValid) return View(idea);
            _ideaService.AddCreatedPost(idea);
            return RedirectToAction(DefaultActions.Index);
        }

        // GET: Administration/Ideas/Edit
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            var post = _ideaService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var postViewModel = _ideaService.MapPost(post);
            return View(postViewModel);
        }

        // POST: Administration/Ideas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CreateIdeaViewDto idea)
        {
            if (ModelState.IsValid)
            {
                _ideaService.UpdatePost(idea);
                return RedirectToAction(DefaultActions.Index);
            }

            idea = _ideaService.CreatePostModel();
            return View(idea);
        }


        // GET: Administration/Ideas/Delete/5
        public ActionResult Delete(int id)
        {
            Idea post = _ideaService.Find(id);
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
            _ideaService.Delete(id);
            return RedirectToAction(DefaultActions.Index);
        }
    }
}