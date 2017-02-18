using Microsoft.AspNet.SignalR;
using ProjektGrupowy.Areas.Game.Hubs;
using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Mobile.Controllers
{
    public class GamePageController : Controller
    {
        GameInstance Game;

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
            return View("GamePage", Game);
        }

        public ActionResult ProposedElements(int gameId, int elementDefinitionId)
        {
            List<ElementInstance> elements = Game.GetProposedElements(elementDefinitionId);
            return View("Partials/ElementsSheet", elements);
        }

        public ActionResult AcceptedElements(int gameId, int elementDefinitionId)
        {
            List<ElementInstance> elements = Game.GetAcceptedElements(elementDefinitionId);
            return View("Partials/ElementsSheet", elements);
        }

        public ActionResult ElementListItem(int gameId, int elementId)
        {
            ElementInstance elements = Game.GetElement(elementId);
            return View("Partials/ElementListItem", elements);
        }

        public ActionResult AddElementForm(int gameId, int elementDefinitionId)
        {
            ElementDefinition elementDef = Game.Definition.GetElementDefinition(elementDefinitionId);
            return View("Forms/AddElementForm", elementDef);
        }

        public ActionResult TokensView(int gameId)
        {
            return View("Partials/TokensView", Platform.GetCurrentPlayer(Game));
        }
    }
}