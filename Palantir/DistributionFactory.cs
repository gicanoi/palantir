using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using Palantir.Windows;
using Palantir.Distributions;

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
                    var unif = new ContinuousUniformWindow();
                    unif.ShowDialog();
                    result = unif.Distribution;
                    break;
                case Enums.Distribuciones.Normal:
                    var normal = new NormalWindow();
                    normal.ShowDialog();
                    result = normal.Distribution;
                    break;
                case Enums.Distribuciones.Beta:
                    var beta = new BetaWindow();
                    beta.ShowDialog();
                    result = beta.Distribution;
                    break;
                case Enums.Distribuciones.BetaPERT:
                    var pert = new BetaPertWindow();
                    pert.ShowDialog();
                    result = pert.Distribution;
                    break;
                case Enums.Distribuciones.Binomial_Discreta:
                    var binom = new BinomialWindow();
                    binom.ShowDialog();
                    result = binom.Distribution;
                    break;
                case Enums.Distribuciones.Custom_Discreta:
                    var custom = new CustomWindow();
                    custom.ShowDialog();
                    result = custom.Distribution;
                    break;
                default:
                    break;
            }
            return result;
        }

        public static IDistribution GetDistribution(Enums.Distribuciones dist, Tuple<object, object, object> parameters)
        {
            IDistribution result = null;
            switch (dist)
            {
                case Enums.Distribuciones.Uniforme:
                    result = new ContinuousUniformDistribution();
                    result.Initialize(parameters);
                    break;
                case Enums.Distribuciones.Normal:
                    result = new NormalDistribution();
                    result.Initialize(parameters);
                    break;
                case Enums.Distribuciones.Beta:
                    result = new BetaDistribution();
                    result.Initialize(parameters);
                    break;
                case Enums.Distribuciones.BetaPERT:
                    result = new BetaPertDistribution();
                    result.Initialize(parameters);
                    break;
                case Enums.Distribuciones.Binomial_Discreta:
                    result = new BinomialDistribution();
                    result.Initialize(parameters);
                    break;
                case Enums.Distribuciones.Custom_Discreta:
                    result = new CustomDistribution();
                    result.Initialize(parameters);
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
