using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ovixon.WebSite.Models;
using Ovixon.Bootstrap;

namespace Ovixon.WebSite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            DemoElement element = new DemoElement();
            return View(element);
        }

        [HttpPost]
        public ActionResult Index(DemoElement element)
        {
            return View(element);
        }
    }
}
