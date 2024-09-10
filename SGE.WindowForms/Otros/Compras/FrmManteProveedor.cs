using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SGE.Common.ApiSunat;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteProveedor : DevExpress.XtraEditors.XtraForm
    {
        public List<EUbicacion> lUbicacion = null;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteProveedor));
        public delegate void DelegadoMensaje(int idProveedor);
        public event DelegadoMensaje MiEvento;
        bool dnivalid;
        bool rucvalid;
        public FrmManteProveedor()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        public List<EProveedor> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BCompras Obl;
        public int CodeProveedor = 0;
        public int Correlative = 0;
        public int anac_icod_analitica = 0;
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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtcodigo.Enabled = !Enabled;
            txtRazonSocial.Enabled = !Enabled;
            txtComercial.Enabled = !Enabled;
            txtruc.Enabled = !Enabled;
            txtNombre.Enabled = !Enabled;
            txtPaterno.Enabled = !Enabled;
            txtMaterno.Enabled = !Enabled;
            txtdireccion.Enabled = !Enabled;
            LkpDistrito.Enabled = !Enabled;
            LkpDepartamento.Enabled = !Enabled;
            dtFecha.Enabled = !Enabled;
            txtcorreo.Enabled = !Enabled;
            LkpProvincia.Enabled = !Enabled;
            lkpTipDoc.Enabled = !Enabled;
            txtdni.Enabled = !Enabled;
            LkpPais.Enabled = !Enabled;
            txttelefono.Enabled = !Enabled;
            txtfax.Enabled = !Enabled;
            txtrepresentante.Enabled = !Enabled;
            LkpTipoPersona.Enabled = !Enabled;
            LkpEstado.Enabled = !Enabled;
            txtCtaBancoNacion.Enabled = !Enabled;
            txtModalidadPago.Enabled = !Enabled;
            txtBanco.Enabled = !Enabled;
            txtCuentaBancaria.Enabled = !Enabled;
            txtCtaInterbancaria.Enabled = !Enabled;
            txtcodigo.Focus();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtcodigo.Enabled = Enabled;

        }

        private void clearControl()
        {
            txtcodigo.Text = "";
            txtRazonSocial.Text = "";
            txtComercial.Text = "";
            dtFecha.EditValue = DateTime.Now;
            lkpTipDoc.EditValue = null;
            txtruc.Text = "";
            txtNombre.Text = "";
            txtcorreo.Text = "";
            txtPaterno.Text = "";
            txtMaterno.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";
            txtdni.Text = "";
            txtfax.Text = "";
            txtrepresentante.Text = "";
            LkpTipoPersona.EditValue = null;
            LkpEstado.EditValue = null;
        }

        private void cargar()
        {
            BSControls.LoaderLook(LkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            BSControls.LoaderLook(LkpTipoPersona, new BGeneral().listarTablaRegistro(22), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipDoc, new BGeneral().listarTablaRegistro(23).Where(ob => ob.tarec_icorrelativo_registro != 3).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            //BSControls.LoaderLook(lkpPaisesNoDomic, new BGeneral().listarTablaRegistro(87), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);

            int index = new BGeneral().listarTablaRegistro(87).FindIndex(x => x.tarec_iid_tabla_registro == 639);
            BSControls.LoaderLook(lkpPaisesNoDomic, new BGeneral().listarTablaRegistro(87).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpPaisesNoDomic.ItemIndex = index;
            /**/
            BSControls.LoaderLook(LkpPais, lUbicacion.Where(obj => obj.tablc_iid_tipo_ubicacion == 4).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
            //BSControls.LoaderLook(LkpEmpresaRelacionada, new BEmpresaRelacionada().ListarEmpresaRelacionada(), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
        }

        private void FrmManteProveedor_Load(object sender, EventArgs e)
        {
            lUbicacion = new BVentas().ListarUbicacion();
            cargar();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            LkpEstado.ItemIndex = 0;
            LkpTipoPersona.ItemIndex = 0;
            lkpTipDoc.ItemIndex = 0;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            if (Convert.ToInt32(LkpTipoPersona.EditValue) == 2)
            {
                txtNombre.Enabled = false;
                txtPaterno.Enabled = false;
                txtMaterno.Enabled = false;
            }
            else if (Convert.ToInt32(LkpTipoPersona.EditValue) == 3)
            {
                txtNombre.Enabled = true;
                txtPaterno.Enabled = true;
                txtMaterno.Enabled = true;
                txtruc.Enabled = true;

            }
            else
            {
                txtRazonSocial.Enabled = true;
                txtComercial.Enabled = true;
                txtruc.Enabled = true;
                txtNombre.Enabled = false;
                txtPaterno.Enabled = false;
                txtMaterno.Enabled = false;
            }
        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void LkpTipoPersona_EditValueChanged(object sender, EventArgs e)
        {
            if (LkpTipoPersona.EditValue != null)
            {
                lblNombre.Text = string.Empty;
                if (Convert.ToInt32(LkpTipoPersona.EditValue) == 1)
                {
                    txtRazonSocial.Enabled = false;
                    txtComercial.Enabled = false;
                    txtruc.Enabled = true;
                    txtNombre.Enabled = true;
                    txtPaterno.Enabled = true;
                    txtMaterno.Enabled = true;
                    //txtdni.Enabled = true;
                    txtcodigo.Focus();
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        txtNombre.Text = "";
                        txtPaterno.Text = "";
                        txtMaterno.Text = "";
                        txtRazonSocial.Text = "";
                        txtComercial.Text = "";
                        txtruc.Text = "";
                        txtdni.Text = "";
                    }
                }
                else if (Convert.ToInt32(LkpTipoPersona.EditValue) == 2)
                {
                    if (Status != BSMaintenanceStatus.View)
                    {
                        txtRazonSocial.Enabled = true;
                        txtComercial.Enabled = true;
                        txtruc.Enabled = true;
                        txtNombre.Enabled = false;
                        txtPaterno.Enabled = false;
                        txtMaterno.Enabled = false;
                        //txtdni.Enabled = false;
                        txtcodigo.Focus();
                        if (Status == BSMaintenanceStatus.CreateNew)
                        {
                            txtNombre.Text = "";
                            txtPaterno.Text = "";
                            txtMaterno.Text = "";
                            txtRazonSocial.Text = "";
                            txtComercial.Text = "";
                            txtruc.Text = "";
                            txtdni.Text = "";
                        }
                    }
                }
                else if (Convert.ToInt32(LkpTipoPersona.EditValue) == 3)
                {
                    if (Status != BSMaintenanceStatus.View)
                    {
                        txtRazonSocial.Enabled = true;
                        txtComercial.Enabled = true;
                        txtruc.Enabled = true;
                        txtNombre.Enabled = true;
                        txtPaterno.Enabled = true;
                        txtMaterno.Enabled = true;
                        //txtdni.Enabled = false;
                        txtcodigo.Focus();
                        if (Status == BSMaintenanceStatus.CreateNew)
                        {
                            txtNombre.Text = "";
                            txtPaterno.Text = "";
                            txtMaterno.Text = "";
                            txtRazonSocial.Text = "";
                            txtComercial.Text = "";
                            txtruc.Text = "";
                            txtdni.Text = "";
                        }
                    }
                }
                else if (Convert.ToInt32(LkpTipoPersona.EditValue) == 4)
                {
                    txtRazonSocial.Enabled = true;
                    txtComercial.Enabled = true;
                    txtruc.Enabled = true;
                    txtNombre.Enabled = false;
                    txtPaterno.Enabled = false;
                    txtMaterno.Enabled = false;
                    //txtdni.Enabled = true;
                    txtcodigo.Focus();
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        txtNombre.Text = "";
                        txtPaterno.Text = "";
                        txtMaterno.Text = "";
                        txtRazonSocial.Text = "";
                        txtComercial.Text = "";
                        txtruc.Text = "";
                        txtdni.Text = "";
                    }

                }
                //}
            }

        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EProveedor oBe = new EProveedor();
            EAnaliticaDetalle objE_AnaliticaDetalle = new EAnaliticaDetalle();
            Obl = new BCompras();
            try
            {
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (string.IsNullOrEmpty(txtcodigo.Text))
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("Ingresar Codigo");
                    }
                }
                if (dtFecha.EditValue == null)
                {
                    oBase = dtFecha;
                    throw new ArgumentException("Ingresar Fecha");
                }
                if (Convert.ToInt32(LkpTipoPersona.EditValue) == 1)
                {
                    if (string.IsNullOrEmpty(txtNombre.Text))
                    {
                        oBase = txtNombre;
                        throw new ArgumentException("Ingresar Nombres");
                    }

                    if (string.IsNullOrEmpty(txtPaterno.Text))
                    {
                        oBase = txtPaterno;
                        throw new ArgumentException("Ingresar Apellido Paterno");
                    }

                    if (txtdni.Text.Length < 8)
                    {
                        oBase = txtdni;
                        throw new ArgumentException("La Longitud del dni debe ser igual a 8");
                    }
                }
                else if (Convert.ToInt32(LkpTipoPersona.EditValue) == 2 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3)
                {
                    if (string.IsNullOrEmpty(txtRazonSocial.Text))
                    {
                        oBase = txtRazonSocial;
                        throw new ArgumentException("Ingresar Razon Social");
                    }

                    if (string.IsNullOrEmpty(txtruc.Text))
                    {
                        oBase = txtruc;
                        throw new ArgumentException("Ingresar Ruc");
                    }

                    if (txtruc.Text.Length < 11)
                    {
                        oBase = txtruc;
                        throw new ArgumentException("La Longitud del Ruc debe ser igual a 12");
                    }
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCodigo = oDetail.Where(oB => oB.vcod_proveedor.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (BuscarCodigo.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo Existe");
                    }
                    if (Convert.ToInt32(LkpTipoPersona.EditValue) == 1)
                    {
                        var BuscarDescripcion = oDetail.Where(oB => oB.vnombrecompleto.ToUpper() == txtRazonSocial.Text.ToUpper()).ToList();
                        if (BuscarDescripcion.Count > 0)
                        {
                            oBase = txtRazonSocial;
                            throw new ArgumentException("La Razon Social Existe");
                        }
                    }
                }
                if (lkpTipDoc.EditValue != null)
                {
                    if (Convert.ToInt32(lkpTipDoc.EditValue) == 2)
                    {
                        if (txtdni.Text == "" || txtdni.Text.Length != 8)
                        {
                            oBase = txtdireccion;
                            throw new ArgumentException("Ingresar DNI");
                        }
                    }
                }

                objE_AnaliticaDetalle.anad_icod_analitica = anac_icod_analitica;
                objE_AnaliticaDetalle.anad_iid_analitica = txtcodigo.Text;
                objE_AnaliticaDetalle.anad_vdescripcion = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1) ? txtPaterno.Text + " " + txtMaterno.Text + " " + txtNombre.Text : txtRazonSocial.Text;
                objE_AnaliticaDetalle.intUsuario = Valores.intUsuario;
                objE_AnaliticaDetalle.tarec_icorrelativo_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                objE_AnaliticaDetalle.anad_nombre = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3) ? txtNombre.Text : "";
                objE_AnaliticaDetalle.anad_apepaterno = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3) ? txtPaterno.Text : "";
                objE_AnaliticaDetalle.anad_apematerno = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3) ? txtMaterno.Text : "";
                objE_AnaliticaDetalle.tarec_icorrelativo_tipo_persona = Convert.ToInt32(LkpTipoPersona.EditValue);

                oBe.iid_icod_proveedor = Correlative;
                oBe.iid_proveedor = Correlative;
                oBe.vcod_proveedor = txtcodigo.Text;
                oBe.vnombre = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3) ? txtNombre.Text : "";
                oBe.vpaterno = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3) ? txtPaterno.Text : "";
                oBe.vmaterno = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3) ? txtMaterno.Text : "";

                oBe.vnombrecompleto = (Convert.ToInt32(LkpTipoPersona.EditValue) == 1) ? txtPaterno.Text + " " + txtMaterno.Text + " " + txtNombre.Text : txtRazonSocial.Text;
                oBe.iid_tipo_persona = Convert.ToInt32(LkpTipoPersona.EditValue);
                oBe.vcomercial = txtComercial.Text;
                oBe.vdireccion = txtdireccion.Text;
                oBe.proc_sfecha = Convert.ToDateTime(dtFecha.EditValue);
                if (Convert.ToInt32(lkpPaisesNoDomic.EditValue) == 0)
                {
                    oBe.proc_pais_nodomic = 0;
                }
                else
                {
                    oBe.proc_pais_nodomic = Convert.ToInt32(lkpPaisesNoDomic.EditValue);
                }
                oBe.proc_tipo_doc = Convert.ToInt32(lkpTipDoc.EditValue);
                oBe.proc_vcorreo = txtcorreo.Text;
                if (LkpDistrito.EditValue != null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpDistrito.EditValue);
                if (LkpProvincia.EditValue != null & LkpDistrito.EditValue == null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpProvincia.EditValue);
                if (LkpDepartamento.EditValue != null & LkpProvincia.EditValue == null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpDepartamento.EditValue);
                if (LkpPais.EditValue != null & LkpDepartamento.EditValue == null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpPais.EditValue);
                if (LkpPais.EditValue == null)
                    oBe.ubicc_icod_ubicacion = null;


                oBe.vtelefono = txttelefono.Text;
                oBe.vdni = txtdni.Text;
                oBe.vfax = txtfax.Text;
                oBe.isituacion = Convert.ToInt32(LkpEstado.EditValue);
                oBe.vrepresentante = txtrepresentante.Text;
                oBe.vruc = txtruc.Text;
                //oBe.tablc_iid_tipo_relacion = Convert.ToInt32(LkpEmpresaRelacionada.EditValue); 
                oBe.iid_usuario_crea = Valores.intUsuario;
                oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.iid_usuario_modifica = Valores.intUsuario;
                oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.iactivo = Convert.ToInt32(LkpEstado.EditValue);
                oBe.anac_icod_analitica = anac_icod_analitica;
                oBe.proc_vcta_bco_nacion = txtCtaBancoNacion.Text.Trim();
                oBe.proc_vmodalidad_pago = txtModalidadPago.Text;
                oBe.proc_vbanco_pago = txtBanco.Text;
                oBe.proc_vcuenta_corriente_banco = txtCuentaBancaria.Text;
                oBe.proc_vcodigo_interbancario = txtCtaInterbancaria.Text;
                if (string.IsNullOrEmpty(txtruc.Text))
                {
                    oBase = txtruc;
                    throw new ArgumentException("Ingresar RUC");
                }

                if (txtruc.Text.Length != 11)
                {
                    oBase = txtruc;
                    throw new ArgumentException("El RUC debe ser de 11 Caracteres");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.iid_icod_proveedor = Obl.InsertarProveedor(objE_AnaliticaDetalle, oBe);
                }
                else
                {
                    oBe.iid_icod_proveedor = Correlative;
                    Obl.ModificarProveedor(objE_AnaliticaDetalle, oBe);
                }
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.iid_icod_proveedor);
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtRazonSocial_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(LkpTipoPersona.EditValue) == 2 || Convert.ToInt32(LkpTipoPersona.EditValue) == 3)
            {
                txtNombre.Text = "";
                txtPaterno.Text = "";
                txtMaterno.Text = "";
            }

            if (e.KeyValue == (char)Keys.Escape)
            {
                cerrar_form(sender, e);
            }
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(LkpTipoPersona.EditValue) == 1)
            {
                txtRazonSocial.Text = "";
                txtComercial.Text = "";
            }

        }

        private void LkpPais_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpDepartamento, lUbicacion.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpPais.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void LkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpProvincia, lUbicacion.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpDepartamento.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void LkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpDistrito, lUbicacion.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpProvincia.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipDoc.ItemIndex == 2)
            {
                txtdni.Enabled = false;
                txtdni.Text = "";
            }
            else
            {
                txtdni.Enabled = true;
                txtdni.Text = "";
            }
        }




        public void setUbicacion(int codigo)
        {
            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == codigo).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    LkpPais.EditValue = codigo;
                    break;
                case 3:
                    LkpPais.EditValue = padre;
                    LkpDepartamento.EditValue = codigo;
                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                    });
                    LkpPais.EditValue = abuelo;
                    LkpDepartamento.EditValue = padre;
                    LkpProvincia.EditValue = codigo;
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                    });

                    LkpPais.EditValue = bisabuelo;
                    LkpDepartamento.EditValue = abuelo;
                    LkpProvincia.EditValue = padre;
                    LkpDistrito.EditValue = codigo;
                    break;
            }
        }
        bool DniValid = true;
        private async void txtdni_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipDoc.EditValue) != 1) return;
            if (txtdni.Text.Trim().Length != 8) return;

            DniValid = false;
            lblNombre.Text = "Obteniendo Datos...";
            var result2 = Services.ConsultaDni(txtdni.Text);
            if (result2.Item2 == ErrorMessage.NoAutorizado)
            {
                lblNombre.Text = string.Empty;
                return;
            }


            if (result2.Item1 != null)
            {
                lblNombre.Text = $"{result2.Item1.nombre_completo}";
                var nombres = result2.Item1.nombre_completo.Split(' ');
                txtMaterno.Text = nombres[1].Trim();
                txtPaterno.Text = nombres[0].Trim();
                txtRazonSocial.Text = "";
                txtNombre.Text = "";
                for (int i = 0; i < nombres.Count(); i++)
                {
                    txtRazonSocial.Text = txtRazonSocial.Text + nombres[i].Trim() + " ";
                    if (i >= 2)
                    {
                        txtNombre.Text = txtNombre.Text + nombres[i].Trim() + " ";

                    }
                }
                DniValid = true;

            }
            if (result2.Item2 == ErrorMessage.NoEncontrado)
            {
                var result1 = await Services.ConsultaDni2(txtdni.Text.Trim());
                if (result1.Item1 == null)
                {
                    lblNombre.Text = string.Empty;
                    DniValid = true;
                    return;
                }


                lblNombre.Text = $"{result1.Item1.nombre}";
                var nombres = result1.Item1.nombre.Split(' ');
                txtMaterno.Text = nombres[1].Trim();
                txtPaterno.Text = nombres[0].Trim();
                txtRazonSocial.Text = "";
                txtNombre.Text = "";
                for (int i = 0; i < nombres.Count(); i++)
                {
                    txtRazonSocial.Text = txtRazonSocial.Text + nombres[i].Trim() + " ";
                    if (i >= 2)
                    {
                        txtNombre.Text = txtNombre.Text + nombres[i].Trim() + " ";

                    }
                }
                DniValid = true;
            }

        }

        bool RucValid = true;

        private void txtruc_EditValueChanged(object sender, EventArgs e)
        {
            if (txtruc.Text.Trim().Length != 11) return;
            var resultado = Services.ConsultaRuc(txtruc.Text.Trim());
            if (resultado == null) { RucValid = false; return; }
            RucValid = true;
            txtRazonSocial.Text = resultado.nombre_o_razon_social;
            txtdireccion.Text = resultado.direccion_completa;
            txtruc.Text = resultado.ruc;
            //txtUbigeo.Text = resultado.ubigeo;
            EUbicacion ubicacion = new EUbicacion();
            if (resultado.distrito != "-")
            {
                ubicacion = lUbicacion.Where(x => x.ubicc_vnombre_ubicacion.ToUpper() == resultado.distrito.ToUpper()).FirstOrDefault();
                if (ubicacion == null)
                {
                    Services.MessageInfo($"El Distrito {resultado.distrito} no se Encuentra Registrado en el Sistema");
                    return;
                }
                setUbicacion(ubicacion.ubicc_icod_ubicacion);
            }

            else if (resultado.provincia != "-")
            {
                ubicacion = lUbicacion.Where(x => x.ubicc_vnombre_ubicacion.ToUpper() == resultado.provincia.ToUpper()).FirstOrDefault();
                if (ubicacion == null)
                {
                    Services.MessageInfo($"La Provincia {resultado.provincia} no se Encuentra Registrado en el Sistema");
                    return;
                }
                setUbicacion(ubicacion.ubicc_icod_ubicacion);
            }

            else if (resultado.departamento != "-")
            {
                ubicacion = lUbicacion.Where(x => x.ubicc_vnombre_ubicacion.ToUpper() == resultado.departamento.ToUpper()).FirstOrDefault();
                if (ubicacion == null)
                {
                    Services.MessageInfo($"El Departamento {resultado.departamento} no se Encuentra Registrado en el Sistema");
                    return;
                }
                setUbicacion(ubicacion.ubicc_icod_ubicacion);
            }
        }

        private void txtdni_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdni.Text))
            {
                if (Convert.ToInt32(lkpTipDoc.EditValue) == 1)
                {

                    if (txtdni.Text.Trim().Length != 8)
                    {
                        XtraMessageBox.Show("Por favor, ingrese número de DNI válido de 8 Carácteres", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtdni.Focus();
                        return;
                    }
                    if (!dnivalid)
                    {
                        txtdni.Focus();
                        XtraMessageBox.Show("Por favor, ingrese número de DNI válido", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }

        }

        private void txtruc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtruc.Text))
            {
                if (txtruc.Text.Trim().Length != 11)
                {
                    XtraMessageBox.Show("Por favor, ingrese número de RUC válido de 11 Carácteres", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtruc.Focus();
                    return;
                }
                if (!rucvalid)
                {
                    txtruc.Focus();
                    XtraMessageBox.Show("Por favor, ingrese número de RUC válido", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}