using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JFB.Business.Domain.Service;
using JFB.Api.ImgCheckApi;
using JFB.Business.Domain.Info;
using System.Threading;
using System.Configuration;
using ViCore.Logging;

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
                string info = "start --- >>  ";
                var list = x_upService.Get(a => a.PerValue == 0 && a.FatherPhoto != "" && a.ChildPhoto != "");
                if (list != null && list.Count > 0)
                {
                    RequestModel rm = new RequestModel();
                    foreach (var item in list)
                    {
                        var res = rm.getResult("http://" + host + item.FatherPhoto, "http://" + host + item.ChildPhoto);
                        //info += res.code + ".." + res.Message + "....http://" + host + item.FatherPhoto + "http://" + host + item.ChildPhoto +"   ";
                        //Logging4net.WriteInfo(info);
                        if (res != null && res.code == 0)
                        {
                            int similar = Convert.ToInt32(res.data.similarity);
                            if (similar <= 0)
                            {
                                similar = -1;
                            }
                            x_upService.Update(() => new UserPhotoInfo() { PerValue = similar, PerValueTime = DateTime.Now }, a => a.ID == item.ID);
                        }
                        else if (res.code != 0)
                        {
                            x_upService.Update(() => new UserPhotoInfo() { PerValue = -1, PerValueTime = DateTime.Now }, a => a.ID == item.ID);
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