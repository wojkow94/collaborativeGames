using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.AvaxStorming
{
    public class AvaxStormingBuilder
    {
        private AvaxStorming avax;

        private ElementDefinition feature;
        private ElementDefinition module;

        private ImageDAO images = new ImageDAO();

        public AvaxStormingBuilder() { }

        public void BuildElements()
        {
            feature = new ElementDefinition("Feature");
            feature.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            feature.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            feature.AddAttribute(new AttributeDefinition("Moduł", new StringType(), isAuto: true));
            feature.AddAttribute(new AttributeDefinition("Priorytet", new EnumType(new string[] { "Niski", "Normalny", "Wysoki" }), isAuto: true));
            feature.AddAttribute(new AttributeDefinition("Status", new EnumType(new string[] { "Wymagany", "Pożądany" }), defValue: "Wymagany"));
            feature.ImageIconId = images.AddImage("/Resources/Images/gears.png");
            feature.Colors.Add("LightSeaGreen");
            feature.Colors.Add("orange");
            feature.PrintedAttribute = "Nazwa";

            module = new ElementDefinition("Moduł");
            module.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            module.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            module.AddAttribute(new AttributeDefinition("Numer modułu", new IntType(), isAuto: true));
            module.ImageIconId = images.AddImage("/Resources/Images/module.png");
            module.Colors.Add("rgb(33, 133, 208)");
            module.PrintedAttribute = "Nazwa";

            avax.AddElementDefinition(module);
            avax.AddElementDefinition(feature);
        }

        private void BuildBoard()
        {
            RegionContainer[] modulesContainers = new RegionContainer[3];
            RegionContainer[] featuresContainers = new RegionContainer[3];

            int[] pos1 = { 0, 33, 67 };
            for (int i = 0; i < 3; i++)
            {
                modulesContainers[i] = new RegionContainer(0, pos1[i], 100, 8, RegionContainer.OrientationType.HORIZONTAL);
                modulesContainers[i].SetAcceptElement(module);

                BoardRegion region = new BoardRegion(new Color(0, 0, 255), 0.5f, "Nazwa");
                region.Attributes.Add(new BoardRegion.Attribute("Numer modułu", (i + 1).ToString()));
                region.PopupAttribute = "Opis";
                modulesContainers[i].AddRegion(region);
                avax.Board.AddContainer(modulesContainers[i]);
            }

            int[] pos2 = { 8, 41, 75 };
            BoardRegion[] regions = new BoardRegion[3];
            for (int i = 0; i < 3; i++)
            {
                featuresContainers[i] = new RegionContainer(0, pos2[i], 100, i == 1 ? 26 : 25, RegionContainer.OrientationType.VERTICAL);
                featuresContainers[i].SetAcceptElement(feature);
                for (int j = 0; j < 3; j++)
                {
                    regions[j] = new BoardRegion(new Color(0, 0, 255), 0.1f * (j + 1), "Nazwa");
                    regions[j].PopupAttribute = "Opis";
                    regions[j].Attributes.Add(new BoardRegion.Attribute("Priorytet", ((EnumType)feature.GetAttribute("Priorytet").Type).Domain[j]));
                    regions[j].CalcAttributes.Add(new BoardRegion.CalcAttribute("Moduł", new ElementQuery().Select("Nazwa").From("Moduł").Where("Numer modułu").Equals((i + 1).ToString())));
                    featuresContainers[i].AddRegion(regions[j]);
                }
                avax.Board.AddContainer(featuresContainers[i]);
            }
        }


        private void BuildTokens()
        {
        }

        public void Build()
        {
            avax = new AvaxStorming();
            avax.BackgorundImageId = images.AddImage("/Resources/Images/avax.png");

            BuildElements();
            BuildBoard();
            BuildTokens();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(avax);
            }
        }
    }
}