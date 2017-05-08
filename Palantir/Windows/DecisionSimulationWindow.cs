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
    public partial class DecisionSimulationWindow : Form
    {
        public List<DecisionVariable> SelectedVariables { get; set; }
        public DecisionSimulationWindow(Dictionary<string,DecisionVariable> variables)
        {
            InitializeComponent();
            lstVariables.DisplayMember = "Name";
            foreach(var i in variables.Values)
            {
                lstVariables.Items.Add(i);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var items = lstVariables.CheckedItems;
            this.SelectedVariables = items.Cast<DecisionVariable>().ToList();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
