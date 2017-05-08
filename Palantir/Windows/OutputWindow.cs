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
    public partial class OutputWindow : Form
    {

        public OutputWindow(SimulationResult simulation)
        {
            txtMin.Text = simulation.MinValue.ToString();
            txtMax.Text = simulation.MaxValue.ToString();
            txtMean.Text = simulation.MeanValue.ToString();
            txtStdDev.Text = simulation.StdDev.ToString();
        }

        public OutputWindow(double min, double max, double mean, double stdDev)
        {
            InitializeComponent();
            txtMin.Text = min.ToString();
            txtMax.Text = max.ToString();
            txtMean.Text = mean.ToString();
            txtStdDev.Text = stdDev.ToString();
        }

        public OutputWindow()
        {
            InitializeComponent();
        }
    }
}
