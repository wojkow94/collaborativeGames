using System.Collections.Generic;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;

namespace ProjektGrupowy.Models.Games.WholeProduct
{
    public class WholeProductBuilder
    {
        private WholeProduct _wholeProduct;
        private ElementDefinition _idea;
        private readonly ImageDAO _images = new ImageDAO();

        public void BuildElements()
        {
            _idea = new ElementDefinition("Idea");

            _idea.AddAttribute(new AttributeDefinition("Nazwa", new StringType()));
            _idea.AddAttribute(new AttributeDefinition("Oryginalność", new EnumType(new[] { "Ogromna", "Duża", "Średnia", "Mała" }), isAuto: true));
            _idea.AddAttribute(new AttributeDefinition("Opis", new LongTextType(), isAuto: false, isRequired: false));

            _idea.ImageIconId = _images.AddImage("/Resources/Images/idea.png");
            _idea.PrintedAttribute = "Nazwa";
            _idea.Colors.Add("rgb(219, 40, 40)");

            _wholeProduct.AddElementDefinition(_idea);
        }

        private void BuildBoard()
        {
            RegionContainer[] containers = new RegionContainer[4];

            for (int i = 0; i < 4; i++)
            {
                containers[i] = new RegionContainer(i*12+8, i*12+8, 85-24*i, 85-24*i, RegionContainer.OrientationType.VERTICAL);
                containers[i].SetAcceptElement(_idea);

                BoardRegion region = new BoardRegion(new Color(0, 0, 0), 0.0f, "Nazwa");
                region.Attributes.Add(new BoardRegion.Attribute("Oryginalność", ((EnumType)_idea.GetAttribute("Oryginalność").Type).Domain[i]));
                region.PopupAttribute = "Nazwa";
                containers[i].AddRegion(region);

                _wholeProduct.Board.AddContainer(containers[i]);
            }
        }


        private void BuildTokens()
        {

        }

        public void Build()
        {
            _wholeProduct = new WholeProduct
            {
                BackgorundImageId = _images.AddImage("/Resources/Images/whole_product.png")
            };

            BuildElements();
            BuildBoard();
            BuildTokens();

            using (var dao = new GameDefinitionDAO())
            {
                dao.AddGameDefinition(_wholeProduct);
            }
        }
    }
}