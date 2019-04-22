using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaglarDurmus.BackOffice.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public string ApplicationName = "BackOffice";

        private string redirectPage = string.Empty;
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (this.AuthenticationRequired(filterContext))
            {
                if (!SystemUserHelper.HasLogin)
                {
                    redirectPage = "/Authentication/Index";
                }
            }
            if (!string.IsNullOrWhiteSpace(redirectPage))
            {
                filterContext.Result = new RedirectResult(redirectPage);
            }
            base.OnActionExecuted(filterContext);
        }
        public void SetPageTitle(string value)
        {
            ViewBag.PageTitle = value;
        }
        public void SetAuthenticationRequired(bool value)
        {
            ViewBag.AuthenticationRequired = value;
        }

        public ActionResult RedirectWithAlertMessage(string message, string actionName, string controllerName, object values)
        {
            TempData["alertMessage"] = message;
            return RedirectToAction(actionName, controllerName, values);
        }

        public ActionResult RedirectWithAlertMessage(string message, string actionName)
        {
            return RedirectWithAlertMessage(message, actionName, null);
        }

        public ActionResult RedirectWithAlertMessage(string message, string actionName, object values)
        {
            return RedirectWithAlertMessage(message, actionName, null, values);
        }
        private bool AuthenticationRequired(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.ViewBag.AuthenticationRequired == null)
                return true;
            else
                return Convert.ToBoolean(filterContext.Controller.ViewBag.AuthenticationRequired);
        }

        protected string GetLink(string linkText, string action, string controller)
        {
            return HtmlHelper.GenerateLink(this.ControllerContext.RequestContext, System.Web.Routing.RouteTable.Routes, linkText, null, action, controller, null, null);
        }
    }
}