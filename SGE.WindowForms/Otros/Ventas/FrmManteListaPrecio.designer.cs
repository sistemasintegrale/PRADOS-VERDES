namespace SGE.WindowForms.Otros.bVentas
{
    partial class FrmManteListaPrecio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteListaPrecio));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.LkpTipoMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bteDProductoEspecifico = new DevExpress.XtraEditors.ButtonEdit();
            this.txtMontounitario = new DevExpress.XtraEditors.TextEdit();
            this.lblProducto = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteDProductoEspecifico.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontounitario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.LkpTipoMoneda);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.bteDProductoEspecifico);
            this.groupControl1.Controls.Add(this.txtMontounitario);
            this.groupControl1.Controls.Add(this.lblProducto);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(482, 95);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Producto con Precio";
            // 
            // LkpTipoMoneda
            // 
            this.LkpTipoMoneda.Enabled = false;
            this.LkpTipoMoneda.Location = new System.Drawing.Point(69, 62);
            this.LkpTipoMoneda.Name = "LkpTipoMoneda";
            this.LkpTipoMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpTipoMoneda.Properties.NullText = "";
            this.LkpTipoMoneda.Size = new System.Drawing.Size(120, 20);
            this.LkpTipoMoneda.TabIndex = 2;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(16, 65);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(42, 13);
            this.labelControl14.TabIndex = 0;
            this.labelControl14.Text = "Moneda:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(240, 65);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Monto Unitario :";
            // 
            // bteDProductoEspecifico
            // 
            this.bteDProductoEspecifico.Location = new System.Drawing.Point(69, 32);
            this.bteDProductoEspecifico.Name = "bteDProductoEspecifico";
            this.bteDProductoEspecifico.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteDProductoEspecifico.Size = new System.Drawing.Size(380, 20);
            this.bteDProductoEspecifico.TabIndex = 1;
            this.bteDProductoEspecifico.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteDProductoEspecifico_ButtonClick);
            // 
            // txtMontounitario
            // 
            this.txtMontounitario.EditValue = "";
            this.txtMontounitario.Enabled = false;
            this.txtMontounitario.Location = new System.Drawing.Point(329, 62);
            this.txtMontounitario.Name = "txtMontounitario";
            this.txtMontounitario.Properties.DisplayFormat.FormatString = "n2";
            this.txtMontounitario.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontounitario.Properties.EditFormat.FormatString = "n2";
            this.txtMontounitario.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontounitario.Properties.Mask.EditMask = "n2";
            this.txtMontounitario.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontounitario.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontounitario.Size = new System.Drawing.Size(120, 20);
            this.txtMontounitario.TabIndex = 3;
            this.txtMontounitario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtdescripcion_KeyUp);
            // 
            // lblProducto
            // 
            this.lblProducto.Location = new System.Drawing.Point(16, 35);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(47, 13);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";
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
            this.BtnGuardar,
            this.BtnCancelar});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnGuardar.Appearance.Options.UseFont = true;
            this.BtnGuardar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnGuardar.Caption = "Guardar";
            this.BtnGuardar.Glyph = ((System.Drawing.Image)(resources.GetObject("BtnGuardar.Glyph")));
            this.BtnGuardar.Hint = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnCancelar.Appearance.Options.UseFont = true;
            this.BtnCancelar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnCancelar.Caption = "Cancelar";
            this.BtnCancelar.Glyph = ((System.Drawing.Image)(resources.GetObject("BtnCancelar.Glyph")));
            this.BtnCancelar.Hint = "Cancelar";
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(482, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 95);
            this.barDockControlBottom.Size = new System.Drawing.Size(482, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 95);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(482, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 95);
            // 
            // FrmManteListaPrecioAutoventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 123);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteListaPrecioAutoventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Productos con Precio";
            this.Load += new System.EventHandler(this.FrmManteProductoPrecio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteDProductoEspecifico.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontounitario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtMontounitario;
        private DevExpress.XtraEditors.LabelControl lblProducto;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.ButtonEdit bteDProductoEspecifico;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit LkpTipoMoneda;
        private DevExpress.XtraEditors.LabelControl labelControl14;
    }
}