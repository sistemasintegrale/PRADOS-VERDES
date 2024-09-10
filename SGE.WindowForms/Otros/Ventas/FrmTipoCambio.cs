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
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Tesoreria.Ventas;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmTipoCambio : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        List<EParametro> lstParametro = new List<EParametro>();
       
        public FrmTipoCambio()
        {
            InitializeComponent();
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
           
        }

      

      

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            lstParametro = new BAdministracionSistema().listarParametro();
            txtTipoCambio.Text = lstParametro[0].pm_ntipo_cambio.ToString();
        }

       


        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lstParametro[0].pm_ntipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
            new BAdministracionSistema().modificarParametro(lstParametro[0]);
            this.Close();
        }

        

      
    }
}