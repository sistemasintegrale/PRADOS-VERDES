using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.bVentas;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteUbicaciones : XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EContratoFallecido Obe = new EContratoFallecido();
        public List<EContratoFallecido> lstDetalle = new List<EContratoFallecido>();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public int icodEspacio;
        public int icodContrato;
        public string numContrato;
        public int asesor;
        public EContrato obeContrato = new EContrato();
        public frmManteUbicaciones()
        {
            InitializeComponent();
        }
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
                btnContrato.Enabled = false;
            }

            if (Status == BSMaintenanceStatus.View)
            {

            }
        }

        private void frmManteUbicaciones_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpNacionalidad1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocFallecido, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpRelogiones, new BGeneral().listarTablaVentaDet(19), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoDeceso, new BGeneral().listarTablaVentaDet(17), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTamanio, new BGeneral().listarTablaVentaDet(27), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNombreIEC, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpManzana, new BGeneral().listarTablaVentaDet(5), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status != BSMaintenanceStatus.CreateNew)
            {
                setValues();
            }
        }

        public void setValues()
        {
            txtNombreFallecido.Text = Obe.cntc_vnombre_fallecido;
            txtApellidoPFallecido.Text = Obe.cntc_vapellido_paterno_fallecido;
            txtApellidoMFallecido.Text = Obe.cntc_vapellido_materno_fallecido;
            txtDOCFallecimiento.Text = Obe.cntc_vdni_fallecido;
            dtFechaNacFallecido.EditValue = Obe.cntc_sfecha_nac_fallecido;
            lkpNacionalidad1.EditValue = Obe.cntc_inacionalidad;
            dtFechaFallecimiento.EditValue = Obe.cntc_sfecha_fallecimiento;
            dtFechaEntierro.EditValue = Obe.cntc_sfecha_entierro;
            lkpTipoDocFallecido.EditValue = Obe.cntc_itipo_documento_fallecido;
            txtDOCFallecimiento.Text = Obe.cntc_vdocumento_fallecido;
            txtDomicilioFallecido.Text = Obe.cntc_vdireccion_fallecido;
            lkpRelogiones.EditValue = Obe.cntc_icod_religiones;
            lkpTipoDeceso.EditValue = Obe.cntc_icod_tipo_deceso;
            txtObservaciones.Text = Obe.cntc_vobservacion;
            dtFechaAccion.EditValue = Obe.cntc_sfecha_accion;
            lkpTamanio.EditValue = Obe.cntc_icod_tamanio_lapida;
            btnContrato.Tag = Obe.cntc_icod_contrato;
            btnContrato.Text = numContrato;
            lkpNombreIEC.EditValue = asesor;
            icodContrato = Obe.cntc_icod_contrato;
            btnNiveles.Tag = Obe.cntc_icod_indicador_espacios;
            btnNiveles.Text = Obe.espad_vnivel;
            txtFrase.Text = Obe.cntc_vfrase;
            txtPensamiento.Text = Obe.cntc_vpensamiento;

            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
            lkpManzana.EditValue = Obe.cntc_icod_manzana;
            bteSepultura.Tag = Obe.cntc_icod_isepultura;
            bteSepultura.Text = Obe.strsepultura;
            btnEspacios.Tag = Convert.ToInt32(Obe.espac_iid_iespacios);
            btnEspacios.Text = Obe.espac_icod_vespacios;
            lkpPlataforma.EditValue = Obe.cntc_icod_plataforma;


        }

        void SetValuesContrato()
        {
            lkpTipoSepultura.EditValue = obeContrato.cntc_itipo_sepultura;
            bteSepultura.Tag = obeContrato.cntc_icod_isepultura;
            bteSepultura.Text = obeContrato.strsepultura;
            btnEspacios.Tag = Convert.ToInt32(obeContrato.espac_iid_iespacios);
            btnEspacios.Text = obeContrato.espac_icod_vespacios;
            lkpPlataforma.EditValue = obeContrato.cntc_icod_plataforma;
            lkpManzana.EditValue = obeContrato.cntc_icod_manzana;

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            txtNombreFallecido.ReadOnly = true;
            lkpTipoDocFallecido.Enabled = false;
            txtApellidoPFallecido.ReadOnly = true;
            dtFechaNacFallecido.Enabled = false;
            dtFechaNacFallecido.Enabled = false;
            lkpNacionalidad1.Enabled = false;
            dtFechaFallecimiento.Enabled = false;
            dtFechaEntierro.Enabled = false;
            btnNiveles.Enabled = false;
            txtDomicilioFallecido.ReadOnly = true;
            lkpRelogiones.Enabled = false;
            txtObservaciones.ReadOnly = true;
            lkpTipoDeceso.Enabled = false;
            lkpTamanio.Enabled = false;
            dtFechaAccion.Enabled = false;
            barButtonItem2.Enabled = false;
            btnContrato.Enabled = false;
            txtApellidoMFallecido.ReadOnly = true;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(btnContrato.Tag) == 0)
                {
                    oBase = btnContrato;
                    throw new ArgumentException("Ingrese Contarto");
                }

                if (Convert.ToInt32(lkpTamanio.EditValue) == 0)
                {
                    oBase = lkpTamanio;
                    throw new ArgumentException("Ingrese Tamaño el Tamaño Lápida");
                }

                Obe.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                Obe.cntc_vnombre_fallecido = txtNombreFallecido.Text;
                Obe.cntc_vapellido_paterno_fallecido = txtApellidoPFallecido.Text;
                Obe.cntc_vapellido_materno_fallecido = txtApellidoMFallecido.Text;
                Obe.cntc_vdni_fallecido = txtDOCFallecimiento.Text;
                if (dtFechaNacFallecido.DateTime == null || dtFechaNacFallecido.Text == "" || dtFechaNacFallecido.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_nac_fallecido = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_nac_fallecido = Convert.ToDateTime(dtFechaNacFallecido.EditValue);
                }
                Obe.cntc_inacionalidad = Convert.ToInt32(lkpNacionalidad1.EditValue);
                if (dtFechaFallecimiento.DateTime == null || dtFechaFallecimiento.Text == "" || dtFechaFallecimiento.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_fallecimiento = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_fallecimiento = Convert.ToDateTime(dtFechaFallecimiento.EditValue);
                }
                if (dtFechaEntierro.DateTime == null || dtFechaEntierro.Text == "" || dtFechaEntierro.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_entierro = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_entierro = Convert.ToDateTime(dtFechaEntierro.EditValue);
                }
                Obe.cntc_itipo_documento_fallecido = Convert.ToInt32(lkpTipoDocFallecido.EditValue);
                Obe.cntc_vdocumento_fallecido = txtDOCFallecimiento.Text;
                Obe.cntc_icod_religiones = Convert.ToInt32(lkpRelogiones.EditValue);
                Obe.cntc_icod_tipo_deceso = Convert.ToInt32(lkpTipoDeceso.EditValue);
                Obe.cntc_vobservacion = txtObservaciones.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.cntc_icod_indicador_espacios = Convert.ToInt32(btnNiveles.Tag);
                Obe.cntc_vdireccion_fallecido = txtDomicilioFallecido.Text;
                /**/

                Obe.cntc_sfecha_accion = dtFechaAccion.DateTime.Year == 1 ? (DateTime?)null : dtFechaAccion.DateTime;
                Obe.cntc_icod_tamanio_lapida = Convert.ToInt32(lkpTamanio.EditValue);
                Obe.cntc_vfrase = txtFrase.Text;
                Obe.cntc_vpensamiento = txtPensamiento.Text;

                Obe.cntc_itipo_sepultura = Convert.ToInt32(lkpTipoSepultura.EditValue);
                Obe.cntc_icod_manzana = Convert.ToInt32(lkpManzana.EditValue);
                Obe.cntc_icod_isepultura = Convert.ToInt32(bteSepultura.Tag);
                Obe.espac_iid_iespacios = Convert.ToInt32(btnEspacios.Tag);
                Obe.cntc_icod_plataforma = Convert.ToInt32(lkpPlataforma.EditValue);



                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    Obe.cntc_icod_contrato_fallecido = new BVentas().insertarContratoFallecido(Obe);

                    List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(Convert.ToInt32(btnEspacios.Tag)).Where(x => x.espad_iid_iespacios == Obe.cntc_icod_indicador_espacios).ToList();
                    lstEspacioDet.ForEach(x =>
                    {
                        x.espad_vnom_fallecido = Obe.cntc_vnombre_fallecido;
                        x.espad_vapellido_paterno_fallecido = Obe.cntc_vapellido_paterno_fallecido;
                        x.espad_vapellido_materno_fallecido = Obe.cntc_vapellido_materno_fallecido;
                        x.espad_vdni_fallecido = Obe.cntc_vdni_fallecido;
                        x.espad_sfecha_nac_fallecido = Obe.cntc_sfecha_nac_fallecido;
                        x.espad_sfecha_fallecido = Obe.cntc_sfecha_fallecimiento;
                        x.espad_sfecha_entierro = Obe.cntc_sfecha_entierro;
                        x.espad_inacionalidad = Obe.cntc_inacionalidad;
                        x.espad_icod_iestado = 16; // ocupado
                        x.cntc_icod_contrato = icodContrato;
                        x.espad_icod_isituacion = 14; //con contrato
                        new BVentas().modificarEspaciosDetConsultas(x);
                    });

                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarContratoFallecido(Obe);
                }

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.cntc_icod_contrato_fallecido);
                    this.Close();
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }



        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            using (FrmListarContrato frm = new FrmListarContrato())
            {

                frm.ingresaCliente = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnContrato.Tag = frm._Be.cntc_icod_contrato;
                    btnContrato.Text = frm._Be.cntc_vnumero_contrato;
                    icodContrato = frm._Be.cntc_icod_contrato;
                    icodEspacio = frm._Be.espac_iid_iespacios;
                    lkpNombreIEC.EditValue = frm._Be.cntc_icod_vendedor;

                    obeContrato = new BVentas().listarContratoPorIcod(frm._Be.cntc_icod_contrato);
                    SetValuesContrato();
                }
            }
        }

        private void btnNiveles_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmListarNiveles frm = new FrmListarNiveles())
                {
                    frm.icodcontrato = icodContrato;
                    frm.icodEspacio = Convert.ToInt32(btnEspacios.Tag);
                    frm.icodPlataforma = Convert.ToInt32(lkpPlataforma.EditValue);
                    frm.icodManzana = Convert.ToInt32(lkpManzana.EditValue);
                    frm.icodNroSepultura = Convert.ToInt32(bteSepultura.Tag);
                    frm.fromUbicaciones = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnNiveles.Tag = frm._Be.espad_iid_iespacios;
                        btnNiveles.Text = frm._Be.espad_vnivel;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpTipoDeceso_Click(object sender, EventArgs e)
        {

        }

        private void lkpTipoDeceso_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEspacios_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarSepultura frm = new FrmListarSepultura())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnEspacios.Tag = frm._Be.espac_iid_iespacios;
                        btnEspacios.Text = frm._Be.espac_icod_vespacios;

                        //CheckBox[] textoCantidad = GetTextoCantidad();
                        List<EEspaciosDet> lstNivelesDet = new List<EEspaciosDet>();
                        lstNivelesDet = new BVentas().listarEspaciosDet(frm._Be.espac_iid_iespacios);
                        if (lstNivelesDet.Count > 0)
                        {
                            lkpPlataforma.EditValue = frm._Be.espac_icod_iplataforma;
                            lkpPlataforma.Text = frm._Be.strplataforma;
                            lkpManzana.EditValue = frm._Be.espac_icod_imanzana;
                            //lkpManzana.Text = frm._Be.strmanzana;
                            bteSepultura.Tag = frm._Be.espac_isepultura;
                            bteSepultura.Text = frm._Be.strsepultura;
                            //int count = 0;

                            //lstNivelesDet.ForEach(x =>
                            //{
                            //    if (x.espad_icod_isituacion == 13)
                            //    {
                            //        textoCantidad[count].Enabled = true;
                            //        icodEspacioDet[count] = x.espad_iid_iespacios;
                            //    }
                            //    else
                            //    {
                            //        textoCantidad[count].Enabled = false;

                            //    }

                            //    count++;
                            //});

                        }

                        //TotalNivel = lstNivelesDet.Count.ToString();
                        //txtCapacTotal.Text = TotalNivel;
                        //Niveles();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteSepultura_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarTablaSepultura frm = new FrmListarTablaSepultura())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteSepultura.Tag = frm._Be.tabvd_iid_tabla_venta_det;
                        bteSepultura.Text = frm._Be.tabvd_vdescripcion;

                        //CheckBox[] textoCantidad = GetTextoCantidad();
                        List<EEspacios> lstNiveles = new List<EEspacios>();
                        lstNiveles = new BVentas().listarEspacios().Where(x => x.espac_isepultura == Convert.ToInt32(bteSepultura.Tag)).ToList();
                        if (lstNiveles.Count > 0)
                        {
                            lkpPlataforma.EditValue = lstNiveles[0].espac_icod_iplataforma;
                            lkpPlataforma.Text = lstNiveles[0].strplataforma;
                            lkpManzana.EditValue = lstNiveles[0].espac_icod_imanzana;
                            lkpManzana.Text = lstNiveles[0].strmanzana;
                            //int count = 0;

                            //for (int i = 0; i < lstNiveles.Count; i++)
                            //{
                            //    textoCantidad[i].Enabled = true;

                            //}

                        }

                        //TotalNivel = lstNiveles.Count.ToString();
                        //txtCapacTotal.Text = TotalNivel;
                        //bteSepultura.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {

            if (XtraMessageBox.Show("¿Esta seguro que desea Limpiar el Espacio, se eliminara toda relacion?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                List<EEspaciosDet> lstEpaDet = new BVentas().listarEspaciosDet(Convert.ToInt32(btnEspacios.Tag));
                lstEpaDet.ForEach(x =>
                {
                    if (x.cntc_icod_contrato == Obe.cntc_icod_contrato)
                    {
                        x.espad_icod_isituacion = 13;
                        x.cntc_icod_contrato = 0;
                        new BVentas().modificarEspaciosDet(x);
                    }
                    //icodEspacioDet[count] = 0;

                });
                lkpPlataforma.EditValue = 0;
                lkpPlataforma.Text = "";
                lkpManzana.EditValue = 0;
                lkpManzana.Text = "";
                bteSepultura.Tag = 0;
                bteSepultura.Text = "";
                btnEspacios.Tag = 0;
                btnEspacios.Text = "";
                btnEspacios.Enabled = true;
                btnNiveles.Text = "";
                btnNiveles.Tag = 0;
            }
        }

        //private CheckBox[] GetTextoCantidad()
        //{
        //    CheckBox[] texto = new CheckBox[] { ckbNivel1, ckbNivel2, ckbNivel3, ckbNivel4, ckbNivel5, ckbNivel6 };
        //    return texto;
        //}
    }
}