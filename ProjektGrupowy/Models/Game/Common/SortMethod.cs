using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Common
{
    public class SortMethod
    {
        public enum BY          { None, Attribute, Token, Player };
        public enum DIRECTION   { None, Asc, Desc };

        public int Id;
        public BY By;
        public DIRECTION Direction;

        public SortMethod(BY type, int id = 0, DIRECTION dir = DIRECTION.Asc)
        {
            By = type;
            Id = id;
            Direction = dir;
        }

        public SortMethod() { }
    }
}