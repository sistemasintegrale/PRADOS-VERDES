using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SGE.WindowForms.UI
{
    public partial class frmMain : XtraForm
    {
        List<EParametro> lstParametro = new List<EParametro>();
        private TreeListNode hotTrackNode;
        private AppearanceDefault AppStyle;
        private List<EMenu> oList;
        private Dictionary<string, XtraForm> Ins;
        private SqlTableDependency<ControlVersiones> _tableDependency;
        

        public frmMain()
        {
            InitializeComponent();
            Ins = new Dictionary<string, XtraForm>();
            StyleTheme();
            oList = new List<EMenu>();
            AppStyle = new AppearanceDefault(Color.Black, Color.FromArgb(252, 235, 199),
                                             Color.Empty, Color.FromArgb(247, 199, 98),
                                             LinearGradientMode.Vertical);
            hotTrackNode = null;
            ConfigurarSqlTableDependency();
        }

        private void ConfigurarSqlTableDependency()
        {


            _tableDependency = new SqlTableDependency<ControlVersiones>(DataAccess.Helper.conexion(), "SGE_CONTROL_VERSIONES");
            _tableDependency.OnChanged += TableDependency_OnChanged;
            _tableDependency.OnError += TableDependency_OnError;

            _tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<ControlVersiones> e)
        {
            if (e.ChangeType == ChangeType.Insert)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    VerificarActualizador();
                }));
            }
        }

        private void VerificarActualizador()
        {
            var objEquipo = new BAdministracionSistema().Equipo_Obtner_Datos(Valores.strNombreEquipo, Valores.strIdCpu);
            var ultimaVersion = new BAdministracionSistema().Listar_Versiones().OrderByDescending(x => x.cvr_sfecha_version).FirstOrDefault();
            if (ultimaVersion.cvr_icod_version != objEquipo.cvr_icod_version)
            {
                this.notifyIcon1.ShowBalloonTip(1000,"Aviso del Sistema Prados Verdes", "Hay una nueva actualización, Vuelva a Iniciar Sessión para Actualizar su Sistema",ToolTipIcon.Info);
                btnActulizacion.Caption = "NUEVA ACTUALIZACIÓN DISPONIBLE";
                btnActulizacion.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            MessageBox.Show($"Error: {e.Error.Message}");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _tableDependency.Stop();
            base.OnFormClosing(e);
        }

        private void StyleTheme()
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            lstParametro = new BAdministracionSistema().listarParametro();

            this.btnVersion.Caption = "Ver. 12.55.09.10";
            fillForm();
            btnUsuario.Caption = "Usuario: " + Valores.strUsuario;
            btnEjercicio.Caption = "Año de Ejercicio: " + Parametros.intEjercicio.ToString();
            //DevExpress.UserSkins.OfficeSkins.Register();
            //DevExpress.UserSkins.BonusSkins.Register();
            //SkinHelper.InitSkinPopupMenu(skinsLink);
            Formulario("SGE.WindowForms.Otros.Administracion_del_Sistema.frmFondo");
            Valores.lstAccesosUsuario = new BAdministracionSistema().listarAccesosPermitidos(Valores.intUsuario);
            var lst = new BAdministracionSistema().listarParametro();
            SGE.WindowForms.Parametros.strPorcIGV = Convert.ToDecimal(lst[0].pm_nigv_parametro).ToString();
            SGE.BusinessLogic.Parametros.strPorcIGV = Convert.ToDecimal(lst[0].pm_nigv_parametro).ToString();

            SGE.WindowForms.Parametros.strPorcRenta4taCat = Convert.ToDecimal(lst[0].pm_ncategoria_parametro).ToString();
            SGE.BusinessLogic.Parametros.strPorcRenta4taCat = Convert.ToDecimal(lst[0].pm_ncategoria_parametro).ToString();

            navBarControl2.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;

            Valores.pm_icod_parametro = lstParametro[0].pm_icod_parametro;
            Valores.longCorrelativoOR = lstParametro[0].pm_correlativo_OR;


        }

        public class Enombre
        {
            public string modulo { get; set; }
            public string sis_name { get; set; }
            public string ui_name { get; set; }
        }

        private void Formulario(string Name)
        {

            TextEdit txt = new TextEdit();
            XtraForm oForm = new XtraForm();
            if (!Ins.TryGetValue(Name, out oForm) || oForm.IsDisposed)
            {
                Type oType = Type.GetType(Name);
                oForm = (XtraForm)Activator.CreateInstance(oType);
                Ins[Name] = oForm;
            }
            oForm.MdiParent = this;
            oForm.Show();
            Focus();
            oForm.BringToFront();
        }

        private Assembly oAss;
        private TreeList tree;
        private TreeListNode mNode;
        private TreeListNode mParent;
        private TreeListNode mChild;

        private void fillForm()
        {
            /**/
            List<Enombre> listaForms = new List<Enombre>();

            /**/
            List<EMenu> _List = new List<EMenu>();
            oAss = Assembly.GetExecutingAssembly();
            Type[] Types = oAss.GetTypes();
            Hashtable Nivel = new Hashtable();
            Hashtable SubNivel = new Hashtable();

            Array.ForEach(Types, oTp =>
            {
                if (string.Compare(oTp.BaseType.Name, "XtraForm", false) == 0 &&
                    !oTp.Namespace.Contains("SGE.WindowForms.UI") &&
                    !oTp.Namespace.Contains("Otros") &&
                    !oTp.Namespace.Contains("Reportes"))
                /*if(string.Compare(oTp.BaseType.Name, "XtraForm", false) == 0 &&
                    ((oTp.Namespace.Contains("Almacén") ||
                    oTp.Namespace.Contains("Ventas") ||
                    oTp.Namespace.Contains("Contabilidad"))
                    && !oTp.Namespace.Contains("Otros")))*/
                {


                    EMenu oBE = new EMenu { FullDescription = oTp.FullName };
                    Type oTy = oAss.GetType(oBE.FullDescription);
                    string[] oFile = oTy.Namespace.Split('.');
                    oBE.Parent = null;
                    oBE.FullNameSpace = oFile[2];
                    object obj = Activator.CreateInstance(Type.GetType(oBE.FullDescription));
                    oBE.Description = ((XtraForm)obj).Text;
                    oBE.Parent = oBE.FullNameSpace;
                    /*Esta parte es para crear el archivo excel con los nombres de los formularios*/
                    Enombre objForm = new Enombre();
                    objForm.modulo = oBE.Parent;
                    objForm.sis_name = oTp.Name;
                    objForm.ui_name = oBE.Description;
                    listaForms.Add(objForm);
                    /**/
                    _List.Add(oBE);
                }
            });
            /*Aquí se ejecuta el método para la exportación del archivo excel con los nombres de los formularios*/
            crearArchivo(listaForms);
            /**/
            var mList = _List.OrderBy(x => x.FullDescription).ToList();
            oList = mList;
            mList.ForEach(obj =>
            {
                string[] oFile = obj.FullDescription.Split('.');
                try
                {
                    Nivel.Add(oFile[2], oFile[2]);
                    var navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
                    NavBarGroup Nbar = new NavBarGroup();
                    Nbar.Caption = oFile[2].Replace("_", " ");
                    navBarControl1.BeginUpdate(); //Nuevo linea
                    navBarControl1.Groups.Add(Nbar);


                    if (Nbar.Caption == "Sistema")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.settings_;
                    else if (Nbar.Caption == "Almacén")
                    {
                        Nbar.Expanded = true;
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.almacen;

                    }
                    else if (Nbar.Caption == "Contabilidad")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.contabilidad;
                    else if (Nbar.Caption == "Compras")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.ctaxpagar;
                    else if (Nbar.Caption == "Ventas")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.ctaxcobrar;
                    else if (Nbar.Caption == "Tesorería")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.tesoreria;
                    else if (Nbar.Caption == "Operaciones")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.operaciones;
                    else if (Nbar.Caption == "Administración")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.administracion_;
                    else if (Nbar.Caption == "Presupuesto")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.notes;
                    else if (Nbar.Caption == "Planillas")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.notes;

                    tree = new TreeList();
                    tree.CustomDrawNodeCell += Object_CustomDrawNodeCell;
                    tree.Click += trlID_Click;
                    tree.MouseMove += this.Object_MouseMove;
                    tree.Columns.Add();
                    tree.Columns[0].Visible = true;
                    tree.BorderStyle = BorderStyles.NoBorder;
                    tree.OptionsBehavior.Editable = false;
                    tree.OptionsSelection.EnableAppearanceFocusedCell = true;
                    tree.OptionsView.ShowIndicator = false;
                    tree.OptionsView.ShowColumns = false;
                    tree.OptionsView.ShowHorzLines = false;
                    tree.OptionsView.ShowVertLines = false;
                    Nbar.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
                    Nbar.ControlContainer.Controls.Add(tree);
                    tree.Dock = DockStyle.Fill;
                    navBarControl1.Width = 255;
                    mParent = null;
                    if (oFile.Length - 1 == 3)
                    {
                        mNode = tree.AppendNode(obj.Description, mParent, obj.Description);
                        mNode.SetValue(0, obj.Description);
                        mNode.SetValue(1, obj.FullNameSpace);
                    }
                    else if (oFile.Length - 1 == 4)
                    {
                        try
                        {
                            SubNivel.Add(oFile[2] + " " + oFile[3].Replace("_", " ").Replace("_", " "), oFile[2] + " " + oFile[3].Replace("_", " "));
                            mNode = tree.AppendNode(oFile[3].Replace("_", " "), mParent, oFile[3].Replace("_", " "));
                            mNode.SetValue(0, oFile[3].Replace("_", " "));
                            mNode.SetValue(1, obj.FullNameSpace);
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                        catch
                        {
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                    }
                    navBarControl1.EndUpdate();// final de agregar modulos
                }
                catch
                {
                    mParent = null;
                    if (oFile.Length - 1 == 3)
                    {
                        mNode = tree.AppendNode(obj.Description, mParent, obj.Description);
                        mNode.SetValue(0, obj.Description);
                        mNode.SetValue(1, obj.FullNameSpace);
                    }
                    else if (oFile.Length - 1 == 4)
                    {
                        try
                        {
                            SubNivel.Add(oFile[2] + " " + oFile[3].Replace("_", " "), oFile[2] + " " + oFile[3].Replace("_", " "));
                            mNode = tree.AppendNode(oFile[3].Replace("_", " "), mParent, oFile[3].Replace("_", " "));
                            mNode.SetValue(0, oFile[3].Replace("_", " "));
                            mNode.SetValue(1, obj.FullNameSpace);
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                        catch
                        {
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                    }
                }
            });

        }
        public void crearArchivo(List<Enombre> lista)
        {
            //string ruta = "C:\\CRAC\\Formularios.xlsx";
            //gridControl1.DataSource = lista.OrderBy(x => x.modulo).ToList();
            //gridControl1.ExportToXlsx(ruta);

        }
        private void Object_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeListNode tl = sender as TreeListNode;//Se modifico
            if (!e.Node.Focused)
            {
                if (!e.Node.HasChildren)
                {
                    SolidBrush brush = GetSolidBrush(SystemColors.Window);
                    e.Graphics.FillRectangle(brush, e.Bounds);
                    SolidBrush highlightBrush = GetSolidBrush(Color.Transparent);
                    Rectangle R = new Rectangle(e.EditViewInfo.ContentRect.Left, e.EditViewInfo.ContentRect.Top,
                        Convert.ToInt32(e.Graphics.MeasureString(e.CellText, e.Appearance.Font).Width + 1), e.EditViewInfo.ContentRect.Height);
                    e.Graphics.FillRectangle(highlightBrush, R);
                    SolidBrush highlightForeBrush = GetSolidBrush(Color.Black);
                    using (StringFormat format = new StringFormat { LineAlignment = StringAlignment.Center })
                    {
                        e.Graphics.DrawString(e.CellText, e.Appearance.Font, highlightForeBrush, R, format);
                    }
                    e.Handled = true;
                }
            }
            if (e.Node == hotTrackNode && !e.Node.HasChildren)
                AppearanceHelper.Apply(e.Appearance, AppStyle);
        }
        private SolidBrush GetSolidBrush(Color systemColor)
        {
            Color color = LookAndFeelHelper.GetSystemColor(tree.LookAndFeel, systemColor);
            SolidBrush brush = new SolidBrush(color);
            return brush;
        }
        private void trlID_Click(object sender, EventArgs e)
        {
            TreeList tree = (TreeList)sender;
            Point pt = tree.PointToClient(Control.MousePosition);
            DoNodeClick(tree, pt);
        }
        private void DoNodeClick(TreeList tree, Point pt)
        {
            TreeListHitInfo info = tree.CalcHitInfo(pt);
            if (info.HitInfoType == HitInfoType.Cell)
            {
                if (tree.FocusedNode != null)
                {
                    if (!tree.FocusedNode.HasChildren)
                    {
                        object oBE = (object)tree.GetDataRecordByNode(tree.FocusedNode);
                        ArrayList oAr = (ArrayList)oBE;
                        string mName = oAr[0].ToString();
                        var Lista = oList.Where(obj => obj.Description == mName).ToList();
                        String Name = Lista[0].FullDescription;
                        string mNameSpace = Lista[0].FullNameSpace;
                        XtraForm oForm;
                        if (!Ins.TryGetValue(Name, out oForm) || oForm.IsDisposed)
                        {
                            Type oType = Type.GetType(Name);
                            oForm = (XtraForm)Activator.CreateInstance(oType);
                            Ins[Name] = oForm;
                        }
                        if (acceso(oForm.Name) == 1)
                        {
                            oForm.MdiParent = this;
                            oForm.Show();
                            Focus();
                            oForm.BringToFront();
                        }
                    }
                }

            }
        }
        public int acceso(string form)
        {
            int flag;
            flag = Modules.Valores.lstAccesosUsuario.FindIndex(x => x.formc_vnombre_forms == form);
            if (flag >= 0)
                flag = 1;
            else
            {
                XtraMessageBox.Show("Acceso no permitido", "Datos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = 0;
            }

            return flag;
        }
        private void Object_MouseMove(object sender, MouseEventArgs e)
        {
            TreeList tl = sender as TreeList;
            TreeListHitInfo info = tl.CalcHitInfo(new Point(e.X, e.Y));
            if ((info.HitInfoType == HitInfoType.Cell) && (info.Node != hotTrackNode))
            {
                TreeListNode tempNode = hotTrackNode;
                hotTrackNode = null;
                tl.InvalidateNode(tempNode);
                hotTrackNode = info.Node;
                tl.Cursor = Cursors.Hand;
                tl.InvalidateNode(hotTrackNode);
            }
            if (info.Node == null)
            {
                TreeListNode tempNode = hotTrackNode;
                hotTrackNode = null;
                tl.InvalidateNode(tempNode);
                hotTrackNode = info.Node;
                tl.Cursor = Cursors.Default;
                tl.InvalidateNode(hotTrackNode);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Application.OpenForms.Count == 3)
                    Application.Exit();
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    string nombreForm = Application.OpenForms[i].ToString();
                    if (nombreForm.Contains("frmGenerarVoucher") != false)
                    {
                        throw new ArgumentException("Se esta ejecutando un proceso de generación de vouchers, no puede salir de la aplicación");
                    }
                }
                Application.Exit();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void btnGaleria_ItemClick(object sender, ItemClickEventArgs e)
        {
            var galeria = new DevExpress.XtraBars.Ribbon.GalleryDropDown();
            galeria.Manager = barManager1;
            SkinHelper.InitSkinGalleryDropDown(galeria);
            galeria.ShowPopup(MousePosition);
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void logo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        //private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    frm04Vehiculo frm = new frm04Vehiculo();
        //    if (!flagOpened(frm.Text))
        //    {
        //        frm.MdiParent = this;
        //        frm.Show();
        //        frm.Focus();
        //    }
        //}

        //private bool flagOpened(string titulo)
        //{
        //    bool babierto = false;
        //    try
        //    {
        //        foreach (DevExpress.XtraTabbedMdi.XtraMdiTabPage item in xtraTabbedMdiManager1.Pages)
        //        {
        //            babierto = titulo == item.Text;
        //            if (babierto)
        //            {
        //                xtraTabbedMdiManager1.SelectedPage = item;
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return babierto;
        //}


    }
}