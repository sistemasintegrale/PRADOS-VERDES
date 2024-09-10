﻿namespace SGE.WindowForms.Planillas.Procesos_de_Planilla
{
    partial class frm13Vacaciones 
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
            this.grdFamilia = new DevExpress.XtraGrid.GridControl();
            this.mnuFamilia = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impFamiliaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impSubfamiliaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFamilia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cbActivarFiltro = new System.Windows.Forms.CheckBox();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.grdSubFamilia = new DevExpress.XtraGrid.GridControl();
            this.mnuSubFamilia = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSubFamilia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdFamilia)).BeginInit();
            this.mnuFamilia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewFamilia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubFamilia)).BeginInit();
            this.mnuSubFamilia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewSubFamilia)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFamilia
            // 
            this.grdFamilia.ContextMenuStrip = this.mnuFamilia;
            this.grdFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdFamilia.Location = new System.Drawing.Point(0, 78);
            this.grdFamilia.MainView = this.viewFamilia;
            this.grdFamilia.Name = "grdFamilia";
            this.grdFamilia.Size = new System.Drawing.Size(738, 197);
            this.grdFamilia.TabIndex = 10;
            this.grdFamilia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewFamilia});
            // 
            // mnuFamilia
            // 
            this.mnuFamilia.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuFamilia.Name = "contextMenuStrip1";
            this.mnuFamilia.Size = new System.Drawing.Size(165, 92);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo - Línea";
            this.nuevoToolStripMenuItem.Visible = false;
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.modificarToolStripMenuItem.Text = "Modificar - Línea";
            this.modificarToolStripMenuItem.Visible = false;
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar - Línea";
            this.eliminarToolStripMenuItem.Visible = false;
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.impFamiliaToolStripMenuItem,
            this.impSubfamiliaToolStripMenuItem});
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Visible = false;
            // 
            // impFamiliaToolStripMenuItem
            // 
            this.impFamiliaToolStripMenuItem.Name = "impFamiliaToolStripMenuItem";
            this.impFamiliaToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.impFamiliaToolStripMenuItem.Text = "Imp. familia";
            this.impFamiliaToolStripMenuItem.Click += new System.EventHandler(this.impFamiliaToolStripMenuItem_Click);
            // 
            // impSubfamiliaToolStripMenuItem
            // 
            this.impSubfamiliaToolStripMenuItem.Name = "impSubfamiliaToolStripMenuItem";
            this.impSubfamiliaToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.impSubfamiliaToolStripMenuItem.Text = "Imp. subfamilia";
            this.impSubfamiliaToolStripMenuItem.Click += new System.EventHandler(this.impSubfamiliaToolStripMenuItem_Click);
            // 
            // viewFamilia
            // 
            this.viewFamilia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn7});
            this.viewFamilia.GridControl = this.grdFamilia;
            this.viewFamilia.GroupPanelText = "Personal";
            this.viewFamilia.Name = "viewFamilia";
            this.viewFamilia.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewFamilia_FocusedRowChanged);
            this.viewFamilia.FocusedRowLoaded += new DevExpress.XtraGrid.Views.Base.RowEventHandler(this.viewFamilia_FocusedRowLoaded);
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Código";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.SummaryItem.FieldName = "perc_iid_personal";
            this.gridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridColumn2.Width = 65;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Apellidos y Nombres";
            this.gridColumn1.FieldName = "ApellNomb";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 226;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Documento";
            this.gridColumn3.FieldName = "perc_vnum_doc";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 102;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Cargo";
            this.gridColumn4.FieldName = "strCargo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 102;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Area";
            this.gridColumn7.FieldName = "strArea";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 118;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cbActivarFiltro);
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
            // cbActivarFiltro
            // 
            this.cbActivarFiltro.AutoSize = true;
            this.cbActivarFiltro.Location = new System.Drawing.Point(440, 51);
            this.cbActivarFiltro.Name = "cbActivarFiltro";
            this.cbActivarFiltro.Size = new System.Drawing.Size(92, 17);
            this.cbActivarFiltro.TabIndex = 27;
            this.cbActivarFiltro.Text = "Activar Filtros";
            this.cbActivarFiltro.UseVisualStyleBackColor = true;
            this.cbActivarFiltro.CheckedChanged += new System.EventHandler(this.cbActivarFiltro_CheckedChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(79, 49);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(355, 20);
            this.txtDescripcion.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Descripción :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(79, 26);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
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
            this.btnEliminar});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEliminar)});
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
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "[F5]";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "[F9]";
            this.btnEliminar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.btnEliminar.Id = 2;
            this.btnEliminar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
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
            // grdSubFamilia
            // 
            this.grdSubFamilia.ContextMenuStrip = this.mnuSubFamilia;
            this.grdSubFamilia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSubFamilia.Location = new System.Drawing.Point(0, 275);
            this.grdSubFamilia.MainView = this.viewSubFamilia;
            this.grdSubFamilia.MenuManager = this.barManager1;
            this.grdSubFamilia.Name = "grdSubFamilia";
            this.grdSubFamilia.Size = new System.Drawing.Size(738, 181);
            this.grdSubFamilia.TabIndex = 15;
            this.grdSubFamilia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewSubFamilia});
            // 
            // mnuSubFamilia
            // 
            this.mnuSubFamilia.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.mnuSubFamilia.Name = "contextMenuStrip1";
            this.mnuSubFamilia.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Nuevo";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Modificar";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "Eliminar";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // viewSubFamilia
            // 
            this.viewSubFamilia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn10,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn5});
            this.viewSubFamilia.GridControl = this.grdSubFamilia;
            this.viewSubFamilia.GroupPanelText = "Vacaciones";
            this.viewSubFamilia.Name = "viewSubFamilia";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Orden";
            this.gridColumn6.FieldName = "vacd_iid_vacaciones";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 65;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Mes";
            this.gridColumn10.FieldName = "strMes";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 165;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Fecha Inicio";
            this.gridColumn8.FieldName = "vacd_sfecha_ini";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 151;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Fecha Fin";
            this.gridColumn9.FieldName = "vacd_sfecha_fin";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 151;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Días";
            this.gridColumn5.FieldName = "vacd_ndias";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 81;
            // 
            // frm13Vacaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 483);
            this.Controls.Add(this.grdSubFamilia);
            this.Controls.Add(this.grdFamilia);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm13Vacaciones";
            this.Text = "Registro de Vacaciones";
            this.Load += new System.EventHandler(this.frmAlamcen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdFamilia)).EndInit();
            this.mnuFamilia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewFamilia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubFamilia)).EndInit();
            this.mnuSubFamilia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewSubFamilia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdFamilia;
        private DevExpress.XtraGrid.Views.Grid.GridView viewFamilia;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        public System.Windows.Forms.ContextMenuStrip mnuFamilia;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraGrid.GridControl grdSubFamilia;
        private DevExpress.XtraGrid.Views.Grid.GridView viewSubFamilia;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        public System.Windows.Forms.ContextMenuStrip mnuSubFamilia;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem impFamiliaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impSubfamiliaToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbActivarFiltro;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}