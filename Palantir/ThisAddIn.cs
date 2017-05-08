using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Palantir.Windows;
using MathNet.Numerics.Statistics;

namespace Palantir
{
    public partial class ThisAddIn
    {

        public Excel.Worksheet Worksheet { get; set; }
        public Excel.Range InputCell { get; set; }
        public Excel.Range OutputCell { get; set; }
        public Dictionary<string, DecisionVariable> DecisionVariables { get; set; }
        public IDistribution Distribution { get; set; }


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.WorkbookBeforeSave += new Microsoft.Office.Interop.Excel.AppEvents_WorkbookBeforeSaveEventHandler(Application_WorkbookBeforeSave);
            this.Worksheet = ((Excel.Worksheet)Application.ActiveSheet);
            DecisionVariables = new Dictionary<string, DecisionVariable>();
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


        private List<SimulationResult> CalculateDecision(double min, double max, double steps, int times)
        {
            return null;
        }

        private SimulationResult CalculateValues(int times)
        {
            var calculatedValues = new List<double>();

            for (int i = 0; i < times; i++)
            {
                var x = Distribution.GetNext();
                InputCell.Value = x;
                calculatedValues.Add((double)OutputCell.Value);
            }
            var meanStdDev = MathNet.Numerics.Statistics.Statistics.MeanStandardDeviation(calculatedValues);
            return new SimulationResult()
            {
                Values = calculatedValues,
                MinValue = calculatedValues.Min(),
                MaxValue = calculatedValues.Max(),
                MeanValue = meanStdDev.Item1,
                StdDev = meanStdDev.Item2

            };
        }

        internal void Simulate(int times)
        {
            if (InputCell == null || OutputCell == null || Distribution == null)
            {
                System.Windows.Forms.MessageBox.Show("Verifique las variables de entrada/salida");
                return;
            }
            var simulationResult = CalculateValues(times);
            var hist = new Histogram(simulationResult.Values, (int)Math.Sqrt(times));
            var output = new OutputWindow(simulationResult);
            output.Show();
        }

        internal void SimulateWithDecision(int times)
        {
            if (InputCell == null || OutputCell == null || Distribution == null)
            {
                System.Windows.Forms.MessageBox.Show("Verifique las variables de entrada/salida");
                return;
            }

            if (this.DecisionVariables.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No existen variables de decisión");
                return;
            }
            var selectedVariables = new DecisionSimulationWindow(this.DecisionVariables);
            selectedVariables.ShowDialog();
            var variables = selectedVariables.SelectedVariables;

            if (variables == null || variables.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No se seleccionó ninguna variable.");
                return;
            }

            if (variables.Count > 2)
            {
                System.Windows.Forms.MessageBox.Show("Debe seleccionar un máximo de 2 variables.");
                return;
            }

            if (variables.Count == 1)
            {
                var singleVariableResults = new Dictionary<string, double>();
                var var1 = variables[0];
                var1.DecisionCell.Value = var1.MinValue;
                var current = var1.MinValue;
                while (current <= var1.MaxValue)
                {
                    var simulationResult = CalculateValues(times);
                    singleVariableResults.Add(string.Format("{0} ({1})", var1.Name, current), simulationResult.MeanValue);
                    current += var1.Step;
                    var1.DecisionCell.Value = current;
                }
            }
            else
            {
                var dualVariableResults = new Dictionary<string, Dictionary<string, double>>();
                var var1 = variables[0];
                var var2 = variables[1];
                var1.DecisionCell.Value = var1.MinValue;
                var v1 = var1.MinValue;
                while (v1 <= var1.MaxValue)
                {
                    var keyV1 = string.Format("{0} ({1})", var1.Name, v1);
                    dualVariableResults.Add(keyV1, new Dictionary<string, double>());
                    var2.DecisionCell.Value = var2.MinValue;
                    var v2 = var2.MinValue;
                    while (v2 <= var2.MaxValue)
                    {
                        var keyV2 = string.Format("{0} ({1})", var2.Name, v2);
                        var simulationResult = CalculateValues(times);
                        dualVariableResults[keyV1].Add(keyV2, simulationResult.MeanValue);
                        v2 += var2.Step;
                        var2.DecisionCell.Value = v2;
                    }
                    v1 += var1.Step;
                    var1.DecisionCell.Value = v1;
                }
            }
        }

        public void SetInputCell(Enums.Distribuciones dist)
        {
            if (InputCell != null)
            {
                this.InputCell.Interior.Color = Excel.XlRgbColor.rgbWhite;
                this.InputCell = null;
            }
            var activeCell = Application.ActiveCell;
            this.Distribution = DistributionFactory.GetDistribution(dist);
            this.InputCell = activeCell;
            this.InputCell.Interior.Color = Excel.XlRgbColor.rgbGreen;
            this.InputCell.Value = this.Distribution.GetNext();
        }

        public void SetOutputCell()
        {

            if (OutputCell != null)
            {
                this.OutputCell.Interior.Color = Excel.XlRgbColor.rgbWhite;
                this.OutputCell = null;
            }
            var activeCell = Application.ActiveCell;
            this.OutputCell = activeCell;
            this.OutputCell.Interior.Color = Excel.XlRgbColor.rgbLightCyan;
        }

        public void SetDecicionCell()
        {
            //if (DecisionCells != null)
            //{
            //    this.DecisionCell.Interior.Color = Excel.XlRgbColor.rgbWhite;
            //    this.DecisionCell = null;
            //    this.DecisionVariable = null;
            //}

            var decisionWindow = new DecisionVariableWindow();
            decisionWindow.ShowDialog();
            if (decisionWindow.Variable == null)
            {
                return;
            }

            var activeCell = Application.ActiveCell;
            decisionWindow.Variable.DecisionCell = activeCell;
            var variable = decisionWindow.Variable;

            if (DecisionVariables.ContainsKey(variable.Key))
            {
                DecisionVariables[variable.Key] = variable;
            }
            else
            {
                DecisionVariables.Add(variable.Key, variable);
            }
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
