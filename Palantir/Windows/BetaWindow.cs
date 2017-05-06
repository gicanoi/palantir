using Palantir.Distributions;
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
    public partial class BetaWindow : Form
    {
        public IDistribution Distribution { get; set; }

        public BetaWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var alpha = txtAlpha.GetDouble();
            var beta = txtBeta.GetDouble();


            if (alpha != null && beta != null)
            {
                this.Distribution = new BetaDistribution(alpha.Value, beta.Value);
                this.Close();
            }
            else
            {
                MessageBox.Show("Verifique los parámetros");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
