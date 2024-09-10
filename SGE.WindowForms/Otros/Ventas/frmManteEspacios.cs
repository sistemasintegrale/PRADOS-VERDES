using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;



namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteEspacios : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteEspacios));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EEspacios Obe = new EEspacios();
        public List<EEspacios> lstEspacios = new List<EEspacios>();
        public List<EEspaciosDet> lstDetalle = new List<EEspaciosDet>();
        public List<EEspaciosDet> lstDelete = new List<EEspaciosDet>();

        public frmManteEspacios()
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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtCodigo.Enabled = !Enabled;
            lkpPlataforma.Enabled = !Enabled;
            lkpManzana.Enabled = !Enabled;
            bteSepultura.Enabled = !Enabled;
            btnGenerar.Enabled = !Enabled;
            chkNiveles.Enabled = !Enabled;
            NIvel1.Enabled = !Enabled;
            Nivel2.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {


                List<EEspaciosDet> lstEspacioDetSit = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_isituacion == 14).ToList();
                if (lstEspacioDetSit.Count > 0)
                {
                    //throw new ArgumentException(String.Format("El Espacio no puede ser Modificado, su Situacion es CON CONTRATO"));
                    txtCodigo.Enabled = Enabled;
                    lkpPlataforma.Enabled = Enabled;
                    lkpManzana.Enabled = Enabled;
                    bteSepultura.Enabled = Enabled;
                    btnGenerar.Enabled = Enabled;
                }
                else
                {
                    txtCodigo.Enabled = Enabled;
                    lkpPlataforma.Enabled = !Enabled;
                    lkpManzana.Enabled = !Enabled;
                    bteSepultura.Enabled = !Enabled;
                    btnGenerar.Enabled = Enabled;
                }

            }
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
            
           
        }
        public void setValues()
        
        {
            txtCodigo.Text = string.Format("{0:000000}", Obe.espac_icod_vespacios);
            lkpPlataforma.EditValue = Obe.espac_icod_iplataforma;
            lkpManzana.EditValue = Obe.espac_icod_imanzana;
            bteSepultura.Tag = Obe.espac_isepultura;
            bteSepultura.Text = Obe.strsepultura;

            lstDetalle = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios);
            grdDetalle.DataSource = lstDetalle;
            //lkpNivel.EditValue = Obe.espac_icod_inivel;
            //lkpSituacion.EditValue = Obe.espac_icod_isituacion;
            //lkpEstado.EditValue = Obe.espac_icod_iestado;
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            NIvel1.Enabled = false;
            Nivel2.Enabled = false;           
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            chkNiveles.Enabled = false;
            NIvel1.Enabled = false;
            Nivel2.Enabled = false;
        }
       
        private void cargar()
        {
            BSControls.LoaderLook(lkpPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpManzana, new BGeneral().listarTablaVentaDet(5), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            //BSControls.LoaderLook(lkpNivel, new BGeneral().listarTablaVentaDet(6), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            //BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(10), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            //BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaVentaDet(11), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            

        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            
            try
            {               
                /*----------------------*/
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código de la Funeraria");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    if (verificarCodigoFuneraria(txtCodigo.Text))
                 {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de las funerarias");
                 }

                }

                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    string codigo = string.Format("{0}-{1}-{2}-{3}", Convert.ToInt32(lkpPlataforma.EditValue), Convert.ToInt32(lkpManzana.EditValue), Convert.ToInt32(bteSepultura.Tag), Convert.ToInt32(lkpNivel.EditValue));
                //    List<EEspacios> lstEspacios = new BVentas().listarEspacios().Where(x => x.codigo == codigo).ToList();

            //    if (lstEspacios.Count > 0)
            //    {
            //        oBase = bteSepultura;
            //        throw new ArgumentException("El Registro de Sepulturas ya Existe");
            //    }

            //}


            /*----------------------*/

                Obe.espac_icod_vespacios = txtCodigo.Text;
                Obe.espac_icod_iplataforma = Convert.ToInt32(lkpPlataforma.EditValue);
                Obe.espac_icod_imanzana = Convert.ToInt32(lkpManzana.EditValue);
                Obe.espac_isepultura = Convert.ToInt32(bteSepultura.Tag);
                Obe.strsepultura = bteSepultura.Text;
                //Obe.espac_icod_inivel = Convert.ToInt32(lkpNivel.EditValue);
                //Obe.espac_icod_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                //Obe.espac_icod_iestado = Convert.ToInt32(lkpEstado.EditValue);              
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //if (chkNiveles.CheckState == CheckState.Checked)
                    //{
                    //    if (XtraMessageBox.Show("Desea Generar Codigos de Nivel del " + NIvel1.Text + " al " + Nivel2.Text, "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    //    {
                    //        for (int y = Convert.ToInt32(NIvel1.Text); y <= Convert.ToInt32(Nivel2.Text); y++)
                    //        {
                               
                    //            x[6] = y;
                    //            List<ETablaVentaDet> mlistaTalla = new List<ETablaVentaDet>();
                    //            mlistaTalla = new BGeneral().listarTablaVentaDet(6);
                    //            var Lista = mlistaTalla.Where(ob => ob.tabvd_vdescripcion == (Convert.ToInt32(x[6])).ToString()).ToList();
                    //            if (Lista.Count == 1)
                    //            {
                    //                string codigo = string.Format("{0}-{1}-{2}-{3}", Obe.espac_icod_iplataforma, Obe.espac_icod_imanzana, Obe.espac_isepultura, Lista[0].tabvd_iid_tabla_venta_det);
                    //                List<EEspacios> lstEspacios = new BVentas().listarEspacios().Where(x => x.codigo == codigo).ToList();
                    //                if (lstEspacios.Count == 0)
                    //                {
                    //                    Obe.espac_icod_inivel = Lista[0].tabvd_iid_tabla_venta_det;
                    //                    Obe.espac_icod_vespacios = Obe.espac_icod_vespacios;
                    //                    Obe.espac_iid_iespacios = new BVentas().insertarEspacios(Obe);
                    //                    Obe.espac_icod_vespacios = String.Format("{0:000000}", Convert.ToInt32(Obe.espac_icod_vespacios) + 1);
                    //                }  
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                        Obe.espac_iid_iespacios = new BVentas().insertarEspacios(Obe,lstDetalle);

                }
                else
                {
                    //if (chkNiveles.CheckState == CheckState.Checked)
                    //{
                    //    if (XtraMessageBox.Show("Desea Generar Codigos de talla del " + NIvel1.Text + " al " + Nivel2.Text, "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    //    {
                    //        for (int y = Convert.ToInt32(NIvel1.Text); y <= Convert.ToInt32(Nivel2.Text); y++)
                    //        {
                    //            x[6] = y;
                    //            List<ETablaVentaDet> mlistaTalla = new List<ETablaVentaDet>();
                    //            mlistaTalla = new BGeneral().listarTablaVentaDet(6);
                    //            var Lista = mlistaTalla.Where(ob => ob.tabvd_vdescripcion == (Convert.ToInt32(x[6])).ToString()).ToList();
                    //            if (Lista.Count == 1)
                    //            {
                    //                Obe.espac_icod_inivel = y;
                    //                Obe.espac_iid_iespacios = new BVentas().insertarEspacios(Obe);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        Obe.espac_iid_iespacios = Obe.espac_iid_iespacios;
                        new BVentas().modificarEspacios(Obe,lstDetalle,lstDelete);
                    //}
                }
                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    Obe.espac_iid_iespacios = new BVentas().insertarEspacios(Obe);
                //}
                //else if (Status == BSMaintenanceStatus.ModifyCurrent)
                //{
                //    new BVentas().modificarEspacios(Obe);
                //}
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
                    this.MiEvento(Obe.espac_iid_iespacios);
                    this.Close();
                }
            }
        }
        public int espacio_codigo { get; set; }
        int[] x = new int[8];
        private bool verificarCodigoFuneraria(string strCodigo)
        {
            try 
            {
                bool exists = false;
                if (lstEspacios.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstEspacios.Where(x => x.espac_icod_vespacios.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstEspacios.Where(x => x.espac_iid_iespacios.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.espac_icod_vespacios != Obe.espac_icod_vespacios).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteFuneraria_Load(object sender, EventArgs e)
        {
            cargar();
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
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkSerie_CheckedChanged(object sender, EventArgs e)
        {
            if(chkNiveles.Checked == true)
            {
                //lkpNivel.Enabled = false;
                NIvel1.Enabled = true;
                Nivel2.Enabled = true;
                btnGenerar.Enabled = true;
            }
            else
            {
                //lkpNivel.Enabled = true;
                NIvel1.Enabled = false;
                Nivel2.Enabled = false;
                btnGenerar.Enabled = false;
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {

                using (frmManteEspaciosDet frm = new frmManteEspaciosDet())
                {
                    if (lstDetalle.Count > 0)
                        frm.txtNivel.Text = String.Format("{0:0}", lstDetalle.Max(x=> Convert.ToInt32(x.espad_vnivel) + 1));
                    else
                        frm.txtNivel.Text = "1";
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        frm.txtNivel.Enabled = true;
                    }
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.Focus();
                    }
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
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEspaciosDet oBe_ = (EEspaciosDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteEspaciosDet frm = new frmManteEspaciosDet())
            {
                frm.oBe = oBe_;
                frm.SetModify();
                frm.lstDetalle = lstDetalle;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.Focus();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEspaciosDet oBe_ = (EEspaciosDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            try
            {
                if (Obe.espac_icod_isituacion == 14)
                {
                    throw new ArgumentException(String.Format("El Nivel no puede ser Eliminado, su Situacion es CON CONTRATO"));
                }

                lstDelete.Add(oBe_);
                lstDetalle.Remove(oBe_);
                viewDetalle.RefreshData();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            grdDetalle.DataSource = 0;
            for (int y = Convert.ToInt32(NIvel1.Text); y <= Convert.ToInt32(Nivel2.Text); y++)
            {
                EEspaciosDet EDet = new EEspaciosDet();
                EDet.espad_vnivel = y.ToString();
                EDet.espad_icod_isituacion = 13;
                EDet.strsituacion = "GENERADO";
                EDet.espad_icod_iestado = 15;
                EDet.strestado = "VACIO";
                EDet.intUsuario = Valores.intUsuario;
                EDet.strPc = WindowsIdentity.GetCurrent().Name;
                lstDetalle.Add(EDet);
            }
            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
            btnGenerar.Enabled = false;
        }
    }
}