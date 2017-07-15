using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFB.Utils;
using JFB.Systems.Domain.Service;
using JFB.Cms.Component;

namespace JFB.Cms.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return Redirect("/user/login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginX(string username, string password)
        {
            AjaxMsgResult result = new AjaxMsgResult();
            OperatorService omService = new OperatorService();
            password = MD5Helper.Md5(password);
            var op = omService.GetbyPwd(username, password);
            if (op != null)
            {
                if (op.Isvalid == 1)
                {
                    Authentication.Instance.SetAuth(op, false);
                    result.Success = true;
                    result.Msg = "登录成功！";
                }
                else
                {
                    result.Success = false;
                    result.Msg = "帐号已被禁用，请联系管理员！";
                }
            }
            else
            {
                result.Success = false;
                result.Msg = "帐号或密码错误！";
            }
            return Json(result);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Authentication.Instance.SignOut();
            return Redirect("/user/login");
        }

    }
}
