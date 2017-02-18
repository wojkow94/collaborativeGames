using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Common
{
    public class ElementQuery
    {
        public string SelectStatement { get; set; }
        public string FromStatement { get; set; }
        public string WhereStatement { get; set; }
        public string EqualsStatement { get; set; }


        public ElementQuery()
        {

        }

        public ElementQuery Select(string select)
        {
            SelectStatement = select;
            return this;
        }

        public ElementQuery From(string from)
        {
            FromStatement = from;
            return this;
        }

        public ElementQuery Where(string where)
        {
            WhereStatement = where;
            return this;
        }

        public ElementQuery Equals(string eq)
        {
            EqualsStatement = eq;
            return this;
        }
    }
}