using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir.Distributions
{
    [Serializable]
    public class BetaPertDistribution : IDistribution
    {
        public Enums.Distribuciones Name = Enums.Distribuciones.BetaPERT;
        public BetaScaled Distribution { get; set; }
        private Tuple<object, object, object> export { get; set; }
        public BetaPertDistribution(double min, double max, double mostLikely)
        {
            this.Distribution = BetaScaled.PERT(min, max, mostLikely);
            export = new Tuple<object, object, object>(min, max, mostLikely);
        }

        public BetaPertDistribution()
        {
        }

        public Enums.Distribuciones GetName()
        {
            return this.Name;
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
            this.Distribution = BetaScaled.PERT((double)data.Item1, (double)data.Item2, (double)data.Item3);
        }
    }
}
