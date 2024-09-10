using Newtonsoft.Json;
using SGE.Entity.Sire;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace SGE.WindowForms.Contabilidad.Sire
{
    public partial class Frm01ConsultaPeriodosVentas : DevExpress.XtraEditors.XtraForm
    {
        public List<ESire> lstSire = new List<ESire>();
        public List<Ejercicio> lstEjercicio = new List<Ejercicio>();
        public SireRequest request = new SireRequest();
        public Frm01ConsultaPeriodosVentas() { InitializeComponent(); }

        private void Frm01ConsultaPeriodosCompras_Load(object sender, EventArgs e) => Cargar();

        public async Task Cargar()
        {
            lstSire.Clear();
            btnRefresh.Text = "Cargando...";
            btnRefresh.Enabled = false;
            request = new SireRequest()
            {
                client_id = SireServiceImpl.CLIENT_ID,
                client_secret = SireServiceImpl.CLIENT_SECRET,
                username = SireServiceImpl.USER_NAME,
                password = SireServiceImpl.PASSWORD,
                operacion = "VENTA"
            };
            using (HttpClient httpClient = new HttpClient())
            {
                var jsonData = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("http://www.apisunat.somee.com/api/Sire/ConsultarPeriodosActivos", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<BaseResponse<string>>(responseBody);
                    lstEjercicio = JsonConvert.DeserializeObject<List<Ejercicio>>(data.Data);
                }
            }

            var lstSireDb = new BContabilidad().SireListar().Where(x => x.operacion == "VENTA");

            lstEjercicio.ForEach(x =>
            {
                x.lisPeriodos.ForEach(y =>
                {
                    var currectSireDb = lstSireDb.Where(s => s.perTributario == y.perTributario).FirstOrDefault() ?? new ESire();
                    lstSire.Add(new ESire
                    {
                        perTributario = y.perTributario,
                        codEstado = y.codEstado,
                        desEstado = y.desEstado,
                        operacion = "VENTA",
                        ticket = currectSireDb.ticket,
                        archivo = currectSireDb.archivo
                    });
                });
            });

            grdLista.DataSource = lstSire;
            grdLista.RefreshDataSource();
            btnRefresh.Text = "Refresh";
            btnRefresh.Enabled = true;

        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await Cargar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                if (save.FileName.Contains(".xlsx"))
                {
                    grdLista.ExportToXlsx(save.FileName);
                    Process.Start(save.FileName);
                }
                else
                {
                    grdLista.ExportToXlsx(save.FileName+ ".xlsx");
                    Process.Start(save.FileName+ ".xlsx");
                }
            }
        }

        private async void generarNuevoTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRefresh.Text = "Generando...";
            btnRefresh.Enabled = false;
            await GenerarTicket();
            btnRefresh.Text = "Refresh";
            btnRefresh.Enabled = true;
        }

        public async Task GenerarTicket() {
            var select = viewLista.GetFocusedRow() as ESire;
            
            request.periodo = select.perTributario;
            using (HttpClient httpClient = new HttpClient())
            {
                var jsonData = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("http://www.apisunat.somee.com/api/Sire/DescargarPropuesta", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<BaseResponse<string>>(responseBody);
                    select.ticket = (JsonConvert.DeserializeObject<TicketInfo>(data.Data)).numTicket;
                }
            }
            new BContabilidad().SireGuardar(select);
            await Cargar();

        }
    }
}