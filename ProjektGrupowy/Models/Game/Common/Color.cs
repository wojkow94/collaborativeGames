using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Game.Common
{
    [System.Serializable]
    public class Color
    {
        public Color() { }

        public int R {get; set; }
        public int G {get; set; }
        public int B {get; set; }

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}