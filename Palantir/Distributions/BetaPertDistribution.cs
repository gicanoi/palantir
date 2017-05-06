using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir.Distributions
{
    public class BetaPertDistribution : IDistribution
    {
        public BetaScaled Distribution { get; set; }
        public BetaPertDistribution(double min, double max, double mostLikely)
        {
            this.Distribution = BetaScaled.PERT(min, max, mostLikely);
        }

        public double GetNext()
        {
            return this.Distribution.Sample();
        }
    }
}
