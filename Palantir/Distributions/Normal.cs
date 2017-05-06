using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    public class NormalDistribution : IDistribution
    {
        public Normal Distribution { get; set; }
        public NormalDistribution(double mean, double stDev)
        {
            this.Distribution = new Normal(mean, stDev);
        }

        public double GetNext()
        {
            return this.Distribution.Sample();
        }
    }
}
