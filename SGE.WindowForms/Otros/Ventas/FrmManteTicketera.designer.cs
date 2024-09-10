namespace SGE.WindowForms.Otros.bVentas
{
    partial class FrmManteTicketera
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
            this.txtSerieImpresora = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSerie = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCorrelativo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtImpresora = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtDireccion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomLocal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieImpresora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpresora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomLocal.Properties)).BeginInit();
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
            this.barDockControlTop.Size = new System.Drawing.Size(397, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 132);
            this.barDockControlBottom.Size = new System.Drawing.Size(397, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 132);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(397, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 132);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(147, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "N° Serie Impresora :";
            // 
            // txtSerieImpresora
            // 
            this.txtSerieImpresora.EditValue = "";
            this.txtSerieImpresora.Enabled = false;
            this.txtSerieImpresora.Location = new System.Drawing.Point(251, 27);
            this.txtSerieImpresora.Name = "txtSerieImpresora";
            this.txtSerieImpresora.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSerieImpresora.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerieImpresora.Properties.MaxLength = 20;
            this.txtSerieImpresora.Size = new System.Drawing.Size(132, 20);
            this.txtSerieImpresora.TabIndex = 1;
            this.txtSerieImpresora.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCaja_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 103);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Serie:";
            // 
            // txtSerie
            // 
            this.txtSerie.EditValue = "";
            this.txtSerie.Enabled = false;
            this.txtSerie.Location = new System.Drawing.Point(56, 99);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSerie.Properties.Mask.EditMask = "0000";
            this.txtSerie.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSerie.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerie.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSerie.Properties.MaxLength = 4;
            this.txtSerie.Size = new System.Drawing.Size(30, 20);
            this.txtSerie.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(108, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Correlativo :";
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.EditValue = "";
            this.txtCorrelativo.Enabled = false;
            this.txtCorrelativo.Location = new System.Drawing.Point(174, 99);
            this.txtCorrelativo.Name = "txtCorrelativo";
            this.txtCorrelativo.Properties.LookAndFeel.SkinName = "Blue";
            this.txtCorrelativo.Properties.Mask.EditMask = "00000000";
            this.txtCorrelativo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCorrelativo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCorrelativo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCorrelativo.Properties.MaxLength = 8;
            this.txtCorrelativo.Size = new System.Drawing.Size(58, 20);
            this.txtCorrelativo.TabIndex = 15;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 30);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(56, 13);
            this.labelControl9.TabIndex = 80;
            this.labelControl9.Text = "Impresora :";
            // 
            // txtImpresora
            // 
            this.txtImpresora.EditValue = "00";
            this.txtImpresora.Location = new System.Drawing.Point(74, 27);
            this.txtImpresora.Name = "txtImpresora";
            this.txtImpresora.Properties.DisplayFormat.FormatString = "d3";
            this.txtImpresora.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtImpresora.Properties.EditFormat.FormatString = "d3";
            this.txtImpresora.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtImpresora.Properties.LookAndFeel.SkinName = "Blue";
            this.txtImpresora.Properties.Mask.EditMask = "d3";
            this.txtImpresora.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtImpresora.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtImpresora.Properties.MaxLength = 3;
            this.txtImpresora.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtImpresora.Size = new System.Drawing.Size(28, 20);
            this.txtImpresora.TabIndex = 81;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtDireccion);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.txtNomLocal);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtImpresora);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtCorrelativo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtSerie);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtSerieImpresora);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(397, 132);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Caja";
            // 
            // txtDireccion
            // 
            this.txtDireccion.EditValue = "";
            this.txtDireccion.Location = new System.Drawing.Point(92, 77);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Properties.LookAndFeel.SkinName = "Blue";
            this.txtDireccion.Properties.Mask.ShowPlaceHolders = false;
            this.txtDireccion.Properties.MaxLength = 80;
            this.txtDireccion.Size = new System.Drawing.Size(291, 20);
            this.txtDireccion.TabIndex = 85;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(15, 80);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(53, 13);
            this.labelControl10.TabIndex = 84;
            this.labelControl10.Text = "Direccion : ";
            // 
            // txtNomLocal
            // 
            this.txtNomLocal.EditValue = "";
            this.txtNomLocal.Location = new System.Drawing.Point(92, 53);
            this.txtNomLocal.Name = "txtNomLocal";
            this.txtNomLocal.Properties.LookAndFeel.SkinName = "Blue";
            this.txtNomLocal.Properties.Mask.ShowPlaceHolders = false;
            this.txtNomLocal.Properties.MaxLength = 20;
            this.txtNomLocal.Size = new System.Drawing.Size(248, 20);
            this.txtNomLocal.TabIndex = 83;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(15, 56);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(71, 13);
            this.labelControl8.TabIndex = 82;
            this.labelControl8.Text = "Nombre Local :";
            // 
            // FrmManteTicketera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 160);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteTicketera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Ticketera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteCaja_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteCaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerieImpresora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpresora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomLocal.Properties)).EndInit();
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
        public DevExpress.XtraEditors.TextEdit txtImpresora;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.TextEdit txtCorrelativo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtSerie;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtSerieImpresora;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtDireccion;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit txtNomLocal;
        private DevExpress.XtraEditors.LabelControl labelControl8;

    }
}