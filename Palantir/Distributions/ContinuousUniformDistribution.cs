using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    [Serializable]
    public class ContinuousUniformDistribution : IDistribution
    {
        public ContinuousUniform Distribution { get; set; }
        public Enums.Distribuciones Name = Enums.Distribuciones.Uniforme;
        private Tuple<object, object, object> export { get; set; }

        public ContinuousUniformDistribution() { }
        public ContinuousUniformDistribution(double lower, double upper)
        {
            this.Distribution = new ContinuousUniform(lower, upper);
            export = new Tuple<object, object, object>(lower, upper, null);
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
            this.Distribution = new ContinuousUniform((double)data.Item1, (double)data.Item2);
        }
    }
}
