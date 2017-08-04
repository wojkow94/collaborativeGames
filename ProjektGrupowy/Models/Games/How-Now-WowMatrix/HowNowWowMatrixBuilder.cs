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
            idea.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));

            idea.Colors.Add("grey");
            idea.PrintedAttribute = "Nazwa";
            idea.ImageIconId = images.AddImage("/Resources/Images/idea.png");

            matrix.AddElementDefinition(idea);
        }

        private void BuildBoard()
        {
            RegionContainer[] originalityContainers = new RegionContainer[3];

            var coordinates = new List<System.Drawing.Point> { new System.Drawing.Point(5, 50), new System.Drawing.Point(50, 5) , new System.Drawing.Point(50, 50) };

            for (int i = 0; i < 3; i++)
            {
                originalityContainers[i] = new RegionContainer(coordinates[i].X, coordinates[i].Y, 45, 45, RegionContainer.OrientationType.VERTICAL);
                originalityContainers[i].SetAcceptElement(idea);
                BoardRegion region = new BoardRegion(new Color(0, 0, 0), 0.0f, "Nazwa");
                region.PopupAttribute = "Nazwa";
                originalityContainers[i].AddRegion(region);
                matrix.Board.AddContainer(originalityContainers[i]);
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