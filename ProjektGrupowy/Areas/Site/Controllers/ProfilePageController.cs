using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Site.Controllers
{
    public class ProfilePageController : Controller
    {
        // GET: Site/ProfilePage
        public ActionResult Index()
        {
            if (Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("Index", "ProfilePage", new { area = "Mobile" });
            }
            return View("ProfilePage");
        }
    }

}