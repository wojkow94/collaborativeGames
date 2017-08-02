using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.How_Now_WowMatrix
{
    public class HowNowWowMatrixBuilder
    {
        public HowNowWowMatrixBuilder() { }

        private HowNowWowMatrix matrix;

        private ElementDefinition idea;

        private ImageDAO images = new ImageDAO();

        public void BuildElements()
        {
            idea = new ElementDefinition("Idea");

            idea.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            //       feature.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            //       feature.AddAttribute(new AttributeDefinition("Moduł", new StringType(), isAuto: true));
            //       feature.AddAttribute(new AttributeDefinition("Priorytet", new EnumType(new string[] { "Niski", "Normalny", "Wysoki" }), isAuto: true));
            //       feature.AddAttribute(new AttributeDefinition("Status", new EnumType(new string[] { "Wymagany", "Pożądany" }), defValue: "Wymagany"));

            idea.Colors.Add("LightSeaGreen");
            idea.Colors.Add("orange");
            idea.PrintedAttribute = "Nazwa";
            idea.ImageIconId = images.AddImage("/Resources/Images/gears.png");

            matrix.AddElementDefinition(idea);
            //        module.ImageIconId = images.AddImage("/Resources/Images/module.png");
        }

        private void BuildBoard()
        {
            
            RegionContainer[] originalityContainers = new RegionContainer[2];
            RegionContainer[] feasibilityContainers = new RegionContainer[2];
            int[] pos1 = { 5, 50 };

            for (int i = 0; i < 2; i++)
            {
                originalityContainers[i] = new RegionContainer(5, pos1[i], 45, 45, RegionContainer.OrientationType.HORIZONTAL);
                originalityContainers[i].SetAcceptElement(idea);
                BoardRegion region = new BoardRegion(new Color(0, 0, 0), 0.0f, "Nazwa");
                region.PopupAttribute = "Nazwa";
                originalityContainers[i].AddRegion(region);
                matrix.Board.AddContainer(originalityContainers[i]);

                feasibilityContainers[i] = new RegionContainer(50, pos1[i], 45, 45, RegionContainer.OrientationType.VERTICAL);
                feasibilityContainers[i].SetAcceptElement(idea);
                BoardRegion region2 = new BoardRegion(new Color(0, 0, 0), 0.0f, "Nazwa");
                region2.PopupAttribute = "Nazwa";
                feasibilityContainers[i].AddRegion(region2);
                matrix.Board.AddContainer(feasibilityContainers[i]);
            }
        }

        public void Build()
        {
            matrix = new HowNowWowMatrix();
            matrix.BackgorundImageId = images.AddImage("/Resources/Images/hnw.png");

            BuildElements();
            BuildBoard();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(matrix);
            }
        }
    }
}