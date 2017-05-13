using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    [Serializable]
    public class NormalDistribution : IDistribution
    {
        public Enums.Distribuciones Name = Enums.Distribuciones.Normal;
        public Normal Distribution { get; set; }
        private Tuple<object, object, object> export { get; set; }

        public Enums.Distribuciones GetName()
        {
            return this.Name;
        }
        public NormalDistribution(double mean, double stDev)
        {
            export = new Tuple<object, object, object>(mean, stDev, null);
            this.Distribution = new Normal(mean, stDev);
        }

        public NormalDistribution()
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
            this.Distribution = new Normal((double)data.Item1, (double)data.Item2);
        }
    }
}
