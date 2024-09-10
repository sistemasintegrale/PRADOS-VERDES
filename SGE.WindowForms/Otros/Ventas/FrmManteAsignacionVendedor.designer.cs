namespace SGE.WindowForms.Otros.bVentas
{
    partial class FrmManteAsignacionVendedor
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
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.TxtCodCaja = new DevExpress.XtraEditors.TextEdit();
            this.lkpPuntoVenta = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.lkpVendedor = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtClave = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTurno = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCodCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpVendedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTurno.Properties)).BeginInit();
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
            this.barDockControlTop.Size = new System.Drawing.Size(330, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 156);
            this.barDockControlBottom.Size = new System.Drawing.Size(330, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 156);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(330, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 156);
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
            this.TxtCodCaja.Enabled = false;
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
            this.lkpPuntoVenta.Enabled = false;
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
            this.groupControl1.Controls.Add(this.chkMostrar);
            this.groupControl1.Controls.Add(this.lkpVendedor);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtClave);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.lkpTurno);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lkpPuntoVenta);
            this.groupControl1.Controls.Add(this.TxtCodCaja);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtCaja);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(330, 156);
            this.groupControl1.TabIndex = 2;
            // 
            // chkMostrar
            // 
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.Location = new System.Drawing.Point(215, 131);
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.Size = new System.Drawing.Size(63, 17);
            this.chkMostrar.TabIndex = 89;
            this.chkMostrar.Text = "Mostrar";
            this.chkMostrar.UseVisualStyleBackColor = true;
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);
            // 
            // lkpVendedor
            // 
            this.lkpVendedor.Location = new System.Drawing.Point(73, 100);
            this.lkpVendedor.Name = "lkpVendedor";
            this.lkpVendedor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpVendedor.Properties.LookAndFeel.SkinName = "Blue";
            this.lkpVendedor.Properties.NullText = "";
            this.lkpVendedor.Size = new System.Drawing.Size(205, 20);
            this.lkpVendedor.TabIndex = 88;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 103);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 13);
            this.labelControl4.TabIndex = 87;
            this.labelControl4.Text = "Vendedor : ";
            // 
            // txtClave
            // 
            this.txtClave.EditValue = "";
            this.txtClave.Location = new System.Drawing.Point(73, 129);
            this.txtClave.Name = "txtClave";
            this.txtClave.Properties.LookAndFeel.SkinName = "Blue";
            this.txtClave.Properties.Mask.ShowPlaceHolders = false;
            this.txtClave.Properties.MaxLength = 20;
            this.txtClave.Properties.UseSystemPasswordChar = true;
            this.txtClave.Size = new System.Drawing.Size(132, 20);
            this.txtClave.TabIndex = 86;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 132);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 13);
            this.labelControl3.TabIndex = 85;
            this.labelControl3.Text = "Clave :";
            // 
            // lkpTurno
            // 
            this.lkpTurno.Location = new System.Drawing.Point(60, 75);
            this.lkpTurno.Name = "lkpTurno";
            this.lkpTurno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTurno.Properties.LookAndFeel.SkinName = "Blue";
            this.lkpTurno.Properties.NullText = "";
            this.lkpTurno.Size = new System.Drawing.Size(126, 20);
            this.lkpTurno.TabIndex = 84;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 13);
            this.labelControl2.TabIndex = 83;
            this.labelControl2.Text = "Turno :";
            // 
            // FrmManteAsignacionVendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 184);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteAsignacionVendedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignacion de Vendedores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteCaja_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteCaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCodCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpVendedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTurno.Properties)).EndInit();
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
        public DevExpress.XtraEditors.TextEdit txtCaja;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpTurno;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtClave;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.LookUpEdit lkpVendedor;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.CheckBox chkMostrar;

    }
}