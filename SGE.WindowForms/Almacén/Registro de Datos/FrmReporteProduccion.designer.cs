namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    partial class FrmReporteProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteProduccion));
            this.grdReporteProduccion = new DevExpress.XtraGrid.GridControl();
            this.mnuRegistroDeDocumentos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anulartoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ingresarPTAlmacentoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarPTDelAlmacénToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarCostosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReporteProduccion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNúmero = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporteProduccion)).BeginInit();
            this.mnuRegistroDeDocumentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewReporteProduccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNúmero.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdReporteProduccion
            // 
            this.grdReporteProduccion.ContextMenuStrip = this.mnuRegistroDeDocumentos;
            this.grdReporteProduccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReporteProduccion.Location = new System.Drawing.Point(0, 47);
            this.grdReporteProduccion.MainView = this.viewReporteProduccion;
            this.grdReporteProduccion.Name = "grdReporteProduccion";
            this.grdReporteProduccion.Size = new System.Drawing.Size(926, 406);
            this.grdReporteProduccion.TabIndex = 5;
            this.grdReporteProduccion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewReporteProduccion});
            // 
            // mnuRegistroDeDocumentos
            // 
            this.mnuRegistroDeDocumentos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.anulartoolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem,
            this.toolStripSeparator1,
            this.ingresarPTAlmacentoolStripMenuItem,
            this.eliminarPTDelAlmacénToolStripMenuItem,
            this.actualizarCostosToolStripMenuItem});
            this.mnuRegistroDeDocumentos.Name = "contextMenuStrip1";
            this.mnuRegistroDeDocumentos.Size = new System.Drawing.Size(204, 208);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // anulartoolStripMenuItem
            // 
            this.anulartoolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_code_red;
            this.anulartoolStripMenuItem.Name = "anulartoolStripMenuItem";
            this.anulartoolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.anulartoolStripMenuItem.Text = "Anular";
            this.anulartoolStripMenuItem.Click += new System.EventHandler(this.anulartoolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // ingresarPTAlmacentoolStripMenuItem
            // 
            this.ingresarPTAlmacentoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ingresarPTAlmacentoolStripMenuItem.Image")));
            this.ingresarPTAlmacentoolStripMenuItem.Name = "ingresarPTAlmacentoolStripMenuItem";
            this.ingresarPTAlmacentoolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.ingresarPTAlmacentoolStripMenuItem.Text = "Ingresar PT al almcén";
            this.ingresarPTAlmacentoolStripMenuItem.Click += new System.EventHandler(this.ingresarPTAlmacentoolStripMenuItem_Click);
            // 
            // eliminarPTDelAlmacénToolStripMenuItem
            // 
            this.eliminarPTDelAlmacénToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eliminarPTDelAlmacénToolStripMenuItem.Image")));
            this.eliminarPTDelAlmacénToolStripMenuItem.Name = "eliminarPTDelAlmacénToolStripMenuItem";
            this.eliminarPTDelAlmacénToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.eliminarPTDelAlmacénToolStripMenuItem.Text = "Eliminar PT del Almacén";
            this.eliminarPTDelAlmacénToolStripMenuItem.Click += new System.EventHandler(this.eliminarPTDelAlmacénToolStripMenuItem_Click);
            // 
            // actualizarCostosToolStripMenuItem
            // 
            this.actualizarCostosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("actualizarCostosToolStripMenuItem.Image")));
            this.actualizarCostosToolStripMenuItem.Name = "actualizarCostosToolStripMenuItem";
            this.actualizarCostosToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.actualizarCostosToolStripMenuItem.Text = "Actualizar costos";
            this.actualizarCostosToolStripMenuItem.Click += new System.EventHandler(this.actualizarCostosToolStripMenuItem_Click);
            // 
            // viewReporteProduccion
            // 
            this.viewReporteProduccion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn13,
            this.gridColumn10,
            this.gridColumn17,
            this.gridColumn32,
            this.gridColumn28,
            this.gridColumn29,
            this.gridColumn30,
            this.gridColumn22,
            this.gridColumn31,
            this.gridColumn36,
            this.gridColumn37});
            this.viewReporteProduccion.GridControl = this.grdReporteProduccion;
            this.viewReporteProduccion.GroupPanelText = "Criterios de Busqueda";
            this.viewReporteProduccion.Name = "viewReporteProduccion";
            this.viewReporteProduccion.OptionsView.ColumnAutoWidth = false;
            this.viewReporteProduccion.DoubleClick += new System.EventHandler(this.viewReporteProduccion_DoubleClick);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "N° Reporte";
            this.gridColumn3.FieldName = "rp_num_produccion";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 67;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Fecha";
            this.gridColumn6.FieldName = "rp_sfecha_produccion";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 63;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Observaciones";
            this.gridColumn9.FieldName = "rp_voservaciones_produccion";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 172;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Cantidad";
            this.gridColumn13.DisplayFormat.FormatString = "#,0.00";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "rp_ncant_pro";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowFocus = false;
            this.gridColumn13.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 63;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "U.M";
            this.gridColumn10.FieldName = "unidc_vabreviatura_unidad_medida";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 57;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Proveedor";
            this.gridColumn17.FieldName = "proc_vnombrecompleto";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.OptionsColumn.AllowFocus = false;
            this.gridColumn17.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn17.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 5;
            this.gridColumn17.Width = 161;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "Almacén Ingreso";
            this.gridColumn32.FieldName = "almac_vdescripcion";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.AllowEdit = false;
            this.gridColumn32.OptionsColumn.AllowFocus = false;
            this.gridColumn32.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn32.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 8;
            this.gridColumn32.Width = 110;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "Fecha Ingreso";
            this.gridColumn28.FieldName = "rp_sfecha_ing_kardex";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.OptionsColumn.AllowEdit = false;
            this.gridColumn28.OptionsColumn.AllowFocus = false;
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 9;
            this.gridColumn28.Width = 61;
            // 
            // gridColumn29
            // 
            this.gridColumn29.Caption = "Costo Total";
            this.gridColumn29.DisplayFormat.FormatString = "n2";
            this.gridColumn29.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn29.FieldName = "rp_nmonto_total_soles";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.OptionsColumn.AllowEdit = false;
            this.gridColumn29.OptionsColumn.AllowFocus = false;
            this.gridColumn29.Visible = true;
            this.gridColumn29.VisibleIndex = 10;
            this.gridColumn29.Width = 62;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Monto Unitario";
            this.gridColumn30.DisplayFormat.FormatString = "n4";
            this.gridColumn30.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn30.FieldName = "MontoUnitario";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.OptionsColumn.AllowEdit = false;
            this.gridColumn30.OptionsColumn.AllowFocus = false;
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 11;
            this.gridColumn30.Width = 73;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Monto Total US$";
            this.gridColumn22.DisplayFormat.FormatString = "n2";
            this.gridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn22.FieldName = "rp_nmonto_total_dolares";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.AllowFocus = false;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 12;
            this.gridColumn22.Width = 73;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "Monto Unitario US$";
            this.gridColumn31.DisplayFormat.FormatString = "n4";
            this.gridColumn31.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn31.FieldName = "MontoUnitarioDolares";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.OptionsColumn.AllowEdit = false;
            this.gridColumn31.OptionsColumn.AllowFocus = false;
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 13;
            this.gridColumn31.Width = 95;
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "Descripción";
            this.gridColumn36.FieldName = "prdc_vdescripcion_larga";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsColumn.AllowEdit = false;
            this.gridColumn36.OptionsColumn.AllowFocus = false;
            this.gridColumn36.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn36.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn36.Visible = true;
            this.gridColumn36.VisibleIndex = 6;
            this.gridColumn36.Width = 186;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "Código";
            this.gridColumn37.FieldName = "prdc_vcode_producto";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsColumn.AllowEdit = false;
            this.gridColumn37.OptionsColumn.AllowFocus = false;
            this.gridColumn37.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn37.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 7;
            this.gridColumn37.Width = 74;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtNúmero);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(926, 47);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Documento:";
            // 
            // txtNúmero
            // 
            this.txtNúmero.Location = new System.Drawing.Point(96, 23);
            this.txtNúmero.Name = "txtNúmero";
            this.txtNúmero.Size = new System.Drawing.Size(133, 20);
            this.txtNúmero.TabIndex = 7;
            this.txtNúmero.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNúmero_KeyUp);
            // 
            // FrmReporteProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 453);
            this.Controls.Add(this.grdReporteProduccion);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmReporteProduccion";
            this.Text = "Reporte de Producción";
            this.Load += new System.EventHandler(this.FrmReporteProduccion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdReporteProduccion)).EndInit();
            this.mnuRegistroDeDocumentos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewReporteProduccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNúmero.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdReporteProduccion;
        private DevExpress.XtraGrid.Views.Grid.GridView viewReporteProduccion;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ContextMenuStrip mnuRegistroDeDocumentos;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anulartoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ingresarPTAlmacentoolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtNúmero;
        private System.Windows.Forms.ToolStripMenuItem eliminarPTDelAlmacénToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizarCostosToolStripMenuItem;
    }
}