using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{
    

    public class DBPermission
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int PlayerID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public Permission.TYPE Type { get; set; }

        public virtual User User { get; set; }
        public virtual DBGameInstance Game { get; set; }
    }
}