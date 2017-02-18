using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Xml
{
    public class XmlGameInstance : XmlEntity<GameInstance>
    {
        public XmlGameInstance() : base(null) {}

        override protected void BeforeSerialization(GameInstance o)
        {
            o.DefinitionId = o.Definition.Id;
            foreach (var e in o.Elements)
            {
                e.DefinitionId = e.Definition.Id;
                if (e.IsAccepted)
                {
                    e.RegionId = e.Region.Id;
                    e.RegionContainerId = e.Region.Container.Id;
                }
                foreach (var t in e.Tokens)
                {
                    t.DefinitionId = t.Token.Id;
                }
            }
            foreach (var t in o.Tokens)
            {
                t.DefinitionId = t.Token.Id;
            }
        }

        
        override protected void AfterDeserialization(GameInstance o)
        {
            var gameDef = Core.Platform.GetGameDefinitionById(o.DefinitionId);
            o.Definition = gameDef;

            foreach (var player in o.Players)
            {
                foreach (var t in player.Tokens)
                {
                    t.Token = gameDef.GetTokenDefinition(t.DefinitionId);
                }
            }
            foreach (var element in o.Elements)
            {
                element.Definition = gameDef.GetElementDefinition(element.DefinitionId);
                if (element.Color == null || element.Color == "")
                {
                    element.Color = element.Definition.Colors.Count > 0 ? element.Definition.Colors[0] : "";
                }
                if (element.IsAccepted)
                {
                    element.Region = gameDef.Board.GetContainer(element.RegionContainerId).GetRegion(element.RegionId);
                }
                foreach (var t in element.Tokens)
                {
                    t.Token = gameDef.GetTokenDefinition(t.DefinitionId);
                }
                foreach (var t in o.Tokens)
                {
                    t.Token = gameDef.GetTokenDefinition(t.DefinitionId);
                }

                element.Refresh();
            }

            gameDef.OnDeserializeInstance(o);
        }
    }
}