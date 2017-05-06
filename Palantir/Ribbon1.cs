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
        }

        private void btnEntrada_Click(object sender, RibbonControlEventArgs e)
        {
            var dist = Enum.Parse(typeof(Enums.Distribuciones), cmbDistribucion.Text);
            Globals.ThisAddIn.Entrada(dist);
        }

        private void btnOutput_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Salida();
        }

        private void btnRun_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Simulate();
        }
    }
}
