using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionSolution.Models
{
    public class Node
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Pollution { get; set; }
        public IEnumerable<int> Children { get; set; }
    }
}
