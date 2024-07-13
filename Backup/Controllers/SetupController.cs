using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIZINVOICING.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup
        public ActionResult Setup()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
    }
}