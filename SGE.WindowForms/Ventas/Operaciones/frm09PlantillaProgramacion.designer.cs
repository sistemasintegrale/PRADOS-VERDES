namespace SGE.WindowForms.Ventas.Operaciones
{
    partial class frm09PlantillaProgramacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSepultura = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbActivarFiltro = new System.Windows.Forms.CheckBox();
            this.txtPlataforma = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtManzana = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.mnuContrato = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevotoolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.modificartoolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminartoolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.grdEspacios = new DevExpress.XtraGrid.GridControl();
            this.viewEspacios = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSepultura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlataforma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManzana.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.mnuContrato.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEspacios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewEspacios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.button1);
            this.groupControl1.Controls.Add(this.txtSepultura);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.cbActivarFiltro);
            this.groupControl1.Controls.Add(this.txtPlataforma);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtManzana);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(338, 54);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de Búsqueda";
            this.groupControl1.Visible = false;
            // 
            // button1
            // 
            this.button1.Image = global::SGE.WindowForms.Properties.Resources.btnRefresh;
            this.button1.Location = new System.Drawing.Point(830, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 30;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSepultura
            // 
            this.txtSepultura.Location = new System.Drawing.Point(546, 27);
            this.txtSepultura.Name = "txtSepultura";
            this.txtSepultura.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSepultura.Size = new System.Drawing.Size(108, 20);
            this.txtSepultura.TabIndex = 29;
            this.txtSepultura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSepultura_KeyUp);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(490, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 13);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Sepultura:";
            // 
            // cbActivarFiltro
            // 
            this.cbActivarFiltro.AutoSize = true;
            this.cbActivarFiltro.Location = new System.Drawing.Point(710, 29);
            this.cbActivarFiltro.Name = "cbActivarFiltro";
            this.cbActivarFiltro.Size = new System.Drawing.Size(92, 17);
            this.cbActivarFiltro.TabIndex = 27;
            this.cbActivarFiltro.Text = "Activar Filtros";
            this.cbActivarFiltro.UseVisualStyleBackColor = true;
            this.cbActivarFiltro.CheckedChanged += new System.EventHandler(this.cbActivarFiltro_CheckedChanged);
            // 
            // txtPlataforma
            // 
            this.txtPlataforma.Location = new System.Drawing.Point(84, 27);
            this.txtPlataforma.Name = "txtPlataforma";
            this.txtPlataforma.Size = new System.Drawing.Size(162, 20);
            this.txtPlataforma.TabIndex = 3;
            this.txtPlataforma.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlataforma_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(269, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Manzana :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Plataforma :";
            // 
            // txtManzana
            // 
            this.txtManzana.Location = new System.Drawing.Point(329, 27);
            this.txtManzana.Name = "txtManzana";
            this.txtManzana.Size = new System.Drawing.Size(133, 20);
            this.txtManzana.TabIndex = 0;
            this.txtManzana.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNuevo,
            this.btnModificar,
            this.btnEliminar});
            this.barManager1.MaxItemId = 5;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNuevo),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEliminar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "[F7]";
            this.btnNuevo.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnNuevo.Id = 0;
            this.btnNuevo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "[F5]";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "[F9]";
            this.btnEliminar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.btnEliminar.Id = 2;
            this.btnEliminar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(338, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 284);
            this.barDockControlBottom.Size = new System.Drawing.Size(338, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 284);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(338, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 284);
            // 
            // mnuContrato
            // 
            this.mnuContrato.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevotoolStripMenuItem4,
            this.modificartoolStripMenuItem5,
            this.eliminartoolStripMenuItem6});
            this.mnuContrato.Name = "contextMenuStrip1";
            this.mnuContrato.Size = new System.Drawing.Size(126, 70);
            // 
            // nuevotoolStripMenuItem4
            // 
            this.nuevotoolStripMenuItem4.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevotoolStripMenuItem4.Name = "nuevotoolStripMenuItem4";
            this.nuevotoolStripMenuItem4.Size = new System.Drawing.Size(125, 22);
            this.nuevotoolStripMenuItem4.Text = "Nuevo";
            this.nuevotoolStripMenuItem4.Click += new System.EventHandler(this.nuevotoolStripMenuItem4_Click);
            // 
            // modificartoolStripMenuItem5
            // 
            this.modificartoolStripMenuItem5.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificartoolStripMenuItem5.Name = "modificartoolStripMenuItem5";
            this.modificartoolStripMenuItem5.Size = new System.Drawing.Size(125, 22);
            this.modificartoolStripMenuItem5.Text = "Modificar";
            this.modificartoolStripMenuItem5.Click += new System.EventHandler(this.modificartoolStripMenuItem5_Click);
            // 
            // eliminartoolStripMenuItem6
            // 
            this.eliminartoolStripMenuItem6.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminartoolStripMenuItem6.Name = "eliminartoolStripMenuItem6";
            this.eliminartoolStripMenuItem6.Size = new System.Drawing.Size(125, 22);
            this.eliminartoolStripMenuItem6.Text = "Eliminar ";
            this.eliminartoolStripMenuItem6.Click += new System.EventHandler(this.eliminartoolStripMenuItem6_Click);
            // 
            // grdEspacios
            // 
            this.grdEspacios.ContextMenuStrip = this.mnuContrato;
            this.grdEspacios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEspacios.Location = new System.Drawing.Point(0, 54);
            this.grdEspacios.MainView = this.viewEspacios;
            this.grdEspacios.Name = "grdEspacios";
            this.grdEspacios.Size = new System.Drawing.Size(338, 230);
            this.grdEspacios.TabIndex = 25;
            this.grdEspacios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewEspacios});
            this.grdEspacios.Click += new System.EventHandler(this.grdCategoria_Click);
            // 
            // viewEspacios
            // 
            this.viewEspacios.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7});
            this.viewEspacios.GridControl = this.grdEspacios;
            this.viewEspacios.GroupPanelText = "Linea";
            this.viewEspacios.Name = "viewEspacios";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Numero";
            this.gridColumn7.DisplayFormat.FormatString = "{0:000}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "plap_inumero_plantilla";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 424;
            // 
            // frm09PlantillaProgramacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 311);
            this.Controls.Add(this.grdEspacios);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm09PlantillaProgramacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plantilla de Programacion";
            this.Load += new System.EventHandler(this.frmAlamcen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSepultura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlataforma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManzana.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.mnuContrato.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdEspacios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewEspacios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtPlataforma;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtManzana;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private System.Windows.Forms.CheckBox cbActivarFiltro;
        public System.Windows.Forms.ContextMenuStrip mnuContrato;
        private System.Windows.Forms.ToolStripMenuItem nuevotoolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem modificartoolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem eliminartoolStripMenuItem6;
        private DevExpress.XtraGrid.GridControl grdEspacios;
        private DevExpress.XtraGrid.Views.Grid.GridView viewEspacios;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.TextEdit txtSepultura;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Button button1;
    }
}