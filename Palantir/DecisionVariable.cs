using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Palantir
{
    public class DecisionVariable
    {
        public Excel.Range DecisionCell
        {
            get
            {
                return this.decisionCell;
            }
            set
            {
                this.decisionCell = value;
                this.key = decisionCell.Column + "," + decisionCell.Row;
                this.decisionCell.Interior.Color = Excel.XlRgbColor.rgbYellow;
            }
        }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Step { get; set; }
        public string Name { get; set; }
        public string Key { get { return this.key; } }
        private string key;
        private Excel.Range decisionCell;
    }
}
