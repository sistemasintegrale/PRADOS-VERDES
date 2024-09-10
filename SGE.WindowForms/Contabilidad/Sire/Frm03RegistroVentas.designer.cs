namespace SGE.WindowForms.Contabilidad.Sire
{
    partial class Frm03RegistroVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm03RegistroVentas));
            this.grd = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportarAExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarZipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpMes = new DevExpress.XtraEditors.LookUpEdit();
            this.sfdRuta = new System.Windows.Forms.SaveFileDialog();
            this.sfdTXT = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grd
            // 
            this.grd.ContextMenuStrip = this.mnu;
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 65);
            this.grd.MainView = this.grv;
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(1403, 394);
            this.grd.TabIndex = 3;
            this.grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv});
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarAExcelToolStripMenuItem,
            this.tXTToolStripMenuItem,
            this.exportarZipToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(158, 70);
            // 
            // exportarAExcelToolStripMenuItem
            // 
            this.exportarAExcelToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_excel;
            this.exportarAExcelToolStripMenuItem.Name = "exportarAExcelToolStripMenuItem";
            this.exportarAExcelToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exportarAExcelToolStripMenuItem.Text = "Exportar a Excel";
            this.exportarAExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarAExcelToolStripMenuItem_Click);
            // 
            // tXTToolStripMenuItem
            // 
            this.tXTToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tXTToolStripMenuItem.Image")));
            this.tXTToolStripMenuItem.Name = "tXTToolStripMenuItem";
            this.tXTToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.tXTToolStripMenuItem.Text = "Exportar a TXT";
            this.tXTToolStripMenuItem.Click += new System.EventHandler(this.tXTToolStripMenuItem_Click);
            // 
            // exportarZipToolStripMenuItem
            // 
            this.exportarZipToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.descargar;
            this.exportarZipToolStripMenuItem.Name = "exportarZipToolStripMenuItem";
            this.exportarZipToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exportarZipToolStripMenuItem.Text = "Exportar ZIP";
            this.exportarZipToolStripMenuItem.Click += new System.EventHandler(this.exportarZipToolStripMenuItem_Click);
            // 
            // grv
            // 
            this.grv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc1,
            this.gc2,
            this.gc3,
            this.gc4,
            this.gc5,
            this.gc6,
            this.gc7,
            this.gc8,
            this.gc9,
            this.gc10,
            this.gc11,
            this.gc12,
            this.gc13,
            this.gc14,
            this.gc15,
            this.gc26,
            this.gc27,
            this.gc28,
            this.gc29,
            this.gc30,
            this.gc31,
            this.gc32,
            this.gc33,
            this.gc34,
            this.gc35,
            this.gc36,
            this.gc37,
            this.gc39,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.grv.GridControl = this.grd;
            this.grv.GroupPanelText = "Registro de Ventas";
            this.grv.Name = "grv";
            this.grv.OptionsView.ColumnAutoWidth = false;
            // 
            // gc1
            // 
            this.gc1.Caption = "SU";
            this.gc1.FieldName = "tdocc_vcodigo_tipo_doc_sunat";
            this.gc1.Name = "gc1";
            this.gc1.OptionsColumn.AllowEdit = false;
            this.gc1.OptionsColumn.AllowFocus = false;
            this.gc1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc1.OptionsColumn.AllowMove = false;
            this.gc1.Width = 43;
            // 
            // gc2
            // 
            this.gc2.Caption = "DOC";
            this.gc2.FieldName = "tdocc_vabreviatura_tipo_doc";
            this.gc2.Name = "gc2";
            this.gc2.OptionsColumn.AllowEdit = false;
            this.gc2.OptionsColumn.AllowFocus = false;
            this.gc2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc2.OptionsColumn.AllowMove = false;
            this.gc2.Visible = true;
            this.gc2.VisibleIndex = 0;
            this.gc2.Width = 49;
            // 
            // gc3
            // 
            this.gc3.Caption = "N° DOC";
            this.gc3.FieldName = "numeroDocumento";
            this.gc3.Name = "gc3";
            this.gc3.OptionsColumn.AllowEdit = false;
            this.gc3.OptionsColumn.AllowFocus = false;
            this.gc3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc3.OptionsColumn.AllowMove = false;
            this.gc3.Visible = true;
            this.gc3.VisibleIndex = 2;
            this.gc3.Width = 89;
            // 
            // gc4
            // 
            this.gc4.Caption = "FECHA";
            this.gc4.FieldName = "doxcc_sfecha_doc";
            this.gc4.Name = "gc4";
            this.gc4.OptionsColumn.AllowEdit = false;
            this.gc4.OptionsColumn.AllowFocus = false;
            this.gc4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc4.OptionsColumn.AllowMove = false;
            this.gc4.Visible = true;
            this.gc4.VisibleIndex = 3;
            this.gc4.Width = 66;
            // 
            // gc5
            // 
            this.gc5.Caption = "CÓDIGO";
            this.gc5.FieldName = "cliec_vcod_cliente";
            this.gc5.Name = "gc5";
            this.gc5.OptionsColumn.AllowEdit = false;
            this.gc5.OptionsColumn.AllowFocus = false;
            this.gc5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc5.OptionsColumn.AllowMove = false;
            this.gc5.Visible = true;
            this.gc5.VisibleIndex = 4;
            // 
            // gc6
            // 
            this.gc6.Caption = "CLIENTE";
            this.gc6.FieldName = "cliec_vnombre_cliente";
            this.gc6.Name = "gc6";
            this.gc6.OptionsColumn.AllowEdit = false;
            this.gc6.OptionsColumn.AllowFocus = false;
            this.gc6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc6.OptionsColumn.AllowMove = false;
            this.gc6.Visible = true;
            this.gc6.VisibleIndex = 5;
            this.gc6.Width = 148;
            // 
            // gc7
            // 
            this.gc7.Caption = "MONEDA";
            this.gc7.FieldName = "simboloMoneda";
            this.gc7.Name = "gc7";
            this.gc7.OptionsColumn.AllowEdit = false;
            this.gc7.OptionsColumn.AllowFocus = false;
            this.gc7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc7.OptionsColumn.AllowMove = false;
            this.gc7.Visible = true;
            this.gc7.VisibleIndex = 6;
            // 
            // gc8
            // 
            this.gc8.Caption = "T/C";
            this.gc8.DisplayFormat.FormatString = "#,0.0000";
            this.gc8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc8.FieldName = "doxcc_nmonto_tipo_cambio";
            this.gc8.Name = "gc8";
            this.gc8.OptionsColumn.AllowEdit = false;
            this.gc8.OptionsColumn.AllowFocus = false;
            this.gc8.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc8.OptionsColumn.AllowMove = false;
            this.gc8.Visible = true;
            this.gc8.VisibleIndex = 14;
            this.gc8.Width = 41;
            // 
            // gc9
            // 
            this.gc9.Caption = "IGV %";
            this.gc9.DisplayFormat.FormatString = "#,0.00";
            this.gc9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc9.FieldName = "doxcc_nporcentaje_igv";
            this.gc9.Name = "gc9";
            this.gc9.OptionsColumn.AllowEdit = false;
            this.gc9.OptionsColumn.AllowFocus = false;
            this.gc9.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc9.Visible = true;
            this.gc9.VisibleIndex = 7;
            // 
            // gc10
            // 
            this.gc10.Caption = "OP.GRAV.";
            this.gc10.DisplayFormat.FormatString = "#,0.00";
            this.gc10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc10.FieldName = "doxcc_nmonto_afecto";
            this.gc10.Name = "gc10";
            this.gc10.OptionsColumn.AllowEdit = false;
            this.gc10.OptionsColumn.AllowFocus = false;
            this.gc10.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc10.OptionsColumn.AllowMove = false;
            this.gc10.Visible = true;
            this.gc10.VisibleIndex = 8;
            // 
            // gc11
            // 
            this.gc11.Caption = "VALOR EX.";
            this.gc11.DisplayFormat.FormatString = "#,0.00";
            this.gc11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc11.FieldName = "doxcc_nmonto_inafecto";
            this.gc11.Name = "gc11";
            this.gc11.OptionsColumn.AllowEdit = false;
            this.gc11.OptionsColumn.AllowFocus = false;
            this.gc11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc11.OptionsColumn.AllowMove = false;
            this.gc11.Visible = true;
            this.gc11.VisibleIndex = 9;
            // 
            // gc12
            // 
            this.gc12.Caption = "VALOR EXP.";
            this.gc12.DisplayFormat.FormatString = "#,0.00";
            this.gc12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc12.FieldName = "doxcc_nmonto_exportacion";
            this.gc12.Name = "gc12";
            this.gc12.OptionsColumn.AllowEdit = false;
            this.gc12.OptionsColumn.AllowFocus = false;
            this.gc12.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc12.OptionsColumn.AllowMove = false;
            this.gc12.Visible = true;
            this.gc12.VisibleIndex = 10;
            // 
            // gc13
            // 
            this.gc13.Caption = "VALOR VENTA";
            this.gc13.DisplayFormat.FormatString = "#,0.00";
            this.gc13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc13.FieldName = "valor_venta";
            this.gc13.Name = "gc13";
            this.gc13.OptionsColumn.AllowEdit = false;
            this.gc13.OptionsColumn.AllowFocus = false;
            this.gc13.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc13.OptionsColumn.AllowMove = false;
            this.gc13.Visible = true;
            this.gc13.VisibleIndex = 11;
            // 
            // gc14
            // 
            this.gc14.Caption = "I.G.V";
            this.gc14.DisplayFormat.FormatString = "#,0.00";
            this.gc14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc14.FieldName = "doxcc_nmonto_impuesto";
            this.gc14.Name = "gc14";
            this.gc14.OptionsColumn.AllowEdit = false;
            this.gc14.OptionsColumn.AllowFocus = false;
            this.gc14.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc14.OptionsColumn.AllowMove = false;
            this.gc14.Visible = true;
            this.gc14.VisibleIndex = 12;
            // 
            // gc15
            // 
            this.gc15.Caption = "I.S.C";
            this.gc15.DisplayFormat.FormatString = "#,0.00";
            this.gc15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc15.FieldName = "doxcc_nmonto_isc";
            this.gc15.Name = "gc15";
            this.gc15.OptionsColumn.AllowEdit = false;
            this.gc15.OptionsColumn.AllowFocus = false;
            this.gc15.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gc15.OptionsColumn.AllowMove = false;
            this.gc15.Visible = true;
            this.gc15.VisibleIndex = 13;
            // 
            // gc26
            // 
            this.gc26.Caption = "TIPO DE CAMBIO";
            this.gc26.DisplayFormat.FormatString = "#,0.0000";
            this.gc26.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc26.FieldName = "doxcc_nmonto_tipo_cambio";
            this.gc26.Name = "gc26";
            // 
            // gc27
            // 
            this.gc27.Caption = "BASE IMPONI. OPER. GRAV.";
            this.gc27.DisplayFormat.FormatString = "#,0.00";
            this.gc27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc27.FieldName = "doxcc_nmonto_afecto";
            this.gc27.Name = "gc27";
            // 
            // gc28
            // 
            this.gc28.Caption = "VARLOR EXONERADO";
            this.gc28.DisplayFormat.FormatString = "#,0.00";
            this.gc28.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc28.FieldName = "valor_ex";
            this.gc28.Name = "gc28";
            // 
            // gc29
            // 
            this.gc29.Caption = "VALOR DOCS. FACTORING";
            this.gc29.DisplayFormat.FormatString = "#,0.00";
            this.gc29.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc29.Name = "gc29";
            // 
            // gc30
            // 
            this.gc30.Caption = "VALOR EXPORTACIÓN";
            this.gc30.DisplayFormat.FormatString = "#,0.00";
            this.gc30.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc30.FieldName = "doxcc_nmonto_exportacion";
            this.gc30.Name = "gc30";
            // 
            // gc31
            // 
            this.gc31.Caption = "BASE IMPONI. I.V.A.P";
            this.gc31.DisplayFormat.FormatString = "#,0.00";
            this.gc31.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc31.FieldName = "base_imp_ivap";
            this.gc31.Name = "gc31";
            // 
            // gc32
            // 
            this.gc32.Caption = "VALOR VENTA";
            this.gc32.DisplayFormat.FormatString = "#,0.00";
            this.gc32.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc32.FieldName = "valor_venta";
            this.gc32.Name = "gc32";
            // 
            // gc33
            // 
            this.gc33.Caption = "MONTO I.G.V";
            this.gc33.DisplayFormat.FormatString = "#,0.00";
            this.gc33.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc33.FieldName = "doxcc_nmonto_impuesto";
            this.gc33.Name = "gc33";
            // 
            // gc34
            // 
            this.gc34.Caption = "MONTO I.S.C";
            this.gc34.DisplayFormat.FormatString = "#,0.00";
            this.gc34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc34.FieldName = "doxcc_nmonto_isc";
            this.gc34.Name = "gc34";
            // 
            // gc35
            // 
            this.gc35.Caption = "MONTO I.V.A.P";
            this.gc35.DisplayFormat.FormatString = "#,0.00";
            this.gc35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc35.FieldName = "doxcc_nmonto_iva";
            this.gc35.Name = "gc35";
            // 
            // gc36
            // 
            this.gc36.Caption = "PRECIO VENTA";
            this.gc36.DisplayFormat.FormatString = "#,0.00";
            this.gc36.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc36.FieldName = "doxcc_nmonto_total";
            this.gc36.Name = "gc36";
            // 
            // gc37
            // 
            this.gc37.Caption = "FECHA DOC. MODIF.";
            this.gc37.DisplayFormat.FormatString = "d";
            this.gc37.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gc37.FieldName = "nc_dxc_fecha";
            this.gc37.Name = "gc37";
            // 
            // gc39
            // 
            this.gc39.Caption = "SERIE DOC REFERENCIA";
            this.gc39.FieldName = "doxcc_num_serie_referencia";
            this.gc39.Name = "gc39";
            this.gc39.OptionsColumn.AllowEdit = false;
            this.gc39.OptionsColumn.AllowFocus = false;
            this.gc39.OptionsColumn.AllowIncrementalSearch = false;
            this.gc39.Visible = true;
            this.gc39.VisibleIndex = 16;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "N° DOC REFERENCIA";
            this.gridColumn1.FieldName = "doxcc_num_comprobante_referencia";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 17;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "FECHA DOC REFERENCIA";
            this.gridColumn2.FieldName = "doxcc_sfecha_emision_referencia";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 18;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "TIPO DOC";
            this.gridColumn3.FieldName = "nc_dxc_tipodoc";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 15;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "SERIE DOC";
            this.gridColumn4.FieldName = "serieDocumento";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lkpMes);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1403, 65);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Registro de Ventas";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 33);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(19, 13);
            this.labelControl2.TabIndex = 45;
            this.labelControl2.Text = "Mes";
            // 
            // lkpMes
            // 
            this.lkpMes.Location = new System.Drawing.Point(41, 29);
            this.lkpMes.Name = "lkpMes";
            this.lkpMes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lkpMes.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lkpMes.Properties.Appearance.Options.UseFont = true;
            this.lkpMes.Properties.Appearance.Options.UseForeColor = true;
            this.lkpMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMes.Properties.NullText = "";
            this.lkpMes.Size = new System.Drawing.Size(141, 20);
            this.lkpMes.TabIndex = 46;
            this.lkpMes.EditValueChanged += new System.EventHandler(this.lkpMes_EditValueChanged);
            // 
            // Frm03RegistroVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 459);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm03RegistroVentas";
            this.Text = "Registro de Ventas - SIRE";
            this.Load += new System.EventHandler(this.frmRegistroVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grd;
        private DevExpress.XtraGrid.Views.Grid.GridView grv;
        private DevExpress.XtraGrid.Columns.GridColumn gc2;
        private DevExpress.XtraGrid.Columns.GridColumn gc1;
        private DevExpress.XtraGrid.Columns.GridColumn gc3;
        private DevExpress.XtraGrid.Columns.GridColumn gc4;
        private DevExpress.XtraGrid.Columns.GridColumn gc5;
        private DevExpress.XtraGrid.Columns.GridColumn gc6;
        private DevExpress.XtraGrid.Columns.GridColumn gc7;
        private DevExpress.XtraGrid.Columns.GridColumn gc8;
        private DevExpress.XtraGrid.Columns.GridColumn gc10;
        private DevExpress.XtraGrid.Columns.GridColumn gc11;
        private DevExpress.XtraGrid.Columns.GridColumn gc12;
        private DevExpress.XtraGrid.Columns.GridColumn gc13;
        private DevExpress.XtraGrid.Columns.GridColumn gc14;
        private DevExpress.XtraGrid.Columns.GridColumn gc15;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.LookUpEdit lkpMes;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private DevExpress.XtraGrid.Columns.GridColumn gc9;
        private System.Windows.Forms.ToolStripMenuItem exportarAExcelToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdRuta;
        private DevExpress.XtraGrid.Columns.GridColumn gc26;
        private DevExpress.XtraGrid.Columns.GridColumn gc27;
        private DevExpress.XtraGrid.Columns.GridColumn gc28;
        private DevExpress.XtraGrid.Columns.GridColumn gc29;
        private DevExpress.XtraGrid.Columns.GridColumn gc30;
        private DevExpress.XtraGrid.Columns.GridColumn gc31;
        private DevExpress.XtraGrid.Columns.GridColumn gc32;
        private DevExpress.XtraGrid.Columns.GridColumn gc34;
        private DevExpress.XtraGrid.Columns.GridColumn gc35;
        private DevExpress.XtraGrid.Columns.GridColumn gc36;
        private DevExpress.XtraGrid.Columns.GridColumn gc37;
        private DevExpress.XtraGrid.Columns.GridColumn gc39;
        private DevExpress.XtraGrid.Columns.GridColumn gc33;
        private System.Windows.Forms.ToolStripMenuItem tXTToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdTXT;
        private System.Windows.Forms.ToolStripMenuItem exportarZipToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}