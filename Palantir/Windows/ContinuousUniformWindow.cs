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
    public partial class ContinuousUniformWindow : Form
    {
        public IDistribution Distribution { get; set; }
        public ContinuousUniformWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var lower = txtLower.GetDouble();
            var beta = txtUpper.GetDouble();


            if (lower != null && beta != null)
            {
                this.Distribution = new ContinuousUniformDistribution(lower.Value, beta.Value);
                this.Close();
            }
            else
            {
                MessageBox.Show("Verifique los parámetros");
            }
        }
    }
}
