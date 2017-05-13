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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Palantir
{
    public partial class ThisAddIn
    {

        public Excel.Worksheet Worksheet { get; set; }
        public Excel.Range InputCell { get; set; }
        public Excel.Range OutputCell { get; set; }
        public Dictionary<string, DecisionVariable> DecisionVariables { get; set; }
        public IDistribution Distribution { get; set; }

        private ExportModel CreateExportModel()
        {
            var model = new ExportModel();

            if (InputCell != null)
            {
                model.InputCell = new ExportCell()
                {
                    Column = InputCell.Column,
                    Row = InputCell.Row,
                    Formula = InputCell.Formula
                };
            }

            if (OutputCell != null)
            {
                model.OutputCell = new ExportCell()
                {
                    Column = OutputCell.Column,
                    Row = OutputCell.Row,
                    Formula = OutputCell.Formula
                };
            }

            if (DecisionVariables?.Count > 0)
            {
                model.DecisionVariables = DecisionVariables;
            }

            if (Distribution != null)
            {
                model.Distribution = new ExportDistribution()
                {
                    Name = Distribution.GetName(),
                    Parameters = Distribution.Export()
                };
            }

            return model;

        }


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

        private Excel.Range GetRange(int row, int column)
        {
            var cell = Application.ActiveSheet.Cells(row, column);
            return cell;
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
            var meanStdDev = Statistics.MeanStandardDeviation(calculatedValues);
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
            Accord.Controls.HistogramBox.Show(simulationResult.Values.ToArray(), "Resultados");
            var output = new OutputWindow(simulationResult);
            output.Show();
        }

        private Dictionary<string, double> SimulateSingleVariable(DecisionVariable variable, int times)
        {
            var result = new Dictionary<string, double>();
            variable.DecisionCell.Value = variable.MinValue;
            var current = variable.MinValue;
            while (current <= variable.MaxValue)
            {
                var simulationResult = CalculateValues(times);
                result.Add(string.Format("{0} ({1})", variable.Name, current), simulationResult.MeanValue);
                current += variable.Step;
                variable.DecisionCell.Value = current;
            }
            return result;
        }

        private Dictionary<string, Dictionary<string, double>> SimulateDualVariable(DecisionVariable var1, DecisionVariable var2, int times)
        {
            var result = new Dictionary<string, Dictionary<string, double>>();
            var1.DecisionCell.Value = var1.MinValue;
            var v1 = var1.MinValue;
            while (v1 <= var1.MaxValue)
            {
                var keyV1 = string.Format("{0} ({1})", var1.Name, v1);
                result.Add(keyV1, new Dictionary<string, double>());
                var2.DecisionCell.Value = var2.MinValue;
                var v2 = var2.MinValue;
                while (v2 <= var2.MaxValue)
                {
                    var keyV2 = string.Format("{0} ({1})", var2.Name, v2);
                    var simulationResult = CalculateValues(times);
                    result[keyV1].Add(keyV2, simulationResult.MeanValue);
                    v2 += var2.Step;
                    var2.DecisionCell.Value = v2;
                }
                v1 += var1.Step;
                var1.DecisionCell.Value = v1;
            }
            return result;
        }

        private void RenderSingleVariableResults(Dictionary<string, double> simulationResults)
        {
            var resultBook = this.Application.Workbooks.Add();
            var sheet = resultBook.ActiveSheet;
            var i = 0;
            foreach (var v in simulationResults)
            {
                sheet.Cells(1, i + 1).Value = v.Key;
                sheet.Cells(2, i + 1).Value = v.Value;
                i++;
            }
        }

        private void RenderDualVariableResults(Dictionary<string, Dictionary<string, double>> simulationResults)
        {
            var resultBook = this.Application.Workbooks.Add();
            var sheet = resultBook.ActiveSheet;
            var i = 0;
            foreach (var v in simulationResults)
            {
                sheet.Cells(1, i + 2).Value = v.Key;
                var j = 0;
                foreach (var w in v.Value)
                {
                    sheet.Cells(j + 2, 1).Value = w.Key;
                    sheet.Cells(j + 2, i + 2).Value = w.Value;
                    j++;
                }
                i++;
            }
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
                var singleVariableResults = SimulateSingleVariable(variables[0], times);
                RenderSingleVariableResults(singleVariableResults);
            }
            else
            {
                var dualVariableResults = SimulateDualVariable(variables[0], variables[1], times);
                RenderDualVariableResults(dualVariableResults);
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
        /// <summary>
        /// Requires Distribution to be set!
        /// </summary>
        /// <param name="cell"></param>
        public void SetInputCell(Excel.Range cell)
        {
            this.InputCell = cell;
            this.InputCell.Interior.Color = Excel.XlRgbColor.rgbGreen;
            this.InputCell.Value = this.Distribution.GetNext();
        }

        public void SetOutputCell(Excel.Range cell)
        {
            this.OutputCell = cell;
            this.OutputCell.Interior.Color = Excel.XlRgbColor.rgbLightCyan;
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

        public bool SaveModel(string path)
        {
            try
            {
                var export = CreateExportModel();
                var stream = File.Create(path);
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, export);
                stream.Close();
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al exportar");
                return false;
            }
            //// Persist to file
            //FileStream stream = File.Create(filename);
            //var formatter = new BinaryFormatter();
            //Console.WriteLine("Serializing vector");
            //formatter.Serialize(stream, u);
            //stream.Close();

            //// Restore from file
            //stream = File.OpenRead(filename);
            //Console.WriteLine("Deserializing vector");
            //var v = (DoubleVector)formatter.Deserialize(stream);
            //stream.Close();
            return true;
        }

        public void LoadModel(string path)
        {
            var stream = File.OpenRead(path);
            var formatter = new BinaryFormatter();
            var model = (ExportModel)formatter.Deserialize(stream);
            stream.Close();

            if (model.InputCell != null)
            {
                Distribution = DistributionFactory.GetDistribution(model.Distribution.Name, model.Distribution.Parameters);
                SetInputCell(Application.ActiveSheet.Cells(model.InputCell.Row, model.InputCell.Column));
            }

            if (model.OutputCell != null)
            {
                SetOutputCell(Application.ActiveSheet.Cells(model.OutputCell.Row, model.OutputCell.Column));
                OutputCell.Formula = model.OutputCell.Formula;
            }

            if (model.DecisionVariables?.Count > 0)
            {
                foreach (var v in model.DecisionVariables.Values)
                {
                    v.DecisionCell = Application.ActiveSheet.Cells(v.Row, v.Column);
                }
            }
            DecisionVariables = model.DecisionVariables;
        }

        public void SetDecisionCell()
        {
            var decisionWindow = new DecisionVariableWindow();
            decisionWindow.ShowDialog();
            if (decisionWindow.Variable == null)
            {
                return;
            }
            if (DecisionVariables.Values.Any(x => x.Name == decisionWindow.Variable.Name))
            {
                System.Windows.Forms.MessageBox.Show("Ya existe una variable con ese nombre");
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
