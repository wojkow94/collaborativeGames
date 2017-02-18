using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Common
{
    [System.Serializable]
    public class TokensOwner
    {
        public List<TokenBunch> Tokens = new List<TokenBunch>();

        public TokensOwner() {}

        public int GetTokenAmount(TokenDefinition token, int playerId = -1)
        {
            if (playerId == -1)
            {
                return Tokens
                    .Where(t => t.Token.Id == token.Id)
                    .Sum(t => t.Amount);
            }

            TokenBunch bunch = Tokens
                .Where(t => t.PlayerId == playerId && t.Token.Id == token.Id)
                .FirstOrDefault();

            return bunch != null ? bunch.Amount : 0;
        }

        public void AddTokens(TokenDefinition token, int playerId, int amount)
        {
            TokenBunch bunch = Tokens
                .Where(t => t.PlayerId == playerId && t.Token.Id == token.Id)
                .FirstOrDefault();
            
            if (bunch == null)
                Tokens.Add(new TokenBunch(token, amount, playerId));
            else
                bunch.Amount += amount;
        }

        public void SetTokens(TokenDefinition token, int playerId, int amount)
        {
            TokenBunch bunch = Tokens
                .Where(t => t.PlayerId == playerId && t.Token.Id == token.Id)
                .FirstOrDefault();

            if (bunch != null)
                AddTokens(token, playerId, amount - bunch.Amount);
            else
                AddTokens(token, playerId, amount);
        }
    }
}