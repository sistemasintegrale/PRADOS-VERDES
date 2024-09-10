namespace SGE.WindowForms.Otros.bVentas
{
    partial class frmManteFacturaDetalle
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
            this.txtPorIGV = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtDPorImpArroz = new DevExpress.XtraEditors.TextEdit();
            this.labelControl42 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipoVenta = new DevExpress.XtraEditors.LookUpEdit();
            this.txtprecio = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtcaracteristicas = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtpartNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtMoneda = new DevExpress.XtraEditors.TextEdit();
            this.txtDescuento = new DevExpress.XtraEditors.TextEdit();
            this.lblDescuento = new DevExpress.XtraEditors.LabelControl();
            this.bteAlmacen = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtPrecioVenta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtUnidadMedida = new DevExpress.XtraEditors.TextEdit();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.txtProducto = new DevExpress.XtraEditors.TextEdit();
            this.bteProducto = new DevExpress.XtraEditors.ButtonEdit();
            this.txtItem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorIGV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDPorImpArroz.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprecio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcaracteristicas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpartNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescuento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAlmacen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecioVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidadMedida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).BeginInit();
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
            this.barDockControlTop.Size = new System.Drawing.Size(492, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 241);
            this.barDockControlBottom.Size = new System.Drawing.Size(492, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 241);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(492, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 241);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtPorIGV);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.txtDPorImpArroz);
            this.groupControl1.Controls.Add(this.labelControl42);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.lkpTipoVenta);
            this.groupControl1.Controls.Add(this.txtprecio);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtcaracteristicas);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtpartNumber);
            this.groupControl1.Controls.Add(this.txtMoneda);
            this.groupControl1.Controls.Add(this.txtDescuento);
            this.groupControl1.Controls.Add(this.lblDescuento);
            this.groupControl1.Controls.Add(this.bteAlmacen);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtPrecioVenta);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtUnidadMedida);
            this.groupControl1.Controls.Add(this.txtCantidad);
            this.groupControl1.Controls.Add(this.txtProducto);
            this.groupControl1.Controls.Add(this.bteProducto);
            this.groupControl1.Controls.Add(this.txtItem);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(492, 241);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos Detalle";
            this.groupControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.groupControl1_MouseMove);
            // 
            // txtPorIGV
            // 
            this.txtPorIGV.EditValue = "0.00";
            this.txtPorIGV.Enabled = false;
            this.txtPorIGV.Location = new System.Drawing.Point(430, 52);
            this.txtPorIGV.Name = "txtPorIGV";
            this.txtPorIGV.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPorIGV.Properties.Appearance.Options.UseBackColor = true;
            this.txtPorIGV.Properties.DisplayFormat.FormatString = "n2";
            this.txtPorIGV.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPorIGV.Properties.EditFormat.FormatString = "n2";
            this.txtPorIGV.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPorIGV.Properties.Mask.EditMask = "n2";
            this.txtPorIGV.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPorIGV.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPorIGV.Size = new System.Drawing.Size(55, 20);
            this.txtPorIGV.TabIndex = 41;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(383, 55);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(35, 13);
            this.labelControl12.TabIndex = 40;
            this.labelControl12.Text = "% IGV:";
            // 
            // txtDPorImpArroz
            // 
            this.txtDPorImpArroz.EditValue = "0.00";
            this.txtDPorImpArroz.Enabled = false;
            this.txtDPorImpArroz.Location = new System.Drawing.Point(319, 52);
            this.txtDPorImpArroz.Name = "txtDPorImpArroz";
            this.txtDPorImpArroz.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDPorImpArroz.Properties.Appearance.Options.UseBackColor = true;
            this.txtDPorImpArroz.Properties.DisplayFormat.FormatString = "n2";
            this.txtDPorImpArroz.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDPorImpArroz.Properties.EditFormat.FormatString = "n2";
            this.txtDPorImpArroz.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDPorImpArroz.Properties.Mask.EditMask = "n2";
            this.txtDPorImpArroz.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDPorImpArroz.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDPorImpArroz.Size = new System.Drawing.Size(55, 20);
            this.txtDPorImpArroz.TabIndex = 39;
            this.txtDPorImpArroz.Leave += new System.EventHandler(this.txtDPorImpArroz_Leave);
            // 
            // labelControl42
            // 
            this.labelControl42.Location = new System.Drawing.Point(272, 55);
            this.labelControl42.Name = "labelControl42";
            this.labelControl42.Size = new System.Drawing.Size(41, 13);
            this.labelControl42.TabIndex = 38;
            this.labelControl42.Text = "% IVAP:";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(5, 150);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(75, 13);
            this.labelControl11.TabIndex = 37;
            this.labelControl11.Text = "Caracteristicas:";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(340, 32);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(38, 13);
            this.labelControl10.TabIndex = 36;
            this.labelControl10.Text = "T.Venta";
            // 
            // lkpTipoVenta
            // 
            this.lkpTipoVenta.Location = new System.Drawing.Point(384, 29);
            this.lkpTipoVenta.Name = "lkpTipoVenta";
            this.lkpTipoVenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipoVenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipoVenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoVenta.Properties.NullText = "";
            this.lkpTipoVenta.Size = new System.Drawing.Size(101, 20);
            this.lkpTipoVenta.TabIndex = 35;
            this.lkpTipoVenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpTipoVenta_KeyDown);
            // 
            // txtprecio
            // 
            this.txtprecio.EditValue = "0";
            this.txtprecio.Location = new System.Drawing.Point(73, 121);
            this.txtprecio.MenuManager = this.barManager1;
            this.txtprecio.Name = "txtprecio";
            this.txtprecio.Properties.Mask.EditMask = "n2";
            this.txtprecio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtprecio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtprecio.Size = new System.Drawing.Size(72, 20);
            this.txtprecio.TabIndex = 20;
            this.txtprecio.EditValueChanged += new System.EventHandler(this.txtprecio_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(36, 124);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(33, 13);
            this.labelControl9.TabIndex = 19;
            this.labelControl9.Text = "Precio:";
            // 
            // txtcaracteristicas
            // 
            this.txtcaracteristicas.Location = new System.Drawing.Point(86, 150);
            this.txtcaracteristicas.MenuManager = this.barManager1;
            this.txtcaracteristicas.Name = "txtcaracteristicas";
            this.txtcaracteristicas.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtcaracteristicas.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtcaracteristicas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcaracteristicas.Size = new System.Drawing.Size(378, 84);
            this.txtcaracteristicas.TabIndex = 10;
            this.txtcaracteristicas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtObservaciones_MouseMove);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(223, 393);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(64, 13);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "Part Number:";
            this.labelControl8.Visible = false;
            // 
            // txtpartNumber
            // 
            this.txtpartNumber.Enabled = false;
            this.txtpartNumber.Location = new System.Drawing.Point(289, 390);
            this.txtpartNumber.MenuManager = this.barManager1;
            this.txtpartNumber.Name = "txtpartNumber";
            this.txtpartNumber.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtpartNumber.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtpartNumber.Properties.ReadOnly = true;
            this.txtpartNumber.Size = new System.Drawing.Size(151, 20);
            this.txtpartNumber.TabIndex = 9;
            this.txtpartNumber.Visible = false;
            this.txtpartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // txtMoneda
            // 
            this.txtMoneda.Enabled = false;
            this.txtMoneda.Location = new System.Drawing.Point(312, 97);
            this.txtMoneda.MenuManager = this.barManager1;
            this.txtMoneda.Name = "txtMoneda";
            this.txtMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMoneda.Size = new System.Drawing.Size(97, 20);
            this.txtMoneda.TabIndex = 7;
            this.txtMoneda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // txtDescuento
            // 
            this.txtDescuento.EditValue = "0";
            this.txtDescuento.Location = new System.Drawing.Point(254, 121);
            this.txtDescuento.MenuManager = this.barManager1;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Properties.Mask.EditMask = "n2";
            this.txtDescuento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDescuento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDescuento.Size = new System.Drawing.Size(72, 20);
            this.txtDescuento.TabIndex = 3;
            this.txtDescuento.EditValueChanged += new System.EventHandler(this.txtDescuento_EditValueChanged);
            this.txtDescuento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // lblDescuento
            // 
            this.lblDescuento.Location = new System.Drawing.Point(168, 124);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(69, 13);
            this.lblDescuento.TabIndex = 15;
            this.lblDescuento.Text = "Descuento %:";
            // 
            // bteAlmacen
            // 
            this.bteAlmacen.Location = new System.Drawing.Point(204, 29);
            this.bteAlmacen.MenuManager = this.barManager1;
            this.bteAlmacen.Name = "bteAlmacen";
            this.bteAlmacen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteAlmacen.Properties.ReadOnly = true;
            this.bteAlmacen.Size = new System.Drawing.Size(122, 20);
            this.bteAlmacen.TabIndex = 1;
            this.bteAlmacen.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteAlmacen_ButtonClick);
            this.bteAlmacen.EditValueChanged += new System.EventHandler(this.bteAlmacen_EditValueChanged);
            this.bteAlmacen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(154, 32);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(44, 13);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "Almacén:";
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.EditValue = "0";
            this.txtPrecioVenta.Location = new System.Drawing.Point(409, 121);
            this.txtPrecioVenta.MenuManager = this.barManager1;
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPrecioVenta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPrecioVenta.Properties.Mask.EditMask = "n2";
            this.txtPrecioVenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPrecioVenta.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPrecioVenta.Size = new System.Drawing.Size(55, 20);
            this.txtPrecioVenta.TabIndex = 8;
            this.txtPrecioVenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(351, 124);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(45, 13);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "P. Venta:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(278, 100);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Mon.:";
            // 
            // txtUnidadMedida
            // 
            this.txtUnidadMedida.Enabled = false;
            this.txtUnidadMedida.Location = new System.Drawing.Point(159, 97);
            this.txtUnidadMedida.MenuManager = this.barManager1;
            this.txtUnidadMedida.Name = "txtUnidadMedida";
            this.txtUnidadMedida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtUnidadMedida.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtUnidadMedida.Size = new System.Drawing.Size(65, 20);
            this.txtUnidadMedida.TabIndex = 6;
            this.txtUnidadMedida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = "0";
            this.txtCantidad.Location = new System.Drawing.Point(73, 97);
            this.txtCantidad.MenuManager = this.barManager1;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCantidad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidad.Properties.Mask.EditMask = "n4";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidad.Size = new System.Drawing.Size(70, 20);
            this.txtCantidad.TabIndex = 5;
            this.txtCantidad.EditValueChanged += new System.EventHandler(this.txtCantidad_EditValueChanged);
            this.txtCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // txtProducto
            // 
            this.txtProducto.Enabled = false;
            this.txtProducto.Location = new System.Drawing.Point(73, 75);
            this.txtProducto.MenuManager = this.barManager1;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtProducto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtProducto.Size = new System.Drawing.Size(407, 20);
            this.txtProducto.TabIndex = 4;
            this.txtProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // bteProducto
            // 
            this.bteProducto.Location = new System.Drawing.Point(73, 52);
            this.bteProducto.MenuManager = this.barManager1;
            this.bteProducto.Name = "bteProducto";
            this.bteProducto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteProducto.Size = new System.Drawing.Size(187, 20);
            this.bteProducto.TabIndex = 2;
            this.bteProducto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteProducto_ButtonClick);
            this.bteProducto.EditValueChanged += new System.EventHandler(this.bteProducto_EditValueChanged);
            this.bteProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Location = new System.Drawing.Point(73, 29);
            this.txtItem.MenuManager = this.barManager1;
            this.txtItem.Name = "txtItem";
            this.txtItem.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtItem.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtItem.Size = new System.Drawing.Size(70, 20);
            this.txtItem.TabIndex = 0;
            this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(22, 100);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Cantidad:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Descripción:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Producto:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(43, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Item:";
            // 
            // frmManteFacturaDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 268);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(468, 215);
            this.Name = "frmManteFacturaDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Factura de Venta Det.";
            this.Load += new System.EventHandler(this.frmManteFacturaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorIGV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDPorImpArroz.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprecio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcaracteristicas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpartNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescuento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAlmacen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecioVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidadMedida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ButtonEdit bteAlmacen;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtPrecioVenta;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtUnidadMedida;
        private DevExpress.XtraEditors.TextEdit txtCantidad;
        private DevExpress.XtraEditors.TextEdit txtProducto;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        public DevExpress.XtraEditors.TextEdit txtItem;
        public DevExpress.XtraEditors.TextEdit txtMoneda;
        public DevExpress.XtraEditors.TextEdit txtDescuento;
        public DevExpress.XtraEditors.LabelControl lblDescuento;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.TextEdit txtprecio;
        public DevExpress.XtraEditors.ButtonEdit bteProducto;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.LookUpEdit lkpTipoVenta;
        private DevExpress.XtraEditors.MemoEdit txtcaracteristicas;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtpartNumber;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        public DevExpress.XtraEditors.TextEdit txtDPorImpArroz;
        private DevExpress.XtraEditors.LabelControl labelControl42;
        public DevExpress.XtraEditors.TextEdit txtPorIGV;
        private DevExpress.XtraEditors.LabelControl labelControl12;
    }
}