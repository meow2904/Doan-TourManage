using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class HomeManagementController : Controller
    {
        // GET: Admin/HomeManagement
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                return View();
            }

        }
    }
}