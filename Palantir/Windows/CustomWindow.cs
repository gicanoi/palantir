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
    public partial class CustomWindow : Form
    {
        public IDistribution Distribution { get; set; }

        public CustomWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var probs = txtProbabilities.GetDoubleList();
            var values = txtValues.GetDoubleList();
            if(probs==null || values == null)
            {
                MessageBox.Show("Verifique los valores");
                return;
            }
            Distribution = new CustomDistribution(values, probs);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
