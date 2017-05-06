using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Palantir
{
    public class DistributionFactory
    {
        public static IDistribution GetDistribution(Enums.Distribuciones dist)
        {
            IDistribution result = null;
            switch (dist)
            {
                case Enums.Distribuciones.Uniforme:
                    //result = new ContinuousUniform();
                    break;
                case Enums.Distribuciones.Normal:
                    var normal = new NormalWindow();
                    normal.ShowDialog();
                    result = normal.Distribution;
                    break;
                case Enums.Distribuciones.Beta:
                    //result = new Beta(0, 1);
                    break;
                case Enums.Distribuciones.BetaPERT:
                    throw new NotImplementedException("falta la betaPERT");
                    break;
                case Enums.Distribuciones.Binomial_Discreta:
                   // result = new Binomial(0.1, 100);
                    break;
                case Enums.Distribuciones.Custom_Discreta:
                    throw new NotImplementedException("Custom discreta no está");
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
