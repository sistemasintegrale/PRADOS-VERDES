namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    partial class frm04FondosPensiones 
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
            this.grdAlmacen = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NuevotoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ModificartoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.EliminartoolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.PorcentajeFtoolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.PorcentajeMtoolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.importarMesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAlmacen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PorcentajesFIjosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PorcentajesMixtosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lkpMes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtAño = new DevExpress.XtraEditors.TextEdit();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.importarDatosDeMesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlmacen)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewAlmacen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAño.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAlmacen
            // 
            this.grdAlmacen.ContextMenuStrip = this.mnu;
            this.grdAlmacen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAlmacen.Location = new System.Drawing.Point(0, 78);
            this.grdAlmacen.MainView = this.viewAlmacen;
            this.grdAlmacen.Name = "grdAlmacen";
            this.grdAlmacen.Size = new System.Drawing.Size(738, 378);
            this.grdAlmacen.TabIndex = 10;
            this.grdAlmacen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewAlmacen});
            this.grdAlmacen.DoubleClick += new System.EventHandler(this.grdAlmacen_DoubleClick);
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NuevotoolStripMenuItem1,
            this.ModificartoolStripMenuItem2,
            this.EliminartoolStripMenuItem3,
            this.PorcentajeFtoolStripMenuItem4,
            this.PorcentajeMtoolStripMenuItem5,
            this.importarMesToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(175, 136);
            // 
            // NuevotoolStripMenuItem1
            // 
            this.NuevotoolStripMenuItem1.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.NuevotoolStripMenuItem1.Name = "NuevotoolStripMenuItem1";
            this.NuevotoolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.NuevotoolStripMenuItem1.Text = "Nuevo";
            this.NuevotoolStripMenuItem1.Click += new System.EventHandler(this.NuevotoolStripMenuItem1_Click);
            // 
            // ModificartoolStripMenuItem2
            // 
            this.ModificartoolStripMenuItem2.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.ModificartoolStripMenuItem2.Name = "ModificartoolStripMenuItem2";
            this.ModificartoolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
            this.ModificartoolStripMenuItem2.Text = "Modificar";
            this.ModificartoolStripMenuItem2.Click += new System.EventHandler(this.ModificartoolStripMenuItem2_Click);
            // 
            // EliminartoolStripMenuItem3
            // 
            this.EliminartoolStripMenuItem3.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.EliminartoolStripMenuItem3.Name = "EliminartoolStripMenuItem3";
            this.EliminartoolStripMenuItem3.Size = new System.Drawing.Size(174, 22);
            this.EliminartoolStripMenuItem3.Text = "Eliminar";
            this.EliminartoolStripMenuItem3.Click += new System.EventHandler(this.EliminartoolStripMenuItem3_Click);
            // 
            // PorcentajeFtoolStripMenuItem4
            // 
            this.PorcentajeFtoolStripMenuItem4.Image = global::SGE.WindowForms.Properties.Resources.ctaxcobrar;
            this.PorcentajeFtoolStripMenuItem4.Name = "PorcentajeFtoolStripMenuItem4";
            this.PorcentajeFtoolStripMenuItem4.Size = new System.Drawing.Size(174, 22);
            this.PorcentajeFtoolStripMenuItem4.Text = "Porcentajes Fijos";
            this.PorcentajeFtoolStripMenuItem4.Click += new System.EventHandler(this.PorcentajeFtoolStripMenuItem4_Click);
            // 
            // PorcentajeMtoolStripMenuItem5
            // 
            this.PorcentajeMtoolStripMenuItem5.Image = global::SGE.WindowForms.Properties.Resources.ctaxpagar;
            this.PorcentajeMtoolStripMenuItem5.Name = "PorcentajeMtoolStripMenuItem5";
            this.PorcentajeMtoolStripMenuItem5.Size = new System.Drawing.Size(174, 22);
            this.PorcentajeMtoolStripMenuItem5.Text = "Porcentajes Mixtos";
            this.PorcentajeMtoolStripMenuItem5.Click += new System.EventHandler(this.PorcentajeMtoolStripMenuItem5_Click);
            // 
            // importarMesToolStripMenuItem
            // 
            this.importarMesToolStripMenuItem.Name = "importarMesToolStripMenuItem";
            this.importarMesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.importarMesToolStripMenuItem.Text = "Importar Mes";
            this.importarMesToolStripMenuItem.Click += new System.EventHandler(this.importarMesToolStripMenuItem_Click);
            // 
            // viewAlmacen
            // 
            this.viewAlmacen.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.viewAlmacen.GridControl = this.grdAlmacen;
            this.viewAlmacen.GroupPanelText = " ";
            this.viewAlmacen.Name = "viewAlmacen";
            this.viewAlmacen.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewAlmacen_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Código";
            this.gridColumn1.DisplayFormat.FormatString = "{0:00}";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "fdpc_iid_vcodigo_fondo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 74;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descripción";
            this.gridColumn2.FieldName = "fdpc_vdescripcion";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 247;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "% Fijo";
            this.gridColumn3.FieldName = "fdpc_nporcentaje_fijo";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "% Mixto";
            this.gridColumn4.FieldName = "fdpc_nporcentaje_mixto";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 97;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "AFP";
            this.gridColumn5.FieldName = "tablc_iid_tipo_fondo_pensiones";
            this.gridColumn5.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 202;
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // PorcentajesFIjosToolStripMenuItem
            // 
            this.PorcentajesFIjosToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.ctaxcobrar;
            this.PorcentajesFIjosToolStripMenuItem.Name = "PorcentajesFIjosToolStripMenuItem";
            this.PorcentajesFIjosToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.PorcentajesFIjosToolStripMenuItem.Text = "Porcentajes Fijos";
            // 
            // PorcentajesMixtosToolStripMenuItem1
            // 
            this.PorcentajesMixtosToolStripMenuItem1.Image = global::SGE.WindowForms.Properties.Resources.ctaxpagar;
            this.PorcentajesMixtosToolStripMenuItem1.Name = "PorcentajesMixtosToolStripMenuItem1";
            this.PorcentajesMixtosToolStripMenuItem1.Size = new System.Drawing.Size(194, 22);
            this.PorcentajesMixtosToolStripMenuItem1.Text = "Porcentajes Mixtos";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpMes);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtAño);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(738, 78);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de Búsqueda";
            // 
            // lkpMes
            // 
            this.lkpMes.Location = new System.Drawing.Point(691, 48);
            this.lkpMes.Name = "lkpMes";
            this.lkpMes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMes.Properties.NullText = "";
            this.lkpMes.Size = new System.Drawing.Size(20, 20);
            this.lkpMes.TabIndex = 24;
            this.lkpMes.Visible = false;
            this.lkpMes.EditValueChanged += new System.EventHandler(this.lkpMes_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(659, 50);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(26, 13);
            this.labelControl9.TabIndex = 25;
            this.labelControl9.Text = "Mes :";
            this.labelControl9.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(25, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(26, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Año :";
            // 
            // txtAño
            // 
            this.txtAño.Enabled = false;
            this.txtAño.Location = new System.Drawing.Point(57, 26);
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(133, 20);
            this.txtAño.TabIndex = 4;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(273, 53);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Size = new System.Drawing.Size(355, 20);
            this.txtDescripcion.TabIndex = 3;
            this.txtDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(206, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Descripción :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(57, 52);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
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
            this.btnNuevo,
            this.btnModificar,
            this.btnEliminar,
            this.btnImprimir});
            this.barManager1.MaxItemId = 5;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNuevo),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEliminar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImprimir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "[F7]";
            this.btnNuevo.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnNuevo.Id = 0;
            this.btnNuevo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "[F5]";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnModificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModificar_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "[F9]";
            this.btnEliminar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.btnEliminar.Id = 2;
            this.btnEliminar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "[F8]";
            this.btnImprimir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.btnImprimir.Id = 3;
            this.btnImprimir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(738, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 456);
            this.barDockControlBottom.Size = new System.Drawing.Size(738, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 456);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(738, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 456);
            // 
            // importarDatosDeMesToolStripMenuItem
            // 
            this.importarDatosDeMesToolStripMenuItem.Name = "importarDatosDeMesToolStripMenuItem";
            this.importarDatosDeMesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.importarDatosDeMesToolStripMenuItem.Text = "Importar Datos de Mes";
            // 
            // frm04FondosPensiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 483);
            this.Controls.Add(this.grdAlmacen);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm04FondosPensiones";
            this.Text = "Fondos de Pensiones ";
            this.Load += new System.EventHandler(this.frmAlamcen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAlmacen)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewAlmacen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAño.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PorcentajesFIjosToolStripMenuItem;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.ToolStripMenuItem PorcentajesMixtosToolStripMenuItem1;
        public DevExpress.XtraGrid.Views.Grid.GridView viewAlmacen;
        public DevExpress.XtraGrid.GridControl grdAlmacen;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtAño;
        public DevExpress.XtraEditors.LookUpEdit lkpMes;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.ToolStripMenuItem importarDatosDeMesToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem NuevotoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ModificartoolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem EliminartoolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem PorcentajeFtoolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem PorcentajeMtoolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem importarMesToolStripMenuItem;
    }
}