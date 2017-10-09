using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.SWOTAnalysis
{
    public class SWOTAnalysisBuilder
    {
        public SWOTAnalysisBuilder() { }

        private SWOTAnalysis SWOT;

        private ElementDefinition strength;
        private ElementDefinition weakness;
        private ElementDefinition opportunity;
        private ElementDefinition threat;

        private ImageDAO images = new ImageDAO();

        public void BuildElements()
        {
            strength = new ElementDefinition("Strength");
            strength.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            strength.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            strength.Colors.Add("grey");
            strength.PrintedAttribute = "Nazwa";
            strength.ImageIconId = images.AddImage("/Resources/Images/strength.png");


            weakness = new ElementDefinition("Weakness");
            weakness.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            weakness.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            weakness.Colors.Add("grey");
            weakness.PrintedAttribute = "Nazwa";
            weakness.ImageIconId = images.AddImage("/Resources/Images/weakness.png");


            opportunity = new ElementDefinition("Opportunity");
            opportunity.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            opportunity.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            opportunity.Colors.Add("grey");
            opportunity.PrintedAttribute = "Nazwa";
            opportunity.ImageIconId = images.AddImage("/Resources/Images/opportunity.png");


            threat = new ElementDefinition("Threat");
            threat.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            threat.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            threat.Colors.Add("grey");
            threat.PrintedAttribute = "Nazwa";
            threat.ImageIconId = images.AddImage("/Resources/Images/threat.png");



            SWOT.AddElementDefinition(strength);
            SWOT.AddElementDefinition(weakness);
            SWOT.AddElementDefinition(opportunity);
            SWOT.AddElementDefinition(threat);
        }

        private void BuildBoard()
        {
            RegionContainer[] originalityContainers = new RegionContainer[4];

            var coordinates = new List<System.Drawing.Point> { new System.Drawing.Point(5,10), new System.Drawing.Point(5, 60), new System.Drawing.Point(55, 10), new System.Drawing.Point(55, 60) };

            for (int i = 0; i < 4; i++)
            {
                originalityContainers[i] = new RegionContainer(coordinates[i].X, coordinates[i].Y, 40, 35, RegionContainer.OrientationType.VERTICAL);
                BoardRegion region = new BoardRegion(new Color(0, 0, 0), 0.0f, "Nazwa");
                region.PopupAttribute = "Nazwa";
                originalityContainers[i].AddRegion(region);
                SWOT.Board.AddContainer(originalityContainers[i]);
            }
            originalityContainers[0].SetAcceptElement(strength);
            originalityContainers[2].SetAcceptElement(weakness);
            originalityContainers[1].SetAcceptElement(opportunity);
            originalityContainers[3].SetAcceptElement(threat);
        }

        public void Build()
        {
            SWOT = new SWOTAnalysis();
            SWOT.BackgorundImageId = images.AddImage("/Resources/Images/SWOTAnalysis.png");

            BuildElements();
            BuildBoard();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(SWOT);
            }
        }
    }
}