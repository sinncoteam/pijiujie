using JFB.Systems.Domain.Service;
using JFB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JFB.Cms.Component
{
    public class AuthLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            string caName = controlName + "." + actionName;
            //不排除，都需登录 V2.0.1(3-10)
            string[] unAuth = new string[] { "user.login", "user.logout", "user.loginx", "home.notice" };
            if (unAuth.Contains(caName))
            {
                return;
            }
            string[] authOp = new string[] { 
                //"home.index", "zcc.menu"
            };
            //BaseController controller = filterContext.Controller as BaseController;
            if (!Authentication.Instance.IsLogin)
            {
                setoAuth(filterContext, "请先登录！");
            }
            //else if (!authOp.Contains(caName))
            //{
            //    if (filterContext.HttpContext.Request.HttpMethod != "POST")
            //    {
            //        bool hasRights = false;
            //        OperatorService oms = new OperatorService();
            //        var mmlist = oms.GetOperatorModules(Authentication.Instance.CurrentUser.Id);
            //        foreach (var item in mmlist)
            //        {
            //            if (!string.IsNullOrEmpty(item.Controller)) { item.Controller = item.Controller.ToLower(); }
            //            if (!string.IsNullOrEmpty(item.Action)) { item.Action = item.Action.ToLower(); }
            //            if (item.Controller + "." + item.Action == controlName + "." + actionName)
            //            {
            //                hasRights = true;
            //                break;
            //            }
            //        }
            //        if (!hasRights)
            //        {
            //            setoAuth(filterContext, "你没有权限进行该操作！", 1);
            //        }
            //    }
            //}
            //if (!filterContext.HttpContext.Request.IsAjaxRequest() && !filterContext.IsChildAction)
            //{
            //    ModuleMenuMService mmService = new ModuleMenuMService();
            //    var mm = mmService.GetMIdByCAName(controlName, actionName);
            //    if (mm != null)
            //    {
            //        filterContext.Controller.ViewBag.RoleP = mm.Parentid;
            //        filterContext.Controller.ViewBag.RoleC = mm.Id;
            //    }
            //    else
            //    {
            //        filterContext.Controller.ViewBag.RoleP = 0;
            //        filterContext.Controller.ViewBag.RoleC = 0;
            //    }
            //    if (Authentication.Instance.CurrentUser != null)
            //    {
            //        filterContext.Controller.ViewBag.UserName = Authentication.Instance.CurrentUser.UserName;
            //    }
            //}
        }

        private void setoAuth(ActionExecutingContext filterContext, string msg, int type = 0)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JsonResult jr = new JsonResult();
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                jr.Data = new AjaxMsgResult() { Success = false, Msg = msg, Source = type };

                filterContext.Result = jr;
            }
            else if (filterContext.IsChildAction)
            {
                filterContext.Result = new ContentResult() { Content = msg };
            }
            else
            {
                if (type == 0)
                {
                    string pq = null;
                    if (filterContext.HttpContext.Request.Url != null)
                    {
                        pq = filterContext.HttpContext.Request.Url.PathAndQuery;
                    }
                    filterContext.Result = new RedirectResult("/user/login?s=" + pq);
                }
                else
                {
                    filterContext.Result = new ContentResult() { Content = msg };
                }
            }
        }
    }
}