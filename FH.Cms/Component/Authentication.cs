using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using JFB.Systems.Domain.Service;
using JFB.Systems.Domain.Info;

namespace JFB.Cms.Component
{
    public class Authentication
    {
        public const string UserSessionKey = "jfb_user_info";
        static Authentication()
        {
        }
        static OperatorService omService = new OperatorService();
        private static Authentication instance = new Authentication();

        public static Authentication Instance
        {
            get
            {
                return instance;
            }
        }

        public OperatorInfo CurrentUser
        {
            get
            {
                if (!IsLogin)
                {
                    return null;
                }
                int uid;
                if (int.TryParse(HttpContext.Current.User.Identity.Name, out uid))
                {
                    if (HttpContext.Current.Session[UserSessionKey] == null )
                    { 
                        SetSession(uid);
                    }
                    else if (uid > 0)
                    {
                        var ut = HttpContext.Current.Session[UserSessionKey] as OperatorInfo;
                        if (ut == null || ut.Id != uid)
                        {
                            SetSession(uid);

                        }
                    }
                    return HttpContext.Current.Session[UserSessionKey] as OperatorInfo;
                }
                return null;
            }
        }

        public bool IsLogin
        {
            get
            {
                return HttpContext.Current.Request.IsAuthenticated;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session[UserSessionKey] = null;
            HttpContext.Current.Session.Clear();
         
        }

        public void SetAuth(OperatorInfo user, bool isPersistent)
        {
            int uid = user.Id;
            FormsAuthentication.SetAuthCookie(uid.ToString(), isPersistent);
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(uid.ToString(), isPersistent);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, user.Isvalid.ToString());
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);

            SetSession(uid);
        }

        public void RefreshSession(int userId)
        {
            OperatorInfo user = omService.GetById(userId);
            HttpContext.Current.Session[UserSessionKey] = user;            
        }



        /// <summary>
        ///保存用户状态
        /// </summary>
        /// <param name="userId"></param>
        void SetSession(int userId)
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
                OperatorInfo u = HttpContext.Current.Session[UserSessionKey] as OperatorInfo;
                if (u.Id != userId) flag = true;
            }

            if (flag)
            {
                OperatorInfo u = omService.GetById(userId);
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
