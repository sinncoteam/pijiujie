using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using JFB.Systems.Domain.Info;
using JFB.Systems.Domain.Service;
using ViData;
using JFB.Utils;
using JFB.Cms.Models;

namespace JFB.Cms.Controllers
{
    public class OperatorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页查询页面
        /// </summary>
        /// <param name="adata"></param>
        /// <returns></returns>
        public JsonResult PageList(JTableData adata)
        {
            string username = Request["username"];
            int orgId = 0;
            if (!string.IsNullOrEmpty(Request["orgid"]))
            {
                orgId = Convert.ToInt32(Request["orgid"]);
            }

            PagingInfo pi = new PagingInfo()
            {
                BeginIndex = adata.iDisplayStart + 1,
                EndIndex = adata.iDisplayStart + adata.iDisplayLength,
                TableName = "t_d_operator op",
                Fileds = "*",
                SortFields= "id desc"
            };
            if (!string.IsNullOrEmpty(adata.sSearch))
            {
                pi.Conditions = "  op.username like '%'+@key+'%' ";
                pi.Parameters.Add("key", adata.sSearch);
            }
            OperatorService x_opService = new OperatorService();
            var list = x_opService.GetPaging(pi);
            JTableResult<OperatorInfo> ar = new JTableResult<OperatorInfo>()
            {
                sEcho = adata.sEcho,
                iTotalRecords = pi.RecordCount,
                iTotalDisplayRecords = pi.RecordCount,
                aaData = list
            };
            return Json(ar, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 封禁激活帐号
        /// </summary>
        /// <returns></returns>
        public JsonResult ResetStatus()
        {
            string ids = Request.Form["ids"];
            int type = Convert.ToInt32(Request.Form["type"]);
            OperatorService oms = new OperatorService();
            int i = oms.DelOp(ids, type);
            AjaxMsgResult msg = new AjaxMsgResult()
            {
                Success = true,
                Msg = "成功处理" + i + "个用户！"
            };
            return Json(msg);
        }

        /// <summary>
        /// 设置角色状态
        /// </summary>
        /// <returns></returns>
        public JsonResult ResetRoleStatus()
        {
            string ids = Request.Form["ids"];
            int type = Convert.ToInt32(Request.Form["type"]);
            RoleService x_rlSerivce = new RoleService();
            int i = x_rlSerivce.DelOp(ids, type);
            AjaxMsgResult msg = new AjaxMsgResult()
            {
                Success = true,
                Msg = "成功处理" + i + "个角色！"
            };
            return Json(msg);
        }

        /// <summary>
        /// 重设密码
        /// </summary>
        /// <returns></returns>
        public JsonResult ResetPass()
        {
            string ids = Request.Form["ids"];
            OperatorService x_opService = new OperatorService();
            string pwd = MD5Helper.Md5small("123456A");
            int i = x_opService.Resetpwd(ids, pwd);
            AjaxMsgResult msg = new AjaxMsgResult()
            {
                Success = true,
                Msg = "成功重设" + i + "个用户密码！"
            };
            return Json(msg);
        }
        /// <summary>
        /// 帐号编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            OperatorEditDto dto = new OperatorEditDto();
            if (id > 0)
            {
                OperatorService x_opservice = new OperatorService();
                dto.OperInfo = x_opservice.Get(a => a.Id == id).FirstOrDefault();
                
            }
            if (dto.OperInfo == null)
            {
                dto.OperInfo = new OperatorInfo();
            }
            return View(dto);
        }

        /// <summary>
        /// 保存管理员信息
        /// </summary>
        /// <param name="opInfo"></param>
        /// <returns></returns>
        public JsonResult EditSave(OperatorInfo opInfo)
        {
            AjaxMsgResult result = new AjaxMsgResult();
            OperatorService x_opService = new OperatorService();
            if (opInfo.Id > 0)  //更新
            {
                int i = x_opService.Update(() => new OperatorInfo() { Username = opInfo.Username, Loginname = opInfo.Loginname }, a => a.Id == opInfo.Id);
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
                opInfo.Isvalid = 1;
                opInfo.Createtime = DateTime.Now;
                opInfo.Lastlogintime = ConstHelper.SqlServerMinTime;
                opInfo.Userpass = MD5Helper.Md5small(opInfo.Userpass);
                x_opService.Insert(opInfo);
                result.Success = true;
            }
            return Json(result);
        }

        /// <summary>
        /// 角色编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoleEdit(int? id)
        {
            OperatorRoleEditDto dto = new OperatorRoleEditDto();
            if (id > 0)
            {
                RoleService x_opservice = new RoleService();
                dto.RlInfo = x_opservice.Get(a => a.Id == id).FirstOrDefault();

            }
            if (dto.RlInfo == null)
            {
                dto.RlInfo = new RoleInfo();
            }
            return View(dto);
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="opInfo"></param>
        /// <returns></returns>
        public JsonResult RoleEditSave(RoleInfo opInfo)
        {
            AjaxMsgResult result = new AjaxMsgResult();
            RoleService x_opService = new RoleService();
            if (opInfo.Id > 0)  //更新
            {
                int i = x_opService.Update(() => new RoleInfo() { Rolecode = opInfo.Rolecode, Rolename = opInfo.Rolename, Summary = opInfo.Summary }, a => a.Id == opInfo.Id);
                if (i > 0)
                {
                    result.Success = true;
                }
                else
                {
                    result.Msg = "更新失败，没有找到该角色！";
                }
            }
            else
            {
                opInfo.Isvalid = 1;
                opInfo.Createtime = DateTime.Now;
                opInfo.Rolename = opInfo.Rolename;
                opInfo.Rolecode = opInfo.Rolecode; 
                x_opService.Insert(opInfo);
                result.Success = true;
            }
            return Json(result);
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Role()
        {
            return View();
        }

        public JsonResult RoleList(JTableData adata)
        {
            PagingInfo pi = new PagingInfo()
            {
                BeginIndex = adata.iDisplayStart + 1,
                EndIndex = adata.iDisplayStart + adata.iDisplayLength,
                TableName = "t_d_role op",
                Fileds = "*",
                SortFields = "id desc"
            };
            if (!string.IsNullOrEmpty(adata.sSearch))
            {
                pi.Conditions = "  op.rolename like '%'+@key+'%' or op.rolecode like '%'+@key+'%'";
                pi.Parameters.Add("key", adata.sSearch);
            }
            RoleService x_opService = new RoleService();
            var list = x_opService.GetPaging(pi);
            JTableResult<RoleInfo> ar = new JTableResult<RoleInfo>()
            {
                sEcho = adata.sEcho,
                iTotalRecords = pi.RecordCount,
                iTotalDisplayRecords = pi.RecordCount,
                aaData = list
            };
            return Json(ar, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RoleMenuTree(string id)
        {
            AjaxMsgResult msg = new AjaxMsgResult();
            ModuleMenuService mmService = new ModuleMenuService();
            var mlist = mmService.Get(a => a.Isvalid == 1);
            RoleModuleService rmmService = new RoleModuleService();
            var rmlist = rmmService.Get(a => a.RoleCode == id);
            List<zTreeData> list = new List<zTreeData>();
            foreach (var item in mlist)
            {
                zTreeData zd = new zTreeData()
                {
                    id = item.Controlleraction,
                    name = item.Modulename,
                    open = true,
                    pId = item.ParentControlleraction
                };
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var ritem in rmlist)
                    {
                        if (ritem.ModuleControlleraction == item.Controlleraction)
                        {
                            zd.@checked = true;
                            break;
                        }
                    }
                }
                list.Add(zd);
            }
            msg.Success = true;
            msg.Source = list;
            return Json(msg);
        }

        public JsonResult SaveRoleMenu(string id)
        {
            string nodes = Request.Form["nodes"];
            if (!string.IsNullOrEmpty(nodes))
            {
                nodes = nodes.Trim(',');
            }
            RoleModuleService rmmService = new RoleModuleService();
            int i = rmmService.UpdateRoleModule(id, nodes);
            AjaxMsgResult msg = new AjaxMsgResult();
            msg.Msg = "保存成功";
            return Json(msg);
        }

        /// <summary>
        /// 角色树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RoleTree(int id)
        {
            AjaxMsgResult msg = new AjaxMsgResult();
            RoleService x_rService = new RoleService();
            var mlist = x_rService.Get(a => a.Isvalid == 1);
            OperatorRoleService x_orService = new OperatorRoleService();
            var rmlist = x_orService.Get(a => a.OperatorId == id);
            List<zTreeData> list = new List<zTreeData>();
            foreach (var item in mlist)
            {
                zTreeData zd = new zTreeData()
                {
                    id = item.Rolecode.ToString(),
                    name = item.Rolename,
                    open = true,
                    pId = "0"
                };
                if (id > 0)
                {
                    foreach (var ritem in rmlist)
                    {
                        if (ritem.RoleCode == item.Rolecode)
                        {
                            zd.@checked = true;
                            break;
                        }
                    }
                }
                list.Add(zd);
            }
            msg.Success = true;
            msg.Source = list;
            return Json(msg);
        }

        public JsonResult SaveRole(int id)
        {
            string nodes = Request.Form["nodes"];
            if (!string.IsNullOrEmpty(nodes))
            {
                nodes = nodes.Trim(',');
            }
            OperatorRoleService x_orService = new OperatorRoleService();
            int i = x_orService.UpdateRoleModule(id, nodes);
            AjaxMsgResult msg = new AjaxMsgResult();
            msg.Msg = "保存成功";
            return Json(msg);
        }
    }
}
