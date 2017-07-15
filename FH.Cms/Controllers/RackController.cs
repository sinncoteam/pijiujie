using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFB.Utils;
using JFB.Cms.Models;

namespace JFB.Cms.Controllers
{
    public class RackController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public JsonResult MapData()
        {
            AjaxMsgResult result = new AjaxMsgResult();

            result.Success = true;
            RackDataDto dto = new RackDataDto();
            dto.Points = new List<RackDataDto.RackPoint>();
            dto.Points.Add(new RackDataDto.RackPoint() { X = 100, Y = 100 });
            dto.Points.Add(new RackDataDto.RackPoint() { X = 200, Y = 100 });
            dto.Points.Add(new RackDataDto.RackPoint() { X = 300, Y = 100 });
            dto.Points.Add(new RackDataDto.RackPoint() { X = 300, Y = 400 });
            dto.Points.Add(new RackDataDto.RackPoint() { X = 400, Y = 500 });

            result.Source = dto;
            return Json(result);
        }
    }
}
