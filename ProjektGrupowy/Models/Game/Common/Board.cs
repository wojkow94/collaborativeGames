using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Game.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjektGrupowy.Models.Game.Common
{

    [System.Serializable]
    public class RegionContainer
    {
        public RegionContainer() { }

        public enum OrientationType { VERTICAL, HORIZONTAL };
        public OrientationType Orientation { get; set; }


        public List<BoardRegion> Regions = new List<BoardRegion>();
        public int AcceptElementId { get; set; }
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public void SetAcceptElement(ElementDefinition el)
        {
            AcceptElementId = el.Id;
        }

        public void AddRegion(BoardRegion region)
        {
            region.Container = this;
            region.Id = Regions.Count;
            Regions.Add(region);
        }

        public BoardRegion GetRegion(int id)
        {
            if (id < Regions.Count) return Regions[id];
            return null;
        }

        public RegionContainer(int x, int y, int w, int h, OrientationType orientation)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Orientation = orientation;
        }
    }

    [System.Serializable]
    public class BoardRegion
    {
        public class Attribute
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public Attribute(string name, string value)
            {
                Name = name;
                Value = value;
            }
            public Attribute() { }
        }

        public class CalcAttribute
        {
            public string Name { get; set; }
            public ElementQuery Query { get; set; }
            public CalcAttribute(string name, ElementQuery q)
            {
                Name = name;
                Query = q;
            }
            public CalcAttribute(){ }
        }

        public BoardRegion() { }

        public List<Attribute> Attributes = new List<Attribute>();
        public List<CalcAttribute> CalcAttributes = new List<CalcAttribute>();

        [XmlIgnore]
        public RegionContainer Container { get; set; }

        public Color Color { get; set; }
        public string PrintedAttribute { get; set; }
        public string PopupAttribute { get; set; }
        public float Opacity { get; set; }
        public int Id { get; set; }


        public BoardRegion(Color color, float opacity, string printedAttribute)
        {
            Color = color;
            Opacity = opacity;
            PrintedAttribute = printedAttribute;
        }
    }

    [System.Serializable]
    public class GameBoard
    {
        public List<RegionContainer> RegionContainers = new List<RegionContainer>();

        public void AddContainer(RegionContainer container)
        {
            container.Id = RegionContainers.Count;
            RegionContainers.Add(container);
        }

        public RegionContainer GetContainer(int id)
        {
            if (id < RegionContainers.Count) return RegionContainers[id];
            return null;
        }

        public GameBoard() { }
    }
}