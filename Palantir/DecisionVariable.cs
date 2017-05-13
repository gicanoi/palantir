using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Palantir
{
    [Serializable]
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
                this.Column = decisionCell.Column;
                this.Row = decisionCell.Row;
                this.key = decisionCell.Column + "," + decisionCell.Row;
                this.decisionCell.Interior.Color = Excel.XlRgbColor.rgbYellow;
            }
        }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Step { get; set; }
        public string Name { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public string Key { get { return this.key; } }
        private string key;

        [NonSerialized]
        private Excel.Range decisionCell;

        public void SetDecisionCell(Excel.Range cell)
        {
            this.DecisionCell = cell;
        }

    }
}
