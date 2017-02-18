using ProjektGrupowy.Models.Database;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.BuyAFeature
{

    [System.Serializable]
    public class BuyAFeature : GameDefinition
    {

        public BuyAFeature() : base("Buy a Feature") {}

        private void CheckElement(ElementInstance element)
        {
            if (element.GetTokenAmount(GetTokenDefinition(0)) >= int.Parse(element.GetAttributeValue("Cena")))
            {
                element.Color = element.Definition.Colors[1];
                element.SetAttributeValue("Czy kupiony?", "Tak");
            }
            else
            {
                element.Color = element.Definition.Colors[0];
                element.SetAttributeValue("Czy kupiony?", "Nie");
            }
        }


        override public void OnEditElement(GameInstance game, ElementInstance element)
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

        override public void OnSetToken(GameInstance game, Player player, ElementInstance element, TokenDefinition token, int amount)
        {
            CheckElement(element);
        }
    }
}