using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
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
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.bVentas;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmCertificadoPerpetuo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContrato Obe = new EContrato();
        public ECertificadoUsoPerpetuo objCertificado = new ECertificadoUsoPerpetuo();
        public int[] icodEspacioDet = new int[7];
        public int[] icodEspacioDetT = new int[7];
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.ReadOnly = true;
                txtNumer.ReadOnly = true;
            }


        }
        public FrmCertificadoPerpetuo() => InitializeComponent();
        private void FrmCertificadoPerpetuo_Load(object sender, EventArgs e) => Cargar();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Dispose();
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => SetSave();
        void Cargar()
        {
            dtfechaEmision.EditValue = DateTime.Now;
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpManzana, new BGeneral().listarTablaVentaDet(5), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoDocContratante, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpsituacion, new BGeneral().listarTablaRegistro(101), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            txtNombreContratante.Text = Obe.cntc_vnombre_contratante;
            txtApellidoPContratante.Text = Obe.cntc_vapellido_paterno_contratante;
            txtApellidoMContratante.Text = Obe.cntc_vapellido_materno_contratante;
            txtRucContratante.Text = Obe.cntc_vruc_contratante;
            if (Obe.cntc_sfecha_nacimineto_contratante == null)
                Obe.cntc_sfecha_nacimineto_contratante = Convert.ToDateTime("01/01/0001");
            dtFechaNacContratante.EditValue = Obe.cntc_sfecha_nacimineto_contratante.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_nacimineto_contratante;
            lkpTipoDocContratante.EditValue = Obe.cntc_itipo_documento_contratante;
            txtDOCContratante.Text = Obe.cntc_vdocumento_contratante;
            if (Obe.cntc_sfecha_nacimiento_representante == null)
                Obe.cntc_sfecha_nacimiento_representante = Convert.ToDateTime("01/01/0001");
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
            lkpPlataforma.EditValue = Obe.cntc_icod_plataforma;
            lkpManzana.EditValue = Obe.cntc_icod_manzana;
            bteSepultura.Tag = Obe.cntc_icod_isepultura;
            bteSepultura.Text = Obe.strsepultura;
            btnEspacios.Tag = Convert.ToInt32(Obe.espac_iid_iespacios);
            btnEspacios.Text = Obe.espac_icod_vespacios;
            ckbNivel1.Checked = Convert.ToBoolean(Obe.cntc_bnivel1);
            ckbNivel2.Checked = Convert.ToBoolean(Obe.cntc_bnivel2);
            ckbNivel3.Checked = Convert.ToBoolean(Obe.cntc_bnivel3);
            ckbNivel4.Checked = Convert.ToBoolean(Obe.cntc_bnivel4);
            ckbNivel5.Checked = Convert.ToBoolean(Obe.cntc_bnivel5);
            ckbNivel6.Checked = Convert.ToBoolean(Obe.cntc_bnivel6);
            if (Obe.cntc_sfecha_cuota == null)
                Obe.cntc_sfecha_cuota = Convert.ToDateTime("01/01/0001");
            txtNombreContratante.Tag = Obe.cntcc_icod_contratante;
            CheckBox[] textoCantidad = GetTextoCantidad();
            List<EEspaciosDet> lstNivelesDetMod = new List<EEspaciosDet>();
            lstNivelesDetMod = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios);
            int count = 0;
            txtDOCContratante.Text = Obe.cntc_vdocumento_contratante;
            lstNivelesDetMod.ForEach(x =>
            {
                if (x.espad_icod_isituacion == 13 || x.cntc_icod_contrato == Obe.cntc_icod_contrato)
                {
                    textoCantidad[count].Enabled = true;
                    icodEspacioDet[count] = x.espad_iid_iespacios;

                }
                else
                {
                    textoCantidad[count].Enabled = false;
                }

                count++;
            });



        }

        private CheckBox[] GetTextoCantidad()
        {
            CheckBox[] texto = new CheckBox[] { ckbNivel1, ckbNivel2, ckbNivel3, ckbNivel4, ckbNivel5, ckbNivel6 };
            return texto;
        }

        public void SetValues()
        {
            txtSerie.Text = objCertificado.cup_vnumero.Substring(0, 3);
            txtNumer.Text = objCertificado.cup_vnumero.Substring(3);
            dtefechaEntrega.EditValue = objCertificado.cup_sfecha_entrega;
            dtfechaEmision.EditValue = objCertificado.cup_sfecha_emision;
            lkpsituacion.EditValue = objCertificado.cup_isituacion;
            chkAutorizado.Checked = objCertificado.cup_bautorizado;
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (string.IsNullOrEmpty(txtSerie.Text))
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Serie");
                }

                if (Convert.ToDecimal(txtNumer.Text) == 0)
                {
                    oBase = txtNumer;
                    throw new ArgumentException("Ingrese Número");
                }
                objCertificado.cup_vnumero = txtSerie.Text + txtNumer.Text;
                objCertificado.cup_sfecha_entrega = (DateTime?)dtefechaEntrega.EditValue;
                objCertificado.cup_sfecha_emision = (DateTime)dtfechaEmision.EditValue;
                objCertificado.cntc_icod_contrato = Obe.cntc_icod_contrato;
                objCertificado.cup_isituacion = Convert.ToInt32(lkpsituacion.EditValue);
                objCertificado.intUsuario = Valores.intUsuario;
                objCertificado.strPc = WindowsIdentity.GetCurrent().Name;
                objCertificado.cup_bautorizado = chkAutorizado.Checked;

                if (Status == BSMaintenanceStatus.CreateNew)
                    objCertificado.cup_icod = new BVentas().CetificadoUsoPerpetuoInsertar(objCertificado);
                else
                    new BVentas().CetificadoUsoPerpetuoModificar(objCertificado);
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(objCertificado.cup_icod);
                    this.Close();
                }
            }
        }
    }
}