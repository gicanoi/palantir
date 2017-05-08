using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palantir.Windows
{
    public partial class DecisionVariableWindow : Form
    {
        public DecisionVariable Variable { get; set; }

        public DecisionVariableWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var min = txtMin.GetDouble();
            var max = txtMax.GetDouble();
            var step = txtStep.GetDouble();
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Debe especificar un nombre");
                return;
            }

            if (min == null || max == null || step == null)
            {
                MessageBox.Show("Verifique los valores");
                return;
            }

            if (min >= max)
            {
                MessageBox.Show("El valor mínimo debe ser menor al máximo");
                return;
            }

            if (step <= 0)
            {
                MessageBox.Show("El intervalo debe ser positivo");
                return;
            }

            Variable = new DecisionVariable()
            {
                Name = txtName.Text,
                MinValue = min.Value,
                MaxValue = max.Value,
                Step = step.Value
            };

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
