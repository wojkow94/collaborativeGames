using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Definitions
{

    

    [System.Serializable]
    public class ElementDefinition
    {
        public List<AttributeDefinition> Attributes = new List<AttributeDefinition>();
        public List<TokenDefinition> Tokens = new List<TokenDefinition>();
        public List<string> Colors = new List<string>();

        public Permission.TYPE AddAccess { get; set; }
        public Permission.TYPE AcceptAccess { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int ImageIconId { get; set; }
        public string PrintedAttribute { get; set; }

        public ElementDefinition(string name)
        {
            Name = name;
            AddAccess = Permission.TYPE.Any;
            AcceptAccess = Permission.TYPE.Moderator;
            Colors = new List<string>();
        }

        public ElementDefinition()
        {
            AddAccess = Permission.TYPE.Any;
            AcceptAccess = Permission.TYPE.Moderator;
        }

        public bool CanAdd(Player player)
        {
            if (AddAccess == Permission.TYPE.Any) return true;
            else return AddAccess == player.Type;
        }

        public bool CanAccept(Player player)
        {
            if (AcceptAccess == Permission.TYPE.Any) return true;
            else return AcceptAccess == player.Type;
        }

        public void AddAttribute(AttributeDefinition attr)
        {
            attr.Id = Attributes.Count;
            Attributes.Add(attr);
        }

        public bool HasAttribute(string attributeName)
        {
            return Attributes.Where(a => a.Name == attributeName).FirstOrDefault() != null;
        }

        public void AddToken(TokenDefinition token)
        {
            Tokens.Add(token);
        }

        public AttributeDefinition GetAttribute(int id)
        {
            if (id < 0 || id >= Attributes.Count)
            {
                throw new Error.AttributeNotFound(id);
            }
            else
            {
                return Attributes[id];
            }
        }

        public AttributeDefinition GetAttribute(string name)
        {
            int id = GetAttributeIdByName(name);
            return GetAttribute(id);
        }

        public int GetAttributeIdByName(string name)
        {
            int id = Attributes.FindIndex(c => c.Name.Equals(name));
            if (id < 0)
            {
                throw new Error.AttributeNotFound(id, name);
            }
            else
            {
                return id;
            }
        }
    }
}