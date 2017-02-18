using ProjektGrupowy.Models.Game.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{
    public class DBCookiePermission
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public int GameID { get; set; }
        public int PlayerID { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public Permission.TYPE Type { get; set; }

        public virtual DBGameInstance Game { get; set; }
    }
}