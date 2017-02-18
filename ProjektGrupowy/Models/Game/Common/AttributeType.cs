using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Common
{
    [System.Serializable]
    public class AttributeType
    {
        public AttributeType() { }

        public enum Type { INT, FLOAT, STRING, LONGTEXT, ENUM };

        public Type Id { get; set; }

        public AttributeType(Type id)
        {
            Id = id;
        }

        public virtual void Validate(string value) {  }
    };

    [System.Serializable]
    public class IntType : AttributeType
    {
        public IntType() : base(AttributeType.Type.INT) { }

        override public void Validate(string value)
        {
            int i;
            if (!int.TryParse(value, out i))
            {
                throw new Error.AttributeTypeError(value, "int");
            }

        }
    }

    [System.Serializable]
    public class FloatType : AttributeType
    {
        public FloatType() : base(AttributeType.Type.FLOAT) { }

        override public void Validate(string value)
        {
            float f;
            if (!float.TryParse(value, out f))
            {
                throw new Error.AttributeTypeError(value, "float");
            }
        }
    }

    [System.Serializable]
    public class StringType : AttributeType
    {
        public StringType() : base(AttributeType.Type.STRING) { }
    }

    [System.Serializable]
    public class LongTextType : AttributeType
    {
        public LongTextType() : base(AttributeType.Type.LONGTEXT) { }
    }

    [System.Serializable]
    public class EnumType : AttributeType
    {
        public string[] Domain { get; set; }

        public EnumType() { }

        public EnumType(string[] domain) : base(AttributeType.Type.ENUM)
        {
            Domain = domain;
        }

        public int GetIndex(string value)
        {
            for (int i=0;i<Domain.Length;i++)
            {
                if (Domain[i] == value) return i;
            }
            return -1;
        }

        override public void Validate(string value)
        {
            if (!Domain.Contains(value))
            {
                throw new Error.AttributeTypeError(value, "enum");
            }
        }
    }

}