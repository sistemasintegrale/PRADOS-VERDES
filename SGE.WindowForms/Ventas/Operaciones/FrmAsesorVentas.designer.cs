namespace SGE.WindowForms.Ventas.Operaciones
{
    partial class FrmVendedor
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
            this.dgrVendedor = new DevExpress.XtraGrid.GridControl();
            this.mnuVendedor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porCódigoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porNombresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNombre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDireccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTelefono = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDNI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTipoVenta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkpgrdSituacion = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.LkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrVendedor)).BeginInit();
            this.mnuVendedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpgrdSituacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrVendedor
            // 
            this.dgrVendedor.ContextMenuStrip = this.mnuVendedor;
            this.dgrVendedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrVendedor.Location = new System.Drawing.Point(0, 62);
            this.dgrVendedor.MainView = this.gridView1;
            this.dgrVendedor.Name = "dgrVendedor";
            this.dgrVendedor.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRichTextEdit1,
            this.lkpgrdSituacion});
            this.dgrVendedor.Size = new System.Drawing.Size(934, 459);
            this.dgrVendedor.TabIndex = 4;
            this.dgrVendedor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // mnuVendedor
            // 
            this.mnuVendedor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuVendedor.Name = "contextMenuStrip1";
            this.mnuVendedor.Size = new System.Drawing.Size(126, 92);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porCódigoToolStripMenuItem,
            this.porNombresToolStripMenuItem});
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // porCódigoToolStripMenuItem
            // 
            this.porCódigoToolStripMenuItem.Name = "porCódigoToolStripMenuItem";
            this.porCódigoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.porCódigoToolStripMenuItem.Text = "Por Código";
            this.porCódigoToolStripMenuItem.Click += new System.EventHandler(this.porCódigoToolStripMenuItem_Click);
            // 
            // porNombresToolStripMenuItem
            // 
            this.porNombresToolStripMenuItem.Name = "porNombresToolStripMenuItem";
            this.porNombresToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.porNombresToolStripMenuItem.Text = "Por Nombres";
            this.porNombresToolStripMenuItem.Click += new System.EventHandler(this.porNombresToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcId,
            this.gcNombre,
            this.gcDireccion,
            this.gcTelefono,
            this.gcDNI,
            this.gcTipoVenta,
            this.gridColumn1});
            this.gridView1.GridControl = this.dgrVendedor;
            this.gridView1.GroupPanelText = "Vendedores";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyUp);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gcId
            // 
            this.gcId.Caption = "Código";
            this.gcId.FieldName = "vendc_iid_vendedor";
            this.gcId.Name = "gcId";
            this.gcId.OptionsColumn.AllowEdit = false;
            this.gcId.OptionsColumn.AllowFocus = false;
            this.gcId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMove = false;
            this.gcId.OptionsColumn.ReadOnly = true;
            this.gcId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "descripcion", "Total : {0}")});
            this.gcId.Visible = true;
            this.gcId.VisibleIndex = 0;
            this.gcId.Width = 54;
            // 
            // gcNombre
            // 
            this.gcNombre.Caption = "Nombre y Apellidos";
            this.gcNombre.FieldName = "vendc_vnombre_vendedor";
            this.gcNombre.Name = "gcNombre";
            this.gcNombre.OptionsColumn.AllowEdit = false;
            this.gcNombre.OptionsColumn.AllowFocus = false;
            this.gcNombre.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcNombre.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcNombre.OptionsColumn.AllowMove = false;
            this.gcNombre.OptionsColumn.ReadOnly = true;
            this.gcNombre.Visible = true;
            this.gcNombre.VisibleIndex = 1;
            this.gcNombre.Width = 182;
            // 
            // gcDireccion
            // 
            this.gcDireccion.Caption = "Dirección";
            this.gcDireccion.FieldName = "vendc_vdireccion";
            this.gcDireccion.Name = "gcDireccion";
            this.gcDireccion.OptionsColumn.AllowEdit = false;
            this.gcDireccion.OptionsColumn.AllowFocus = false;
            this.gcDireccion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcDireccion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcDireccion.OptionsColumn.AllowMove = false;
            this.gcDireccion.OptionsColumn.ReadOnly = true;
            this.gcDireccion.Visible = true;
            this.gcDireccion.VisibleIndex = 2;
            this.gcDireccion.Width = 273;
            // 
            // gcTelefono
            // 
            this.gcTelefono.Caption = "Teléfono";
            this.gcTelefono.FieldName = "vendc_vnumero_telefono";
            this.gcTelefono.Name = "gcTelefono";
            this.gcTelefono.OptionsColumn.AllowEdit = false;
            this.gcTelefono.OptionsColumn.AllowFocus = false;
            this.gcTelefono.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcTelefono.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcTelefono.OptionsColumn.AllowMove = false;
            this.gcTelefono.OptionsColumn.ReadOnly = true;
            this.gcTelefono.Visible = true;
            this.gcTelefono.VisibleIndex = 3;
            this.gcTelefono.Width = 91;
            // 
            // gcDNI
            // 
            this.gcDNI.Caption = "Doc. Identidad";
            this.gcDNI.FieldName = "vendc_cnumero_dni";
            this.gcDNI.Name = "gcDNI";
            this.gcDNI.OptionsColumn.AllowEdit = false;
            this.gcDNI.OptionsColumn.AllowFocus = false;
            this.gcDNI.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcDNI.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcDNI.OptionsColumn.AllowMove = false;
            this.gcDNI.OptionsColumn.ReadOnly = true;
            this.gcDNI.Visible = true;
            this.gcDNI.VisibleIndex = 4;
            this.gcDNI.Width = 91;
            // 
            // gcTipoVenta
            // 
            this.gcTipoVenta.Caption = "Zona";
            this.gcTipoVenta.FieldName = "zonc_vdescripcion";
            this.gcTipoVenta.Name = "gcTipoVenta";
            this.gcTipoVenta.OptionsColumn.AllowEdit = false;
            this.gcTipoVenta.OptionsColumn.AllowFocus = false;
            this.gcTipoVenta.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcTipoVenta.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcTipoVenta.OptionsColumn.AllowMove = false;
            this.gcTipoVenta.OptionsColumn.ReadOnly = true;
            this.gcTipoVenta.Visible = true;
            this.gcTipoVenta.VisibleIndex = 5;
            this.gcTipoVenta.Width = 85;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Situación";
            this.gridColumn1.ColumnEdit = this.lkpgrdSituacion;
            this.gridColumn1.FieldName = "tablc_iid_situacion_vendedor";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 77;
            // 
            // lkpgrdSituacion
            // 
            this.lkpgrdSituacion.AutoHeight = false;
            this.lkpgrdSituacion.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpgrdSituacion.Name = "lkpgrdSituacion";
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.LkpSituacion);
            this.groupControl1.Controls.Add(this.txtNombre);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(934, 62);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(435, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Situación:";
            this.labelControl4.Visible = false;
            // 
            // LkpSituacion
            // 
            this.LkpSituacion.Location = new System.Drawing.Point(488, 27);
            this.LkpSituacion.Name = "LkpSituacion";
            this.LkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpSituacion.Properties.NullText = "";
            this.LkpSituacion.Size = new System.Drawing.Size(120, 20);
            this.LkpSituacion.TabIndex = 7;
            this.LkpSituacion.Visible = false;
            this.LkpSituacion.EditValueChanged += new System.EventHandler(this.LkpSituacion_EditValueChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(264, 27);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(133, 20);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Código:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(221, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nombre:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(51, 25);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.EditValueChanged += new System.EventHandler(this.txtCodigo_EditValueChanged);
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyUp);
            // 
            // FrmVendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 521);
            this.Controls.Add(this.dgrVendedor);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmVendedor";
            this.Text = "Asesor de Ventas";
            this.Load += new System.EventHandler(this.FrmVendedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrVendedor)).EndInit();
            this.mnuVendedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpgrdSituacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgrVendedor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcId;
        private DevExpress.XtraGrid.Columns.GridColumn gcNombre;
        private DevExpress.XtraGrid.Columns.GridColumn gcDireccion;
        private DevExpress.XtraGrid.Columns.GridColumn gcTelefono;
        private DevExpress.XtraGrid.Columns.GridColumn gcDNI;
        private DevExpress.XtraGrid.Columns.GridColumn gcTipoVenta;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private System.Windows.Forms.ContextMenuStrip mnuVendedor;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit LkpSituacion;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porCódigoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porNombresToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpgrdSituacion;
    }
}