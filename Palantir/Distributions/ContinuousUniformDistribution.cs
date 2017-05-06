using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    public class ContinuousUniformDistribution : IDistribution
    {
        public ContinuousUniform Distribution { get; set; }
        public ContinuousUniformDistribution(double lower, double upper)
        {
            this.Distribution = new ContinuousUniform(lower, upper);
        }

        public double GetNext()
        {
            return this.Distribution.Sample();
        }
    }
}
