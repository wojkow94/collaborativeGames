using ProjektGrupowy.Models.Database;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ProjektGrupowy.Models.Platform.Authentication
{
    public class IdentityAuthentication : GameAuthenticationMethod 
    {
        override public void AddPermission(int gameId, int playerId, string identifier, string password, Permission.TYPE type)
        {
            using (var permissionsDAO = new PermissionDAO())
            {
                permissionsDAO.AddPermission(gameId, playerId, identifier, type, password);
            }
        }

        override public Object GetPermission(int gameId)
        {
            using (PermissionDAO dao = new PermissionDAO())
            {
                return dao.GetPermissions(gameId, ClaimsPrincipal.Current.Identity.Name);
            }
        }

        public override int GetPlayerId(int gameId)
        {
            return (GetPermission(gameId) as DBPermission).PlayerID;
        }

    }
}