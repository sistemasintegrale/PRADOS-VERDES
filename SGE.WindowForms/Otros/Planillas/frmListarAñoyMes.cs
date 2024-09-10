using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmListarAñoyMes : DevExpress.XtraEditors.XtraForm
    {
        List<EFondosPensiones> lstAlmacenes = new List<EFondosPensiones>();
        public EFondosPensiones _Be { get; set; }
        public int mes;
        public int año;
        public int estadoMes;
        public frmListarAñoyMes()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {

            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = Convert.ToInt32(DateTime.Now.Month);
            txtAño.Text = Parametros.intEjercicio.ToString();


        }        
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            mes= Convert.ToInt32(lkpMes.EditValue);
            año = Convert.ToInt32(txtAño.Text);
            this.DialogResult = DialogResult.OK;
        }
      
        private void buscarCriterio()
        {
            
        }
       

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        void reload(int intIcod)
        {
           
        } 
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (estadoMes == 1)
            {
                txtAño.Enabled = true;
            }
            else
            {
                txtAño.Enabled = false;
            }

        }     
    }
}