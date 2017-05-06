namespace Palantir
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.Palantir = this.Factory.CreateRibbonGroup();
            this.cmbDistribucion = this.Factory.CreateRibbonComboBox();
            this.btnEntrada = this.Factory.CreateRibbonButton();
            this.btnOutput = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.btnRun = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.Palantir.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.Palantir);
            this.tab1.Label = "Palantir";
            this.tab1.Name = "tab1";
            // 
            // Palantir
            // 
            this.Palantir.Items.Add(this.cmbDistribucion);
            this.Palantir.Items.Add(this.btnEntrada);
            this.Palantir.Items.Add(this.btnOutput);
            this.Palantir.Items.Add(this.separator1);
            this.Palantir.Items.Add(this.btnRun);
            this.Palantir.Label = "Variables";
            this.Palantir.Name = "Palantir";
            // 
            // cmbDistribucion
            // 
            this.cmbDistribucion.Label = "Distribución";
            this.cmbDistribucion.Name = "cmbDistribucion";
            this.cmbDistribucion.Text = null;
            // 
            // btnEntrada
            // 
            this.btnEntrada.Label = "Entrada";
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnEntrada_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.Label = "Salida";
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOutput_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // btnRun
            // 
            this.btnRun.Label = "Correr";
            this.btnRun.Name = "btnRun";
            this.btnRun.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRun_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.Palantir.ResumeLayout(false);
            this.Palantir.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Palantir;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnEntrada;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cmbDistribucion;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnOutput;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRun;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
