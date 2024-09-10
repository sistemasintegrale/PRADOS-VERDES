
namespace SGE.WindowForms.Otros.Ventas
{
    partial class FrmManteTipoPlanNombrePlan
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
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCodigoPlan = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpNombrePLan = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpTipoSepultura = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCuotaInicial = new DevExpress.XtraEditors.TextEdit();
            this.txtPrecioLista = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescuento = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCodigoPlan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpNombrePLan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoSepultura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuotaInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecioLista.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescuento.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barEditItem1,
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager2.MaxItemId = 3;
            this.barManager2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager2.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Barra de estado";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Barra de estado";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Cancelar";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.Cancelar_16x16;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem2.Caption = "Guardar";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager2;
            this.barDockControl1.Size = new System.Drawing.Size(433, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 149);
            this.barDockControl2.Manager = this.barManager2;
            this.barDockControl2.Size = new System.Drawing.Size(433, 28);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Manager = this.barManager2;
            this.barDockControl3.Size = new System.Drawing.Size(0, 149);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(433, 0);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Size = new System.Drawing.Size(0, 149);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 0;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Nombre Plan :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(38, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Tipo Plan :";
            // 
            // lkpCodigoPlan
            // 
            this.lkpCodigoPlan.Location = new System.Drawing.Point(97, 38);
            this.lkpCodigoPlan.MenuManager = this.barManager2;
            this.lkpCodigoPlan.Name = "lkpCodigoPlan";
            this.lkpCodigoPlan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCodigoPlan.Size = new System.Drawing.Size(302, 20);
            this.lkpCodigoPlan.TabIndex = 10;
            // 
            // lkpNombrePLan
            // 
            this.lkpNombrePLan.Location = new System.Drawing.Point(97, 64);
            this.lkpNombrePLan.MenuManager = this.barManager2;
            this.lkpNombrePLan.Name = "lkpNombrePLan";
            this.lkpNombrePLan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpNombrePLan.Size = new System.Drawing.Size(302, 20);
            this.lkpNombrePLan.TabIndex = 11;
            // 
            // lkpTipoSepultura
            // 
            this.lkpTipoSepultura.Location = new System.Drawing.Point(97, 12);
            this.lkpTipoSepultura.MenuManager = this.barManager2;
            this.lkpTipoSepultura.Name = "lkpTipoSepultura";
            this.lkpTipoSepultura.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoSepultura.Size = new System.Drawing.Size(302, 20);
            this.lkpTipoSepultura.TabIndex = 17;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Tipo Sepultura :";
            // 
            // txtCuotaInicial
            // 
            this.txtCuotaInicial.EditValue = "0";
            this.txtCuotaInicial.Location = new System.Drawing.Point(299, 90);
            this.txtCuotaInicial.Name = "txtCuotaInicial";
            this.txtCuotaInicial.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCuotaInicial.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCuotaInicial.Properties.Mask.EditMask = "n2";
            this.txtCuotaInicial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCuotaInicial.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCuotaInicial.Size = new System.Drawing.Size(100, 20);
            this.txtCuotaInicial.TabIndex = 19;
            // 
            // txtPrecioLista
            // 
            this.txtPrecioLista.EditValue = "0";
            this.txtPrecioLista.Location = new System.Drawing.Point(97, 90);
            this.txtPrecioLista.Name = "txtPrecioLista";
            this.txtPrecioLista.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPrecioLista.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPrecioLista.Properties.Mask.EditMask = "n2";
            this.txtPrecioLista.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPrecioLista.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPrecioLista.Size = new System.Drawing.Size(100, 20);
            this.txtPrecioLista.TabIndex = 20;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(27, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(61, 13);
            this.labelControl4.TabIndex = 21;
            this.labelControl4.Text = "Precio Lista :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(224, 93);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(66, 13);
            this.labelControl5.TabIndex = 22;
            this.labelControl5.Text = "Cuota Inicial :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(27, 119);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(58, 13);
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "Descuento :";
            // 
            // txtDescuento
            // 
            this.txtDescuento.EditValue = "0";
            this.txtDescuento.Location = new System.Drawing.Point(97, 116);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDescuento.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDescuento.Properties.Mask.EditMask = "n2";
            this.txtDescuento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDescuento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDescuento.Size = new System.Drawing.Size(100, 20);
            this.txtDescuento.TabIndex = 27;
            // 
            // FrmManteTipoPlanNombrePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 177);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtDescuento);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtPrecioLista);
            this.Controls.Add(this.txtCuotaInicial);
            this.Controls.Add(this.lkpTipoSepultura);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lkpNombrePLan);
            this.Controls.Add(this.lkpCodigoPlan);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "FrmManteTipoPlanNombrePlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManteTipoPlanNombrePlan";
            this.Load += new System.EventHandler(this.FrmManteTipoPlanNombrePlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCodigoPlan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpNombrePLan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoSepultura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuotaInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecioLista.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescuento.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.LookUpEdit lkpNombrePLan;
        private DevExpress.XtraEditors.LookUpEdit lkpCodigoPlan;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoSepultura;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtPrecioLista;
        private DevExpress.XtraEditors.TextEdit txtCuotaInicial;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtDescuento;
    }
}