using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.Concrete;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaglarDurmus.BackOffice.WebUI.Helpers
{
    public static class SystemUserHelper
    {

        private static string UserSessionKey = "Session_User";
        private static string DefaultPageSessionKey = "Session_DefaultPage";
        private static string RedirectPageSessionKey = "Session_RedirectPage";

        public static bool HasLogin
        {
            get
            {
                return System.Web.HttpContext.Current.Session[UserSessionKey] != null;
            }
        }
        public static int? CurrentUserId
        {
            get
            {
                return HasLogin ? (int?)CurrentUser.Id : null;

            }
        }
        public static User CurrentUser
        {
            get
            {
                if (HasLogin)
                    return (System.Web.HttpContext.Current.Session[UserSessionKey] as User);
                else
                    return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session[UserSessionKey] = value;
            }
        }

        public static void LoginUser(int userId)
        {
            var user = InstanceFactory.GetInstance<IUserService>().GetById(userId);

            CurrentUser = user;
            System.Web.HttpContext.Current.Session.Timeout = 30;
        }
        public static void LogoutUser()
        {
            System.Web.HttpContext.Current.Session.Clear();
        }
        public static string DefaultPage
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[DefaultPageSessionKey] != null)
                    return System.Web.HttpContext.Current.Session[DefaultPageSessionKey].ToString();

                if (!HasLogin)
                {
                    return "Login/Index";
                }
                else
                {
                    DefaultPage = "Products/Index";
                }

                return DefaultPage;
            }
            set
            {
                System.Web.HttpContext.Current.Session[DefaultPageSessionKey] = value;
            }
        }
        public static string RedirectPage
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[RedirectPageSessionKey] != null)
                    return System.Web.HttpContext.Current.Session[RedirectPageSessionKey].ToString();
                else
                    return DefaultPage;
            }
            set
            {
                System.Web.HttpContext.Current.Session[RedirectPageSessionKey] = value;
            }
        }


    }
}