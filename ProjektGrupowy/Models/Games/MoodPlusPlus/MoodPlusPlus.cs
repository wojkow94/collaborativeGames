﻿using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.MoodPlusPlusT
{
    [System.Serializable]
    public class MoodPlusPlus : GameDefinition
    {
        public MoodPlusPlus() : base("Mood++") { }

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