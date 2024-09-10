namespace SGE.WindowForms.Otros.bVentas
{
    partial class FrmManteCajaTurno
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnSalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCaja = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSerieFactura = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCorrelativoFactura = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtSerieBoleta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCorrelativoBoleta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtSerieNotaCredito = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtCorrelativoNotaCredito = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.TxtCodCaja = new DevExpress.XtraEditors.TextEdit();
            this.lkpPuntoVenta = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieFactura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativoFactura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieBoleta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativoBoleta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieNotaCredito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativoNotaCredito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCodCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.btnGuardar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGuardar.Id = 0;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSalir.Caption = "Salir";
            this.btnSalir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
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
            this.barDockControlTop.Size = new System.Drawing.Size(341, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 171);
            this.barDockControlBottom.Size = new System.Drawing.Size(341, 29);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 171);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(341, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 171);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(114, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Descripción:";
            // 
            // txtCaja
            // 
            this.txtCaja.EditValue = "";
            this.txtCaja.Enabled = false;
            this.txtCaja.Location = new System.Drawing.Point(178, 50);
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.Properties.LookAndFeel.SkinName = "Blue";
            this.txtCaja.Properties.Mask.ShowPlaceHolders = false;
            this.txtCaja.Properties.MaxLength = 20;
            this.txtCaja.Size = new System.Drawing.Size(132, 20);
            this.txtCaja.TabIndex = 1;
            this.txtCaja.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCaja_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 85);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Factura Serie:";
            // 
            // txtSerieFactura
            // 
            this.txtSerieFactura.EditValue = "";
            this.txtSerieFactura.Enabled = false;
            this.txtSerieFactura.Location = new System.Drawing.Point(119, 82);
            this.txtSerieFactura.Name = "txtSerieFactura";
            this.txtSerieFactura.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSerieFactura.Properties.Mask.EditMask = "000";
            this.txtSerieFactura.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSerieFactura.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerieFactura.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSerieFactura.Properties.MaxLength = 3;
            this.txtSerieFactura.Size = new System.Drawing.Size(30, 20);
            this.txtSerieFactura.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(178, 85);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Correlativo:";
            // 
            // txtCorrelativoFactura
            // 
            this.txtCorrelativoFactura.EditValue = "";
            this.txtCorrelativoFactura.Enabled = false;
            this.txtCorrelativoFactura.Location = new System.Drawing.Point(265, 82);
            this.txtCorrelativoFactura.Name = "txtCorrelativoFactura";
            this.txtCorrelativoFactura.Properties.LookAndFeel.SkinName = "Blue";
            this.txtCorrelativoFactura.Properties.Mask.EditMask = "000000";
            this.txtCorrelativoFactura.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCorrelativoFactura.Properties.Mask.ShowPlaceHolders = false;
            this.txtCorrelativoFactura.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCorrelativoFactura.Properties.MaxLength = 6;
            this.txtCorrelativoFactura.Size = new System.Drawing.Size(45, 20);
            this.txtCorrelativoFactura.TabIndex = 15;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(21, 111);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Boleta Serie:";
            // 
            // txtSerieBoleta
            // 
            this.txtSerieBoleta.EditValue = "";
            this.txtSerieBoleta.Enabled = false;
            this.txtSerieBoleta.Location = new System.Drawing.Point(119, 108);
            this.txtSerieBoleta.Name = "txtSerieBoleta";
            this.txtSerieBoleta.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSerieBoleta.Properties.Mask.EditMask = "000";
            this.txtSerieBoleta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSerieBoleta.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerieBoleta.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSerieBoleta.Properties.MaxLength = 3;
            this.txtSerieBoleta.Size = new System.Drawing.Size(30, 20);
            this.txtSerieBoleta.TabIndex = 16;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(178, 111);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(57, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Correlativo:";
            // 
            // txtCorrelativoBoleta
            // 
            this.txtCorrelativoBoleta.EditValue = "";
            this.txtCorrelativoBoleta.Enabled = false;
            this.txtCorrelativoBoleta.Location = new System.Drawing.Point(265, 108);
            this.txtCorrelativoBoleta.Name = "txtCorrelativoBoleta";
            this.txtCorrelativoBoleta.Properties.LookAndFeel.SkinName = "Blue";
            this.txtCorrelativoBoleta.Properties.Mask.EditMask = "000000";
            this.txtCorrelativoBoleta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCorrelativoBoleta.Properties.Mask.ShowPlaceHolders = false;
            this.txtCorrelativoBoleta.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCorrelativoBoleta.Properties.MaxLength = 6;
            this.txtCorrelativoBoleta.Size = new System.Drawing.Size(45, 20);
            this.txtCorrelativoBoleta.TabIndex = 17;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(21, 137);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(92, 13);
            this.labelControl7.TabIndex = 11;
            this.labelControl7.Text = "Nota Crédito Serie:";
            // 
            // txtSerieNotaCredito
            // 
            this.txtSerieNotaCredito.EditValue = "";
            this.txtSerieNotaCredito.Enabled = false;
            this.txtSerieNotaCredito.Location = new System.Drawing.Point(119, 134);
            this.txtSerieNotaCredito.Name = "txtSerieNotaCredito";
            this.txtSerieNotaCredito.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSerieNotaCredito.Properties.Mask.EditMask = "000";
            this.txtSerieNotaCredito.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSerieNotaCredito.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerieNotaCredito.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSerieNotaCredito.Properties.MaxLength = 3;
            this.txtSerieNotaCredito.Size = new System.Drawing.Size(30, 20);
            this.txtSerieNotaCredito.TabIndex = 18;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(178, 137);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(57, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Correlativo:";
            // 
            // txtCorrelativoNotaCredito
            // 
            this.txtCorrelativoNotaCredito.EditValue = "";
            this.txtCorrelativoNotaCredito.Enabled = false;
            this.txtCorrelativoNotaCredito.Location = new System.Drawing.Point(265, 134);
            this.txtCorrelativoNotaCredito.Name = "txtCorrelativoNotaCredito";
            this.txtCorrelativoNotaCredito.Properties.LookAndFeel.SkinName = "Blue";
            this.txtCorrelativoNotaCredito.Properties.Mask.EditMask = "000000";
            this.txtCorrelativoNotaCredito.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCorrelativoNotaCredito.Properties.Mask.ShowPlaceHolders = false;
            this.txtCorrelativoNotaCredito.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCorrelativoNotaCredito.Properties.MaxLength = 6;
            this.txtCorrelativoNotaCredito.Size = new System.Drawing.Size(45, 20);
            this.txtCorrelativoNotaCredito.TabIndex = 19;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 30);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(42, 13);
            this.labelControl8.TabIndex = 20;
            this.labelControl8.Text = "P.Venta:";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(13, 53);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(41, 13);
            this.labelControl9.TabIndex = 80;
            this.labelControl9.Text = "Caja Nº:";
            // 
            // TxtCodCaja
            // 
            this.TxtCodCaja.EditValue = "00";
            this.TxtCodCaja.Location = new System.Drawing.Point(60, 50);
            this.TxtCodCaja.Name = "TxtCodCaja";
            this.TxtCodCaja.Properties.DisplayFormat.FormatString = "d2";
            this.TxtCodCaja.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TxtCodCaja.Properties.EditFormat.FormatString = "d2";
            this.TxtCodCaja.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TxtCodCaja.Properties.LookAndFeel.SkinName = "Blue";
            this.TxtCodCaja.Properties.Mask.EditMask = "d2";
            this.TxtCodCaja.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.TxtCodCaja.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.TxtCodCaja.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtCodCaja.Size = new System.Drawing.Size(48, 20);
            this.TxtCodCaja.TabIndex = 81;
            // 
            // lkpPuntoVenta
            // 
            this.lkpPuntoVenta.Location = new System.Drawing.Point(59, 27);
            this.lkpPuntoVenta.Name = "lkpPuntoVenta";
            this.lkpPuntoVenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPuntoVenta.Properties.LookAndFeel.SkinName = "Blue";
            this.lkpPuntoVenta.Properties.NullText = "";
            this.lkpPuntoVenta.Size = new System.Drawing.Size(251, 20);
            this.lkpPuntoVenta.TabIndex = 82;
            this.lkpPuntoVenta.EditValueChanged += new System.EventHandler(this.lkpPuntoVenta_EditValueChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lookUpEdit1);
            this.groupControl1.Controls.Add(this.lkpPuntoVenta);
            this.groupControl1.Controls.Add(this.TxtCodCaja);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtCorrelativoNotaCredito);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtSerieNotaCredito);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtCorrelativoBoleta);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtSerieBoleta);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtCorrelativoFactura);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtSerieFactura);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtCaja);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(341, 171);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Caja";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(60, 25);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.LookAndFeel.SkinName = "Blue";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Size = new System.Drawing.Size(251, 20);
            this.lookUpEdit1.TabIndex = 82;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lkpPuntoVenta_EditValueChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 28);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(42, 13);
            this.labelControl10.TabIndex = 20;
            this.labelControl10.Text = "P.Venta:";
            // 
            // FrmManteCajaTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 200);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteCajaTurno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Caja";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteCaja_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteCaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieFactura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativoFactura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieBoleta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativoBoleta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieNotaCredito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativoNotaCredito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCodCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        public DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnSalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpPuntoVenta;
        public DevExpress.XtraEditors.TextEdit TxtCodCaja;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        public DevExpress.XtraEditors.TextEdit txtCorrelativoNotaCredito;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtSerieNotaCredito;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.TextEdit txtCorrelativoBoleta;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtSerieBoleta;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtCorrelativoFactura;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtSerieFactura;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtCaja;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl10;

    }
}