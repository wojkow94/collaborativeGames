using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjektGrupowy.Models.Game.Instances
{
    [System.Serializable]
    public class TokenBunch
    {
        [XmlIgnore]
        public TokenDefinition Token;

        public TokenBunch()
        {

        }

        public int DefinitionId { get; set; }
        public int Amount { get; set; }
        public int PlayerId { get; set; }

        public TokenBunch(TokenDefinition token, int amount, int player)
        {
            Token = token;
            Amount = amount;
            PlayerId = player;
        }
    };
}