namespace SGE.WindowForms.Otros.Compras
{
    partial class FrmManteCostosReporteProduccion
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
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTipoDeCambio = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl36 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipoCosto = new DevExpress.XtraEditors.LookUpEdit();
            this.txtProveedor = new DevExpress.XtraEditors.TextEdit();
            this.btnProveedor = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnDocumentoPorPagar = new DevExpress.XtraEditors.SimpleButton();
            this.deFechaDoc = new DevExpress.XtraEditors.DateEdit();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtDocumento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnSalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoDeCambio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProveedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProveedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDoc.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpMoneda);
            this.groupControl1.Controls.Add(this.txtTipoDeCambio);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl36);
            this.groupControl1.Controls.Add(this.lkpTipoCosto);
            this.groupControl1.Controls.Add(this.txtProveedor);
            this.groupControl1.Controls.Add(this.btnProveedor);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnDocumentoPorPagar);
            this.groupControl1.Controls.Add(this.deFechaDoc);
            this.groupControl1.Controls.Add(this.txtNumero);
            this.groupControl1.Controls.Add(this.labelControl17);
            this.groupControl1.Controls.Add(this.txtDocumento);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtMonto);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(503, 129);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos";
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Enabled = false;
            this.lkpMoneda.Location = new System.Drawing.Point(285, 80);
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(109, 20);
            this.lkpMoneda.TabIndex = 13;
            // 
            // txtTipoDeCambio
            // 
            this.txtTipoDeCambio.EditValue = "0.0000";
            this.txtTipoDeCambio.Enabled = false;
            this.txtTipoDeCambio.Location = new System.Drawing.Point(428, 80);
            this.txtTipoDeCambio.Name = "txtTipoDeCambio";
            this.txtTipoDeCambio.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoDeCambio.Properties.Appearance.Options.UseFont = true;
            this.txtTipoDeCambio.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTipoDeCambio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTipoDeCambio.Properties.DisplayFormat.FormatString = "n4";
            this.txtTipoDeCambio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTipoDeCambio.Properties.EditFormat.FormatString = "n4";
            this.txtTipoDeCambio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTipoDeCambio.Properties.Mask.EditMask = "n4";
            this.txtTipoDeCambio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTipoDeCambio.Properties.Mask.ShowPlaceHolders = false;
            this.txtTipoDeCambio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTipoDeCambio.Size = new System.Drawing.Size(70, 20);
            this.txtTipoDeCambio.TabIndex = 15;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(244, 83);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(42, 13);
            this.labelControl9.TabIndex = 12;
            this.labelControl9.Text = "Moneda:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(400, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(21, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "T/C:";
            // 
            // labelControl36
            // 
            this.labelControl36.Location = new System.Drawing.Point(16, 83);
            this.labelControl36.Name = "labelControl36";
            this.labelControl36.Size = new System.Drawing.Size(55, 13);
            this.labelControl36.TabIndex = 10;
            this.labelControl36.Text = "Tipo Costo:";
            // 
            // lkpTipoCosto
            // 
            this.lkpTipoCosto.Location = new System.Drawing.Point(75, 80);
            this.lkpTipoCosto.Name = "lkpTipoCosto";
            this.lkpTipoCosto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoCosto.Properties.NullText = "";
            this.lkpTipoCosto.Size = new System.Drawing.Size(163, 20);
            this.lkpTipoCosto.TabIndex = 11;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Enabled = false;
            this.txtProveedor.Location = new System.Drawing.Point(201, 35);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Properties.ReadOnly = true;
            this.txtProveedor.Size = new System.Drawing.Size(297, 20);
            this.txtProveedor.TabIndex = 2;
            // 
            // btnProveedor
            // 
            this.btnProveedor.Location = new System.Drawing.Point(75, 35);
            this.btnProveedor.Name = "btnProveedor";
            this.btnProveedor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnProveedor.Properties.ReadOnly = true;
            this.btnProveedor.Size = new System.Drawing.Size(119, 20);
            this.btnProveedor.TabIndex = 1;
            this.btnProveedor.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnProveedor_ButtonClick);
            this.btnProveedor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnProveedor_PreviewKeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(161, 60);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Fecha:";
            // 
            // btnDocumentoPorPagar
            // 
            //this.btnDocumentoPorPagar.Image = global::SGI.WindowsForm.Properties.Resources.Consultar_16x16;
            this.btnDocumentoPorPagar.Location = new System.Drawing.Point(75, 57);
            this.btnDocumentoPorPagar.Name = "btnDocumentoPorPagar";
            this.btnDocumentoPorPagar.Size = new System.Drawing.Size(33, 20);
            this.btnDocumentoPorPagar.TabIndex = 9;
            this.btnDocumentoPorPagar.ToolTip = "Buscar Documentos Por Pagar";
            this.btnDocumentoPorPagar.Click += new System.EventHandler(this.btnDocumentoPorPagar_Click);
            // 
            // deFechaDoc
            // 
            this.deFechaDoc.EditValue = null;
            this.deFechaDoc.Location = new System.Drawing.Point(201, 57);
            this.deFechaDoc.Name = "deFechaDoc";
            this.deFechaDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFechaDoc.Properties.ReadOnly = true;
            this.deFechaDoc.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFechaDoc.Size = new System.Drawing.Size(91, 20);
            this.deFechaDoc.TabIndex = 6;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(345, 57);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Properties.MaxLength = 20;
            this.txtNumero.Properties.ReadOnly = true;
            this.txtNumero.Size = new System.Drawing.Size(163, 20);
            this.txtNumero.TabIndex = 8;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(298, 61);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(41, 13);
            this.labelControl17.TabIndex = 7;
            this.labelControl17.Text = "Número:";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(114, 57);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Properties.MaxLength = 20;
            this.txtDocumento.Properties.ReadOnly = true;
            this.txtDocumento.Size = new System.Drawing.Size(41, 20);
            this.txtDocumento.TabIndex = 4;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(13, 62);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(58, 13);
            this.labelControl16.TabIndex = 3;
            this.labelControl16.Text = "Documento:";
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(14, 38);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(57, 13);
            this.labelControl20.TabIndex = 0;
            this.labelControl20.Text = "Proveedor :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(37, 106);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(34, 13);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "0.00";
            this.txtMonto.Location = new System.Drawing.Point(75, 102);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.DisplayFormat.FormatString = "n2";
            this.txtMonto.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMonto.Size = new System.Drawing.Size(85, 20);
            this.txtMonto.TabIndex = 17;
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
            this.btnGuardar,
            this.btnSalir});
            this.barManager1.MaxItemId = 2;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSalir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnGuardar.Caption = "Agregar";
            //this.btnGuardar.Glyph = global::SGI.WindowsForm.Properties.Resources.doc_mini_anadir;
            this.btnGuardar.Id = 0;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Caption = "Salir";
            //this.btnSalir.Glyph = global::SGI.WindowsForm.Properties.Resources.Shutdown;
            this.btnSalir.Id = 1;
            this.btnSalir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSalir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(503, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 128);
            this.barDockControlBottom.Size = new System.Drawing.Size(503, 29);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 128);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(503, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 128);
            // 
            // FrmManteCostosReporteProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 157);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteCostosReporteProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SGI-Registro de Costos de Reporte de Producción";
            this.Load += new System.EventHandler(this.FrmManteCostosReporteProduccion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoDeCambio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProveedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProveedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDoc.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        public DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.SimpleButton btnDocumentoPorPagar;
        public DevExpress.XtraEditors.DateEdit deFechaDoc;
        public DevExpress.XtraEditors.TextEdit txtNumero;
        public DevExpress.XtraEditors.LabelControl labelControl17;
        public DevExpress.XtraEditors.TextEdit txtDocumento;
        public DevExpress.XtraEditors.LabelControl labelControl16;
        public DevExpress.XtraEditors.TextEdit txtProveedor;
        public DevExpress.XtraEditors.ButtonEdit btnProveedor;
        private DevExpress.XtraEditors.LabelControl labelControl36;
        public DevExpress.XtraEditors.LookUpEdit lkpTipoCosto;
        public DevExpress.XtraEditors.LookUpEdit lkpMoneda;
        public DevExpress.XtraEditors.TextEdit txtTipoDeCambio;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnSalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem btnGuardar;
    }
}