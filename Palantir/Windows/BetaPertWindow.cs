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
    public partial class BetaPertWindow : Form
    {
        public IDistribution Distribution { get; set; }

        public BetaPertWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var min = txtMin.GetDouble();
            var max = txtMax.GetDouble();
            var mostLikely = txtMostLikely.GetDouble();

            if (min != null && max != null && mostLikely != null)
            {
                this.Distribution = new BetaPertDistribution(min.Value, max.Value, mostLikely.Value);
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
