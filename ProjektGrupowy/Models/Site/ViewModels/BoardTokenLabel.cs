using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Site.ViewModels
{
    public class BoardTokenLabel
    {
        public TokenBunch Tokens { get; set; }
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public int ElementId { get; set; }

        public BoardTokenLabel(TokenBunch bunch, int playerId, string playerName, int elementId)
        {
            Tokens = bunch;
            PlayerId = playerId;
            PlayerName = playerName;
            ElementId = elementId;
        }
    }
}