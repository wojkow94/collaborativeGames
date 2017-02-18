using ProjektGrupowy.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Mobile.Controllers
{
    public class HomePageController : Controller
    {
        public ActionResult Index()
        {
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
    }
}