
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ProjektGrupowy.Models.Game.Instances
{

    public class OrderedElementsSet
    {
        public IEnumerable<ElementInstance> Elements;
        public SortMethod Sorting;

        public OrderedElementsSet(IEnumerable<ElementInstance> elements, SortMethod sorting)
        {
            Elements = elements;
            Sorting = sorting;
        }
    }

    [System.Serializable]
    public class GameInstance : TokensOwner
    {
        [XmlIgnore]
        public GameDefinition Definition { get; set; }

        public List<ElementInstance> Elements = new List<ElementInstance>();
        public List<Player> Players = new List<Player>();

        public List<List<SortMethod>> Sorting = new List<List<SortMethod>>();

        public int Id { get; set; }
        public int DefinitionId { get; set; }


        public GameInstance(GameDefinition def)
        {
            Definition = def;
        }

        public GameInstance()
        {

        }

        public int AddElement(ElementInstance element)
        {
            element.Id = Elements.Count > 0 ? Elements.Max(e => e.Id) + 1 : 0;
            element.GameId = Id;
            element.Validate();
            Elements.Add(element);

            Definition.OnAddElement(this, element);
            return element.Id;
        }

        public void AddTokens(Player player, TokenDefinition token, ElementInstance element, int amount)
        {
            player.AddTokensToElement(token, element, amount);
            Definition.OnAddToken(this, player, element, token, amount);
        }

        public void SetTokens(Player player, TokenDefinition token, ElementInstance element, int amount)
        {
            player.SetTokensToElement(token, element, amount);
            Definition.OnSetToken(this, player, element, token, amount);
        }

        public Player GetPlayerById(int? id)
        {
            if (id == null) return null;
            return Players.Where(p => p.Id == id).FirstOrDefault();
        }

        public int AddPlayer(Player player)
        {
            player.Id = Players.Count > 0 ? Players.Max(p => p.Id) + 1 : 0;
            foreach (var tokenDef in Definition.Tokens)
            {
                player.AddTokens(tokenDef, player.Id, tokenDef.Amount);
            }
            //player.GameId = Id;
            Players.Add(player);
            player.OnJoinGame(this);

            return player.Id;
        }

        public ElementInstance GetElement(int elementId)
        {
            return Elements.FirstOrDefault(e => e.Id == elementId);
        }

        public Player GetPlayer(int playerId)
        {
            return Players.FirstOrDefault(p => p.Id == playerId);
        }

        public bool AcceptElement(int elementId, int containerId, int regionId)
        {
            ElementInstance element = GetElement(elementId);
            if (element == null) return false;

            RegionContainer container = Definition.Board.GetContainer(containerId);
            if (container == null) return false;

            if (container.AcceptElementId != element.Definition.Id) return false;

            BoardRegion region = container.GetRegion(regionId);
            if (region == null) return false;

            if (element.IsAccepted == true && (element.Region.Container.Id != containerId || element.Region.Id != regionId))
            {
                GetAcceptedElements(element.Region.Container.Id, element.Region.Id).ForEach(el => {
                    if (el.Order > element.Order) el.Order--;
                });
            }

            var elements = GetAcceptedElements(containerId, regionId);
            if (element.IsAccepted == true && element.Region.Container.Id == containerId && element.Region.Id == regionId)
            {
                elements.ForEach(el => {
                    if (el.Order > element.Order) el.Order--;
                });
            }

            foreach (var a in region.Attributes)
            {
                element[a.Name] = a.Value;
            }

            foreach (var calcAttr in region.CalcAttributes)
            {
                element[calcAttr.Name] = ExecuteQuery(calcAttr.Query);
            }

            element.IsAccepted = true;
            element.Region = region;
            element.Order = elements.Count;

            Definition.OnAcceptElement(this, element);

            return true;
        }

        public string ExecuteQuery(ElementQuery query)
        {
            ElementInstance result = Elements.Where(el => el.Definition.Name == query.FromStatement && el.GetAttributeValue(query.WhereStatement) == query.EqualsStatement).FirstOrDefault();
            if (result != null)
            {
                return result.GetAttributeValue(query.SelectStatement);
            }
            return "";
        }

        public void RejectElement(int elementId)
        {
            ElementInstance element = GetElement(elementId);
            foreach (var tokenBunch in element.Tokens)
            {
                GetPlayer(tokenBunch.PlayerId).AddTokens(tokenBunch.Token, tokenBunch.PlayerId, tokenBunch.Amount);
            }
            element.Tokens.Clear();
            foreach (var a in element.Region.Attributes)
            {
                element[a.Name] = "";
            }
            element.IsAccepted = false;
            element.CanBeAccepted = true;
        }

        public OrderedElementsSet GetOrderedElements(int elementDefId, SortMethod sorting)
        {
            var elements = Elements.Where(el => el.Definition.Id == elementDefId).ToList();

            IEnumerable<ElementInstance> sorted;
            if (sorting.By == SortMethod.BY.None || sorting.Direction == SortMethod.DIRECTION.None)
            {
                sorted = elements;
            }
            else if (sorting.By == SortMethod.BY.Attribute)
            {
                var attrType = Definition.GetElementDefinition(elementDefId).GetAttribute(sorting.Id).Type;
                if (attrType.Id == AttributeType.Type.ENUM)
                {
                    sorted = sorting.Direction == SortMethod.DIRECTION.Asc ? elements.OrderBy(e => ((EnumType)attrType).GetIndex(e.Values[sorting.Id])) :
                    elements.OrderByDescending(e => ((EnumType)attrType).GetIndex(e.Values[sorting.Id]));
                }
                else
                {
                    sorted = sorting.Direction == SortMethod.DIRECTION.Asc ? elements.OrderBy(e => e.Values[sorting.Id]) :
                    elements.OrderByDescending(e => e.Values[sorting.Id]);
                }
            }
            else if (sorting.By == SortMethod.BY.Token)
            {
                sorted = sorting.Direction == SortMethod.DIRECTION.Asc ? elements.OrderBy(e => e.GetTokenAmount(Definition.GetTokenDefinition(sorting.Id))) :
                    elements.OrderByDescending(e => e.GetTokenAmount(Definition.GetTokenDefinition(sorting.Id)));
            }
            else if (sorting.By == SortMethod.BY.Player)
            {
                sorted = sorting.Direction == SortMethod.DIRECTION.Asc ? elements.OrderBy(e => e.PlayerId) : elements.OrderByDescending(e => e.PlayerId);
            }
            else sorted = null;

            return new OrderedElementsSet(sorted, sorting);
        }

        public List<ElementInstance> GetProposedElements(int elementDefId = -1) 
        {
            return Elements.FindAll(el => el.IsAccepted == false && (el.Definition.Id == elementDefId || elementDefId < 0));
        }

        public List<ElementInstance> GetAcceptedElements(int elementDefId = -1)
        {
            return Elements.FindAll(el => el.IsAccepted == true && (el.Definition.Id == elementDefId || elementDefId < 0));
        }

        public List<ElementInstance> GetAcceptedElements(int containerId, int regionId)
        {
            return Elements.FindAll(el => el.IsAccepted == true && el.Region.Container.Id == containerId && el.Region.Id == regionId)
                .OrderBy(e => e.Order).ToList();
        }
    }
}