using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir
{
    public class SimulationResult 
    {
        public List<double> Values { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double MeanValue { get; set; }
        public double StdDev { get; set; }
    }
}
