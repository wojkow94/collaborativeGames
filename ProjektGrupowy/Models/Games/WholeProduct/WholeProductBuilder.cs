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

            _idea.AddAttribute(new AttributeDefinition("Name", new StringType()));
            _idea.AddAttribute(new AttributeDefinition("Area", new EnumType(new[] { "Potential", "Augmented", "Expected", "Generic" }), isAuto: true));
            _idea.AddAttribute(new AttributeDefinition("Description", new LongTextType(), isAuto: false, isRequired: false));

            _idea.ImageIconId = _images.AddImage("/Resources/Images/idea.png");
            _idea.PrintedAttribute = "Name";
            _idea.Colors.Add("rgb(219, 40, 40)");

            _wholeProduct.AddElementDefinition(_idea);
        }

        private void BuildBoard()
        {
            RegionContainer[] containers = new RegionContainer[4];

            for (int i = 0; i < 4; i++)
            {
                containers[i] = new RegionContainer(i*22, i*25, 100-(i*22), 25, RegionContainer.OrientationType.VERTICAL);
                containers[i].SetAcceptElement(_idea);

                BoardRegion region = new BoardRegion(new Color(0, 0, 0), 0.0f, "Name");
                region.Attributes.Add(new BoardRegion.Attribute("Area", ((EnumType)_idea.GetAttribute("Area").Type).Domain[i]));
                region.PopupAttribute = "Name";
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