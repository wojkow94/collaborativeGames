using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Game.Instances;
using ProjektGrupowy.Models.Platform;
using ProjektGrupowy.Models.Site.FormModels;
using System;
using System.Web.Mvc;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Database.DAO;
using System.Web.Helpers;
using ProjektGrupowy.Models.Game.Common;

namespace ProjektGrupowy.Areas.Api.Controllers
{
    public class GameController : Controller
    {
        private GameInstance Game;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("gameId"))
            {
                int gameId = (int)filterContext.ActionParameters["gameId"];
                if (!Platform.GameAuthentication.HasPermission(gameId))
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                }
                else
                {
                    Game = Platform.GetGameInstanceById(gameId);
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        public JsonResult SaveGame(int gameId)
        {
            lock (Game)
            {
                using (var dao = new GameInstanceDAO())
                {
                    dao.UpdateGameInstance(Game);
                }

                return Json(Result.Succes);
            }
        }

        public JsonResult JoinGame(JoinGameModel joinGameModel)
        {
            try
            {
                int gameId;
                int.TryParse(joinGameModel.GameId, out gameId);
                Game = Platform.GetGameInstanceById(gameId);

                lock (Game)
                {
                    if (Platform.IsUserLogged()) joinGameModel.Nick = User.Identity.Name;

                    joinGameModel.Validate();
                    Platform.GameAuthentication.CheckGamePassword(gameId, joinGameModel.Password);

                    if (!Platform.GameAuthentication.HasPermission(gameId))
                    {
                        Player player = new Player(joinGameModel.Nick, Permission.TYPE.Normal);
                        int playerId = Game.AddPlayer(player);
                        SaveGame(gameId);

                        Platform.GameAuthentication.AddPermission(gameId, playerId, joinGameModel.Nick, Crypto.HashPassword(joinGameModel.Password), Permission.TYPE.Normal);
                    }

                    return Json(new Result(new { gameId = gameId }).AsSuccess());

                }
            }
            catch (FormatException)
            {
                return Json(new Result(Resources.Errors.InvalidGameId).AsFailure());
            }
            catch (Error.ValidationException validation)
            {
                return Json(validation.Result);
            }
            catch (Error.AppError ex)
            {
                return Json(new Result(ex.Message).AsFailure());
            }
        }

        public JsonResult NewGame(int gameDefId, NewGameModel newGameModel)
        {
            if (Platform.IsUserLogged()) newGameModel.Nick = User.Identity.Name;

            try
            {
                newGameModel.Validate();
                Game = Platform.NewGameInstance(Platform.GetGameDefinitionById(gameDefId), newGameModel.Name, newGameModel.Password);
                int playerId = Game.AddPlayer(new Player(newGameModel.Nick, Permission.TYPE.Moderator));
                SaveGame(Game.Id);

                Platform.GameAuthentication.AddPermission(Game.Id, playerId, newGameModel.Nick, Crypto.HashPassword(newGameModel.Password), Permission.TYPE.Moderator);

                return Json(new Result(new { gameId = Game.Id }).AsSuccess());
            }
            catch (Error.ValidationException validation)
            {
                return Json(validation.Result);
            }
        }

        public JsonResult SetSorting(int gameId, int elementDef, int type, int id)
        {
            Player player = Platform.GetCurrentPlayer(Game);

            player.Sorting[elementDef].By = (SortMethod.BY)type;
            player.Sorting[elementDef].Direction = (SortMethod.DIRECTION)(((int)player.Sorting[elementDef].Direction + 1) % 3);
            player.Sorting[elementDef].Id = id;

            return Json(Result.Succes);
        }

        public JsonResult AcceptElement(int gameId, int elementId, int containerId, int regionId)
        {
            lock (Game)
            {
                try
                {
                    var element = Game.GetElement(elementId);
                    if (element.Definition.CanAccept(Platform.GetCurrentPlayer(Game)))
                    {
                        if (!Game.AcceptElement(elementId, containerId, regionId))
                        {
                            return Json(Result.Failure);
                        }
                    }
                    else
                    {
                        throw new Error.AccessDenied();
                    }

                    element.CanBeAccepted = false;
                    return Json(Result.Succes);
                }
                catch (Error.AppError ex)
                {
                    return Json(new Result(ex.Message).AsFailure());
                }
            }   
        }

        public JsonResult RejectElement(int gameId, int elementId)
        {
            lock (Game)
            {
                Game.RejectElement(elementId);
                return Json(Result.Succes);
            }
        }

        public JsonResult IsElementAccepted(int gameId, int elementId)
        {
            return Json(new Result(new {
                accepted = Game.GetElement(elementId).IsAccepted
            }).AsSuccess());
        }

        public JsonResult CanAcceptElement(int gameId, int elementId)
        {
            return Json(new Result(new
            {
                canAccept = Game.GetElement(elementId).CanBeAccepted
            }).AsSuccess());
        }

        public JsonResult CanAccept(int gameId, int elementDefId)
        {
            return Json(new Result(new
            {
                canAccept = Game.Definition.GetElementDefinition(elementDefId).CanAccept(Platform.GetCurrentPlayer(Game))
            }).AsSuccess());
        }

        public JsonResult AddElement(int gameId, int elementDefinitionId, FormCollection elementData)
        {
            lock (Game)
            {
                try
                {
                    ElementInstance element = new ElementInstance(Game.Definition.GetElementDefinition(elementDefinitionId));
                    element.PlayerId = Platform.GameAuthentication.GetPlayerId(gameId);

                    foreach (var key in elementData.AllKeys)
                    {
                        if (element.Definition.HasAttribute(key))
                        {
                            element.SetAttributeValue(key, elementData.Get(key));
                        }
                    }

                    if (element.Definition.CanAdd(Platform.GetCurrentPlayer(Game)))
                    {
                        int id = Game.AddElement(element);
                        return Json(new Result(new { elementId = id }).AsSuccess());
                    }
                    else
                    {
                        throw new Error.AccessDenied();
                    }
                }
                catch (Error.AppError ex)
                {
                    return Json(new Result(ex.Message).AsFailure());
                }
            }
        }

        public JsonResult EditElement(int gameId, int elementId, FormCollection elementData)
        {
            lock (Game)
            {
                try
                {
                    Player player = Platform.GetCurrentPlayer(Game);
                    ElementInstance element = Game.GetElement(elementId);

                    if (element.PlayerId != player.Id && !player.IsModerator())
                    {
                        throw new Error.AccessDenied();
                    }

                    foreach (var key in elementData.AllKeys)
                    {
                        if (element.Definition.HasAttribute(key))
                        {
                            element.Definition.GetAttribute(key).Validate(elementData.Get(key));
                            element.SetAttributeValue(key, elementData.Get(key));
                        }
                    }
                    Game.Definition.OnEditElement(Game, element);

                    return Json(Result.Succes);
                }
                catch (Error.AppError ex)
                {
                    return Json(new Result(ex.Message).AsFailure());
                }
            }
        }

        public JsonResult AddTokens(int gameId, int tokenDefinitionId, int elementId, int amount)
        {
            lock (Game)
            {
                ElementInstance element = Game.GetElement(elementId);
                TokenDefinition token = Game.Definition.GetTokenDefinition(tokenDefinitionId);
                Game.AddTokens(Platform.GetCurrentPlayer(Game), token, element, amount);
                return Json(Result.Succes);
            }
        }

        public JsonResult SetTokens(int gameId, int tokenDefinitionId, int elementId, int amount)
        {
            lock (Game)
            {
                ElementInstance element = Game.GetElement(elementId);
                TokenDefinition token = Game.Definition.GetTokenDefinition(tokenDefinitionId);
                Game.SetTokens(Platform.GetCurrentPlayer(Game), token, element, amount);
                return Json(Result.Succes);
            }
        }

        public JsonResult GetPlayerTokensAmount(int gameId, int tokenDefinitionId, int elementId)
        {
            ElementInstance element = Game.GetElement(elementId);
            TokenDefinition token = Game.Definition.GetTokenDefinition(tokenDefinitionId);

            int amount = element.GetTokenAmount(token, Platform.GetCurrentPlayer(Game).Id);

            return Json(new Result(new { tokensAmount = amount }).AsSuccess());
        }

        public JsonResult SetTokensConfig(int gameId, FormCollection elementData)
        {
            foreach (var key in elementData.AllKeys)
            {
                string[] pair = key.Split('.');
                int playerId = int.Parse(pair[0]);
                int tokenId = int.Parse(pair[1]);

                Game.GetPlayerById(playerId).SetTokens(Game.Definition.Tokens[tokenId], playerId, int.Parse(elementData.Get(key)));
            }

            return Json(Result.Succes);
        }

        public JsonResult ClearCache()
        {
            Platform.ClearCache();
            return Json(Result.Succes);
        }
    }
}