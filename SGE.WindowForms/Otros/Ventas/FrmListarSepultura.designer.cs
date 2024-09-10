namespace SGE.WindowForms.Otros.bVentas
{
    partial class FrmListarSepultura
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
            this.mnuCliente = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnPrev = new DevExpress.XtraBars.BarButtonItem();
            this.btnNext = new DevExpress.XtraBars.BarButtonItem();
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnsalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lkpManzana = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpSepultura = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpPlataforma = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.grdEspacios = new DevExpress.XtraGrid.GridControl();
            this.viewEspacios = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdlkpTipoSepultura = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdlkpPlataforma = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdlkpManzana = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.mnuCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpManzana.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSepultura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPlataforma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEspacios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewEspacios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlkpTipoSepultura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlkpPlataforma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlkpManzana)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuCliente
            // 
            this.mnuCliente.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem});
            this.mnuCliente.Name = "contextMenuStrip1";
            this.mnuCliente.Size = new System.Drawing.Size(126, 48);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificarToolStripMenuItem.Text = "Consultar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
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
            this.btnsalir,
            this.btnAceptar,
            this.btnPrev,
            this.btnNext});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPrev),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNext),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAceptar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnsalir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnPrev
            // 
            this.btnPrev.Id = 2;
            this.btnPrev.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Up);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrev_ItemClick);
            // 
            // btnNext
            // 
            this.btnNext.Id = 4;
            this.btnNext.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Down);
            this.btnNext.Name = "btnNext";
            this.btnNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNext_ItemClick);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAceptar.Caption = "Aceptar";
            this.btnAceptar.Id = 1;
            this.btnAceptar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
            // 
            // btnsalir
            // 
            this.btnsalir.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnsalir.Caption = "Cancelar";
            this.btnsalir.Id = 0;
            this.btnsalir.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnsalir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnsalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnsalir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(533, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 407);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(533, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 407);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(533, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 407);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Manzana:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Plataforma:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpManzana);
            this.groupControl1.Controls.Add(this.lkpSepultura);
            this.groupControl1.Controls.Add(this.lkpPlataforma);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(533, 80);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // lkpManzana
            // 
            this.lkpManzana.Location = new System.Drawing.Point(75, 49);
            this.lkpManzana.MenuManager = this.barManager1;
            this.lkpManzana.Name = "lkpManzana";
            this.lkpManzana.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpManzana.Size = new System.Drawing.Size(130, 20);
            this.lkpManzana.TabIndex = 7;
            this.lkpManzana.EditValueChanged += new System.EventHandler(this.lkpManzana_EditValueChanged);
            // 
            // lkpSepultura
            // 
            this.lkpSepultura.Location = new System.Drawing.Point(288, 27);
            this.lkpSepultura.MenuManager = this.barManager1;
            this.lkpSepultura.Name = "lkpSepultura";
            this.lkpSepultura.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSepultura.Size = new System.Drawing.Size(124, 20);
            this.lkpSepultura.TabIndex = 6;
            this.lkpSepultura.EditValueChanged += new System.EventHandler(this.lkpSepultura_EditValueChanged);
            // 
            // lkpPlataforma
            // 
            this.lkpPlataforma.Location = new System.Drawing.Point(75, 27);
            this.lkpPlataforma.MenuManager = this.barManager1;
            this.lkpPlataforma.Name = "lkpPlataforma";
            this.lkpPlataforma.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPlataforma.Size = new System.Drawing.Size(130, 20);
            this.lkpPlataforma.TabIndex = 5;
            this.lkpPlataforma.EditValueChanged += new System.EventHandler(this.lkpPlataforma_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(223, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Sepultura:";
            // 
            // grdEspacios
            // 
            this.grdEspacios.ContextMenuStrip = this.mnuCliente;
            this.grdEspacios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEspacios.Location = new System.Drawing.Point(0, 80);
            this.grdEspacios.MainView = this.viewEspacios;
            this.grdEspacios.Name = "grdEspacios";
            this.grdEspacios.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grdlkpPlataforma,
            this.grdlkpManzana,
            this.grdlkpTipoSepultura});
            this.grdEspacios.Size = new System.Drawing.Size(533, 327);
            this.grdEspacios.TabIndex = 26;
            this.grdEspacios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewEspacios});
            // 
            // viewEspacios
            // 
            this.viewEspacios.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14});
            this.viewEspacios.GridControl = this.grdEspacios;
            this.viewEspacios.GroupPanelText = "Linea";
            this.viewEspacios.Name = "viewEspacios";
            this.viewEspacios.DoubleClick += new System.EventHandler(this.viewEspacios_DoubleClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Codigo";
            this.gridColumn7.FieldName = "espac_icod_vespacios";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 424;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Sepultura";
            this.gridColumn12.ColumnEdit = this.grdlkpTipoSepultura;
            this.gridColumn12.FieldName = "espac_isepultura";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.AllowFocus = false;
            this.gridColumn12.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn12.OptionsColumn.AllowMove = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 274;
            // 
            // grdlkpTipoSepultura
            // 
            this.grdlkpTipoSepultura.AutoHeight = false;
            this.grdlkpTipoSepultura.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grdlkpTipoSepultura.Name = "grdlkpTipoSepultura";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Plataforma";
            this.gridColumn13.ColumnEdit = this.grdlkpPlataforma;
            this.gridColumn13.FieldName = "espac_icod_iplataforma";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowFocus = false;
            this.gridColumn13.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn13.OptionsColumn.AllowMove = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            this.gridColumn13.Width = 272;
            // 
            // grdlkpPlataforma
            // 
            this.grdlkpPlataforma.AutoHeight = false;
            this.grdlkpPlataforma.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grdlkpPlataforma.Name = "grdlkpPlataforma";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Manzana";
            this.gridColumn14.ColumnEdit = this.grdlkpManzana;
            this.gridColumn14.FieldName = "espac_icod_imanzana";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn14.OptionsColumn.AllowMove = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            this.gridColumn14.Width = 272;
            // 
            // grdlkpManzana
            // 
            this.grdlkpManzana.AutoHeight = false;
            this.grdlkpManzana.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grdlkpManzana.Name = "grdlkpManzana";
            // 
            // FrmListarSepultura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 435);
            this.Controls.Add(this.grdEspacios);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmListarSepultura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Sepulturas";
            this.Load += new System.EventHandler(this.FrmListarCliente_Load);
            this.mnuCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpManzana.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSepultura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPlataforma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEspacios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewEspacios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlkpTipoSepultura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlkpPlataforma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlkpManzana)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnsalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarButtonItem btnPrev;
        private DevExpress.XtraBars.BarButtonItem btnNext;
        private System.Windows.Forms.ContextMenuStrip mnuCliente;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl grdEspacios;
        private DevExpress.XtraGrid.Views.Grid.GridView viewEspacios;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grdlkpPlataforma;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grdlkpManzana;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grdlkpTipoSepultura;
        private DevExpress.XtraEditors.LookUpEdit lkpManzana;
        private DevExpress.XtraEditors.LookUpEdit lkpSepultura;
        private DevExpress.XtraEditors.LookUpEdit lkpPlataforma;
    }
}