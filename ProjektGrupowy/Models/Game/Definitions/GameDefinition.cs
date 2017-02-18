using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Instances;
using ProjektGrupowy.Models.Games.BuyAFeature;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjektGrupowy.Models.Game.Definitions
{
    [System.Serializable]
    public class GameDefinition
    {
        public GameDefinition() { }

        public List<ElementDefinition> Elements = new List<ElementDefinition>();
        public List<TokenDefinition> Tokens = new List<TokenDefinition>();

        public string Name {get; set; }
        public GameBoard Board { get; set; }
        public int Id { get; set; }

        public int BackgorundImageId { get; set; }

      

        public GameDefinition(string name)
        {
            BackgorundImageId = -1;
            Name = name;
            Board = new GameBoard();
        }

        public void AddElementDefinition(ElementDefinition elementDef)
        {
            elementDef.Id = Elements.Count;
            Elements.Add(elementDef);
        }

        public void AddTokenDefiniton(TokenDefinition tokenDef)
        {
            tokenDef.Id = Tokens.Count;
            Tokens.Add(tokenDef);
        }

        public TokenDefinition GetTokenDefinition(int id)
        {
            if (id < 0 || id >= Tokens.Count)
            {
                throw new Error.TokenDefinitionNotFound(id);
            }
            else
            {
                return Tokens[id];
            }
        }

        public ElementDefinition GetElementDefinition(int id)
        {
            if (id < 0 || id >= Elements.Count)
            {
                throw new Error.ElementDefinitionNotFound(id);
            }
            else
            {
                return Elements[id];
            }
        }

        public ElementDefinition GetElementDefinition(string name)
        {
            int id = Elements.FindIndex(c => c.Name.Equals(name));
            if (id >= 0)
            {
                return GetElementDefinition(id);
            }
            else
            {
                throw new Error.ElementDefinitionNotFound(id, name);
            }
        }

        virtual public void OnEditElement(GameInstance game, ElementInstance element) { }
        virtual public void OnAddElement(GameInstance game, ElementInstance element) { }
        virtual public void OnAcceptElement(GameInstance game, ElementInstance element) { }
        virtual public void OnAddToken(GameInstance game, Player player, ElementInstance element, TokenDefinition token, int amount) { }
        virtual public void OnSetToken(GameInstance game, Player player, ElementInstance element, TokenDefinition token, int amount) { }
        virtual public void OnDeserialize() { }
        virtual public void OnDeserializeInstance(GameInstance game) { }

    }
}