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


namespace Palantir
{
    public partial class NormalWindow : Form
    {
        public NormalWindow()
        {
            InitializeComponent();
        }

        public IDistribution Distribution { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var mean = txtMean.GetDouble();
            var stDev = txtStdDev.GetDouble();
                       

            if(mean!=null && stDev!=null)
            {
                this.Distribution = new NormalDistribution(mean.Value, stDev.Value);
                this.Close();
            }
            else
            {
                MessageBox.Show("Verifique los parámetros");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
