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
    public class CookieAuthentication : GameAuthenticationMethod 
    {
        private string GenerateGuid()
        {
            HttpCookie cookie = new HttpCookie("_guid");
            string guid = Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("+", string.Empty).Substring(0, 24);
            cookie.Value = guid;
            cookie.Expires = DateTime.MaxValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
            return guid;
        }

        override public void AddPermission(int gameId, int playerId, string identifier, string password, Permission.TYPE type)
        {
            string guid;
            if (!HttpContext.Current.Request.Cookies.AllKeys.Contains("_guid"))
            {
                guid = GenerateGuid();
            }
            else
            {
                guid = HttpContext.Current.Request.Cookies["_guid"].Value;
            }

            using (var permissionsDAO = new PermissionDAO())
            {
                permissionsDAO.AddCookiePermission(gameId, guid, playerId, identifier, type, password);
            }
        }

        override public Object GetPermission(int gameId)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains("_guid"))
            {
                using (PermissionDAO dao = new PermissionDAO())
                {
                    return dao.GetCookiePermissions(gameId, HttpContext.Current.Request.Cookies["_guid"].Value);
                }
            }

            return null;
        }

        public override int GetPlayerId(int gameId)
        {
            return (GetPermission(gameId) as DBCookiePermission).PlayerID;
        }

    }
}