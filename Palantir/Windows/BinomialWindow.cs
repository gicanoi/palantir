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
    public partial class BinomialWindow : Form
    {
        public IDistribution Distribution { get; set; }
        public BinomialWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var success = txtSuccess.GetDouble();
            var trials = txtTrials.GetDouble();

            if (success != null && trials != null)
            {
                this.Distribution = new BinomialDistribution(success.Value, (int)trials.Value);
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
