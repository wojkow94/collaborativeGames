using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database.DAO
{
    public class PermissionDAO : DataAccessObject
    {
        public void AddCookiePermission(int gameId, string guid, int playerId, string nick, Permission.TYPE type, string password)
        {
            db.CookiePermissions.Add(new DBCookiePermission
            {
                Nick = nick,
                GameID = gameId,
                PlayerID = playerId,
                Type = type,
                Guid = guid,
                Password = password
            });
            db.SaveChanges();
        }

        public void AddPermission(int gameId, int playerId, string userName, Permission.TYPE type, string password)
        {
            User user = db.GetUser(userName);
            if (user != null)
            {
                db.Permissions.Add(new DBPermission
                {
                    User = user,
                    GameID = gameId,
                    PlayerID = playerId,
                    Type = type,
                    Password = password
                });
                db.SaveChanges();
            }
        }

        public DBPermission GetPermissions(int gameId, string email)
        {
            return db.Permissions.Where(p => p.UserEmail == email && p.GameID == gameId).FirstOrDefault();
        }

        public DBCookiePermission GetCookiePermissions(int gameId, string guid)
        {
            return db.CookiePermissions.Where(p => p.Guid == guid && p.GameID == gameId).FirstOrDefault();
        }
    }
}