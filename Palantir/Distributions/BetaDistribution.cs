using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    [Serializable]
    public class BetaDistribution : IDistribution
    {
        public Enums.Distribuciones Name = Enums.Distribuciones.Beta;
        public Beta Distribution { get; set; }
        private Tuple<object, object, object>  export { get; set; }
        public BetaDistribution(double alpha, double beta)
        {
            this.Distribution = new Beta(alpha, beta);
            export = new Tuple<object, object, object>(alpha, beta, null);
        }

        public BetaDistribution()
        {
        }

        public double GetNext()
        {
            return this.Distribution.Sample();
        }

        public Tuple<object, object, object> Export()
        {
            return export;
        }

        public void Initialize(Tuple<object, object, object> data)
        {
            export = data;
            this.Distribution = new Beta((double)data.Item1, (double)data.Item2);
        }

        public Enums.Distribuciones GetName()
        {
            return this.Name;
        }
    }
}
