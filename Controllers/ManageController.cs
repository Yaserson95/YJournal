using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using YJournal.Models;
using System.Security.Claims;
using System.Collections.Generic;
//using YJournal.Classes;

namespace YJournal.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private DbJournal db = new DbJournal();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private void SignInUser(string username, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index() {
            if (User.Identity.IsAuthenticated)
            {
                //var Users = db.User.Find(User.Identity.Name);
                return View();
            }
            else return RedirectToAction("index");
        }
        

        ////
        //// POST: /Manage/RemoveLogin
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        ////{
        ////    ManageMessageId? message;
        ////    var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        ////    if (result.Succeeded)
        ////    {
        ////        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        ////        if (user != null)
        ////        {
        ////            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        ////        }
        ////        message = ManageMessageId.RemoveLoginSuccess;
        ////    }
        ////    else
        ////    {
        ////        message = ManageMessageId.Error;
        ////    }
        ////    return RedirectToAction("ManageLogins", new { Message = message });
        ////}

        ////
        //// GET: /Manage/AddPhoneNumber
        ////public ActionResult AddPhoneNumber()
        ////{
        ////    return View();
        ////}

        ////
        //// POST: /Manage/AddPhoneNumber
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        ////{
        ////    if (!ModelState.IsValid)
        ////    {
        ////        return View(model);
        ////    }
        ////    // Создание и отправка маркера
        ////    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
        ////    if (UserManager.SmsService != null)
        ////    {
        ////        var message = new IdentityMessage
        ////        {
        ////            Destination = model.Number,
        ////            Body = "Ваш код безопасности: " + code
        ////        };
        ////        await UserManager.SmsService.SendAsync(message);
        ////    }
        ////    return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        ////}

        ////
        //// POST: /Manage/EnableTwoFactorAuthentication
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> EnableTwoFactorAuthentication()
        ////{
        ////    await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
        ////    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        ////    if (user != null)
        ////    {
        ////        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        ////    }
        ////    return RedirectToAction("Index", "Manage");
        ////}

        ////
        //// POST: /Manage/DisableTwoFactorAuthentication
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> DisableTwoFactorAuthentication()
        ////{
        ////    await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
        ////    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        ////    if (user != null)
        ////    {
        ////        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        ////    }
        ////    return RedirectToAction("Index", "Manage");
        ////}

        ////
        //// GET: /Manage/VerifyPhoneNumber
        //public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        //{
        //    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
        //    // Отправка SMS через поставщик SMS для проверки номера телефона
        //    return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        //}

        ////
        //// POST: /Manage/VerifyPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
        //    }
        //    // Это сообщение означает наличие ошибки; повторное отображение формы
        //    ModelState.AddModelError("", "Не удалось проверить телефон");
        //    return View(model);
        //}

        //
        // POST: /Manage/RemovePhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RemovePhoneNumber()
        //{
        //    var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
        //    if (!result.Succeeded)
        //    {
        //        return RedirectToAction("Index", new { Message = ManageMessageId.Error });
        //    }
        //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    }
        //    return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        //}

        //
        // GET: /Manage/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult ChangePassword(ChangePasswordViewModel model){
            
            if (!User.Identity.IsAuthenticated) return RedirectToRoute("index");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.NewPassword == model.OldPassword) {
                ModelState.AddModelError(string.Empty,"Новый пароль не должен совпадать с текущим");
                return View();
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Новый пароль не совпадает с подтверждением");
                return View();
            }
            var UserCh =  db.Users.Where(p=>p.Email == User.Identity.Name);
            if (UserCh != null&& UserCh.Count()>0) {
                var changedUser = UserCh.First();
                if (model.OldPassword != changedUser.Password)
                {
                    ModelState.AddModelError(string.Empty, "Неверно введён текущий пароль");
                    return View();
                }
                else {
                    changedUser.Password = model.NewPassword;
                    db.Entry(changedUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
                
            }
            ModelState.AddModelError(string.Empty, "Пароль не изменён по дебильной ошибке");
            return View(model);
        }

        public ActionResult ChangeEmail() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeEmailModel model){
            if (!User.Identity.IsAuthenticated) return RedirectToRoute("index");
            var users = db.Users.Where(p => p.Email == User.Identity.Name);
            if (users.Count() > 0)
            {
                var chUser = users.First();
                if (chUser.Password == model.Password)
                {
                    if (chUser.Email == model.NewEmail) {
                        ModelState.AddModelError(string.Empty, "Новая электронная почта должна отличаться от текущей");
                        return View();
                    }
                    if(db.Users.Where(d => d.Email == model.NewEmail).Count() > 0)
                    {
                        ModelState.AddModelError(string.Empty, "Пользователь с такой электронной почтой уже существуют");
                        return View();
                    }
                    chUser.Email = model.NewEmail;
                    db.Entry(chUser).State = EntityState.Modified;
                    db.SaveChanges();
                    SignInUser(model.NewEmail,true);
                    return RedirectToAction("index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пароль введён неверно");
                    return View();
                }

            }
            else { return View(); }
        }
        //
        // GET: /Manage/SetPassword
        //public ActionResult SetPassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Manage/SetPassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //            if (user != null)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //            }
        //            return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
        //        }
        //        AddErrors(result);
        //    }

        //    // Это сообщение означает наличие ошибки; повторное отображение формы
        //    return View(model);
        //}

        ////
        //// GET: /Manage/ManageLogins
        //public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
        //        : message == ManageMessageId.Error ? "Произошла ошибка."
        //        : "";
        //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
        //    var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
        //    ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
        //    return View(new ManageLoginsViewModel
        //    {
        //        CurrentLogins = userLogins,
        //        OtherLogins = otherLogins
        //    });
        //}

        ////
        //// POST: /Manage/LinkLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
        //    return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        //}

        ////
        //// GET: /Manage/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //}

        public ActionResult ChangeData()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToRoute("index");
            var susers = db.GetChUserData(User.Identity.Name).ToList();
            if (susers.Count() != 1) return View();
            else return View(susers.First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeData(GetChUserData_Result model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Что-то неправильно!");
                return View();
                
            }
            if (User.Identity.IsAuthenticated)
            {
                var users = db.Users.Where(p => p.Email == User.Identity.Name);
                if (users.Count() != 1)
                {
                    ModelState.AddModelError(string.Empty, "Произошла ошибка: выйдите из своего аккаунта и заново войдите!");
                    return RedirectToRoute("index");
                }
                var ChangedUser = users.First();
                ChangedUser.UserName = model.Firstname;
                ChangedUser.Surname = model.Surname;
                ChangedUser.Phone = model.Phone;
                ChangedUser.Birth = model.Birth;
                db.Entry(ChangedUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");


            }
            else return RedirectToRoute("index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
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