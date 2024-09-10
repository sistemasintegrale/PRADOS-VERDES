namespace SGE.WindowForms.Ventas.Operaciones
{
    partial class frm02RegistroFunerarias
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
            this.txtDNI = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtRUC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cbActivarFiltro = new System.Windows.Forms.CheckBox();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.mnuFuneraria = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevotoolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.modificartoolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminartoolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirtoolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.calcularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdFuneraria = new DevExpress.XtraGrid.GridControl();
            this.viewFuneraria = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sfdRuta = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDNI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRUC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.mnuFuneraria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFuneraria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFuneraria)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtDNI);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.txtRUC);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.cbActivarFiltro);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1081, 54);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de Búsqueda";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(787, 26);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDNI.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDNI.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDNI.Properties.MaxLength = 40;
            this.txtDNI.Size = new System.Drawing.Size(146, 20);
            this.txtDNI.TabIndex = 31;
            this.txtDNI.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDNI_KeyUp);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(531, 29);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(28, 13);
            this.labelControl10.TabIndex = 30;
            this.labelControl10.Text = "RUC :";
            // 
            // txtRUC
            // 
            this.txtRUC.Location = new System.Drawing.Point(563, 26);
            this.txtRUC.Name = "txtRUC";
            this.txtRUC.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtRUC.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtRUC.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRUC.Properties.MaxLength = 40;
            this.txtRUC.Size = new System.Drawing.Size(123, 20);
            this.txtRUC.TabIndex = 29;
            this.txtRUC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRUC_KeyUp);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(709, 29);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 13);
            this.labelControl8.TabIndex = 28;
            this.labelControl8.Text = "DNI Contacto :";
            // 
            // cbActivarFiltro
            // 
            this.cbActivarFiltro.AutoSize = true;
            this.cbActivarFiltro.Location = new System.Drawing.Point(977, 29);
            this.cbActivarFiltro.Name = "cbActivarFiltro";
            this.cbActivarFiltro.Size = new System.Drawing.Size(92, 17);
            this.cbActivarFiltro.TabIndex = 27;
            this.cbActivarFiltro.Text = "Activar Filtros";
            this.cbActivarFiltro.UseVisualStyleBackColor = true;
            this.cbActivarFiltro.CheckedChanged += new System.EventHandler(this.cbActivarFiltro_CheckedChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(272, 27);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(247, 20);
            this.txtDescripcion.TabIndex = 3;
            this.txtDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(197, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Razon Social :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(58, 27);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1081, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 421);
            this.barDockControlBottom.Size = new System.Drawing.Size(1081, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 421);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1081, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 421);
            // 
            // mnuFuneraria
            // 
            this.mnuFuneraria.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevotoolStripMenuItem4,
            this.modificartoolStripMenuItem5,
            this.eliminartoolStripMenuItem6,
            this.imprimirtoolStripMenuItem7,
            this.calcularToolStripMenuItem,
            this.exportarExcelToolStripMenuItem});
            this.mnuFuneraria.Name = "contextMenuStrip1";
            this.mnuFuneraria.Size = new System.Drawing.Size(186, 136);
            // 
            // nuevotoolStripMenuItem4
            // 
            this.nuevotoolStripMenuItem4.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevotoolStripMenuItem4.Name = "nuevotoolStripMenuItem4";
            this.nuevotoolStripMenuItem4.Size = new System.Drawing.Size(185, 22);
            this.nuevotoolStripMenuItem4.Text = "Nuevo - Funeraria";
            this.nuevotoolStripMenuItem4.Click += new System.EventHandler(this.nuevotoolStripMenuItem4_Click);
            // 
            // modificartoolStripMenuItem5
            // 
            this.modificartoolStripMenuItem5.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificartoolStripMenuItem5.Name = "modificartoolStripMenuItem5";
            this.modificartoolStripMenuItem5.Size = new System.Drawing.Size(185, 22);
            this.modificartoolStripMenuItem5.Text = "Modificar - Funeraria";
            this.modificartoolStripMenuItem5.Click += new System.EventHandler(this.modificartoolStripMenuItem5_Click);
            // 
            // eliminartoolStripMenuItem6
            // 
            this.eliminartoolStripMenuItem6.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminartoolStripMenuItem6.Name = "eliminartoolStripMenuItem6";
            this.eliminartoolStripMenuItem6.Size = new System.Drawing.Size(185, 22);
            this.eliminartoolStripMenuItem6.Text = "Eliminar - Funeraria";
            this.eliminartoolStripMenuItem6.Click += new System.EventHandler(this.eliminartoolStripMenuItem6_Click);
            // 
            // imprimirtoolStripMenuItem7
            // 
            this.imprimirtoolStripMenuItem7.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirtoolStripMenuItem7.Name = "imprimirtoolStripMenuItem7";
            this.imprimirtoolStripMenuItem7.Size = new System.Drawing.Size(185, 22);
            this.imprimirtoolStripMenuItem7.Text = "Imprimir";
            this.imprimirtoolStripMenuItem7.Click += new System.EventHandler(this.imprimirtoolStripMenuItem7_Click);
            // 
            // calcularToolStripMenuItem
            // 
            this.calcularToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.gnome_session_switch;
            this.calcularToolStripMenuItem.Name = "calcularToolStripMenuItem";
            this.calcularToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.calcularToolStripMenuItem.Text = "Renumerar";
            this.calcularToolStripMenuItem.Click += new System.EventHandler(this.calcularToolStripMenuItem_Click);
            // 
            // exportarExcelToolStripMenuItem
            // 
            this.exportarExcelToolStripMenuItem.Name = "exportarExcelToolStripMenuItem";
            this.exportarExcelToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exportarExcelToolStripMenuItem.Text = "Exportar Excel";
            this.exportarExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarExcelToolStripMenuItem_Click);
            // 
            // grdFuneraria
            // 
            this.grdFuneraria.ContextMenuStrip = this.mnuFuneraria;
            this.grdFuneraria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFuneraria.Location = new System.Drawing.Point(0, 54);
            this.grdFuneraria.MainView = this.viewFuneraria;
            this.grdFuneraria.Name = "grdFuneraria";
            this.grdFuneraria.Size = new System.Drawing.Size(1081, 367);
            this.grdFuneraria.TabIndex = 25;
            this.grdFuneraria.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewFuneraria});
            // 
            // viewFuneraria
            // 
            this.viewFuneraria.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn10});
            this.viewFuneraria.GridControl = this.grdFuneraria;
            this.viewFuneraria.GroupPanelText = "Linea";
            this.viewFuneraria.Name = "viewFuneraria";
            this.viewFuneraria.OptionsView.ColumnAutoWidth = false;
            this.viewFuneraria.DoubleClick += new System.EventHandler(this.viewFuneraria_DoubleClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Código";
            this.gridColumn7.DisplayFormat.FormatString = "{0:0000}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "func_iid_funeraria";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 127;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Razon Social";
            this.gridColumn8.FieldName = "func_vrazon_social";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 278;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nombre Comercial";
            this.gridColumn1.FieldName = "func_vnombre_comercial";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 219;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "RUC";
            this.gridColumn2.FieldName = "func_cnumero_docum_fun";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 115;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Dirección";
            this.gridColumn3.FieldName = "func_vdireccion";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            this.gridColumn3.Width = 193;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Telefono";
            this.gridColumn4.FieldName = "func_vtelefonos";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 111;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Correo";
            this.gridColumn5.FieldName = "func_vcorreo";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 7;
            this.gridColumn5.Width = 175;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Contacto";
            this.gridColumn6.FieldName = "func_vcontacto";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 8;
            this.gridColumn6.Width = 177;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Distrito";
            this.gridColumn9.FieldName = "disc_vdescripcion";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 213;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "DNI";
            this.gridColumn10.FieldName = "func_vruc";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            // 
            // frm02RegistroFunerarias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 448);
            this.Controls.Add(this.grdFuneraria);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm02RegistroFunerarias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de  Funerarias";
            this.Load += new System.EventHandler(this.frmAlamcen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDNI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRUC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.mnuFuneraria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFuneraria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFuneraria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
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
        public System.Windows.Forms.ContextMenuStrip mnuFuneraria;
        private System.Windows.Forms.ToolStripMenuItem nuevotoolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem modificartoolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem eliminartoolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem imprimirtoolStripMenuItem7;
        private DevExpress.XtraGrid.GridControl grdFuneraria;
        private DevExpress.XtraGrid.Views.Grid.GridView viewFuneraria;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.ToolStripMenuItem calcularToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        public DevExpress.XtraEditors.TextEdit txtDNI;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit txtRUC;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private System.Windows.Forms.ToolStripMenuItem exportarExcelToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdRuta;
    }
}