using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGrupowy.Models.Database.DAO;
using System.Web.Helpers;

namespace ProjektGrupowy.Models.Platform.Authentication
{
    public class GameAuthentication : GameAuthenticationMethod
    {
        List<GameAuthenticationMethod> Methods = new List<GameAuthenticationMethod>();

        public GameAuthentication()
        {
            Methods.Add(new IdentityAuthentication());
            Methods.Add(new CookieAuthentication());
        }

        private GameAuthenticationMethod ChoseMethod()
        {
            return Core.Platform.IsUserLogged() ? Methods[(int)Type.Identity] : Methods[(int)Type.Cookie];
        }

        override public void AddPermission(int gameId, int playerId, string identifier, string password, Permission.TYPE type)
        {
            ChoseMethod().AddPermission(gameId, playerId, identifier, password, type);
        }

        override public Object GetPermission(int gameId)
        {
            return ChoseMethod().GetPermission(gameId);
        }

        override public int GetPlayerId(int gameId)
        {
            return ChoseMethod().GetPlayerId(gameId);
        }

        public void CheckGamePassword(int gameId, string password)
        {
            using (var dao = new GameInstanceDAO())
            {
                string gamePassword = dao.GetGameInstance(gameId).Password;

                if (!(gamePassword == "" && ((password == null || password == ""))))
                {
                    if (!Crypto.VerifyHashedPassword(gamePassword, password != null ? password : ""))
                    {
                        throw new Error.InvalidPassword();
                    }
                }
            }
        }
    }
}