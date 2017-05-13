using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir
{
    public interface IDistribution
    {
        double GetNext();

        Tuple<object, object, object> Export();

        void Initialize(Tuple<object, object, object> data);

        Enums.Distribuciones GetName();
    }
}
