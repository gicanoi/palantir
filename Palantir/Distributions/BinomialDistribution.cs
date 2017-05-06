using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir.Distributions
{
    public class BinomialDistribution : IDistribution
    {
        public Binomial Distribution { get; set; }

        public BinomialDistribution(double success, int trials)
        {
            this.Distribution = new Binomial(success, trials);
        }

        public double GetNext()
        {
            return this.Distribution.Sample();
        }
    }
}
