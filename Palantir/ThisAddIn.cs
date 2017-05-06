using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace Palantir
{
    public partial class ThisAddIn
    {

        public Excel.Worksheet Worksheet { get; set; }
        public Excel.Range InputCell { get; set; }
        public Excel.Range OutputCell { get; set; }
        public IDistribution Distribution { get; set; }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.WorkbookBeforeSave += new Microsoft.Office.Interop.Excel.AppEvents_WorkbookBeforeSaveEventHandler(Application_WorkbookBeforeSave);
            this.Worksheet = ((Excel.Worksheet)Application.ActiveSheet);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private Excel.Range GetRange(string r)
        {
            Excel.Worksheet activeWorksheet = ((Excel.Worksheet)Application.ActiveSheet);
            Excel.Range range = activeWorksheet.get_Range(r);
            return range;
        }

        internal async void Simulate(int times)
        {
            if(InputCell == null || OutputCell == null || Distribution == null)
            {
                System.Windows.Forms.MessageBox.Show("Verifique las variables de entrada/salida");
                return;
            }
            var calculatedValues = new List<double>();

            for(int i =0; i< times;i++)
            {
                var x = Distribution.GetNext();
                InputCell.Value = x;
                calculatedValues.Add((double)OutputCell.Value);
            }
        }

        public void Entrada(Enums.Distribuciones dist)
        {
            var activeCell = Application.ActiveCell;
            this.Distribution = DistributionFactory.GetDistribution(dist);
            this.InputCell = activeCell;
            this.InputCell.Interior.Color = Excel.XlRgbColor.rgbGreen;
        }

        public void Salida()
        {
            var activeCell = Application.ActiveCell;
            this.OutputCell = activeCell;
            this.OutputCell.Interior.Color = Excel.XlRgbColor.rgbLightCyan;
        }

        void Application_WorkbookBeforeSave(Microsoft.Office.Interop.Excel.Workbook Wb, bool SaveAsUI, ref bool Cancel)
        {
            //Excel.Worksheet activeWorksheet = ((Excel.Worksheet)Application.ActiveSheet);
            //Excel.Range firstRow = activeWorksheet.get_Range("A1");
            //firstRow.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            //Excel.Range newFirstRow = activeWorksheet.get_Range("A1");
            //newFirstRow.Value2 = "This text was added by using code";
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
