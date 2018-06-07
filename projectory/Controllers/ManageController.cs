using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using projectory.Common.StringConsts;
using projectory.Interfaces.Facades;
using projectory.Interfaces.Services.Contracts;
using projectory.Models;
using projectory.Models.repository;
using projectory.UserIdentity;

namespace projectory.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
      
        private readonly IAvatarService _avatarService;
        private readonly IUserIdentityFacade _identityFacade;
        private readonly IApplicationUserManagerFacade _applicationUserManager;
        private readonly IApplicationSignInManagerFacade _applicationSignInManager;
        public ManageController(IAvatarService avatarService,
            IUserIdentityFacade identityFacade,
            IApplicationUserManagerFacade applicationUserManager,
            IApplicationSignInManagerFacade applicationSignInManager)
        {
            _avatarService = avatarService;
            _identityFacade = identityFacade;
            _applicationUserManager = applicationUserManager;
            _applicationSignInManager=applicationSignInManager;
        }


        public ApplicationSignInManager SignInManager => _applicationSignInManager.GetApplicationSignInManager();

        public ApplicationUserManager UserManager => _applicationUserManager.GetApplicationUserManager();

        //
        // GET: /Manage/Index
        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                HasPassword = HasPassword()
            };
            return View(model);
        }
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = _identityFacade.GetUserId();
            var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction(DefaultActions.Index, new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        // GET: /Manage/ChangeAvatar
        public ActionResult ChangeAvatar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAvatar(HttpPostedFileBase imageFile)
        {
            if (imageFile == null) return View();
            var userAvatar = new UserAvatar
            {
                UserId = _identityFacade.GetUserId(),
                Image = new byte[imageFile.ContentLength]
            };
            imageFile.InputStream.Read(userAvatar.Image, 0, imageFile.ContentLength);
            _avatarService.Add(userAvatar);

            return RedirectToAction(DefaultActions.Index);
        }

 

        #region Helpers


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var userId = _identityFacade.GetUserId();
            var user = UserManager.FindById(userId);
            return user?.PasswordHash != null;
        }



        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}