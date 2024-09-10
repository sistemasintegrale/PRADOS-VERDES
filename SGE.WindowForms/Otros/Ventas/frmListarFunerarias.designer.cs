namespace SGE.WindowForms.Otros.bVentas
{
    partial class frmListarFunerarias
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
            this.grdTransportista = new DevExpress.XtraGrid.GridControl();
            this.mnuFuneraria = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevotoolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.modificartoolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminartoolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirtoolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.calcularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTransportista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtDistrito = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNombreComercial = new DevExpress.XtraEditors.TextEdit();
            this.cbActivarFiltro = new System.Windows.Forms.CheckBox();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtRUC = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransportista)).BeginInit();
            this.mnuFuneraria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewTransportista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistrito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreComercial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRUC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTransportista
            // 
            this.grdTransportista.ContextMenuStrip = this.mnuFuneraria;
            this.grdTransportista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTransportista.Location = new System.Drawing.Point(0, 78);
            this.grdTransportista.MainView = this.viewTransportista;
            this.grdTransportista.Name = "grdTransportista";
            this.grdTransportista.Size = new System.Drawing.Size(893, 307);
            this.grdTransportista.TabIndex = 10;
            this.grdTransportista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTransportista});
            // 
            // mnuFuneraria
            // 
            this.mnuFuneraria.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevotoolStripMenuItem4,
            this.modificartoolStripMenuItem5,
            this.eliminartoolStripMenuItem6,
            this.imprimirtoolStripMenuItem7,
            this.calcularToolStripMenuItem});
            this.mnuFuneraria.Name = "contextMenuStrip1";
            this.mnuFuneraria.Size = new System.Drawing.Size(186, 114);
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
            this.eliminartoolStripMenuItem6.Visible = false;
            // 
            // imprimirtoolStripMenuItem7
            // 
            this.imprimirtoolStripMenuItem7.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirtoolStripMenuItem7.Name = "imprimirtoolStripMenuItem7";
            this.imprimirtoolStripMenuItem7.Size = new System.Drawing.Size(185, 22);
            this.imprimirtoolStripMenuItem7.Text = "Imprimir";
            this.imprimirtoolStripMenuItem7.Visible = false;
            // 
            // calcularToolStripMenuItem
            // 
            this.calcularToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.gnome_session_switch;
            this.calcularToolStripMenuItem.Name = "calcularToolStripMenuItem";
            this.calcularToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.calcularToolStripMenuItem.Text = "Renumerar";
            this.calcularToolStripMenuItem.Visible = false;
            // 
            // viewTransportista
            // 
            this.viewTransportista.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.viewTransportista.GridControl = this.grdTransportista;
            this.viewTransportista.GroupPanelText = " ";
            this.viewTransportista.Name = "viewTransportista";
            this.viewTransportista.DoubleClick += new System.EventHandler(this.viewTransportista_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Código";
            this.gridColumn1.DisplayFormat.FormatString = "{0:000}";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "func_iid_funeraria";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Distrito";
            this.gridColumn2.FieldName = "disc_vdescripcion";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Razon Social";
            this.gridColumn3.FieldName = "func_vrazon_social";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 200;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nombre Comercial";
            this.gridColumn4.FieldName = "func_vnombre_comercial";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 300;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.txtRUC);
            this.groupControl1.Controls.Add(this.txtDistrito);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtNombreComercial);
            this.groupControl1.Controls.Add(this.cbActivarFiltro);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(893, 78);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de Búsqueda";
            // 
            // txtDistrito
            // 
            this.txtDistrito.Location = new System.Drawing.Point(445, 45);
            this.txtDistrito.Name = "txtDistrito";
            this.txtDistrito.Size = new System.Drawing.Size(138, 20);
            this.txtDistrito.TabIndex = 32;
            this.txtDistrito.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDistrito_KeyUp);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(234, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(93, 13);
            this.labelControl3.TabIndex = 31;
            this.labelControl3.Text = "Nombre Comercial :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(398, 48);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "Distrito :";
            // 
            // txtNombreComercial
            // 
            this.txtNombreComercial.Location = new System.Drawing.Point(333, 23);
            this.txtNombreComercial.Name = "txtNombreComercial";
            this.txtNombreComercial.Size = new System.Drawing.Size(250, 20);
            this.txtNombreComercial.TabIndex = 29;
            this.txtNombreComercial.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNombreComercial_KeyUp);
            // 
            // cbActivarFiltro
            // 
            this.cbActivarFiltro.AutoSize = true;
            this.cbActivarFiltro.Location = new System.Drawing.Point(789, 51);
            this.cbActivarFiltro.Name = "cbActivarFiltro";
            this.cbActivarFiltro.Size = new System.Drawing.Size(92, 17);
            this.cbActivarFiltro.TabIndex = 28;
            this.cbActivarFiltro.Text = "Activar Filtros";
            this.cbActivarFiltro.UseVisualStyleBackColor = true;
            this.cbActivarFiltro.Visible = false;
            this.cbActivarFiltro.CheckedChanged += new System.EventHandler(this.cbActivarFiltro_CheckedChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(85, 49);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(270, 20);
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
            this.labelControl1.Location = new System.Drawing.Point(12, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Razon Social :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(79, 26);
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
            this.btnAceptar,
            this.btnCancelar});
            this.barManager1.MaxItemId = 7;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAceptar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAceptar.Caption = "Aceptar";
            this.btnAceptar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.btnAceptar.Id = 5;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 6;
            this.btnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(893, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 385);
            this.barDockControlBottom.Size = new System.Drawing.Size(893, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 385);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(893, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 385);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(610, 29);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(28, 13);
            this.labelControl10.TabIndex = 34;
            this.labelControl10.Text = "RUC :";
            // 
            // txtRUC
            // 
            this.txtRUC.Location = new System.Drawing.Point(644, 26);
            this.txtRUC.Name = "txtRUC";
            this.txtRUC.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtRUC.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtRUC.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRUC.Properties.MaxLength = 40;
            this.txtRUC.Size = new System.Drawing.Size(123, 20);
            this.txtRUC.TabIndex = 33;
            this.txtRUC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRUC_KeyUp);
            // 
            // frmListarFunerarias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 412);
            this.Controls.Add(this.grdTransportista);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmListarFunerarias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Funerarias";
            this.Load += new System.EventHandler(this.frmAlamcen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransportista)).EndInit();
            this.mnuFuneraria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewTransportista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistrito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreComercial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRUC.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdTransportista;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTransportista;
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
        private System.Windows.Forms.CheckBox cbActivarFiltro;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        public System.Windows.Forms.ContextMenuStrip mnuFuneraria;
        private System.Windows.Forms.ToolStripMenuItem nuevotoolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem modificartoolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem eliminartoolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem imprimirtoolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem calcularToolStripMenuItem;
        private DevExpress.XtraEditors.TextEdit txtDistrito;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtNombreComercial;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit txtRUC;
    }
}