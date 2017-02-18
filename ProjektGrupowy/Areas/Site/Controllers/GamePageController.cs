using Microsoft.AspNet.SignalR;
using ProjektGrupowy.Areas.Game.Hubs;
using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Site.Controllers
{
    public class GamePageController : Controller
    {
        private GameInstance Game;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("gameId"))
            {
                int gameId = (int)filterContext.ActionParameters["gameId"];
                if (!Platform.GameAuthentication.HasPermission(gameId))
                {
                    filterContext.Result = View("ErrorPage", new Error.GameNotFound(gameId));
                }
                else
                {
                    Game = Platform.GetGameInstanceById(gameId);
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        public ActionResult Index(int gameId)
        {
            if (Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("Index", "GamePage", new { area = "Mobile", gameId = gameId });
            }
            return View("GamePage", Game);   
        }

        public ActionResult AddElementForm(int gameDefinitionId, int elementDefinitionId)
        {
            GameDefinition gameDef = Platform.GetGameDefinitionById(gameDefinitionId);
            ElementDefinition elementDef = gameDef.GetElementDefinition(elementDefinitionId);

            return View("Forms/AddElementForm", elementDef);
        }

        public ActionResult EditElementForm(int gameId, int elementId)
        {
            return View("Forms/EditElement", Game.GetElement(elementId));
        }

        public ActionResult ElementsSheet(int gameId, int elementDefinitionId)
        {
            Player player = Platform.GetCurrentPlayer(Game);
            OrderedElementsSet elements = Game.GetOrderedElements(elementDefinitionId, player.Sorting[elementDefinitionId]);

            return View("Partials/ElementsSheet", elements);
        }

        public ActionResult BoardElementLabel(int gameId, int elementId)
        {
            return View("Partials/BoardElementLabel", Game.GetElement(elementId)); 
        }

        public ActionResult ElementsSheetsView(int gameId)
        {
            return View("Partials/SheetsView", Game.Definition);
        }

        public ActionResult ElementPropertiesView(int gameId, int elementId)
        {
            return View("Partials/ElementPropertiesView", Game.GetElement(elementId));
        }

        public ActionResult ProposedElementsView(int gameId)
        {
            return View("Partials/ProposedElements", Game.GetProposedElements());
        }

        public ActionResult GameBoardView(int gameId)
        {
            return View("Partials/GameBoard", Game);
        }

        public ActionResult TokensView(int gameId)
        {
            return View("Partials/TokensView", Platform.GetCurrentPlayer(Game));
        }

        public ActionResult PlayersView(int gameId)
        {
            return View("Partials/PlayersView", Game.Players);
        }

        public ActionResult PlayerLabel(int gameId, int playerId)
        {
            return View("Partials/PlayerLabel", Game.Players[playerId]);
        }

        public ActionResult TokensConfig(int gameId)
        {
            return View("Forms/TokensConfig", Game);
        }
    }
}