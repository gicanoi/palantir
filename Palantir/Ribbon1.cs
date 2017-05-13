using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;


namespace Palantir
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            var enumItems = Enum.GetValues(typeof(Enums.Distribuciones));
            foreach (var i in enumItems)
            {
                var item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                item.Label = i.ToString();
                cmbDistribucion.Items.Add(item);
            }
            cmbDistribucion.Text = Enums.Distribuciones.Normal.ToString();
        }

        private void btnEntrada_Click(object sender, RibbonControlEventArgs e)
        {
            if(string.IsNullOrEmpty(cmbDistribucion.Text))
            {
                System.Windows.Forms.MessageBox.Show("Debe seleccionar una distribución");
            }
            var dist = (Enums.Distribuciones) Enum.Parse(typeof(Enums.Distribuciones), cmbDistribucion.Text);
            Globals.ThisAddIn.SetInputCell(dist);
        }

        private void btnOutput_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SetOutputCell();
        }

        private void btnRun_Click(object sender, RibbonControlEventArgs e)
        {
            int times;
            if(!int.TryParse(txtTimes.Text, out times))
            {
                System.Windows.Forms.MessageBox.Show("Verifique las repeticiones");
                return;
            }
            Globals.ThisAddIn.Simulate(times);
        }

        private void btnDecision_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SetDecisionCell();
        }

        private void btnRunWithDecision_Click(object sender, RibbonControlEventArgs e)
        {
            int times;
            if (!int.TryParse(txtTimes.Text, out times))
            {
                System.Windows.Forms.MessageBox.Show("Verifique las repeticiones");
                return;
            }
            Globals.ThisAddIn.SimulateWithDecision(times);
        }

        private void btnExport_Click(object sender, RibbonControlEventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (string.IsNullOrEmpty(saveFileDialog1.FileName)) return;
            Globals.ThisAddIn.SaveModel(saveFileDialog1.FileName);
        }

        private void btnImport_Click(object sender, RibbonControlEventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog1.FileName)) return;
            Globals.ThisAddIn.LoadModel(openFileDialog1.FileName);
        }
    }
}
