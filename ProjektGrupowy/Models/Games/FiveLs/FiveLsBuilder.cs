using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.FiveLs
{
    public class FiveLsBuilder
    {
        public FiveLsBuilder() { }

        private FiveLs matrix;

        private ElementDefinition idea;

        private ImageDAO images = new ImageDAO();

        public void BuildElements()
        {
            idea = new ElementDefinition("Idea");

            idea.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            idea.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));

            idea.Colors.Add("grey");
            idea.PrintedAttribute = "Nazwa";
            idea.ImageIconId = images.AddImage("/Resources/Images/finger.png");

            matrix.AddElementDefinition(idea);
        }

        private void BuildBoard()
        {
            RegionContainer[] originalityContainers = new RegionContainer[5];
            var coordinatesLo = new List<System.Drawing.Point> { new System.Drawing.Point(0, 50), new System.Drawing.Point(34, 50), new System.Drawing.Point(67, 50) };
            var coordinatesUp = new List<System.Drawing.Point> { new System.Drawing.Point(0, 0), new System.Drawing.Point(50, 0) };
            
            for (int i = 0; i < 2; i++)
            {
                originalityContainers[i] = new RegionContainer(coordinatesUp[i].X, coordinatesUp[i].Y, 50, 50, RegionContainer.OrientationType.VERTICAL);
                originalityContainers[i].SetAcceptElement(idea);
                BoardRegion region = new BoardRegion(new Color(0, 0, 0), 0.0f, "Nazwa");
                region.PopupAttribute = "Nazwa";
                originalityContainers[i].AddRegion(region);
                matrix.Board.AddContainer(originalityContainers[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                originalityContainers[i] = new RegionContainer(coordinatesLo[i].X, coordinatesLo[i].Y, 33, 50, RegionContainer.OrientationType.VERTICAL);
                originalityContainers[i].SetAcceptElement(idea);
                BoardRegion region = new BoardRegion(new Color(0, 250, 0), 0.0f, "Nazwa");
                region.PopupAttribute = "Nazwa";
                originalityContainers[i].AddRegion(region);
                matrix.Board.AddContainer(originalityContainers[i]);
            }
        }

        public void Build()
        {
            matrix = new FiveLs();
            matrix.BackgorundImageId = images.AddImage("/Resources/Images/5l.png");

            BuildElements();
            BuildBoard();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(matrix);
            }
        }
    }
}