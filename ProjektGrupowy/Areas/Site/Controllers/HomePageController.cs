using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Threading;
using ProjektGrupowy.Models.Platform;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Core;

namespace ProjektGrupowy.Areas.Site.Controllers
{
    public class HomePageController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("Index", "HomePage", new { area = "Mobile" });
            }
            Platform.OnEnterSite();
            return View("HomePage");
        }

        public ActionResult LoginForm()
        {
            return View("Forms/LoginForm");
        }

        public ActionResult JoinGameForm()
        {
            return View("Forms/JoinGameForm");
        }

        public ActionResult RegisterForm()
        {
            return View("Forms/RegisterForm");
        }

        public ActionResult NewGameForm()
        {
            return View("Forms/NewGameForm");
        }

    }
}