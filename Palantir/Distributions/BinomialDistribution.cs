using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir.Distributions
{
    [Serializable]
    public class BinomialDistribution : IDistribution
    {
        public Binomial Distribution { get; set; }
        public Enums.Distribuciones Name = Enums.Distribuciones.Binomial_Discreta;
        private Tuple<object,object,object> export { get; set; }

        public BinomialDistribution(double success, int trials)
        {
            this.Distribution = new Binomial(success, trials);
            export = new Tuple<object, object, object>(success, trials, null);
        }

        public BinomialDistribution()
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
            this.Distribution = new Binomial((double)data.Item1, (int) data.Item2);
        }
    }
}
