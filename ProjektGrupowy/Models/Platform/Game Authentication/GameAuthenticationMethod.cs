using ProjektGrupowy.Models.Game.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Platform.Authentication
{
    public enum Type {
        Identity,
        Cookie
    }

    public abstract class GameAuthenticationMethod
    {
        abstract public void AddPermission(int gameId, int playerId, string identifier, string password, Permission.TYPE type);

        abstract public Object GetPermission(int gameId);

        abstract public int GetPlayerId(int gameId);

        public bool HasPermission(int gameId)
        {
            return GetPermission(gameId) != null;
        }
    }
}