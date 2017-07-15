using JFB.Business.Domain.Info;
using JFB.Business.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace JFB.Wx.Component
{
    public class Authentication
    {
        public const string UserSessionKey = "yc_user_info";
        static Authentication()
        {
        }
        static UserService omService = new UserService();
        private static Authentication instance = new Authentication();
        public static Authentication Instance
        {
            get
            {
                return instance;
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                if (!IsLogin)
                {
                    return null;
                }
                return HttpContext.Current.Session[UserSessionKey] as UserInfo;
                int uid;
                if (int.TryParse(HttpContext.Current.User.Identity.Name, out uid))
                {
                    if (HttpContext.Current.Session[UserSessionKey] == null )
                    { 
                        SetSession(uid);
                    }
                    else if (uid > 0)
                    {
                        var ut = HttpContext.Current.Session[UserSessionKey] as UserInfo;
                        if (ut == null || ut.ID != uid)
                        {
                            SetSession(uid);

                        }
                    }
                    return HttpContext.Current.Session[UserSessionKey] as UserInfo;
                }
                return null;
            }
        }

        public bool IsLogin
        {
            get
            {
                return HttpContext.Current.Session[UserSessionKey] != null;
                //return HttpContext.Current.Request.IsAuthenticated;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session[UserSessionKey] = null;
            HttpContext.Current.Session.Clear();
         
        }

        public void SetAuth(UserInfo user, bool isPersistent)
        {
            long uid = user.ID;
            FormsAuthentication.SetAuthCookie(uid.ToString(), isPersistent);
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(uid.ToString(), isPersistent);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, user.IsValid.ToString());
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);

            SetSession(uid);
        }

        public void RefreshSession(long userId)
        {
            UserInfo user = omService.GetById(userId);
            HttpContext.Current.Session[UserSessionKey] = user;            
        }



        /// <summary>
        ///保存用户状态
        /// </summary>
        /// <param name="userId"></param>
        void SetSession(long userId)
        {
            bool flag = false;
            if (HttpContext.Current.Session == null)
            {
                throw new ArgumentNullException("SessionState Failed");
            }

            if (HttpContext.Current.Session[UserSessionKey] == null)
            {
                flag = true;
            }
            else
            {
                UserInfo u = HttpContext.Current.Session[UserSessionKey] as UserInfo;
                if (u.ID != userId) flag = true;
            }

            if (flag)
            {
                UserInfo u = omService.GetById(userId);
                if (u == null)
                {
                    if (System.Web.HttpContext.Current.Request.Path != null && !System.Web.HttpContext.Current.Request.Path.ToLower().Contains("user/logout"))
                        System.Web.HttpContext.Current.Response.Redirect("/user/logout");
                    return;
                }
                HttpContext.Current.Session[UserSessionKey] = u;
            }
        }
    }
}
