using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Definitions
{
    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(StringType))]
    [System.Xml.Serialization.XmlInclude(typeof(IntType))]
    [System.Xml.Serialization.XmlInclude(typeof(FloatType))]
    [System.Xml.Serialization.XmlInclude(typeof(EnumType))]
    [System.Xml.Serialization.XmlInclude(typeof(LongTextType))]
    public class AttributeDefinition
    {
        public AttributeDefinition() { }

        public AttributeType Type { get; set; }

        public string Name {get; set; }
        public int Id { get; set; }
        public bool IsAuto { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }

        public AttributeDefinition(string name, AttributeType type, string defValue = "", bool isAuto = false, bool isRequired = true)
        { 
            Name = name;
            Type = type;
            IsAuto = isAuto;
            DefaultValue = defValue;
            IsRequired = isAuto == true ? false : isRequired;
        }

        public void Validate(string value)
        {
            if (IsAuto == false)
            {
                Type.Validate(value);
            }
            if (IsRequired && value == "")
            {
                throw new Error.AttributeIsRequired(Name);
            }
        }
    }
}