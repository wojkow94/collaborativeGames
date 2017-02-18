using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{

    static public class DatabaseInitializer
    {
        /*
        static public void SetUpAvaxStorming()
        {
            ImageDAO Images = new ImageDAO();
            GameDefinition avax = new GameDefinition("Avax Storming");
            avax.BackgorundImageId = Images.AddImage("/Resources/Images/avax.png");

            ElementDefinition feature = new ElementDefinition("Feature");
            feature.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            feature.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            feature.AddAttribute(new AttributeDefinition("Moduł", new StringType(), isAuto: true));
            feature.AddAttribute(new AttributeDefinition("Priorytet", new EnumType(new string[] { "Niski", "Normalny", "Wysoki" }), isAuto: true));
            feature.AddAttribute(new AttributeDefinition("Status", new EnumType(new string[] { "Wymagany", "Pożądany" }), defValue: "Wymagany"));
            feature.ImageIconId = Images.AddImage("/Resources/Images/gears.png");
            feature.Color = "LightSeaGreen";
            avax.AddElementDefinition(feature);
            
            ElementDefinition module = new ElementDefinition("Moduł");
            module.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            module.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            module.AddAttribute(new AttributeDefinition("Numer modułu", new IntType(), isAuto: true));
            module.ImageIconId = Images.AddImage("/Resources/Images/module.png");
            module.Color = "rgb(33, 133, 208)";
            avax.AddElementDefinition(module);


            RegionContainer[] modulesContainers = new RegionContainer[3];
            RegionContainer[] featuresContainers = new RegionContainer[3];

            int[] pos1 = { 0, 33, 67 };
            for (int i=0;i<3;i++)
            {
                modulesContainers[i] = new RegionContainer(0, pos1[i], 100, 8, RegionContainer.OrientationType.HORIZONTAL);
                modulesContainers[i].SetAcceptElement(module);

                BoardRegion region = new BoardRegion(new Color(0, 0, 255), 0.5f, "Nazwa");
                region.Attributes.Add(new BoardRegion.Attribute("Numer modułu", (i+1).ToString()));
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
                    regions[j].CalcAttributes.Add(new BoardRegion.CalcAttribute("Moduł", new ElementQuery().Select("Nazwa").From("Moduł").Where("Numer modułu").Equals((i+1).ToString())));
                    featuresContainers[i].AddRegion(regions[j]);
                }
                avax.Board.AddContainer(featuresContainers[i]);
            }

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(avax);
            }
        }

        static public void SetUpBuyAFeatureGame()
        {
            ImageDAO Images = new ImageDAO();

            GameDefinition buyAFeature = new GameDefinition("Buy a Feature");
            buyAFeature.BackgorundImageId = Images.AddImage("/Resources/Images/baf.png");

            ElementDefinition feature = new ElementDefinition("Feature");
            feature.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            feature.AddAttribute(new AttributeDefinition("Cena", new IntType()));
            feature.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            feature.ImageIconId = Images.AddImage("/Resources/Images/gears.png");
            feature.Color = "forestgreen";

            buyAFeature.AddElementDefinition(feature);

            RegionContainer featuresContainer = new RegionContainer(4, 25, 92, 71, RegionContainer.OrientationType.HORIZONTAL);
            featuresContainer.SetAcceptElement(feature);
            BoardRegion r1 = new BoardRegion(new Color(255, 255, 0), 0.1f, "Nazwa");
            r1.PopupAttribute = "Opis";

            featuresContainer.AddRegion(r1);
            buyAFeature.Board.AddContainer(featuresContainer);

            TokenDefinition token = new TokenDefinition("Kasa", 50);
            token.ImageIconId = Images.AddImage("/Resources/Images/dolar.png");
            token.Color = "green";

            buyAFeature.AddTokenDefiniton(token);
            feature.AddToken(token);

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(buyAFeature);
            }
        }

        public static void SetUpSpeedBoatGame()
        {
            ImageDAO Images = new ImageDAO();

            GameDefinition speedBoat = new GameDefinition("Speed Boat");
            speedBoat.BackgorundImageId = Images.AddImage("/Resources/Images/boat.png");

            //---------- Anchor definition

            ElementDefinition anchor = new ElementDefinition("Kotwica");
            anchor.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            anchor.AddAttribute(new AttributeDefinition("Głębokość", new EnumType(new string[] { "Mała", "Średnia", "Duża" }), isAuto: true));
            anchor.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            anchor.ImageIconId = Images.AddImage("/Resources/Images/anchor.png");
            anchor.Color = "rgb(219, 40, 40)";
            speedBoat.AddElementDefinition(anchor);

            RegionContainer anchorsContainer = new RegionContainer(0, 50, 100, 50, RegionContainer.OrientationType.HORIZONTAL);
            anchorsContainer.SetAcceptElement(anchor);
            BoardRegion[] regions = new BoardRegion[3];
            Color red = new Color(255, 0, 0);
            for (int i=0;i<3;i++)
            {
                regions[i] = new BoardRegion(red, 0.1f * (i + 1), "Nazwa");
                regions[i].PopupAttribute = "Opis";
                regions[i].Attributes.Add(new BoardRegion.Attribute("Głębokość", ((EnumType)anchor.GetAttribute("Głębokość").Type).Domain[i]));
                anchorsContainer.AddRegion(regions[i]);
            }
            speedBoat.Board.AddContainer(anchorsContainer);

            //---------- Propeller definition

            ElementDefinition propeller = new ElementDefinition("Śmigło");
            propeller.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            propeller.AddAttribute(new AttributeDefinition("Siła", new EnumType(new string[] { "Mała", "Średnia", "Duża" }), isAuto: true));
            propeller.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));
            propeller.ImageIconId = Images.AddImage("/Resources/Images/propeller.png");
            propeller.Color = "rgb(33, 133, 208)";
            speedBoat.AddElementDefinition(propeller);

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

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(speedBoat);
            }
        }*/

    }
}

/*
DBCC CHECKIDENT ('DBCookiePermission', RESEED, 0)
DBCC CHECKIDENT ('DBGameDefinition', RESEED, 0)
DBCC CHECKIDENT ('DBGameInstance', RESEED, 0)
DBCC CHECKIDENT ('DBPermission', RESEED, 0)
DBCC CHECKIDENT ('Image', RESEED, 0)
*/