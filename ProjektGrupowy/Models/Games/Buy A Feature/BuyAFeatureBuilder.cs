using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.BuyAFeature
{
    public class BuyAFeatureBuilder
    {
        private BuyAFeature baf;
        private ElementDefinition feature;

        private ImageDAO images = new ImageDAO();

        public BuyAFeatureBuilder() {}

        public void BuildElements()
        {
            feature = new ElementDefinition("Feature");
            feature.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            feature.AddAttribute(new AttributeDefinition("Cena", new IntType()));
            feature.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            feature.AddAttribute(new AttributeDefinition("Czy kupiony?", new StringType(), isAuto: true, isRequired: false));
            feature.ImageIconId = images.AddImage("/Resources/Images/gears.png");
            feature.Colors.Add("darkcyan");
            feature.Colors.Add("forestgreen");
            feature.AddAccess = Permission.TYPE.Moderator;
            feature.AcceptAccess = Permission.TYPE.Any;
            feature.PrintedAttribute = "Nazwa";

            baf.AddElementDefinition(feature);
        }

        private void BuildBoard()
        {
            var featureContainer = new RegionContainer(4, 25, 92, 71, RegionContainer.OrientationType.HORIZONTAL);
            featureContainer.SetAcceptElement(feature);
            BoardRegion r1 = new BoardRegion(new Color(255, 255, 0), 0.1f, "Nazwa");
            r1.PopupAttribute = "Opis";

            featureContainer.AddRegion(r1);
            baf.Board.AddContainer(featureContainer);
        }


        private void BuildTokens()
        {
            var money = new TokenDefinition("Kasa", 50);
            money.ImageIconId = images.AddImage("/Resources/Images/dolar.png");
            money.Color = "green";
            money.ReferenceAttribute = "Cena";

            baf.AddTokenDefiniton(money);
            feature.AddToken(money);
        }

        public void Build()
        {
            baf = new BuyAFeature();
            baf.BackgorundImageId = images.AddImage("/Resources/Images/baf.png");

            BuildElements();
            BuildBoard();
            BuildTokens();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(baf);
            }
        }
    }
}