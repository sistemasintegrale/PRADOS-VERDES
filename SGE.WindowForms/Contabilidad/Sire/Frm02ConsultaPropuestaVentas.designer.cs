namespace SGE.WindowForms.Contabilidad.Sire
{
    partial class Frm02ConsultaPropuestaVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm02ConsultaPropuestaVentas));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnPropuesta = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lkpMes = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpAnio = new DevExpress.XtraEditors.LookUpEdit();
            this.grdLista = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.descargarCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportaListaExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compararToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NumRuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NomRazonSocial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PerPeriodoTributario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodCar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodTipoCDP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NumSerieCDP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NumCDP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodTipoCarga = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodSituacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FecEmision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodTipoDocIdentidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NumDocIdentidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NomRazonSocialCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoValFactExpo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoBIGravada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoDsctoBI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoIGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoDsctoIGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoInafecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoISC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoBIIvap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoIvap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoIcbp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoOtrosTrib = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoTotalCP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodMoneda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoTipoCambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodEstadoComprobante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DesEstadoComprobante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IndOperGratuita = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoValorOpGratuitas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoValorFob = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IndTipoOperacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoPorcParticipacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MtoValorFobDolar = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAnio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLista)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewLista)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnPropuesta);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lkpMes);
            this.groupControl1.Controls.Add(this.lkpAnio);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1499, 77);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos de entrada";
            // 
            // btnPropuesta
            // 
            this.btnPropuesta.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPropuesta.ImageOptions.Image")));
            this.btnPropuesta.Location = new System.Drawing.Point(462, 35);
            this.btnPropuesta.Name = "btnPropuesta";
            this.btnPropuesta.Size = new System.Drawing.Size(75, 23);
            this.btnPropuesta.TabIndex = 3;
            this.btnPropuesta.Text = "Buscar";
            this.btnPropuesta.Click += new System.EventHandler(this.btnPropuesta_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Periodo :";
            // 
            // lkpMes
            // 
            this.lkpMes.Location = new System.Drawing.Point(182, 37);
            this.lkpMes.Name = "lkpMes";
            this.lkpMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMes.Properties.NullText = "";
            this.lkpMes.Size = new System.Drawing.Size(245, 20);
            this.lkpMes.TabIndex = 1;
            this.lkpMes.EditValueChanged += new System.EventHandler(this.lkpMes_EditValueChanged);
            // 
            // lkpAnio
            // 
            this.lkpAnio.Location = new System.Drawing.Point(75, 37);
            this.lkpAnio.Name = "lkpAnio";
            this.lkpAnio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpAnio.Properties.NullText = "";
            this.lkpAnio.Size = new System.Drawing.Size(100, 20);
            this.lkpAnio.TabIndex = 0;
            this.lkpAnio.EditValueChanged += new System.EventHandler(this.lkpAnio_EditValueChanged);
            // 
            // grdLista
            // 
            this.grdLista.ContextMenuStrip = this.contextMenuStrip1;
            this.grdLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLista.Location = new System.Drawing.Point(0, 77);
            this.grdLista.MainView = this.viewLista;
            this.grdLista.Name = "grdLista";
            this.grdLista.Size = new System.Drawing.Size(1499, 427);
            this.grdLista.TabIndex = 1;
            this.grdLista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewLista});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargarCSVToolStripMenuItem,
            this.exportaListaExcelToolStripMenuItem,
            this.resumenToolStripMenuItem,
            this.compararToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 92);
            // 
            // descargarCSVToolStripMenuItem
            // 
            this.descargarCSVToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.descargar;
            this.descargarCSVToolStripMenuItem.Name = "descargarCSVToolStripMenuItem";
            this.descargarCSVToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.descargarCSVToolStripMenuItem.Text = "Descargar txt";
            this.descargarCSVToolStripMenuItem.Click += new System.EventHandler(this.descargarCSVToolStripMenuItem_Click);
            // 
            // exportaListaExcelToolStripMenuItem
            // 
            this.exportaListaExcelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportaListaExcelToolStripMenuItem.Image")));
            this.exportaListaExcelToolStripMenuItem.Name = "exportaListaExcelToolStripMenuItem";
            this.exportaListaExcelToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exportaListaExcelToolStripMenuItem.Text = "Exporta Lista Excel";
            this.exportaListaExcelToolStripMenuItem.Click += new System.EventHandler(this.exportaListaExcelToolStripMenuItem_Click);
            // 
            // resumenToolStripMenuItem
            // 
            this.resumenToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_sub1;
            this.resumenToolStripMenuItem.Name = "resumenToolStripMenuItem";
            this.resumenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.resumenToolStripMenuItem.Text = "Resumen";
            this.resumenToolStripMenuItem.Click += new System.EventHandler(this.resumenToolStripMenuItem_Click);
            // 
            // compararToolStripMenuItem
            // 
            this.compararToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_sub1;
            this.compararToolStripMenuItem.Name = "compararToolStripMenuItem";
            this.compararToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.compararToolStripMenuItem.Text = "Comparar";
            this.compararToolStripMenuItem.Click += new System.EventHandler(this.compararToolStripMenuItem_Click);
            // 
            // viewLista
            // 
            this.viewLista.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NumRuc,
            this.NomRazonSocial,
            this.PerPeriodoTributario,
            this.CodCar,
            this.CodTipoCDP,
            this.NumSerieCDP,
            this.NumCDP,
            this.CodTipoCarga,
            this.CodSituacion,
            this.FecEmision,
            this.CodTipoDocIdentidad,
            this.NumDocIdentidad,
            this.NomRazonSocialCliente,
            this.MtoValFactExpo,
            this.MtoBIGravada,
            this.MtoDsctoBI,
            this.MtoIGV,
            this.MtoDsctoIGV,
            this.gridColumn19,
            this.MtoInafecto,
            this.MtoISC,
            this.MtoBIIvap,
            this.MtoIvap,
            this.MtoIcbp,
            this.MtoOtrosTrib,
            this.MtoTotalCP,
            this.CodMoneda,
            this.MtoTipoCambio,
            this.CodEstadoComprobante,
            this.DesEstadoComprobante,
            this.IndOperGratuita,
            this.MtoValorOpGratuitas,
            this.MtoValorFob,
            this.IndTipoOperacion,
            this.MtoPorcParticipacion,
            this.MtoValorFobDolar});
            this.viewLista.GridControl = this.grdLista;
            this.viewLista.Name = "viewLista";
            this.viewLista.OptionsView.ColumnAutoWidth = false;
            this.viewLista.OptionsView.ShowFooter = true;
            // 
            // NumRuc
            // 
            this.NumRuc.FieldName = "NumRuc";
            this.NumRuc.Name = "NumRuc";
            this.NumRuc.OptionsColumn.AllowEdit = false;
            this.NumRuc.OptionsColumn.AllowFocus = false;
            this.NumRuc.OptionsColumn.AllowIncrementalSearch = false;
            this.NumRuc.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "NumRuc", "{0}")});
            this.NumRuc.Visible = true;
            this.NumRuc.VisibleIndex = 0;
            // 
            // NomRazonSocial
            // 
            this.NomRazonSocial.FieldName = "NomRazonSocial";
            this.NomRazonSocial.Name = "NomRazonSocial";
            this.NomRazonSocial.OptionsColumn.AllowEdit = false;
            this.NomRazonSocial.OptionsColumn.AllowFocus = false;
            this.NomRazonSocial.OptionsColumn.AllowIncrementalSearch = false;
            this.NomRazonSocial.Visible = true;
            this.NomRazonSocial.VisibleIndex = 1;
            // 
            // PerPeriodoTributario
            // 
            this.PerPeriodoTributario.FieldName = "PerPeriodoTributario";
            this.PerPeriodoTributario.Name = "PerPeriodoTributario";
            this.PerPeriodoTributario.OptionsColumn.AllowEdit = false;
            this.PerPeriodoTributario.OptionsColumn.AllowFocus = false;
            this.PerPeriodoTributario.OptionsColumn.AllowIncrementalSearch = false;
            this.PerPeriodoTributario.Visible = true;
            this.PerPeriodoTributario.VisibleIndex = 2;
            // 
            // CodCar
            // 
            this.CodCar.FieldName = "CodCar";
            this.CodCar.Name = "CodCar";
            this.CodCar.OptionsColumn.AllowEdit = false;
            this.CodCar.OptionsColumn.AllowFocus = false;
            this.CodCar.OptionsColumn.AllowIncrementalSearch = false;
            this.CodCar.Visible = true;
            this.CodCar.VisibleIndex = 3;
            // 
            // CodTipoCDP
            // 
            this.CodTipoCDP.FieldName = "CodTipoCdp";
            this.CodTipoCDP.Name = "CodTipoCDP";
            this.CodTipoCDP.OptionsColumn.AllowEdit = false;
            this.CodTipoCDP.OptionsColumn.AllowFocus = false;
            this.CodTipoCDP.OptionsColumn.AllowIncrementalSearch = false;
            this.CodTipoCDP.Visible = true;
            this.CodTipoCDP.VisibleIndex = 4;
            // 
            // NumSerieCDP
            // 
            this.NumSerieCDP.FieldName = "NumSerieCdp";
            this.NumSerieCDP.Name = "NumSerieCDP";
            this.NumSerieCDP.OptionsColumn.AllowEdit = false;
            this.NumSerieCDP.OptionsColumn.AllowFocus = false;
            this.NumSerieCDP.OptionsColumn.AllowIncrementalSearch = false;
            this.NumSerieCDP.Visible = true;
            this.NumSerieCDP.VisibleIndex = 5;
            // 
            // NumCDP
            // 
            this.NumCDP.FieldName = "NumCdp";
            this.NumCDP.Name = "NumCDP";
            this.NumCDP.OptionsColumn.AllowEdit = false;
            this.NumCDP.OptionsColumn.AllowFocus = false;
            this.NumCDP.OptionsColumn.AllowIncrementalSearch = false;
            this.NumCDP.Visible = true;
            this.NumCDP.VisibleIndex = 6;
            // 
            // CodTipoCarga
            // 
            this.CodTipoCarga.FieldName = "CodTipoCarga";
            this.CodTipoCarga.Name = "CodTipoCarga";
            this.CodTipoCarga.OptionsColumn.AllowEdit = false;
            this.CodTipoCarga.OptionsColumn.AllowFocus = false;
            this.CodTipoCarga.OptionsColumn.AllowIncrementalSearch = false;
            this.CodTipoCarga.Visible = true;
            this.CodTipoCarga.VisibleIndex = 7;
            // 
            // CodSituacion
            // 
            this.CodSituacion.FieldName = "CodSituacion";
            this.CodSituacion.Name = "CodSituacion";
            this.CodSituacion.OptionsColumn.AllowEdit = false;
            this.CodSituacion.OptionsColumn.AllowFocus = false;
            this.CodSituacion.OptionsColumn.AllowIncrementalSearch = false;
            this.CodSituacion.Visible = true;
            this.CodSituacion.VisibleIndex = 8;
            // 
            // FecEmision
            // 
            this.FecEmision.FieldName = "FecEmision";
            this.FecEmision.Name = "FecEmision";
            this.FecEmision.OptionsColumn.AllowEdit = false;
            this.FecEmision.OptionsColumn.AllowFocus = false;
            this.FecEmision.OptionsColumn.AllowIncrementalSearch = false;
            this.FecEmision.Visible = true;
            this.FecEmision.VisibleIndex = 9;
            // 
            // CodTipoDocIdentidad
            // 
            this.CodTipoDocIdentidad.FieldName = "CodTipoDocIdentidad";
            this.CodTipoDocIdentidad.Name = "CodTipoDocIdentidad";
            this.CodTipoDocIdentidad.OptionsColumn.AllowEdit = false;
            this.CodTipoDocIdentidad.OptionsColumn.AllowFocus = false;
            this.CodTipoDocIdentidad.OptionsColumn.AllowIncrementalSearch = false;
            this.CodTipoDocIdentidad.Visible = true;
            this.CodTipoDocIdentidad.VisibleIndex = 10;
            // 
            // NumDocIdentidad
            // 
            this.NumDocIdentidad.FieldName = "NumDocIdentidad";
            this.NumDocIdentidad.Name = "NumDocIdentidad";
            this.NumDocIdentidad.OptionsColumn.AllowEdit = false;
            this.NumDocIdentidad.OptionsColumn.AllowFocus = false;
            this.NumDocIdentidad.OptionsColumn.AllowIncrementalSearch = false;
            this.NumDocIdentidad.Visible = true;
            this.NumDocIdentidad.VisibleIndex = 11;
            // 
            // NomRazonSocialCliente
            // 
            this.NomRazonSocialCliente.FieldName = "NomRazonSocialCliente";
            this.NomRazonSocialCliente.Name = "NomRazonSocialCliente";
            this.NomRazonSocialCliente.OptionsColumn.AllowEdit = false;
            this.NomRazonSocialCliente.OptionsColumn.AllowFocus = false;
            this.NomRazonSocialCliente.OptionsColumn.AllowIncrementalSearch = false;
            this.NomRazonSocialCliente.Visible = true;
            this.NomRazonSocialCliente.VisibleIndex = 12;
            // 
            // MtoValFactExpo
            // 
            this.MtoValFactExpo.DisplayFormat.FormatString = "n2";
            this.MtoValFactExpo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoValFactExpo.FieldName = "MtoValFactExpo";
            this.MtoValFactExpo.Name = "MtoValFactExpo";
            this.MtoValFactExpo.OptionsColumn.AllowEdit = false;
            this.MtoValFactExpo.OptionsColumn.AllowFocus = false;
            this.MtoValFactExpo.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoValFactExpo.Visible = true;
            this.MtoValFactExpo.VisibleIndex = 13;
            // 
            // MtoBIGravada
            // 
            this.MtoBIGravada.DisplayFormat.FormatString = "n2";
            this.MtoBIGravada.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoBIGravada.FieldName = "MtoBiGravada";
            this.MtoBIGravada.Name = "MtoBIGravada";
            this.MtoBIGravada.OptionsColumn.AllowEdit = false;
            this.MtoBIGravada.OptionsColumn.AllowFocus = false;
            this.MtoBIGravada.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoBIGravada.Visible = true;
            this.MtoBIGravada.VisibleIndex = 14;
            // 
            // MtoDsctoBI
            // 
            this.MtoDsctoBI.DisplayFormat.FormatString = "n2";
            this.MtoDsctoBI.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoDsctoBI.FieldName = "MtoDsctoBi";
            this.MtoDsctoBI.Name = "MtoDsctoBI";
            this.MtoDsctoBI.OptionsColumn.AllowEdit = false;
            this.MtoDsctoBI.OptionsColumn.AllowFocus = false;
            this.MtoDsctoBI.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoDsctoBI.Visible = true;
            this.MtoDsctoBI.VisibleIndex = 15;
            // 
            // MtoIGV
            // 
            this.MtoIGV.DisplayFormat.FormatString = "n2";
            this.MtoIGV.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoIGV.FieldName = "MtoIgv";
            this.MtoIGV.Name = "MtoIGV";
            this.MtoIGV.OptionsColumn.AllowEdit = false;
            this.MtoIGV.OptionsColumn.AllowFocus = false;
            this.MtoIGV.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoIGV.Visible = true;
            this.MtoIGV.VisibleIndex = 16;
            // 
            // MtoDsctoIGV
            // 
            this.MtoDsctoIGV.DisplayFormat.FormatString = "n2";
            this.MtoDsctoIGV.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoDsctoIGV.FieldName = "MtoDsctoIgv";
            this.MtoDsctoIGV.Name = "MtoDsctoIGV";
            this.MtoDsctoIGV.OptionsColumn.AllowEdit = false;
            this.MtoDsctoIGV.OptionsColumn.AllowFocus = false;
            this.MtoDsctoIGV.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoDsctoIGV.Visible = true;
            this.MtoDsctoIGV.VisibleIndex = 17;
            // 
            // gridColumn19
            // 
            this.gridColumn19.DisplayFormat.FormatString = "n2";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn19.FieldName = "MtoExonerado";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.OptionsColumn.AllowFocus = false;
            this.gridColumn19.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 18;
            // 
            // MtoInafecto
            // 
            this.MtoInafecto.DisplayFormat.FormatString = "n2";
            this.MtoInafecto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoInafecto.FieldName = "MtoInafecto";
            this.MtoInafecto.Name = "MtoInafecto";
            this.MtoInafecto.OptionsColumn.AllowEdit = false;
            this.MtoInafecto.OptionsColumn.AllowFocus = false;
            this.MtoInafecto.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoInafecto.Visible = true;
            this.MtoInafecto.VisibleIndex = 19;
            // 
            // MtoISC
            // 
            this.MtoISC.DisplayFormat.FormatString = "n2";
            this.MtoISC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoISC.FieldName = "MtoISC";
            this.MtoISC.Name = "MtoISC";
            this.MtoISC.OptionsColumn.AllowEdit = false;
            this.MtoISC.OptionsColumn.AllowFocus = false;
            this.MtoISC.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoISC.Visible = true;
            this.MtoISC.VisibleIndex = 20;
            // 
            // MtoBIIvap
            // 
            this.MtoBIIvap.Caption = "MtoBIIvap";
            this.MtoBIIvap.DisplayFormat.FormatString = "n2";
            this.MtoBIIvap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoBIIvap.FieldName = "MtoBIIvap";
            this.MtoBIIvap.Name = "MtoBIIvap";
            this.MtoBIIvap.OptionsColumn.AllowEdit = false;
            this.MtoBIIvap.OptionsColumn.AllowFocus = false;
            this.MtoBIIvap.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoBIIvap.Visible = true;
            this.MtoBIIvap.VisibleIndex = 21;
            // 
            // MtoIvap
            // 
            this.MtoIvap.Caption = "MtoIvap";
            this.MtoIvap.DisplayFormat.FormatString = "n2";
            this.MtoIvap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoIvap.FieldName = "MtoIvap";
            this.MtoIvap.Name = "MtoIvap";
            this.MtoIvap.OptionsColumn.AllowEdit = false;
            this.MtoIvap.OptionsColumn.AllowFocus = false;
            this.MtoIvap.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoIvap.Visible = true;
            this.MtoIvap.VisibleIndex = 22;
            // 
            // MtoIcbp
            // 
            this.MtoIcbp.Caption = "MtoIcbp";
            this.MtoIcbp.DisplayFormat.FormatString = "n2";
            this.MtoIcbp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoIcbp.FieldName = "MtoIcbp";
            this.MtoIcbp.Name = "MtoIcbp";
            this.MtoIcbp.OptionsColumn.AllowEdit = false;
            this.MtoIcbp.OptionsColumn.AllowFocus = false;
            this.MtoIcbp.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoIcbp.Visible = true;
            this.MtoIcbp.VisibleIndex = 23;
            // 
            // MtoOtrosTrib
            // 
            this.MtoOtrosTrib.Caption = "MtoOtrosTrib";
            this.MtoOtrosTrib.DisplayFormat.FormatString = "n2";
            this.MtoOtrosTrib.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoOtrosTrib.FieldName = "MtoOtrosTrib";
            this.MtoOtrosTrib.Name = "MtoOtrosTrib";
            this.MtoOtrosTrib.OptionsColumn.AllowEdit = false;
            this.MtoOtrosTrib.OptionsColumn.AllowFocus = false;
            this.MtoOtrosTrib.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoOtrosTrib.Visible = true;
            this.MtoOtrosTrib.VisibleIndex = 24;
            // 
            // MtoTotalCP
            // 
            this.MtoTotalCP.Caption = "MtoTotalCP";
            this.MtoTotalCP.DisplayFormat.FormatString = "n2";
            this.MtoTotalCP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoTotalCP.FieldName = "MtoTotalCp";
            this.MtoTotalCP.Name = "MtoTotalCP";
            this.MtoTotalCP.OptionsColumn.AllowEdit = false;
            this.MtoTotalCP.OptionsColumn.AllowFocus = false;
            this.MtoTotalCP.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoTotalCP.Visible = true;
            this.MtoTotalCP.VisibleIndex = 25;
            // 
            // CodMoneda
            // 
            this.CodMoneda.Caption = "CodMoneda";
            this.CodMoneda.FieldName = "CodMoneda";
            this.CodMoneda.Name = "CodMoneda";
            this.CodMoneda.OptionsColumn.AllowEdit = false;
            this.CodMoneda.OptionsColumn.AllowFocus = false;
            this.CodMoneda.OptionsColumn.AllowIncrementalSearch = false;
            this.CodMoneda.Visible = true;
            this.CodMoneda.VisibleIndex = 26;
            // 
            // MtoTipoCambio
            // 
            this.MtoTipoCambio.Caption = "MtoTipoCambio";
            this.MtoTipoCambio.DisplayFormat.FormatString = "n3";
            this.MtoTipoCambio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MtoTipoCambio.FieldName = "MtoTipoCambio";
            this.MtoTipoCambio.Name = "MtoTipoCambio";
            this.MtoTipoCambio.OptionsColumn.AllowEdit = false;
            this.MtoTipoCambio.OptionsColumn.AllowFocus = false;
            this.MtoTipoCambio.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoTipoCambio.Visible = true;
            this.MtoTipoCambio.VisibleIndex = 27;
            // 
            // CodEstadoComprobante
            // 
            this.CodEstadoComprobante.Caption = "CodEstadoComprobante";
            this.CodEstadoComprobante.FieldName = "CodEstadoComprobante";
            this.CodEstadoComprobante.Name = "CodEstadoComprobante";
            this.CodEstadoComprobante.OptionsColumn.AllowEdit = false;
            this.CodEstadoComprobante.OptionsColumn.AllowFocus = false;
            this.CodEstadoComprobante.OptionsColumn.AllowIncrementalSearch = false;
            this.CodEstadoComprobante.Visible = true;
            this.CodEstadoComprobante.VisibleIndex = 28;
            // 
            // DesEstadoComprobante
            // 
            this.DesEstadoComprobante.Caption = "DesEstadoComprobante";
            this.DesEstadoComprobante.FieldName = "DesEstadoComprobante";
            this.DesEstadoComprobante.Name = "DesEstadoComprobante";
            this.DesEstadoComprobante.OptionsColumn.AllowEdit = false;
            this.DesEstadoComprobante.OptionsColumn.AllowFocus = false;
            this.DesEstadoComprobante.OptionsColumn.AllowIncrementalSearch = false;
            this.DesEstadoComprobante.Visible = true;
            this.DesEstadoComprobante.VisibleIndex = 29;
            // 
            // IndOperGratuita
            // 
            this.IndOperGratuita.Caption = "IndOperGratuita";
            this.IndOperGratuita.FieldName = "IndOperGratuita";
            this.IndOperGratuita.Name = "IndOperGratuita";
            this.IndOperGratuita.OptionsColumn.AllowEdit = false;
            this.IndOperGratuita.OptionsColumn.AllowFocus = false;
            this.IndOperGratuita.OptionsColumn.AllowIncrementalSearch = false;
            this.IndOperGratuita.Visible = true;
            this.IndOperGratuita.VisibleIndex = 30;
            // 
            // MtoValorOpGratuitas
            // 
            this.MtoValorOpGratuitas.Caption = "MtoValorOpGratuitas";
            this.MtoValorOpGratuitas.FieldName = "MtoValorOpGratuitas";
            this.MtoValorOpGratuitas.Name = "MtoValorOpGratuitas";
            this.MtoValorOpGratuitas.OptionsColumn.AllowEdit = false;
            this.MtoValorOpGratuitas.OptionsColumn.AllowFocus = false;
            this.MtoValorOpGratuitas.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoValorOpGratuitas.Visible = true;
            this.MtoValorOpGratuitas.VisibleIndex = 31;
            // 
            // MtoValorFob
            // 
            this.MtoValorFob.Caption = "MtoValorFob";
            this.MtoValorFob.FieldName = "MtoValorFob";
            this.MtoValorFob.Name = "MtoValorFob";
            this.MtoValorFob.OptionsColumn.AllowEdit = false;
            this.MtoValorFob.OptionsColumn.AllowFocus = false;
            this.MtoValorFob.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoValorFob.Visible = true;
            this.MtoValorFob.VisibleIndex = 32;
            // 
            // IndTipoOperacion
            // 
            this.IndTipoOperacion.Caption = "IndTipoOperacion";
            this.IndTipoOperacion.FieldName = "IndTipoOperacion";
            this.IndTipoOperacion.Name = "IndTipoOperacion";
            this.IndTipoOperacion.OptionsColumn.AllowEdit = false;
            this.IndTipoOperacion.OptionsColumn.AllowFocus = false;
            this.IndTipoOperacion.OptionsColumn.AllowIncrementalSearch = false;
            this.IndTipoOperacion.Visible = true;
            this.IndTipoOperacion.VisibleIndex = 33;
            // 
            // MtoPorcParticipacion
            // 
            this.MtoPorcParticipacion.Caption = "MtoPorcParticipacion";
            this.MtoPorcParticipacion.FieldName = "MtoPorcParticipacion";
            this.MtoPorcParticipacion.Name = "MtoPorcParticipacion";
            this.MtoPorcParticipacion.OptionsColumn.AllowEdit = false;
            this.MtoPorcParticipacion.OptionsColumn.AllowFocus = false;
            this.MtoPorcParticipacion.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoPorcParticipacion.Visible = true;
            this.MtoPorcParticipacion.VisibleIndex = 34;
            // 
            // MtoValorFobDolar
            // 
            this.MtoValorFobDolar.Caption = "MtoValorFobDolar";
            this.MtoValorFobDolar.FieldName = "MtoValorFobDolar";
            this.MtoValorFobDolar.Name = "MtoValorFobDolar";
            this.MtoValorFobDolar.OptionsColumn.AllowEdit = false;
            this.MtoValorFobDolar.OptionsColumn.AllowFocus = false;
            this.MtoValorFobDolar.OptionsColumn.AllowIncrementalSearch = false;
            this.MtoValorFobDolar.Visible = true;
            this.MtoValorFobDolar.VisibleIndex = 35;
            // 
            // Frm02ConsultaPropuestaVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 504);
            this.Controls.Add(this.grdLista);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm02ConsultaPropuestaVentas";
            this.Text = "Consulta Propuestas Ventas";
            this.Load += new System.EventHandler(this.Frm02ConsultaPropuestasCompras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAnio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLista)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnPropuesta;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpMes;
        private DevExpress.XtraEditors.LookUpEdit lkpAnio;
        private DevExpress.XtraGrid.GridControl grdLista;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLista;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn IndOperGratuita;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn NumRuc;
        private DevExpress.XtraGrid.Columns.GridColumn NomRazonSocial;
        private DevExpress.XtraGrid.Columns.GridColumn PerPeriodoTributario;
        private DevExpress.XtraGrid.Columns.GridColumn CodCar;
        private DevExpress.XtraGrid.Columns.GridColumn CodTipoCDP;
        private DevExpress.XtraGrid.Columns.GridColumn NumSerieCDP;
        private DevExpress.XtraGrid.Columns.GridColumn NumCDP;
        private DevExpress.XtraGrid.Columns.GridColumn CodTipoCarga;
        private DevExpress.XtraGrid.Columns.GridColumn CodSituacion;
        private DevExpress.XtraGrid.Columns.GridColumn FecEmision;
        private DevExpress.XtraGrid.Columns.GridColumn CodTipoDocIdentidad;
        private DevExpress.XtraGrid.Columns.GridColumn NumDocIdentidad;
        private DevExpress.XtraGrid.Columns.GridColumn NomRazonSocialCliente;
        private DevExpress.XtraGrid.Columns.GridColumn MtoValFactExpo;
        private DevExpress.XtraGrid.Columns.GridColumn MtoBIGravada;
        private DevExpress.XtraGrid.Columns.GridColumn MtoDsctoBI;
        private DevExpress.XtraGrid.Columns.GridColumn MtoIGV;
        private DevExpress.XtraGrid.Columns.GridColumn MtoDsctoIGV;
        private DevExpress.XtraGrid.Columns.GridColumn MtoInafecto;
        private DevExpress.XtraGrid.Columns.GridColumn MtoISC;
        private DevExpress.XtraGrid.Columns.GridColumn MtoBIIvap;
        private DevExpress.XtraGrid.Columns.GridColumn MtoIvap;
        private DevExpress.XtraGrid.Columns.GridColumn MtoIcbp;
        private DevExpress.XtraGrid.Columns.GridColumn MtoOtrosTrib;
        private DevExpress.XtraGrid.Columns.GridColumn MtoTotalCP;
        private DevExpress.XtraGrid.Columns.GridColumn CodMoneda;
        private DevExpress.XtraGrid.Columns.GridColumn MtoTipoCambio;
        private DevExpress.XtraGrid.Columns.GridColumn CodEstadoComprobante;
        private DevExpress.XtraGrid.Columns.GridColumn DesEstadoComprobante;
        private DevExpress.XtraGrid.Columns.GridColumn MtoValorOpGratuitas;
        private DevExpress.XtraGrid.Columns.GridColumn MtoValorFob;
        private DevExpress.XtraGrid.Columns.GridColumn IndTipoOperacion;
        private DevExpress.XtraGrid.Columns.GridColumn MtoPorcParticipacion;
        private DevExpress.XtraGrid.Columns.GridColumn MtoValorFobDolar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem descargarCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportaListaExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compararToolStripMenuItem;
    }
}