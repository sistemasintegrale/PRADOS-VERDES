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
using SGE.Entity;
using SGE.BusinessLogic;
using SGI.WindowsForm;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteDetalleEntregaFormulario : DevExpress.XtraEditors.XtraForm
    {
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EEntregaFormulario obj = new EEntregaFormulario();
        public List<EEntregaFormulario> lstobj = new List<EEntregaFormulario>();
        public FrmManteDetalleEntregaFormulario()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            txtserie.Text = new BVentas().listarRegistroParametro()[0].rgpmc_vserie_contrato;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            txtnumero.Text = obj.entf_vnumero_formulario.Substring(4);
            txtserie.Text = obj.entf_vnumero_formulario.Substring(0, 4);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setsave();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        public void setsave()
        {
            BaseEdit oBase = null;
            try
            {

                if (string.IsNullOrEmpty(txtserie.Text))
                {
                    oBase = txtserie;
                    throw new ArgumentException("Ingrese Serie de Formato");
                }

                if (txtserie.Text != "D003" && txtserie.Text != "0002")
                {
                    oBase = txtserie;
                    throw new ArgumentException("La Serie debe ser D003 o 0002");
                }

                if (String.IsNullOrEmpty(txtnumero.Text))
                {
                    oBase = txtnumero;
                    throw new ArgumentException("Ingrese Número de Formato");
                }

                bool existe;
                string mensaje;
               new BVentas().ExistenciaSerieEnEntregas(string.Format("{0}{1}", txtserie.Text, txtnumero.Text), out existe, out mensaje);

                if (existe)
                {
                    oBase = txtnumero;
                    throw new ArgumentException(mensaje);
                }


                existe = new BVentas().ObtenerExistenciaSerie(string.Format("{0}{1}", txtserie.Text, txtnumero.Text));
                if (existe)
                {
                    if (XtraMessageBox.Show("El Número de Formato ya está \"EN USO\", ¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    {
                        return;
                    }

                    obj.entf_icod_estado = 7442;
                }
                else
                    obj.entf_icod_estado = 7441;


                existe = lstobj.Exists(x => x.entf_vnumero_formulario == string.Format("{0}{1}", txtserie.Text, txtnumero.Text));
                if (existe && Status != BSMaintenanceStatus.ModifyCurrent)
                {
                    oBase = txtnumero;
                    throw new ArgumentException("El Formato ya se Encuentra Registrado en el Formulario");
                }

                obj.entf_vnumero_formulario = string.Format("{0}{1}", txtserie.Text, txtnumero.Text);

                this.DialogResult = DialogResult.OK;
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

        private void FrmManteDetalleEntregaFormulario_Load(object sender, EventArgs e)
        {

        }
    }
}