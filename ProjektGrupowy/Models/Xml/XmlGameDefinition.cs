using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Xml
{
    public class XmlGameDefinition : XmlEntity<GameDefinition>
    {
        public XmlGameDefinition() : base(Core.Platform.GameTypes) { }

        override protected void BeforeSerialization(GameDefinition o)
        {

        }

        override protected void AfterDeserialization(GameDefinition o)
        {
            foreach (var container in o.Board.RegionContainers)
            {
                foreach (var region in container.Regions)
                {
                    region.Container = container;
                }
            }

            o.OnDeserialize();
        }
    }
}