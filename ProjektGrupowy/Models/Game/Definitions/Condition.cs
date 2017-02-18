using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Definitions
{

    [System.Serializable]
    public class ConditionPart
    {
        public ConditionPart() { }

        public enum ConditionOperand { NULL, ELEMENT, ATTRIBUTE, OPERATOR };
        public enum ConditionOperator { NULL, EQUALS, GREATER, LOWER };

        public ConditionOperand OperandType { get; set; }
        public ConditionOperator OperatorType { get; set; }

        public string Operand { get; set; }

        public ConditionPart(string op, ConditionOperand type, ConditionOperator oper = ConditionOperator.EQUALS)
        {
            Operand = op;
            OperandType = type;
            OperatorType = oper;
        }
    }

    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(ConditionPart))]
    public class Condition
    {
        public Condition() { }

        public ConditionPart ElementCondition { get; set; }
        public ConditionPart AttributeCondition { get; set; }
        public ConditionPart ValueCondition { get; set; }

        public IEnumerable<ElementInstance> FilterList(List<ElementInstance> elements)
        {
            Func<ElementInstance, bool> predicate;

            switch (ValueCondition.OperatorType)
            {
                case ConditionPart.ConditionOperator.EQUALS:
                    predicate = el => el[AttributeCondition.Operand].Equals(ValueCondition.Operand);
                    break;
                case ConditionPart.ConditionOperator.GREATER:
                    predicate = el => float.Parse(el[AttributeCondition.Operand]) > float.Parse(ValueCondition.Operand);
                    break;
                case ConditionPart.ConditionOperator.LOWER:
                    predicate = el => float.Parse(el[AttributeCondition.Operand]) < float.Parse(ValueCondition.Operand);
                    break;
                default:
                    predicate = el => true;
                    break;
            }

            return elements.Where(el => el.Definition.Name == ElementCondition.Operand).Where(predicate);
        }

        public Condition Element(string name)
        {
            ElementCondition = new ConditionPart(name, ConditionPart.ConditionOperand.ELEMENT);
            return this;
        }
        public Condition Attribute(string name)
        {
            AttributeCondition = new ConditionPart(name, ConditionPart.ConditionOperand.ATTRIBUTE);
            return this;
        }

        public Condition Equals(string value)
        {
            ValueCondition = new ConditionPart(value, ConditionPart.ConditionOperand.OPERATOR);
            return this;
        }
    }
}