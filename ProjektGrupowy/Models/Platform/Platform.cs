
using ProjektGrupowy.Models.Database;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using ProjektGrupowy.Models.Games.AvaxStorming;
using ProjektGrupowy.Models.Games.BuyAFeature;
using ProjektGrupowy.Models.Games.How_Now_WowMatrix;
using ProjektGrupowy.Models.Games.SWOTAnalysis;
using ProjektGrupowy.Models.Games.SpeedBoat;
using ProjektGrupowy.Models.Platform;
using ProjektGrupowy.Models.Platform.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ProjektGrupowy.Models.Games.WholeProduct;

namespace ProjektGrupowy.Models.Core
{

    static public class Platform
    {
        private static Dictionary<int, GameDefinition> GameDefinitions;
        private static Dictionary<int, GameInstance> Games;

        public static System.Type[] GameTypes =
        {
            typeof(GameDefinition),
            typeof(BuyAFeature),
            typeof(AvaxStorming),
            typeof(SpeedBoat),
            typeof(HowNowWowMatrix),
            typeof(WholeProduct),
			typeof(SWOTAnalysis)
        };

        static public GameAuthentication GameAuthentication = new GameAuthentication();

        static public void OnEnterSite()
        {
        }

        static public bool IsUserLogged()
        {
            return ClaimsPrincipal.Current.Identity.IsAuthenticated;
        }

        public static List<DBGameInstance> GetGamesByUser(Permission.TYPE permission)
        {
            using (var dao = new GameInstanceDAO())
            {
                if (IsUserLogged())
                {
                    return dao.GetGameInstancesByUser(HttpContext.Current.User.Identity.Name, permission);
                }
                else if (HttpContext.Current.Request.Cookies.AllKeys.Contains("_guid"))
                {
                    return dao.GetGameInstancesByGuid(HttpContext.Current.Request.Cookies["_guid"].Value, permission);
                }
                return null;
            }
        }

        public static Player GetCurrentPlayer(GameInstance game)
        {
            return game.GetPlayerById(GameAuthentication.GetPlayerId(game.Id));
        }

        public static string GetImage(int id)
        {
            using (var dao = new ImageDAO())
            {
                return dao.GetImage(id);
            }
        }

        public static GameInstance NewGameInstance(GameDefinition definition, string name, string password)
        {
            GameInstance gameInstance = new GameInstance(definition);

            GameInstanceDAO dao = new GameInstanceDAO();
            dao.AddGameInstance(gameInstance, name, password);

            Games.Add(gameInstance.Id, gameInstance);
            return gameInstance;
        }

        public static void Init()
        {
            GameDefinitions = new Dictionary<int, GameDefinition>();
            Games = new Dictionary<int, GameInstance>();

            (new SpeedBoatBuilder()).Build();
            (new BuyAFeatureBuilder()).Build();
            (new AvaxStormingBuilder()).Build();
            (new HowNowWowMatrixBuilder()).Build();
			(new SWOTAnalysisBuilder()).Build();
            new WholeProductBuilder().Build();
        }



        public static GameDefinition GetGameDefinitionById(int i)
        {
            lock (GameDefinitions)
            {
                if (GameDefinitions.ContainsKey(i))
                {
                    return GameDefinitions[i];
                }
                else
                {
                    using (var dao = new GameDefinitionDAO())
                    {
                        var game = dao.RestoreGameDefinition(i);
                        GameDefinitions.Add(i, game);
                        return game;
                    }
                }
            }
        }

        public static GameInstance GetGameInstanceById(int i)
        {
            lock (Games)
            {
                if (Games.ContainsKey(i))
                {
                    return Games[i];
                }
                else
                {
                    using (var dao = new GameInstanceDAO())
                    {
                        var game = dao.RestoreGameInstance(i);

                        if (game == null)
                        {
                            throw new Error.GameNotExists();
                        }
                        else
                        {
                            Games.Add(i, game);
                        }

                        return game;
                    }
                }
            }
        }

        public static void ClearCache()
        {
            Games.Clear();
            GameDefinitions.Clear();
        }
    }
}