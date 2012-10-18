using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ovixon.WebSite.Models;
using Ovixon.Bootstrap;
using Ovixon.WebSite.codes;

namespace Ovixon.WebSite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            DemoElement element = new DemoElement() { Prop4 = DateTime.Now.ToShortDateString() };
            return View(element);
        }

        [HttpPost]
        public ActionResult Index(DemoElement element)
        {
            return View(element);
        }

        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "None", Order = 1)]
        public ActionResult GetAjaxData(AjaxDataTableGetParams getParams)
        {
            var list = DemoElement.ListOfElementsForTable2;

            var filteredlist = list.Where(x => string.IsNullOrEmpty(getParams.sSearch) || x.Any(y => y.IndexOf(getParams.sSearch, StringComparison.InvariantCultureIgnoreCase) >= 0));

            var orderedlist =
                filteredlist.OrderByWithDirection
                (x => (x[getParams.iSortCol_0]).Parse(), getParams.iSortDir_0 == "desc")
                .Skip(getParams.iDisplayStart)
                .Take(getParams.iDisplayLength);

            var model = new
            {
                aaData = orderedlist.ToArray(),
                iTotalDisplayRecords = filteredlist.Count(),
                iTotalRecords = list.Count(),
                sEcho = getParams.sEcho.ToString()
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
