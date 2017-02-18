using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.AvaxStorming
{
    public class AvaxStorming : GameDefinition
    {

        public AvaxStorming() : base("Avax Storming") { }

        private void CheckElement(ElementInstance element)
        {
            if (element.GetAttributeValue("Status") == "Pożądany")
            {
                element.Color = element.Definition.Colors[1] ;
            }
            else
            {
                element.Color = element.Definition.Colors[0];
            }
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