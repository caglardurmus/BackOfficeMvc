using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CaglarDurmus.BackOffice.WebUI.Controllers
{
    public class AuthenticationController : BaseController
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            this.SetAuthenticationRequired(false);
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            this.SetPageTitle(string.Join(" | ", "Kullanıcı Girişi", this.ApplicationName));
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            var _userService = InstanceFactory.GetInstance<IUserService>();
            var user = _userService.GetUser(userName);
            if (user == null)
            {
                return this.RedirectWithAlertMessage("Kullanıcı Adı Hatalı", "Login");
            }
            else
            {
                if (user != null && user.Password == password)
                {
                    SystemUserHelper.LoginUser(user.Id);
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    return this.RedirectWithAlertMessage("Şifre Hatalı", "Login");
                }
            }
        }

        public ActionResult Logout()
        {
            SystemUserHelper.LogoutUser();
            return RedirectToAction("Login");
        }
    }
}