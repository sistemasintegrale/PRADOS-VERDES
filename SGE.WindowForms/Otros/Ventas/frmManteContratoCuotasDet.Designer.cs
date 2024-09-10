namespace SGE.WindowForms.Otros.bVentas
{
    partial class frmManteContratoCuotasDet
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
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblCronograma = new DevExpress.XtraEditors.LabelControl();
            this.txtCronograma = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMora = new DevExpress.XtraEditors.TextEdit();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.dtFechaCuota = new DevExpress.XtraEditors.DateEdit();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtNroCuotas = new DevExpress.XtraEditors.TextEdit();
            this.lkpTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCronograma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaCuota.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaCuota.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroCuotas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).BeginInit();
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
            this.btnAceptar,
            this.btnCancelar});
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
            this.btnAceptar.Id = 0;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 1;
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
            this.barDockControlTop.Size = new System.Drawing.Size(362, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 106);
            this.barDockControlBottom.Size = new System.Drawing.Size(362, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 106);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(362, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 106);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblCronograma);
            this.groupControl1.Controls.Add(this.txtCronograma);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtMora);
            this.groupControl1.Controls.Add(this.labelControl25);
            this.groupControl1.Controls.Add(this.txtMonto);
            this.groupControl1.Controls.Add(this.dtFechaCuota);
            this.groupControl1.Controls.Add(this.labelControl30);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.txtNroCuotas);
            this.groupControl1.Controls.Add(this.lkpTipo);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(362, 106);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos";
            // 
            // lblCronograma
            // 
            this.lblCronograma.Location = new System.Drawing.Point(202, 138);
            this.lblCronograma.Name = "lblCronograma";
            this.lblCronograma.Size = new System.Drawing.Size(66, 13);
            this.lblCronograma.TabIndex = 184;
            this.lblCronograma.Text = "Cronograma :";
            this.lblCronograma.Visible = false;
            // 
            // txtCronograma
            // 
            this.txtCronograma.EditValue = "";
            this.txtCronograma.Location = new System.Drawing.Point(287, 135);
            this.txtCronograma.Name = "txtCronograma";
            this.txtCronograma.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCronograma.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCronograma.Size = new System.Drawing.Size(51, 20);
            this.txtCronograma.TabIndex = 185;
            this.txtCronograma.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(49, 138);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 13);
            this.labelControl1.TabIndex = 182;
            this.labelControl1.Text = "Mora :";
            this.labelControl1.Visible = false;
            // 
            // txtMora
            // 
            this.txtMora.EditValue = "";
            this.txtMora.Location = new System.Drawing.Point(86, 135);
            this.txtMora.Name = "txtMora";
            this.txtMora.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMora.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMora.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMora.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMora.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMora.Properties.Mask.EditMask = "n2";
            this.txtMora.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMora.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMora.Size = new System.Drawing.Size(84, 20);
            this.txtMora.TabIndex = 183;
            this.txtMora.Visible = false;
            // 
            // labelControl25
            // 
            this.labelControl25.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl25.Location = new System.Drawing.Point(202, 65);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(37, 13);
            this.labelControl25.TabIndex = 180;
            this.labelControl25.Text = "Monto :";
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "";
            this.txtMonto.Location = new System.Drawing.Point(254, 62);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMonto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMonto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMonto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMonto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.Size = new System.Drawing.Size(84, 20);
            this.txtMonto.TabIndex = 181;
            // 
            // dtFechaCuota
            // 
            this.dtFechaCuota.EditValue = null;
            this.dtFechaCuota.Location = new System.Drawing.Point(86, 62);
            this.dtFechaCuota.Name = "dtFechaCuota";
            this.dtFechaCuota.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtFechaCuota.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtFechaCuota.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.dtFechaCuota.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.dtFechaCuota.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.dtFechaCuota.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.dtFechaCuota.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaCuota.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtFechaCuota.Properties.MaxValue = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.dtFechaCuota.Properties.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtFechaCuota.Size = new System.Drawing.Size(84, 20);
            this.dtFechaCuota.TabIndex = 176;
            // 
            // labelControl30
            // 
            this.labelControl30.Location = new System.Drawing.Point(12, 65);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(68, 13);
            this.labelControl30.TabIndex = 177;
            this.labelControl30.Text = "Fecha Cuota :";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(29, 39);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(51, 13);
            this.labelControl10.TabIndex = 76;
            this.labelControl10.Text = "Nº Cuota :";
            // 
            // txtNroCuotas
            // 
            this.txtNroCuotas.EditValue = "";
            this.txtNroCuotas.Enabled = false;
            this.txtNroCuotas.Location = new System.Drawing.Point(86, 36);
            this.txtNroCuotas.Name = "txtNroCuotas";
            this.txtNroCuotas.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtNroCuotas.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtNroCuotas.Size = new System.Drawing.Size(51, 20);
            this.txtNroCuotas.TabIndex = 77;
            // 
            // lkpTipo
            // 
            this.lkpTipo.Location = new System.Drawing.Point(254, 36);
            this.lkpTipo.Name = "lkpTipo";
            this.lkpTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipo.Properties.NullText = "";
            this.lkpTipo.Size = new System.Drawing.Size(84, 20);
            this.lkpTipo.TabIndex = 73;
            this.lkpTipo.EditValueChanged += new System.EventHandler(this.lkpTipo_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(212, 39);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(27, 13);
            this.labelControl8.TabIndex = 72;
            this.labelControl8.Text = "Tipo :";
            // 
            // frmManteContratoCuotasDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 133);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteContratoCuotasDet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Contrato Detalle";
            this.Load += new System.EventHandler(this.frmMantePercepcionDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCronograma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaCuota.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaCuota.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroCuotas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.LookUpEdit lkpTipo;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit txtNroCuotas;
        private DevExpress.XtraEditors.LabelControl labelControl30;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        public DevExpress.XtraEditors.DateEdit dtFechaCuota;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtMora;
        private DevExpress.XtraEditors.LabelControl lblCronograma;
        public DevExpress.XtraEditors.TextEdit txtCronograma;
    }
}