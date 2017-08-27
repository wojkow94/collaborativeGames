
using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjektGrupowy.Models.Game.Instances
{
    [System.Serializable]
    public class ElementInstance : TokensOwner
    {
        public ElementInstance() {}

        [XmlIgnore]
        public bool CanBeAccepted { get; set; }

        [XmlIgnore]
        public ElementDefinition Definition { get; set; }

        [XmlIgnore]
        public BoardRegion Region { get; set; }


        public int DefinitionId { get; set; }
        public int RegionId { get; set; }
        public int RegionContainerId { get; set; }
        public string[] Values { get; set; }
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public bool IsAccepted { get; set; }
        public int Order { get; set; }
        public string Color { get; set; }

        public void Validate()
        {
            foreach (var attributeDef in Definition.Attributes)
            {
                attributeDef.Validate(Values[attributeDef.Id]);
            }
        }

        public string this[string attrName]
        {
            get
            {
                return this.GetAttributeValue(attrName);
            }
            set
            {
                SetAttributeValue(attrName, value);
            }
        }

        public ElementInstance(ElementDefinition def)
        {
            Definition = def;
            Color = def.Colors.Count > 0 ? def.Colors[0] : "";
            Values = new string[Definition.Attributes.Count];
            CanBeAccepted = true;

            for (int i = 0; i < Definition.Attributes.Count; i++)
            {
                Values[i] = def.Attributes[i].DefaultValue;
            }
        }

        public void Refresh()
        {
            if (Values.Length == Definition.Attributes.Count) return;

            var newValues = new string[Definition.Attributes.Count];

            for (int i=0;i<Definition.Attributes.Count;i++)
            {
                string value = GetAttributeValue(Definition.GetAttribute(i).Name);
                if (value == "") newValues[i] = Definition.Attributes[i].DefaultValue;
                else newValues[i] = value;
            }

            Values = newValues;
        }

        public void SetAttributeValue(string attr, string value)
        {
            int id = Definition.GetAttributeIdByName(attr);
            SetAttributeValue(id, value);
        }

        private void SetAttributeValue(int attrId, string value)
        {
            if (attrId < Values.Count()) Values[attrId] = value;
        }


        public string GetAttributeValue(string attr)
        {
            try
            {
                int id = Definition.GetAttributeIdByName(attr);
                if (id < Values.Length) return Values[id];
                return "";
            }
            catch(Error.AppError ex)
            {
                return "";
            }
        }
    }
}