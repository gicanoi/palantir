using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir.Distributions
{
    [Serializable]
    public class Range
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public Range(double min, double max)
        {
            this.Max = max;
            this.Min = min;
        }

        public bool Contains(double n)
        {
            if (Max == 1)
            {
                return n >= Min && n <= Max;
            }
            return n >= Min && n < Max;
        }
    }

    [Serializable]
    public class DiscreteValue
    {
        public double Value { get; set; }
        public Range Range { get; set; }
        public DiscreteValue(double value, double min, double max)
        {
            this.Value = value;
            this.Range = new Range(min, max);
        }
    }

    [Serializable]
    public class CustomDistribution : IDistribution
    {
        public Enums.Distribuciones Name = Enums.Distribuciones.Custom_Discreta;
        private List<DiscreteValue> PossibleValues { get; set; }
        private ContinuousUniform Random { get; set; }
        private Tuple<object, object, object> export { get; set; }
        public Enums.Distribuciones GetName()
        {
            return this.Name;
        }
        public CustomDistribution(List<double> values, List<double> probabilities)
        {
            export = new Tuple<object, object, object>(values, probabilities, null);
            Initialize(export);
            //if (values == null || probabilities == null)
            //{
            //    throw new Exception("Deben especificarse los parámetros values e intervals");
            //}

            //if (probabilities.Count != values.Count)
            //{
            //    throw new Exception("Debe existir un valor de probabilidad para cada valor");
            //}

            //if (probabilities.Sum() != 1)
            //{
            //    throw new Exception("La suma de probabilidades debe ser igual a 1");
            //}

            //this.Random = new ContinuousUniform(0, 1);
            //PossibleValues = new List<DiscreteValue>();
            //PossibleValues.Add(new DiscreteValue(values[0], 0, probabilities[0]));

            //for (int i = 1; i < probabilities.Count; i++)
            //{
            //    var minProb = PossibleValues[i - 1].Range.Max;
            //    var maxProb = minProb + probabilities[i];
            //    PossibleValues.Add(new DiscreteValue(values[i], minProb, maxProb));
            //}
        }

        public CustomDistribution()
        {
        }

        public double GetNext()
        {
            var sample = Random.Sample();
            var next = PossibleValues.First(x => x.Range.Contains(sample));
            return next.Value;
        }

        public Tuple<object, object, object> Export()
        {
            return export;
        }

        public void Initialize(Tuple<object, object, object> data)
        {
            export = data;
            var values = (List<double>)data.Item1;
            var probabilities = (List<double>)data.Item2;
            if (values == null || probabilities == null)
            {
                throw new Exception("Deben especificarse los parámetros values e intervals");
            }

            if (probabilities.Count != values.Count)
            {
                throw new Exception("Debe existir un valor de probabilidad para cada valor");
            }

            if (probabilities.Sum() != 1)
            {
                throw new Exception("La suma de probabilidades debe ser igual a 1");
            }

            this.Random = new ContinuousUniform(0, 1);
            PossibleValues = new List<DiscreteValue>();
            PossibleValues.Add(new DiscreteValue(values[0], 0, probabilities[0]));

            for (int i = 1; i < probabilities.Count; i++)
            {
                var minProb = PossibleValues[i - 1].Range.Max;
                var maxProb = minProb + probabilities[i];
                PossibleValues.Add(new DiscreteValue(values[i], minProb, maxProb));
            }
        }
    }
}
