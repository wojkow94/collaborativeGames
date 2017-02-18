using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Definitions
{
    public class TokenDefinition
    {
        public string Name;
        public int Amount;
        public int Id;

        public int ImageIconId;
        public string Color;
        public string ReferenceAttribute;

        public TokenDefinition()
        {

        }

        public TokenDefinition(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}