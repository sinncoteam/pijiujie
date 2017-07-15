using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFB.Utils;
using ViData;
using JFB.Systems.Domain.Service;
using JFB.Systems.Domain.Info;
using JFB.Business.Domain.Service;
using JFB.Business.Domain.Model;
using JFB.Cms.Models;
using JFB.Business.Domain.Info;
using System.Threading;
using JFB.Api.RedPackApi;

namespace JFB.Cms.Controllers
{
    public class PrizeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult IndexList(JTableData adata)
        {
            PagingInfo pi = new PagingInfo()
            {
                BeginIndex = adata.iDisplayStart + 1,
                EndIndex = adata.iDisplayStart + adata.iDisplayLength,
                TableName = "t_d_redpack op",
                Fileds = "*",
                SortFields = "id desc"
            };
            //if (!string.IsNullOrEmpty(adata.sSearch))
            //{
            //    pi.Conditions = "  op.username like '%'+@key+'%' ";
            //    pi.Parameters.Add("key", adata.sSearch);
            //}
            RedPackService x_rpService = new RedPackService();
            var list = x_rpService.GetPaging(pi);

            JTableResult<RedPack> ar = new JTableResult<RedPack>()
            {
                sEcho = adata.sEcho,
                iTotalRecords = pi.RecordCount,
                iTotalDisplayRecords = pi.RecordCount,
                aaData = list
            };
            return Json(ar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResetStatus(string ids, int type)
        {
            AjaxMsgResult result = new AjaxMsgResult();
            RedPackService x_rpService = new RedPackService();
            int i = x_rpService.ResetStatus(ids, type);
            if (i > 0)
            {
                result.Success = true;
                result.Msg = "重置状态成功";
            }
            else
            {
                result.Success = false;
                result.Msg = "重置状态处理失败";
            }
            return Json(result);
        }

        public ActionResult Edit(int? id)
        {
            RedPackEditDto dto = new RedPackEditDto();
            if (id > 0)
            {
                RedPackService x_rpService = new RedPackService();
                dto.RPInfo = x_rpService.GetById(id);

            }
            if (dto.RPInfo == null)
            {
                dto.RPInfo = new RedPack();
            }
            return View(dto);
        }

        public JsonResult EditSave(RedPack rp)
        {
            AjaxMsgResult result = new AjaxMsgResult();
            RedPackService x_opService = new RedPackService();
            if (rp.ID > 0)  //更新
            {
                int i = x_opService.Update(() => new RedPack()
                {
                    RbCount = rp.RbCount,
                    RbMoney = rp.RbMoney,
                    RbTotal = rp.RbTotal,
                    UpdateTime = DateTime.Now,
                    GetPercent = rp.GetPercent
                }, a => a.ID == rp.ID);
                if (i > 0)
                {
                    result.Success = true;
                }
                else
                {
                    result.Msg = "更新失败，没有找到该用户！";
                }
            }
            else
            {
                rp.IsValid = 1;
                rp.CreateTime = DateTime.Now;
                rp.UpdateTime = DateTime.Now;
                x_opService.Insert(rp);
                result.Success = true;
            }
            return Json(result);
        }

        public ActionResult UserList()
        {
            return View();
        }

        public JsonResult UserListData(JTableData adata)
        {
            PagingInfo pi = new PagingInfo()
            {
                BeginIndex = adata.iDisplayStart + 1,
                EndIndex = adata.iDisplayStart + adata.iDisplayLength,
                TableName = "t_d_redpack_list rpl inner join t_d_redpack rp on rp.ID = rpl.pack_id inner join t_d_user u on u.ID = rpl.user_id",
                Fileds = "u.nickname, u.openid, rp.rbname, rpl.*",
                SortFields = " rpl.id desc"
            };
            if (!string.IsNullOrEmpty(adata.sSearch))
            {
                pi.Conditions = "  u.nickname like '%'+@key+'%' ";
                pi.Parameters.Add("key", adata.sSearch);
            }
            RedPackListService x_rpService = new RedPackListService();
            var list = x_rpService.GetPaging(pi);

            JTableResult<RedPackListInfo> ar = new JTableResult<RedPackListInfo>()
            {
                sEcho = adata.sEcho,
                iTotalRecords = pi.RecordCount,
                iTotalDisplayRecords = pi.RecordCount,
                aaData = list
            };
            return Json(ar, JsonRequestBehavior.AllowGet);
        }

        static object locker = new object();
        public JsonResult SetAllPack()
        {
            AjaxMsgResult result = new AjaxMsgResult();
            RedPackListService x_rplService = new RedPackListService();
            var list = x_rplService.getAllUser();
            if (list.Count > 0)
            {
                lock (locker)
                {
                    list = x_rplService.getAllUser();
                    int count = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        string noncestr = "";
                        string paysing = "";
                        var item = list[i];
                        //Senparc.Weixin.MP.TenPayLibV3.RedPackApi.SendNormalRedPack("appid", "mchid", "tenpaykey", "certpath", "openid", "sendername", "ip", 125, "wishing word", "actionname", "remark", out noncestr, out paysing, "mchBillNo");
                        RequestModel model = new RequestModel()
                        {
                            openid = item.OpenId,
                             amount = item.PackMoney.ToString(),
                              clientip = "127.0.0.1",
                               clientport = "80",
                                hdclass = "17",
                                 sendtxt = "解放碑地下环道游戏红包",
                                  timecontrol = "1"
                        };
                        string req = SendRedPack.SendTo(model);
                        if (!req.Contains("Error") && req.Contains("{\"State\":\"0\"}"))
                        {
                            x_rplService.Update(() => new RedPackListInfo() { Noncestr = req, PaySign = paysing, PackStatus = 1 }, a => a.ID == item.ID);
                            count++;
                        }
                        Thread.Sleep(10);
                        if (i > 0 && i % 300 == 0)
                        {
                            Thread.Sleep(15000);
                        }
                    }
                    result.Success = true;
                    result.Msg = "该发"+ list.Count +"个，实发" + count + "个用户发送了红包！";
                    
                }
            }
            else
            {
                result.Success = false;
                result.Msg = "所有用户都已发送过红包";
            }
            return Json(result);
        }
    }
}
