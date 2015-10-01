using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prima
{
    public class Edge
    {
        public int V1 { get; set; }
        public int V2 { get; set; }
        public int Weight { get; set; }

        public Edge(int v1, int v2, int weight)
        {
            V1 = v1;
            V2 = v2;
            Weight = weight;
        }
    }
}
