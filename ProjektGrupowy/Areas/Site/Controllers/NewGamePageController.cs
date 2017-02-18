using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Platform;
using ProjektGrupowy.Models.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Site.Controllers
{
    public class NewGamePageController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult Index()
        {
            if (Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("Index", "NewGamePage", new { area = "Mobile" });
            }
            return View("NewGamePage", db.GameDefinitions.ToList());
        }

        public ActionResult GameRules(string gameId)
        {
            int id;
            if (int.TryParse(gameId, out id)) {
                GameDefinition gameDef = Platform.GetGameDefinitionById(id);
                return View("Partials/Rules", new GameRules(gameDef));
            }
            return null;
        }

        public ActionResult NewGameForm()
        {
            return View("Forms/NewGameForm");
        }
    }
}