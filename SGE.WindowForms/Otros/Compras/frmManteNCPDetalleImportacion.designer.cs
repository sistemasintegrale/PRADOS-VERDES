﻿namespace SGE.WindowForms.Otros.Compras
{
    partial class frmManteNCPDetalleImportacion
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
            this.lkpTipoProd = new DevExpress.XtraEditors.LookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtMoneda = new DevExpress.XtraEditors.TextEdit();
            this.txtPrecio = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtUnidadMedida = new DevExpress.XtraEditors.TextEdit();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.bteProducto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtItem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoProd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidadMedida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpTipoProd);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtMoneda);
            this.groupControl1.Controls.Add(this.txtPrecio);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtUnidadMedida);
            this.groupControl1.Controls.Add(this.txtCantidad);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.bteProducto);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtItem);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(493, 108);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos";
            // 
            // lkpTipoProd
            // 
            this.lkpTipoProd.Location = new System.Drawing.Point(127, 25);
            this.lkpTipoProd.MenuManager = this.barManager1;
            this.lkpTipoProd.Name = "lkpTipoProd";
            this.lkpTipoProd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoProd.Properties.NullText = "";
            this.lkpTipoProd.Size = new System.Drawing.Size(161, 20);
            this.lkpTipoProd.TabIndex = 24;
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
            this.btnAceptar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnAceptar.Id = 0;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
            // 
            // btnCancelar
            // 
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
            this.barDockControlTop.Size = new System.Drawing.Size(493, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 108);
            this.barDockControlBottom.Size = new System.Drawing.Size(493, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 108);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(493, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 108);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(97, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 13);
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "Tipo:";
            // 
            // txtMoneda
            // 
            this.txtMoneda.Enabled = false;
            this.txtMoneda.Location = new System.Drawing.Point(222, 79);
            this.txtMoneda.MenuManager = this.barManager1;
            this.txtMoneda.Name = "txtMoneda";
            this.txtMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMoneda.Size = new System.Drawing.Size(93, 20);
            this.txtMoneda.TabIndex = 22;
            // 
            // txtPrecio
            // 
            this.txtPrecio.EditValue = "0";
            this.txtPrecio.Location = new System.Drawing.Point(401, 80);
            this.txtPrecio.MenuManager = this.barManager1;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPrecio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPrecio.Properties.Mask.EditMask = "n5";
            this.txtPrecio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPrecio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPrecio.Size = new System.Drawing.Size(87, 20);
            this.txtPrecio.TabIndex = 21;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(322, 83);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(54, 13);
            this.labelControl6.TabIndex = 20;
            this.labelControl6.Text = "P. Unitario:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(188, 81);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 13);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Mon.:";
            // 
            // txtUnidadMedida
            // 
            this.txtUnidadMedida.Enabled = false;
            this.txtUnidadMedida.Location = new System.Drawing.Point(107, 78);
            this.txtUnidadMedida.MenuManager = this.barManager1;
            this.txtUnidadMedida.Name = "txtUnidadMedida";
            this.txtUnidadMedida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtUnidadMedida.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtUnidadMedida.Size = new System.Drawing.Size(74, 20);
            this.txtUnidadMedida.TabIndex = 19;
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = "0";
            this.txtCantidad.Location = new System.Drawing.Point(58, 78);
            this.txtCantidad.MenuManager = this.barManager1;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCantidad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidad.Properties.Mask.EditMask = "n2";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidad.Size = new System.Drawing.Size(43, 20);
            this.txtCantidad.TabIndex = 18;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 82);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Cantidad:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(151, 51);
            this.txtDescripcion.MenuManager = this.barManager1;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(337, 20);
            this.txtDescripcion.TabIndex = 5;
            // 
            // bteProducto
            // 
            this.bteProducto.Location = new System.Drawing.Point(58, 51);
            this.bteProducto.MenuManager = this.barManager1;
            this.bteProducto.Name = "bteProducto";
            this.bteProducto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteProducto.Size = new System.Drawing.Size(87, 20);
            this.bteProducto.TabIndex = 3;
            this.bteProducto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteProducto_ButtonClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Producto:";
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Location = new System.Drawing.Point(37, 25);
            this.txtItem.MenuManager = this.barManager1;
            this.txtItem.Name = "txtItem";
            this.txtItem.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtItem.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtItem.Size = new System.Drawing.Size(54, 20);
            this.txtItem.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Item:";
            // 
            // frmManteNCPDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 135);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximumSize = new System.Drawing.Size(509, 173);
            this.MinimumSize = new System.Drawing.Size(509, 173);
            this.Name = "frmManteNCPDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota de Crédito - Detalle";
            this.Load += new System.EventHandler(this.frmManteNCPDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoProd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidadMedida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.ButtonEdit bteProducto;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtMoneda;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.TextEdit txtPrecio;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtUnidadMedida;
        private DevExpress.XtraEditors.TextEdit txtCantidad;
        public DevExpress.XtraEditors.TextEdit txtItem;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoProd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}