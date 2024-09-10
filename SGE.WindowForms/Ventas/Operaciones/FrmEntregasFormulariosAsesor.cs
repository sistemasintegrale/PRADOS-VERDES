using SGE.WindowForms.Otros.bVentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGE.Entity;
using SGE.BusinessLogic;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class FrmEntregasFormulariosAsesor : DevExpress.XtraEditors.XtraForm
    {
        public List<EEntregaFormulario> lista = new List<EEntregaFormulario>();
        public DateTime dtIninio = new DateTime(Parametros.intEjercicio, 1, 1);
        public DateTime dtfinal = DateTime.Today;
        public bool cargaCompleta = false;
        public FrmEntregasFormulariosAsesor()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteEntregasFormulario frm = new FrmManteEntregasFormulario();
            frm.MiEvento += new FrmManteEntregasFormulario.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
          

        }

        private void grdControl_Load(object sender, EventArgs e)
        {
            
            var lstVenderdor = new BVentas().listarVendedor();
            EVendedor objVendedor = new EVendedor()
            {
                vendc_vnombre_vendedor = "TODOS",
                vendc_icod_vendedor = 0
            };
            lstVenderdor.Add(objVendedor);

            BSControls.LoaderLook(lkpAsesor, lstVenderdor.OrderBy(x => x.vendc_icod_vendedor), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            lkpAsesor.EditValue = 0;
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit1, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit2, new BGeneral().listarTablaVentaDet(24), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            dteFechaInicio.DateTime = dtIninio;
            dteFechaFinal.DateTime = dtfinal;
            cargaCompleta = true;
            cargar();
        }

        void reload(int intIcod)
        {
            cargar();

            int index = lista.FindIndex(x => x.entf_icod_entrega == intIcod);
            viewControl.FocusedRowHandle = index;
            viewControl.Focus();
        }

        private void cargar()
        {
            if (cargaCompleta != true)
                return;
            dtIninio = Convert.ToDateTime(dteFechaInicio.DateTime);
            dtfinal = Convert.ToDateTime(dteFechaFinal.DateTime);
            lista = new BVentas().listarEntregaFormularios(dtIninio,dtfinal, Convert.ToInt32(lkpAsesor.EditValue)).OrderBy(x => x.entf_icod_vendedor).ToList();
            grdControl.DataSource = lista;
            grdControl.Refresh();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntregaFormulario obe = (EEntregaFormulario)viewControl.GetRow(viewControl.FocusedRowHandle);
            if (obe == null)
                return;
            var listaV = new BVentas().listarEntregaFormularios(dtIninio, dtfinal, Convert.ToInt32(lkpAsesor.EditValue)).OrderBy(x => x.entf_vnumero_formulario).ToList();
            obe = listaV.Where(x => x.entf_icod_entrega == obe.entf_icod_entrega).FirstOrDefault();
            if (obe.entf_icod_estado == 7442)
            {
                XtraMessageBox.Show("No se Puede Modificar Formato, si Situación es EN USO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (obe.entf_icod_estado == 7443)
            {
                XtraMessageBox.Show("No se Puede Modificar Formato, si Situación es ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmManteEntregasFormulario frm = new FrmManteEntregasFormulario();
            frm.obj = obe;
            frm.numeroFormato = obe.entf_vnumero_formulario;
            frm.MiEvento += new FrmManteEntregasFormulario.DelegadoMensaje(reload);
            frm.SetModify();
            frm.Show();
            frm.setvalues();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntregaFormulario obe = (EEntregaFormulario)viewControl.GetRow(viewControl.FocusedRowHandle);
            if (obe == null)
                return;
            var listaV = new BVentas().listarEntregaFormularios(dtIninio, dtfinal, Convert.ToInt32(lkpAsesor.EditValue)).OrderBy(x => x.entf_vnumero_formulario).ToList();
            obe = listaV.Where(x => x.entf_icod_entrega == obe.entf_icod_entrega).FirstOrDefault();
            if (obe.entf_icod_estado == 7442)
            {
                if (XtraMessageBox.Show("El formato ya se Encuentra EN USO, lo Desea Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    eliminar(obe);
                return;
            }
            if (obe.entf_icod_estado == 7443)
            {
                if (XtraMessageBox.Show("El formato ya se Encuentra EN ANULADO, lo Desea Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    eliminar(obe);
                return;
            }
            if (XtraMessageBox.Show("Esta Seguro de Eliminar el Formato " + obe.entf_vnumero_formulario, "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                eliminar(obe);
            return;
            
        }

        void eliminar(EEntregaFormulario obe)
        {
            obe.entf_iusuario_modifica = Valores.intUsuario;
            obe.entf_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            obe.entf_sfecha_modifica = DateTime.Today;
            new BVentas().eliminarEntregaFormulario(obe);
            reload(obe.entf_icod_entrega - 1);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (lista.Count > 0)
                {
                    SaveFileDialog sfdRuta = new SaveFileDialog();
                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {
                        string fileName = sfdRuta.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdControl.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdControl.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                    }
                }
                else
                    throw new ArgumentException("No hay registros para exportar");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


        private void dteFecha_EditValueChanged(object sender, EventArgs e)
        {
            dteFechaFinal.DateTime = dtfinal; 
            cargar();
        }

        private void buscarCriterio()
        {
            if (Convert.ToInt32(lkpAsesor.EditValue) != 0 && dteFechaInicio.EditValue != null)
                grdControl.DataSource = lista.Where(x => x.entf_sfecha_entrega == Convert.ToDateTime(dteFechaInicio.EditValue) && x.entf_icod_vendedor == Convert.ToInt32(lkpAsesor.EditValue)).ToList();
            if (Convert.ToInt32(lkpAsesor.EditValue) == 0 && dteFechaInicio.EditValue != null)
                grdControl.DataSource = lista.Where(x => x.entf_sfecha_entrega == Convert.ToDateTime(dteFechaInicio.EditValue)).ToList();
            if (Convert.ToInt32(lkpAsesor.EditValue) != 0 && dteFechaInicio.EditValue == null)
                grdControl.DataSource = lista.Where(x => x.entf_icod_vendedor == Convert.ToInt32(lkpAsesor.EditValue)).ToList();

            grdControl.DataSource = lista.Where(x => x.entf_vnumero_formulario.Contains(txtNumFormato.Text)).ToList();

            grdControl.RefreshDataSource();
            
        }


        private void txtAsesor_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }

        private void lkpAsesor_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        private void nuevoVariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteEntregasFormularioVarios frm = new FrmManteEntregasFormularioVarios();
            frm.MiEvento += new FrmManteEntregasFormularioVarios.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntregaFormulario obe = (EEntregaFormulario)viewControl.GetRow(viewControl.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.entf_icod_estado == 7442)
            {
                XtraMessageBox.Show("No se Puede Anualr Formato, si Situación es EN USO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (obe.entf_icod_estado == 7443)
            {
                XtraMessageBox.Show("El Formato ya se Encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (XtraMessageBox.Show("Esta Seguro de Anular el Formato " + obe.entf_vnumero_formulario, "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;
            obe.entf_iusuario_modifica = Valores.intUsuario;
            obe.entf_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            obe.entf_sfecha_modifica = DateTime.Today;
            obe.entf_icod_estado = 7443;
            new BVentas().modificarEntregaFormulario(obe);
            crearcontrato(obe);
            reload(obe.entf_icod_entrega);
        }

        public void crearcontrato(EEntregaFormulario obe)
        {
            EContrato obe_return = new EContrato()
            {
                cntc_vnumero_contrato = obe.entf_vnumero_formulario,
                cntc_sfecha_contrato = (DateTime)obe.entf_sfecha_modifica,
                cntc_icod_vendedor = obe.entf_icod_vendedor,
                cntc_iorigen_venta = 1,
                cntc_icod_funeraria = 0,
                cntc_vnombre_comercial = string.Empty,
                cntc_inacionalidad_contratante = 0,
                cntc_itipo_documento_contratante = 0,
                cntc_iestado_civil_representante = 0,
                cntc_inacionalidad_respresentante = 0,
                cntc_idistrito_representante = 0,
                cntc_itipo_documento_representante = 0,
                cntc_inacionalidad = 0,
                cntc_icodigo_plan = 3,
                cntc_icod_nombre_plan = 326,
                cntc_vnombre_plan = string.Empty,
                intUsuario = obe.entf_iusuario_modifica,
                strPc = WindowsIdentity.GetCurrent().Name,
                cntc_flag_estado = true,
                cntc_icod_situacion = 332,//anulado
                cntc_flag_verificacion = true,
                cntc_indicador_pre_contrato = 2,
                cntc_vnombre_contratante = string.Empty,
                cntc_vapellido_paterno_contratante = string.Empty,
                cntc_vapellido_materno_contratante = string.Empty,
                cntc_vruc_contratante = string.Empty,
                cntc_vtelefono_contratante = string.Empty,
                cntc_vdireccion_correo_contratante = string.Empty,
                cntc_vdireccion_contratante = string.Empty,
                cntc_vnacionalidad_cotratante = string.Empty,
                cntc_vdocumento_contratante = string.Empty,
                cntc_vnombre_representante = string.Empty,
                cntc_vapellido_paterno_representante = string.Empty,
                cntc_vapellido_materno_representante = string.Empty,
                cntc_ruc_representante = string.Empty,
                cntc_vtelefono_representante = string.Empty,
                cntc_vdireccion_representante = string.Empty,
                cntc_vnumero_direccion_representante = string.Empty,
                cntc_vdepartamento_representante = string.Empty,
                cntc_vcodigo_postal_representante = string.Empty,
                cntc_vdocumento_respresentante = string.Empty,
                cntc_vcapacidad_contrato = string.Empty,
                cntc_vcapacidad_total = string.Empty,
                cntc_vurnas = string.Empty,
                 cntc_vservico_inhumacion = string.Empty,
                cntc_bnivel1  = false,   cntc_bnivel2 = false,
                cntc_bnivel3 = false,
                cntc_bnivel4 = false,
                cntc_bnivel5 = false,
                cntc_bnivel6 = false,
                cntc_vcodigo_sepultura = string.Empty,
                cntc_vnumero_reserva = string.Empty,
                cntc_vobservaciones = string.Empty,
                cntc_sfecha_cuota  = (DateTime?)null
            };
            new BVentas().insertarContrato(obe_return);
        }

        private void dteFechaFinal_EditValueChanged(object sender, EventArgs e)
        {
            dteFechaInicio.DateTime = dtIninio;
            cargar();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNumFormato_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }
    }
}
