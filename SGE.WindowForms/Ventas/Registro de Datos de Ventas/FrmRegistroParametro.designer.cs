﻿namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    partial class FrmRegistroParametro
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
            this.txtGiro = new DevExpress.XtraEditors.TextEdit();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dgr = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGiro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.brnagregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnmosdificar = new DevExpress.XtraBars.BarButtonItem();
            this.btneliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnimprimir = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnAnular = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtGiro);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(784, 67);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código:";
            // 
            // txtGiro
            // 
            this.txtGiro.Location = new System.Drawing.Point(288, 31);
            this.txtGiro.Name = "txtGiro";
            this.txtGiro.Size = new System.Drawing.Size(133, 20);
            this.txtGiro.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(55, 31);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(224, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Descripción:";
            // 
            // dgr
            // 
            this.dgr.ContextMenuStrip = this.mnu;
            this.dgr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgr.Location = new System.Drawing.Point(0, 67);
            this.dgr.MainView = this.gridView1;
            this.dgr.Name = "dgr";
            this.dgr.Size = new System.Drawing.Size(784, 317);
            this.dgr.TabIndex = 8;
            this.dgr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(153, 92);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Visible = false;
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Visible = false;
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcId,
            this.gridColumn1,
            this.gcGiro});
            this.gridView1.GridControl = this.dgr;
            this.gridView1.GroupPanelText = "Resultado de la consulta";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyUp);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gcId
            // 
            this.gcId.Caption = "Código";
            this.gcId.DisplayFormat.FormatString = "{0:00}";
            this.gcId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcId.FieldName = "rgpmc_vcod_registro_parametro";
            this.gcId.Name = "gcId";
            this.gcId.OptionsColumn.AllowEdit = false;
            this.gcId.OptionsColumn.AllowFocus = false;
            this.gcId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMove = false;
            this.gcId.OptionsColumn.ReadOnly = true;
            this.gcId.Visible = true;
            this.gcId.VisibleIndex = 0;
            this.gcId.Width = 58;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Situación";
            this.gridColumn1.FieldName = "Situacion";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gcGiro
            // 
            this.gcGiro.Caption = "Descripción";
            this.gcGiro.FieldName = "rgpmc_vdescripcion";
            this.gcGiro.Name = "gcGiro";
            this.gcGiro.OptionsColumn.AllowEdit = false;
            this.gcGiro.OptionsColumn.AllowFocus = false;
            this.gcGiro.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcGiro.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcGiro.OptionsColumn.AllowMove = false;
            this.gcGiro.OptionsColumn.ReadOnly = true;
            this.gcGiro.Visible = true;
            this.gcGiro.VisibleIndex = 2;
            this.gcGiro.Width = 193;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.brnagregar,
            this.btnmosdificar,
            this.btneliminar,
            this.btnimprimir});
            this.barManager1.MaxItemId = 6;
            this.barManager1.StatusBar = this.bar2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 2";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 2";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 384);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 384);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 384);
            // 
            // brnagregar
            // 
            this.brnagregar.Caption = "barButtonItem1";
            this.brnagregar.Id = 0;
            this.brnagregar.Name = "brnagregar";
            this.brnagregar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brnagregar_ItemClick);
            // 
            // btnmosdificar
            // 
            this.btnmosdificar.Caption = "barButtonItem2";
            this.btnmosdificar.Id = 2;
            this.btnmosdificar.Name = "btnmosdificar";
            this.btnmosdificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnmosdificar_ItemClick);
            // 
            // btneliminar
            // 
            this.btneliminar.Caption = "barButtonItem3";
            this.btneliminar.Id = 3;
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btneliminar_ItemClick);
            // 
            // btnimprimir
            // 
            this.btnimprimir.Caption = "barButtonItem4";
            this.btnimprimir.Id = 4;
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnimprimir_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(65, 161);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brnagregar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnmosdificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btneliminar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnimprimir)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAnular
            // 
            this.btnAnular.Caption = "[F9]";
            this.btnAnular.Id = 2;
            this.btnAnular.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // FrmRegistroParametro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 406);
            this.Controls.Add(this.dgr);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmRegistroParametro";
            this.Text = "Registro Parametro";
            this.Load += new System.EventHandler(this.FrmPuntoVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtGiro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraGrid.GridControl dgr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gcGiro;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem brnagregar;
        private DevExpress.XtraBars.BarButtonItem btnmosdificar;
        private DevExpress.XtraBars.BarButtonItem btneliminar;
        private DevExpress.XtraBars.BarButtonItem btnimprimir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnAnular;
        private DevExpress.XtraBars.Bar bar2;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
    }
}