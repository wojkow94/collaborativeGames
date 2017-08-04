using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Games.SpeedBoat
{
    public class SpeedBoatBuilder
    {
        private SpeedBoat speedBoat;

        private ElementDefinition anchor;
        private ElementDefinition propeller;

        private ImageDAO images = new ImageDAO();

        public SpeedBoatBuilder() { }

        public void BuildElements()
        {
            anchor = new ElementDefinition("Kotwica");
            anchor.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            anchor.AddAttribute(new AttributeDefinition("Głębokość", new EnumType(new string[] { "Mała", "Średnia", "Duża" }), isAuto: true));
            anchor.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            anchor.ImageIconId = images.AddImage("/Resources/Images/anchor.png");
            anchor.PrintedAttribute = "Nazwa";
            anchor.Colors.Add("rgb(219, 40, 40)");

            

            propeller = new ElementDefinition("Śmigło");
            propeller.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            propeller.AddAttribute(new AttributeDefinition("Siła", new EnumType(new string[] { "Mała", "Średnia", "Duża" }), isAuto: true));
            propeller.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            propeller.ImageIconId = images.AddImage("/Resources/Images/propeller.png");
            propeller.PrintedAttribute = "Nazwa";
            propeller.Colors.Add("rgb(33, 133, 208)");

            speedBoat.AddElementDefinition(anchor);
            speedBoat.AddElementDefinition(propeller);
        }

        private void BuildBoard()
        {
            RegionContainer anchorsContainer = new RegionContainer(0, 50, 100, 50, RegionContainer.OrientationType.HORIZONTAL);
            anchorsContainer.SetAcceptElement(anchor);
            BoardRegion[] regions = new BoardRegion[3];
            Color red = new Color(255, 0, 0);
            for (int i = 0; i < 3; i++)
            {
                regions[i] = new BoardRegion(red, 0.1f * (i + 1), "Nazwa");
                regions[i].PopupAttribute = "Opis";
                regions[i].Attributes.Add(new BoardRegion.Attribute("Głębokość", ((EnumType)anchor.GetAttribute("Głębokość").Type).Domain[i]));
                anchorsContainer.AddRegion(regions[i]);
            }
            speedBoat.Board.AddContainer(anchorsContainer);

            RegionContainer propellersContainer = new RegionContainer(0, 0, 50, 50, RegionContainer.OrientationType.HORIZONTAL);
            propellersContainer.SetAcceptElement(propeller);

            Color blue = new Color(0, 0, 255);
            for (int i = 0; i < 3; i++)
            {
                regions[i] = new BoardRegion(blue, 0.1f * (i + 1), "Nazwa");
                regions[i].PopupAttribute = "Opis";
                regions[i].Attributes.Add(new BoardRegion.Attribute("Siła", ((EnumType)propeller.GetAttribute("Siła").Type).Domain[i]));
                propellersContainer.AddRegion(regions[i]);
            }
            speedBoat.Board.AddContainer(propellersContainer);
        }


        private void BuildTokens()
        {
        }

        public void Build()
        {
            speedBoat = new SpeedBoat();
            speedBoat.BackgorundImageId = images.AddImage("/Resources/Images/boat.png");

            BuildElements();
            BuildBoard();
            BuildTokens();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(speedBoat);
            }
        }
    }
}