using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ViData;
using System.Threading;
using PJJ.Wx.Controllers;

namespace PJJ.Wx
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            log4net.Config.XmlConfigurator.Configure();
            DMHelper.Instance.ExportMapping();

            startCheckImg();
        }

        void startCheckImg()
        {
            ThreadStart ts = new ThreadStart(ChecktoImg.getPerValue);
            Thread th = new Thread(ts);
            th.Start();
        }
    }
}