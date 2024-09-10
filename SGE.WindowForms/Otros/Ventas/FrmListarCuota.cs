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
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmListarCuota : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        public List<EContratoCuotas> lstContrato = new List<EContratoCuotas>();
        public EContratoCuotas _Be { get; set; }
        public int cntc_icod_contrato;
        public bool flag_multiple = false;
        public int situacion = 0;
        public int modificar = 1;
        public int nuevo = 0;
        public bool View = false;
        public FrmListarCuota()
        {
            InitializeComponent();
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (flag_multiple)
            {
                view.MoveLast();
                view.MoveFirst();
            }
            bool valid = true;
            int posicion = 0;
            lstContrato.ForEach(Obe =>
            {
                //if (Obe.monto_pagar > (Obe.cntc_nsaldo + Obe.cntc_npagado))
                //{
                //    posicion = Obe.cntc_icod_contrato_cuotas;
                //    Obe.monto_pagar = 0;
                //    valid = false;
                //    new BVentas().modificarContratoCuotas(Obe);
                //    XtraMessageBox.Show("El monto del Pago Ingresado es Mayor al Monto del Saldo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}


                if (Obe.cntc_nmonto_mora_pago > Obe.cntc_nmonto_mora)
                {

                    posicion = Obe.cntc_icod_contrato_cuotas;
                    Obe.cntc_nmonto_mora_pago = 0;
                    valid = false;
                    new BVentas().modificarContratoCuotas(Obe);
                    XtraMessageBox.Show("El monto del Pago Mora Ingresado  es Mayor al Monto de la Mora", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            });

            if (!valid)
            {
                reload(posicion);
                txtMontoCuota.Text = (lstContrato.Sum(x => x.monto_pagar)).ToString();
                txtMontoMora.Text = (lstContrato.Sum(x => x.cntc_nmonto_mora_pago)).ToString();
                txtMontoTotal.Text = (Convert.ToDecimal(txtMontoCuota.Text) + Convert.ToDecimal(txtMontoMora.Text)).ToString();
                return;
            }

            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }


        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
            if (View)//Consulta
            {
                enableColumns(false);
            }
            else
            {
                enableColumns(true);
            }

        }

        void enableColumns(bool enable)
        {
            gridColumn9.OptionsColumn.AllowEdit = enable;
            gridColumn9.OptionsColumn.AllowFocus = enable;
            gridColumn9.OptionsColumn.AllowIncrementalSearch = enable;

            gridColumn11.OptionsColumn.AllowEdit = enable;
            gridColumn11.OptionsColumn.AllowFocus = enable;
            gridColumn11.OptionsColumn.AllowIncrementalSearch = enable;

            gridColumn1.OptionsColumn.AllowEdit = enable;
            gridColumn1.OptionsColumn.AllowFocus = enable;
            gridColumn1.OptionsColumn.AllowIncrementalSearch = enable;
        }

        private void cargar(bool cargar = false)
        {
            if (lstContrato.Count() == 0 || cargar)
            {
                lstContrato = new BVentas().listarContratoCuotas(cntc_icod_contrato).Where(x => (x.cntc_icod_situacion != 340) && (x.cntc_icod_situacion != 6437)).OrderBy(x => x.cntc_itipo_cuota).ToList();
                if (flag_multiple)
                {
                    lstContrato.ForEach(x =>
                    {
                        x.flag_multiple = false;
                    });
                }
            }

            txtMontoCuota.Text = (lstContrato.Sum(x => x.monto_pagar)).ToString();      
            txtMontoMora.Text = (lstContrato.Sum(x => x.cntc_nmonto_mora_pago)).ToString();
            txtMontoTotal.Text = (Convert.ToDecimal(txtMontoCuota.Text) + Convert.ToDecimal(txtMontoMora.Text)).ToString();
            dgr.DataSource = lstContrato;
            view.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstContrato.FindIndex(x => x.cntc_icod_contrato_cuotas == intIcod);
            view.FocusedRowHandle = index;
            view.Focus();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DgAcept();
        }
        bool validar()
        {
            bool result = true;
            lstContrato.ForEach(Obe =>
            {
                if (Obe.monto_pagar > (Obe.cntc_nsaldo + Obe.cntc_npagado))
                {
                    result = false;
                    XtraMessageBox.Show("El monto del Pago Ingresado es Mayor al Monto del Saldo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                if (Obe.cntc_nmonto_mora_pago > Obe.cntc_nmonto_mora)
                {
                    result = false;
                    Obe.cntc_nmonto_mora_pago = 0;
                    XtraMessageBox.Show("El monto del Pago Mora Ingresado  es Mayor al Monto de la Mora", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            });
            return result;
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (view.FocusedRowHandle == 0)
                view.MoveLast();
            else
                view.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (view.FocusedRowHandle == view.RowCount - 1)
                view.MoveFirst();
            else
                view.MoveNext();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void txtNroContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }
        private void filtrar()
        {
            dgr.DataSource = lstContrato.Where(x => x.NumContrato.Contains(txtNroContrato.Text)
            && x.cntc_vnombre_contratante.Contains(txtContratante.Text.ToUpper())).ToList();
        }

        private void view_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            a = e;
            EContratoCuotas Obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;
            if (situacion == nuevo)
            {
                if (Obe.strSituacion != "PENDIENTE" && Obe.strSituacion != "PARCIALMENTE PAGADO")
                {
                    gridColumn9.OptionsColumn.AllowEdit = false;
                    gridColumn9.OptionsColumn.AllowFocus = false;
                    gridColumn9.OptionsColumn.AllowIncrementalSearch = false;
                    gridColumn11.OptionsColumn.AllowEdit = false;
                    gridColumn11.OptionsColumn.AllowFocus = false;
                    gridColumn11.OptionsColumn.AllowIncrementalSearch = false;

                }
                else
                {
                    if (Obe.flag_multiple == true)
                    {
                        gridColumn11.OptionsColumn.AllowEdit = true;
                        gridColumn11.OptionsColumn.AllowFocus = true;
                        gridColumn11.OptionsColumn.AllowIncrementalSearch = true;
                        gridColumn1.OptionsColumn.AllowEdit = true;
                        gridColumn1.OptionsColumn.AllowFocus = true;
                        gridColumn1.OptionsColumn.AllowIncrementalSearch = true;
                    }
                    else
                    {
                        gridColumn11.OptionsColumn.AllowEdit = false;
                        gridColumn11.OptionsColumn.AllowFocus = false;
                        gridColumn11.OptionsColumn.AllowIncrementalSearch = false;
                        gridColumn1.OptionsColumn.AllowEdit = false;
                        gridColumn1.OptionsColumn.AllowFocus = false;
                        gridColumn1.OptionsColumn.AllowIncrementalSearch = false;

                    }
                    gridColumn9.OptionsColumn.AllowEdit = true;
                    gridColumn9.OptionsColumn.AllowFocus = true;
                    gridColumn9.OptionsColumn.AllowIncrementalSearch = true;

                }
            }
            else
            {
                if (Obe.strSituacion == "REPROGRAMADO")
                {

                    gridColumn9.OptionsColumn.AllowEdit = false;
                    gridColumn9.OptionsColumn.AllowFocus = false;
                    gridColumn9.OptionsColumn.AllowIncrementalSearch = false;
                }
                else
                {
                    gridColumn9.OptionsColumn.AllowEdit = true;
                    gridColumn9.OptionsColumn.AllowFocus = true;
                    gridColumn9.OptionsColumn.AllowIncrementalSearch = true;

                }
                if (Obe.flag_multiple == true)
                {
                    gridColumn11.OptionsColumn.AllowEdit = true;
                    gridColumn11.OptionsColumn.AllowFocus = true;
                    gridColumn11.OptionsColumn.AllowIncrementalSearch = true;
                    gridColumn1.OptionsColumn.AllowEdit = true;
                    gridColumn1.OptionsColumn.AllowFocus = true;
                    gridColumn1.OptionsColumn.AllowIncrementalSearch = true;

                }
                else
                {
                    gridColumn11.OptionsColumn.AllowEdit = false;
                    gridColumn11.OptionsColumn.AllowFocus = false;
                    gridColumn11.OptionsColumn.AllowIncrementalSearch = false;
                    gridColumn1.OptionsColumn.AllowEdit = false;
                    gridColumn1.OptionsColumn.AllowFocus = false;
                    gridColumn1.OptionsColumn.AllowIncrementalSearch = false;
                    if (Obe.strSituacion == "CANCELADO" && Obe.cntc_icod_documento == 0)
                    {

                        gridColumn9.OptionsColumn.AllowEdit = false;
                        gridColumn9.OptionsColumn.AllowFocus = false;
                        gridColumn9.OptionsColumn.AllowIncrementalSearch = false;
                    }

                }
            }

        }

        private void dgr_Click(object sender, EventArgs e)
        {

        }

        private void view_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            EContratoCuotas Obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;
            if (Obe.flag_multiple == true && Obe.monto_pagar == 0)
            {
                Obe.monto_pagar = Obe.cntc_nsaldo;
                Obe.cntc_nmonto_mora_pago = Obe.cntc_nmonto_mora;
            }
            else if (Obe.flag_multiple == false)
            {
                Obe.monto_pagar = 0;
                Obe.cntc_nmonto_mora_pago = 0;
            }

            if (Obe.monto_pagar > (Obe.cntc_nsaldo + Obe.cntc_npagado))
            {
                if (XtraMessageBox.Show("El monto del Pago Ingresado es Mayor al Monto del Saldo, ¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    Obe.monto_pagar = Convert.ToDecimal(0);
                };

            }


            txtMontoCuota.Text = (lstContrato.Sum(x => x.monto_pagar)).ToString();
            txtMontoMora.Text = (lstContrato.Sum(x => x.cntc_nmonto_mora_pago)).ToString();
            txtMontoTotal.Text = (Convert.ToDecimal(txtMontoCuota.Text) + Convert.ToDecimal(txtMontoMora.Text)).ToString();

            new BVentas().modificarContratoCuotas(Obe);

            dgr.RefreshDataSource();
            dgr.Refresh();
            view.RefreshData();

            int index = lstContrato.FindIndex(x => x.cntc_icod_contrato_cuotas == Obe.cntc_icod_contrato_cuotas);
            view.FocusedRowHandle = index + 1;
            view.Focus();
            var evento = a as DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs;
            view_FocusedRowChanged(sender, evento);
        }

        object a;

        private void view_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            EContratoCuotas Obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;
            lstContrato.ForEach(x =>
            {
                if (x.flag_multiple == true)
                {

                }
            });
            //int index = lstContrato.FindIndex(x => x.cntc_icod_contrato_cuotas == Obe.cntc_icod_contrato_cuotas);
            //view.FocusedRowHandle = index+1;
            //view.Focus();
        }

        private void view_ColumnChanged(object sender, EventArgs e)
        {
            EContratoCuotas Obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;
        }

        private void dgr_DataSourceChanged(object sender, EventArgs e)
        {
            EContratoCuotas Obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;

        }

        private void dgr_MouseClick(object sender, MouseEventArgs e)
        {
            EContratoCuotas Obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;
        }

        private void nuevoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (frmManteContratoCuotasDet frm = new frmManteContratoCuotasDet())
            {
                if (lstContrato.Count > 0)
                {
                    frm.txtNroCuotas.Text = String.Format("{0:0}", lstContrato.Max(x => x.cntc_inro_cuotas) + 1);
                    frm.correlativo = Convert.ToInt32(frm.txtNroCuotas.Text);
                }
                else
                {
                    frm.txtNroCuotas.Text = "1";
                    frm.correlativo = 1;
                }
                frm.SetInsert();
                frm.cntc_icod_contrato = cntc_icod_contrato;
                frm.lstDetalle = lstContrato;
                frm.txtCronograma.Text = "0";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    new BVentas().insertarContratoCuotas(frm.oBe);
                    cargar(true);
                }
            }
        }

        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteContratoCuotasDet frm = new frmManteContratoCuotasDet())
            {
                frm.oBe = oBe_;
                frm.cntc_icod_contrato = cntc_icod_contrato;
                frm.SetModify();
                frm.lstDetalle = lstContrato;
                frm.correlativo = oBe_.cntc_inro_cuotas;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    new BVentas().modificarContratoCuotas(frm.oBe);
                    cargar(true);
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (oBe_ == null)
                return;
            oBe_.intUsuario = Valores.intUsuario;
            new BVentas().eliminarContratoCuotas(oBe_);
            cargar(true);
        }
    }
}