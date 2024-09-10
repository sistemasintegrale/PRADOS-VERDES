namespace SGE.WindowForms.Otros.bVentas
{
    partial class FrmMantePagoCuotas
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMontoPagado = new DevExpress.XtraEditors.TextEdit();
            this.txtMontoAPagar = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ckManual = new DevExpress.XtraEditors.CheckEdit();
            this.ckAutomatico = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoPagado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoAPagar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckManual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutomatico.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.ckAutomatico);
            this.groupControl1.Controls.Add(this.ckManual);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtMontoPagado);
            this.groupControl1.Controls.Add(this.txtMontoAPagar);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(361, 96);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos de Entrada:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(27, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Monto Pagado :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(104, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Monto Mora a Pagar :";
            // 
            // txtMontoPagado
            // 
            this.txtMontoPagado.EditValue = "0.00";
            this.txtMontoPagado.Location = new System.Drawing.Point(130, 59);
            this.txtMontoPagado.Name = "txtMontoPagado";
            this.txtMontoPagado.Properties.DisplayFormat.FormatString = "n2";
            this.txtMontoPagado.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontoPagado.Properties.EditFormat.FormatString = "n2";
            this.txtMontoPagado.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontoPagado.Properties.Mask.EditMask = "n2";
            this.txtMontoPagado.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontoPagado.Properties.Mask.PlaceHolder = '0';
            this.txtMontoPagado.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontoPagado.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMontoPagado.Size = new System.Drawing.Size(71, 20);
            this.txtMontoPagado.TabIndex = 1;
            // 
            // txtMontoAPagar
            // 
            this.txtMontoAPagar.EditValue = "0.00";
            this.txtMontoAPagar.Location = new System.Drawing.Point(130, 33);
            this.txtMontoAPagar.Name = "txtMontoAPagar";
            this.txtMontoAPagar.Properties.DisplayFormat.FormatString = "n2";
            this.txtMontoAPagar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontoAPagar.Properties.EditFormat.FormatString = "n2";
            this.txtMontoAPagar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontoAPagar.Properties.Mask.EditMask = "n2";
            this.txtMontoAPagar.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontoAPagar.Properties.Mask.PlaceHolder = '0';
            this.txtMontoAPagar.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontoAPagar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMontoAPagar.Size = new System.Drawing.Size(71, 20);
            this.txtMontoAPagar.TabIndex = 0;
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
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Barra de estado";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Barra de estado";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Salir";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.Cancelar_16x16;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem2.Caption = "Aceptar";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(361, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 96);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(361, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 96);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(361, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 96);
            // 
            // ckManual
            // 
            this.ckManual.Location = new System.Drawing.Point(257, 34);
            this.ckManual.MenuManager = this.barManager1;
            this.ckManual.Name = "ckManual";
            this.ckManual.Properties.Caption = "Manual";
            this.ckManual.Size = new System.Drawing.Size(75, 19);
            this.ckManual.TabIndex = 4;
            this.ckManual.CheckedChanged += new System.EventHandler(this.ckManualChanged);
            // 
            // ckAutomatico
            // 
            this.ckAutomatico.Location = new System.Drawing.Point(257, 60);
            this.ckAutomatico.MenuManager = this.barManager1;
            this.ckAutomatico.Name = "ckAutomatico";
            this.ckAutomatico.Properties.Caption = "Automático";
            this.ckAutomatico.Size = new System.Drawing.Size(75, 19);
            this.ckAutomatico.TabIndex = 5;
            this.ckAutomatico.CheckedChanged += new System.EventHandler(this.ckAutomatico_CheckedChanged);
            // 
            // FrmMantePagoCuotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 124);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMantePagoCuotas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMantePagoCuotas";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoPagado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoAPagar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckManual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutomatico.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtMontoAPagar;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMontoPagado;
        private DevExpress.XtraEditors.CheckEdit ckAutomatico;
        private DevExpress.XtraEditors.CheckEdit ckManual;
    }
}