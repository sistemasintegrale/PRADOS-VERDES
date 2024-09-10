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
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using SGE.Entity;
using SGE.WindowForms.Otros.Ventas;
using System.Windows.Forms.DataVisualization.Charting;


namespace SGE.WindowForms.Ventas.Asesores
{
    public partial class FrmVentasPorAsesorPorMes : DevExpress.XtraEditors.XtraForm
    {
        List<EProyeccionVendedor> lista = new List<EProyeccionVendedor>();
        public FrmVentasPorAsesorPorMes()
        {
            InitializeComponent();
        }

        private void FrmVentasPorAsesorPorMes_Load(object sender, EventArgs e)
        {
            var lstEjercicio = new BContabilidad().listarAnioEjercicio();

            BSControls.LoaderLook(lkpAnio, lstEjercicio, "anioc_iid_anio_ejercicio", "anioc_iid_anio_ejercicio", true);
            if (lstEjercicio.Where(x => x.anioc_iid_anio_ejercicio == DateTime.Now.Year).ToList().Count == 1)
                lkpAnio.EditValue = DateTime.Now.Year;

            var lstMeses = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList();

            BSControls.LoaderLook(lkpMes, lstMeses, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpMes.EditValue = lstMeses.Where(x => x.tarec_icorrelativo_registro == DateTime.Now.Month).FirstOrDefault().tarec_iid_tabla_registro;


            var lstAsesor = new BVentas().listarVendedor();

            lstAsesor.Add(new EVendedor() { vendc_vnombre_vendedor = "Todos...", vendc_icod_vendedor = 0 });

            BSControls.LoaderLook(lkpAsesor, lstAsesor, "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            lkpAsesor.EditValue = 0;
            cargar();
            grdLista.Height = (this.Height) / 2;
            chart1.Titles.Add("Ventas por Mes");
        }

        private void lkpAnio_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        private void lkpAsesor_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        void cargar()
        {
            lista = new BVentas().listar_informe_ventas_por_mes(Convert.ToInt32(lkpAnio.EditValue), Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpAsesor.EditValue));
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }

        private void barrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmbarras frm = new Frmbarras();
            frm.lista = lista;
            frm.ShowDialog();

        }

        private void viewLista_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            var obj = (EProyeccionVendedor)viewLista.GetRow(viewLista.FocusedRowHandle);

            if (obj == null)
                return;


            groupControl2.Text = $"Ventas de Mes de {lkpMes.Text} de : {obj.vendc_vnombre_vendedor}";
            string[] series = { "cant_necesidad_futura", "cant_necesidad_inmediata", "cant_credito", "cant_contado", "proyc_icantidad_estimada" };
            int[] puntos = { obj.cant_necesidad_futura, obj.cant_necesidad_inmediata, obj.cant_credito, obj.cant_contado, obj.proyc_icantidad_estimada };

            
            chart1.Series.Clear();
            for (int i = 0; i < series.Length; i++)
            {
                
                Series serie = chart1.Series.Add(series[i]);
                serie.Label = puntos[i].ToString();
                serie.Points.Add(puntos[i]);
            }
            chart1.Refresh();
            chart1.ResetAutoValues();
 
        }
    }
}