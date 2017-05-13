using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir
{
    [Serializable]
    public class ExportModel
    {
        public ExportCell InputCell { get; set; }
        public ExportCell OutputCell { get; set; }
        public Dictionary<string, DecisionVariable> DecisionVariables { get; set; }
        public ExportDistribution Distribution { get; set; }
    }

    [Serializable]
    public class ExportCell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Formula { get; set; }
    }

    [Serializable]
    public class ExportDistribution
    {
        public Enums.Distribuciones Name { get; set; }
        public Tuple<object, object, object> Parameters { get; set; }
    }
}
