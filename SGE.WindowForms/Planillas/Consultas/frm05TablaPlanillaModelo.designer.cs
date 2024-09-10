namespace SGE.WindowForms.Planillas.Consultas
{
    partial class frm05TablaPlanillaModelo
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.grdplanilla = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PorcentajesFIjosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PorcentajesMixtosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarRentaDelPersonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPlanilla = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtUIT = new DevExpress.XtraEditors.TextEdit();
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lkpMes = new DevExpress.XtraEditors.LookUpEdit();
            this.btnPersonal = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.grdplanilla)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPlanilla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUIT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPersonal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdplanilla
            // 
            this.grdplanilla.ContextMenuStrip = this.mnu;
            this.grdplanilla.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.grdplanilla.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdplanilla.Location = new System.Drawing.Point(0, 78);
            this.grdplanilla.MainView = this.viewPlanilla;
            this.grdplanilla.Name = "grdplanilla";
            this.grdplanilla.Size = new System.Drawing.Size(1460, 379);
            this.grdplanilla.TabIndex = 10;
            this.grdplanilla.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewPlanilla});
            this.grdplanilla.Click += new System.EventHandler(this.grdplanilla_Click);
            this.grdplanilla.DoubleClick += new System.EventHandler(this.grdAlmacen_DoubleClick);
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.PorcentajesFIjosToolStripMenuItem,
            this.cambiarDatosToolStripMenuItem,
            this.PorcentajesMixtosToolStripMenuItem1,
            this.actualizarRentaDelPersonalToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(227, 158);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Visible = false;
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Visible = false;
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Visible = false;
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // PorcentajesFIjosToolStripMenuItem
            // 
            this.PorcentajesFIjosToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_gear;
            this.PorcentajesFIjosToolStripMenuItem.Name = "PorcentajesFIjosToolStripMenuItem";
            this.PorcentajesFIjosToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.PorcentajesFIjosToolStripMenuItem.Text = "Detalle de la Tabla";
            this.PorcentajesFIjosToolStripMenuItem.Visible = false;
            this.PorcentajesFIjosToolStripMenuItem.Click += new System.EventHandler(this.PorcentajesFIjosToolStripMenuItem_Click);
            // 
            // cambiarDatosToolStripMenuItem
            // 
            this.cambiarDatosToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.settings_;
            this.cambiarDatosToolStripMenuItem.Name = "cambiarDatosToolStripMenuItem";
            this.cambiarDatosToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.cambiarDatosToolStripMenuItem.Text = "Cambiar Datos";
            this.cambiarDatosToolStripMenuItem.Click += new System.EventHandler(this.cambiarDatosToolStripMenuItem_Click);
            // 
            // PorcentajesMixtosToolStripMenuItem1
            // 
            this.PorcentajesMixtosToolStripMenuItem1.Image = global::SGE.WindowForms.Properties.Resources.page_white_excel;
            this.PorcentajesMixtosToolStripMenuItem1.Name = "PorcentajesMixtosToolStripMenuItem1";
            this.PorcentajesMixtosToolStripMenuItem1.Size = new System.Drawing.Size(226, 22);
            this.PorcentajesMixtosToolStripMenuItem1.Text = "Exportar a Excel";
            this.PorcentajesMixtosToolStripMenuItem1.Click += new System.EventHandler(this.PorcentajesMixtosToolStripMenuItem1_Click);
            // 
            // actualizarRentaDelPersonalToolStripMenuItem
            // 
            this.actualizarRentaDelPersonalToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.arrow_circle_double;
            this.actualizarRentaDelPersonalToolStripMenuItem.Name = "actualizarRentaDelPersonalToolStripMenuItem";
            this.actualizarRentaDelPersonalToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.actualizarRentaDelPersonalToolStripMenuItem.Text = "Actualizar Renta del Personal";
            this.actualizarRentaDelPersonalToolStripMenuItem.Click += new System.EventHandler(this.actualizarRentaDelPersonalToolStripMenuItem_Click);
            // 
            // viewPlanilla
            // 
            this.viewPlanilla.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gr1,
            this.gr2,
            this.gr3,
            this.gr4,
            this.gr5,
            this.gr6,
            this.gr7,
            this.gr8,
            this.gr9,
            this.gr10,
            this.gr11,
            this.gr12,
            this.gridColumn3});
            this.viewPlanilla.GridControl = this.grdplanilla;
            this.viewPlanilla.GroupPanelText = " ";
            this.viewPlanilla.Name = "viewPlanilla";
            this.viewPlanilla.OptionsView.ColumnAutoWidth = false;
            this.viewPlanilla.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewAlmacen_FocusedRowChanged);
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
            this.gridColumn1.FieldName = "plcd_iid";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 50;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descripción";
            this.gridColumn2.FieldName = "plcd_vdescrpcion";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 300;
            // 
            // gr1
            // 
            this.gr1.Caption = "Enero";
            this.gr1.DisplayFormat.FormatString = "n2";
            this.gr1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr1.FieldName = "plcd_montos_enero";
            this.gr1.Name = "gr1";
            this.gr1.OptionsColumn.AllowEdit = false;
            this.gr1.OptionsColumn.AllowFocus = false;
            this.gr1.OptionsColumn.AllowIncrementalSearch = false;
            this.gr1.Visible = true;
            this.gr1.VisibleIndex = 3;
            this.gr1.Width = 90;
            // 
            // gr2
            // 
            this.gr2.Caption = "Febrero";
            this.gr2.DisplayFormat.FormatString = "n2";
            this.gr2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr2.FieldName = "plcd_montos_febrero";
            this.gr2.Name = "gr2";
            this.gr2.OptionsColumn.AllowEdit = false;
            this.gr2.OptionsColumn.AllowFocus = false;
            this.gr2.OptionsColumn.AllowIncrementalSearch = false;
            this.gr2.Visible = true;
            this.gr2.VisibleIndex = 4;
            this.gr2.Width = 90;
            // 
            // gr3
            // 
            this.gr3.Caption = "Marzo";
            this.gr3.DisplayFormat.FormatString = "n2";
            this.gr3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr3.FieldName = "plcd_montos_marzo";
            this.gr3.Name = "gr3";
            this.gr3.OptionsColumn.AllowEdit = false;
            this.gr3.OptionsColumn.AllowFocus = false;
            this.gr3.OptionsColumn.AllowIncrementalSearch = false;
            this.gr3.Visible = true;
            this.gr3.VisibleIndex = 5;
            this.gr3.Width = 90;
            // 
            // gr4
            // 
            this.gr4.Caption = "Abril";
            this.gr4.DisplayFormat.FormatString = "n2";
            this.gr4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr4.FieldName = "plcd_montos_abril";
            this.gr4.Name = "gr4";
            this.gr4.OptionsColumn.AllowEdit = false;
            this.gr4.OptionsColumn.AllowFocus = false;
            this.gr4.OptionsColumn.AllowIncrementalSearch = false;
            this.gr4.Visible = true;
            this.gr4.VisibleIndex = 6;
            this.gr4.Width = 90;
            // 
            // gr5
            // 
            this.gr5.Caption = "Mayo";
            this.gr5.DisplayFormat.FormatString = "n2";
            this.gr5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr5.FieldName = "plcd_montos_mayo";
            this.gr5.Name = "gr5";
            this.gr5.OptionsColumn.AllowEdit = false;
            this.gr5.OptionsColumn.AllowFocus = false;
            this.gr5.OptionsColumn.AllowIncrementalSearch = false;
            this.gr5.Visible = true;
            this.gr5.VisibleIndex = 7;
            this.gr5.Width = 90;
            // 
            // gr6
            // 
            this.gr6.Caption = "Junio";
            this.gr6.DisplayFormat.FormatString = "n2";
            this.gr6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr6.FieldName = "plcd_montos_junio";
            this.gr6.Name = "gr6";
            this.gr6.OptionsColumn.AllowEdit = false;
            this.gr6.OptionsColumn.AllowFocus = false;
            this.gr6.OptionsColumn.AllowIncrementalSearch = false;
            this.gr6.Visible = true;
            this.gr6.VisibleIndex = 8;
            this.gr6.Width = 90;
            // 
            // gr7
            // 
            this.gr7.Caption = "Julio";
            this.gr7.DisplayFormat.FormatString = "n2";
            this.gr7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr7.FieldName = "plcd_montos_julio";
            this.gr7.Name = "gr7";
            this.gr7.OptionsColumn.AllowEdit = false;
            this.gr7.OptionsColumn.AllowFocus = false;
            this.gr7.OptionsColumn.AllowIncrementalSearch = false;
            this.gr7.Visible = true;
            this.gr7.VisibleIndex = 9;
            this.gr7.Width = 90;
            // 
            // gr8
            // 
            this.gr8.Caption = "Agosto";
            this.gr8.DisplayFormat.FormatString = "n2";
            this.gr8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr8.FieldName = "plcd_montos_agosto";
            this.gr8.Name = "gr8";
            this.gr8.OptionsColumn.AllowEdit = false;
            this.gr8.OptionsColumn.AllowFocus = false;
            this.gr8.OptionsColumn.AllowIncrementalSearch = false;
            this.gr8.Visible = true;
            this.gr8.VisibleIndex = 10;
            this.gr8.Width = 90;
            // 
            // gr9
            // 
            this.gr9.Caption = "Setiembre";
            this.gr9.DisplayFormat.FormatString = "n2";
            this.gr9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr9.FieldName = "plcd_montos_setiembre";
            this.gr9.Name = "gr9";
            this.gr9.OptionsColumn.AllowEdit = false;
            this.gr9.OptionsColumn.AllowFocus = false;
            this.gr9.OptionsColumn.AllowIncrementalSearch = false;
            this.gr9.Visible = true;
            this.gr9.VisibleIndex = 11;
            this.gr9.Width = 90;
            // 
            // gr10
            // 
            this.gr10.Caption = "Octubre";
            this.gr10.DisplayFormat.FormatString = "n2";
            this.gr10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr10.FieldName = "plcd_montos_octubre";
            this.gr10.Name = "gr10";
            this.gr10.OptionsColumn.AllowEdit = false;
            this.gr10.OptionsColumn.AllowFocus = false;
            this.gr10.OptionsColumn.AllowIncrementalSearch = false;
            this.gr10.Visible = true;
            this.gr10.VisibleIndex = 12;
            this.gr10.Width = 90;
            // 
            // gr11
            // 
            this.gr11.Caption = "Noviembre";
            this.gr11.DisplayFormat.FormatString = "n2";
            this.gr11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr11.FieldName = "plcd_montos_noviembre";
            this.gr11.Name = "gr11";
            this.gr11.OptionsColumn.AllowEdit = false;
            this.gr11.OptionsColumn.AllowFocus = false;
            this.gr11.OptionsColumn.AllowIncrementalSearch = false;
            this.gr11.Visible = true;
            this.gr11.VisibleIndex = 13;
            this.gr11.Width = 90;
            // 
            // gr12
            // 
            this.gr12.Caption = "Diciembre";
            this.gr12.DisplayFormat.FormatString = "n2";
            this.gr12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gr12.FieldName = "plcd_montos_diciembre";
            this.gr12.Name = "gr12";
            this.gr12.OptionsColumn.AllowEdit = false;
            this.gr12.OptionsColumn.AllowFocus = false;
            this.gr12.OptionsColumn.AllowIncrementalSearch = false;
            this.gr12.Visible = true;
            this.gr12.VisibleIndex = 14;
            this.gr12.Width = 90;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Valores";
            this.gridColumn3.FieldName = "strValores";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 100;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtUIT);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.lkpMes);
            this.groupControl1.Controls.Add(this.btnPersonal);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1460, 78);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de Búsqueda";
            // 
            // txtUIT
            // 
            this.txtUIT.Location = new System.Drawing.Point(1123, 37);
            this.txtUIT.MenuManager = this.barManager1;
            this.txtUIT.Name = "txtUIT";
            this.txtUIT.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUIT.Properties.Appearance.Options.UseFont = true;
            this.txtUIT.Properties.ReadOnly = true;
            this.txtUIT.Size = new System.Drawing.Size(100, 20);
            this.txtUIT.TabIndex = 28;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1460, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 457);
            this.barDockControlBottom.Size = new System.Drawing.Size(1460, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 457);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1460, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 457);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(1082, 40);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 13);
            this.labelControl3.TabIndex = 27;
            this.labelControl3.Text = "UIT :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = global::SGE.WindowForms.Properties.Resources.Refresh;
            this.simpleButton1.Location = new System.Drawing.Point(616, 34);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 23);
            this.simpleButton1.TabIndex = 26;
            this.simpleButton1.Text = "Generar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lkpMes
            // 
            this.lkpMes.Location = new System.Drawing.Point(409, 37);
            this.lkpMes.Name = "lkpMes";
            this.lkpMes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMes.Properties.NullText = "";
            this.lkpMes.Size = new System.Drawing.Size(84, 20);
            this.lkpMes.TabIndex = 25;
            // 
            // btnPersonal
            // 
            this.btnPersonal.Location = new System.Drawing.Point(69, 37);
            this.btnPersonal.MenuManager = this.barManager1;
            this.btnPersonal.Name = "btnPersonal";
            this.btnPersonal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnPersonal.Properties.ReadOnly = true;
            this.btnPersonal.Size = new System.Drawing.Size(246, 20);
            this.btnPersonal.TabIndex = 3;
            this.btnPersonal.Click += new System.EventHandler(this.buttonEdit1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Personal : ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(377, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Mes :";
            // 
            // frm05TablaPlanillaModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1460, 483);
            this.Controls.Add(this.grdplanilla);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm05TablaPlanillaModelo";
            this.Text = "Parámetros Detalle  Planilla ";
            this.Load += new System.EventHandler(this.frmAlamcen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdplanilla)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewPlanilla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUIT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPersonal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdplanilla;
        private DevExpress.XtraGrid.Views.Grid.GridView viewPlanilla;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public System.Windows.Forms.ContextMenuStrip mnu;
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
        private System.Windows.Forms.ToolStripMenuItem PorcentajesMixtosToolStripMenuItem1;
        public DevExpress.XtraEditors.LookUpEdit lkpMes;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gr1;
        private DevExpress.XtraEditors.ButtonEdit btnPersonal;
        private System.Windows.Forms.SaveFileDialog sfd;
        private DevExpress.XtraGrid.Columns.GridColumn gr2;
        private DevExpress.XtraGrid.Columns.GridColumn gr3;
        private DevExpress.XtraGrid.Columns.GridColumn gr4;
        private DevExpress.XtraGrid.Columns.GridColumn gr5;
        private DevExpress.XtraGrid.Columns.GridColumn gr6;
        private DevExpress.XtraGrid.Columns.GridColumn gr7;
        private DevExpress.XtraGrid.Columns.GridColumn gr8;
        private DevExpress.XtraGrid.Columns.GridColumn gr9;
        private DevExpress.XtraGrid.Columns.GridColumn gr10;
        private DevExpress.XtraGrid.Columns.GridColumn gr11;
        private DevExpress.XtraGrid.Columns.GridColumn gr12;
        private System.Windows.Forms.ToolStripMenuItem cambiarDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizarRentaDelPersonalToolStripMenuItem;
        private DevExpress.XtraEditors.TextEdit txtUIT;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}