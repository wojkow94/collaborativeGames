using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.FiveLs
{
    [System.Serializable]
    public class FiveLs : GameDefinition
    {
        public FiveLs() : base("5L's") { }

        private void CheckElement(ElementInstance element)
        {
        }

        override public void OnEditElement(GameInstance game, ElementInstance element)
        {
            CheckElement(element);
        }

        override public void OnAddElement(GameInstance game, ElementInstance element)
        {
            CheckElement(element);
        }

        public override void OnDeserializeInstance(GameInstance game)
        {
            foreach (var element in game.Elements)
            {
                CheckElement(element);
            }
        }
    }
}