using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;

namespace ProjektGrupowy.Models.Site.ViewModels
{
    public class GameRules
    {
        public GameDefinition GameDefinition;
        public string Description { get; set; }
        public string Elements { get; set; }
        public string Tokens { get; set; }
        public string Applications { get; set; }

        public GameRules(GameDefinition gameDef)
        {
            GameDefinition = gameDef;
            string gameName = gameDef.Name.Replace(" ", "").Replace("-","");

            ResourceManager rm = Resources.Rules.ResourceManager;
            Description = rm.GetString(gameName + "_Description");
            Elements = rm.GetString(gameName + "_Elements");
            Tokens = rm.GetString(gameName + "_Tokens");
            Applications = rm.GetString(gameName + "_Applications");
        }
    } 
}