using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGrupowy.Models.Game.Instances;

namespace ProjektGrupowy.Models.Game.Common
{
    [System.Serializable]
    public class Player : TokensOwner
    {
        public List<SortMethod> Sorting = new List<SortMethod>();
        public Permission.TYPE Type { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int GameId { get; set; }

        public Player(string name, Permission.TYPE type)
        {
            Type = type;
            Name = name;
        }

        public Player()
        {

        }

        public void AddTokensToElement(TokenDefinition token, ElementInstance element, int amount)
        {
            if (amount >= 0 && amount <= GetTokenAmount(token))
            {
                element.AddTokens(token, Id, amount);
                AddTokens(token, Id, -amount);
            }
        }

        public bool IsModerator()
        {
            return Type == Permission.TYPE.Moderator;
        }

        public void SetTokensToElement(TokenDefinition token, ElementInstance element, int amount)
        {
            if (amount < 0) return;
            int currentAmount = element.GetTokenAmount(token, Id);
            int diff =  amount - currentAmount;

            if (diff <= GetTokenAmount(token))
            {
                element.AddTokens(token, Id, diff);
                AddTokens(token, Id, -diff);
            }
        }

        public void OnJoinGame(GameInstance game)
        {
            foreach (var element in game.Definition.Elements)
            {
                Sorting.Add(new SortMethod(SortMethod.BY.None));
            }
            GameId = game.Id;
        }
    }
}