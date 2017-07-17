using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JFB.Business.Domain.Service;
using JFB.Api.ImgCheckApi;
using JFB.Business.Domain.Info;
using System.Threading;
using System.Configuration;

namespace PJJ.Wx.Controllers
{
    public class ChecktoImg
    {
        public static void getPerValue()
        {
            string host = ConfigurationManager.AppSettings["host"];
            while (true)
            {
                UserPhotoService x_upService = new UserPhotoService();
                var list = x_upService.Get(a => a.PerValue == 0 && a.FatherPhoto != "" && a.ChildPhoto != "");
                if (list != null && list.Count > 0)
                {
                    RequestModel rm = new RequestModel();
                    foreach (var item in list)
                    {
                        var res = rm.getResult("http://" + host + item.FatherPhoto, "http://" + host + item.ChildPhoto);
                        if (res != null && res.code == 0)
                        {
                            x_upService.Update(() => new UserPhotoInfo() { PerValue = Convert.ToInt32(res.data.similarity), PerValueTime = DateTime.Now }, a => a.ID == item.ID);
                        }
                        Thread.Sleep(300);
                    }
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}