using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    public class BetaDistribution : IDistribution
    {
        public Beta Distribution { get; set; }
        public BetaDistribution(double alpha, double beta)
        {
            this.Distribution = new Beta(alpha, beta);
        }

        public double GetNext()
        {
            return this.Distribution.Sample();
        }
    }
}
