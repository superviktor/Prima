using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Prima
{
    public class VisualPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<Edge> Edges { get; set; } 
        public VisualPoint(int x, int y)
        {
            X = x;
            Y = y;
            Edges = new List<Edge>();
        }
    }
}
