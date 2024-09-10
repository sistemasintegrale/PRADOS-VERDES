using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OpenInvoicePeru.Comun.Dto.Modelos;
//using OpenInvoicePeru.Comun.Dto.Intercambio;
using System.Globalization;
using System.Configuration;
using SGE.BusinessLogic;
using SIDE.COMUN.DTO.Modelos;
using SGE.Entity.FacturaElectronica;

namespace SGE.WindowForms
{
    public static class FacturaElectronicaDto
    {
        public static DocumentoElectronico ModelDto(EFacturaVentaElectronica obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.IdDocumento = obe.idDocumento;
            objdocumento.FechaEmision = Convert.ToDateTime(obe.fechaEmision).ToString("yyyy-MM-dd");
            objdocumento.HoraEmision = DateTime.Now.ToString("hh:mm:ss");
            objdocumento.FechaVencimiento = Convert.ToDateTime(obe.fechaVencimiento).ToString("yyyy-MM-dd");
            objdocumento.TipoDocumento = obe.tipoDocumento;
            objdocumento.Moneda = obe.moneda;
            objdocumento.CantidadItems = obe.cantidadItems.ToString();
            

            objdocumento.Emisor = new Contribuyente();
            objdocumento.Emisor.NombreComercial = obe.nombreComercialEmisor;
            objdocumento.Emisor.NombreLegal = obe.nombreLegalEmisor;
            objdocumento.Emisor.TipoDocumento = obe.tipoDocumentoEmisor;
            objdocumento.Emisor.NroDocumento = obe.nroDocumentoEmisior;

            objdocumento.Emisor.Ubigeo = "0000";

            //objdocumento.CodLocalEmisor = obe.CodLocalEmisor;

            objdocumento.Receptor = new Contribuyente();
            objdocumento.Receptor.NroDocumento = obe.nroDocumentoReceptor;
            objdocumento.Receptor.TipoDocumento = obe.tipoDocumentoReceptor;
            objdocumento.Receptor.NombreLegal = obe.nombreLegalReceptor;

            objdocumento.CodMotivoDescuento = obe.CodMotivoDescuento.ToString();
            objdocumento.PorcDescuento = obe.PorcDescuento;
            objdocumento.MontoDescuentoGlobal = obe.MontoDescuentoGlobal;
            objdocumento.BaseMontoDescuento = obe.BaseMontoDescuento;
            objdocumento.MontoTotalImpuesto = obe.MontoTotalImpuesto;
            objdocumento.MontoGravadosIGV = obe.MontoGravadasIGV;
            //objdocumento.CodigoTributo = obe.CodigoTributo.ToString();
            objdocumento.MontoExonerado = obe.MontoExonerado;
            objdocumento.MontoInafecto = obe.MontoInafecto;
            objdocumento.MontoGratuitoImpuesto = obe.MontoGratuitoImpuesto;
            objdocumento.MontoBaseGratuito = obe.MontoBaseGratuito;

            objdocumento.MontoGravadosISC = obe.MontoGravadosISC;
            objdocumento.TotalIsc = obe.totalIsc;
            objdocumento.MontoGravadosOtros = obe.MontoGravadosOtros;
            objdocumento.TotalOtrosTributos = obe.totalOtrosTributos;
            objdocumento.TotalValorVenta = obe.TotalValorVenta;
            objdocumento.TotalPrecioVenta = obe.TotalPrecioVenta;
            objdocumento.MontoDescuento = obe.MontoDescuento;
            objdocumento.MontoTotalCargo = obe.MontoTotalCargo;
            objdocumento.MontoTotalAnticipo = obe.MontoTotalAnticipo;
            objdocumento.ImporteTotalVenta = obe.ImporteTotalVenta;

            //Nuevas Variables

            if (obe.tipoDocumento == "01" || obe.tipoDocumento == "03")
            {
                //Variable Forma de Pago

                objdocumento.FormaPago = obe.FormaPagoS; //Contado o Credito


                //Variable Lista de Cuotas
                if (obe.FormaPagoS == "Credito")
                {
                    objdocumento.MontoTotalPago = obe.MontoTotalPago;// //Monto del Pago
                    objdocumento.Cuotas = new List<Cuotas>();
                    objdocumento.Cuotas.Add(new Cuotas
                    {
                        NroCuota = "Cuota001",
                        MontoCuota = obe.MontoCuota,
                        FechaPago = Convert.ToDateTime(obe.FechaPago).ToString("yyyy-MM-dd")
                    }
                    );
                }
            }


            //Items
            objdocumento.Items = new List<DetalleDocumento>();
            foreach (var item in mlisdet)
            {
                objdocumento.Items.Add(new DetalleDocumento
                {
                    NumeroOrdenItem = item.NumeroOrdenItem,
                    Cantidad = item.cantidad,
                    UnidadMedida = item.unidadMedida,
                    ValorVentaItem = item.ValorVentaItem,
                    CodMotivoDescuentoItem = item.CodMotivoDescuentoItem.ToString(),
                    FactorDescuentoItem = Convert.ToInt32(item.FactorDescuentoItem),
                    DescuentoItem = item.DescuentoItem,
                    BaseDescuentotem = Convert.ToInt32(item.BaseDescuentotem),
                    CodMotivoCargoItem = item.CodMotivoCargoItem.ToString(),
                    FactorCargoItem = (item.FactorCargoItem),
                    MontoCargoItem = (item.MontoCargoItem),
                    BaseCargoItem = (item.BaseCargoItem),
                    MontoTotalImpuestosItem = (item.MontoTotalImpuestosItem),
                    MontoImpuestoIgvItem = item.MontoImpuestoIgvItem,
                    MontoAfectoImpuestoIgvItem = (item.MontoAfectoImpuestoIgv),
                    PorcentajeIGVItem = (item.PorcentajeIGVItem),
                    MontoInafectoItem = item.MontoInafectoItem,
                    MontoImpuestoISCItem = (item.MontoImpuestoISCItem),
                    MontoAfectoImpuestoISCItem = (item.MontoAfectoImpuestoIsc),
                    PorcentajeISCtem = (item.PorcentajeISCtem),
                    MontoImpuestoIVAPtem = (item.MontoImpuestoIVAPtem),
                    MontoAfectoImpuestoIVAPItem = (item.MontoAfectoImpuestoIVAPItem),
                    PorcentajeIVAPItem =(item.PorcentajeIVAPItem),
                    Descripcion = item.descripcion,
                    CodigoItem = item.codigoItem,
                    //ObservacionesItem = item.ObservacionesItem,
                    ValorUnitarioItem = item.ValorUnitarioItem,
                    PrecioVentaUnitarioItem = item.PrecioVentaUnitarioItem,
                    //PlacaVehiculo = item.placaVehiculo,
                    TipoImpuesto = item.tipoImpuesto.Trim(),



                });
            }
            return objdocumento;
        }

        public static ComunicacionBaja DocumentoElectronicoBaja(ESunatDocumentosBaja Obe)
        {
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            ComunicacionBaja obj = new ComunicacionBaja();


            obj.Bajas = new List<DocumentoBaja>();


            obj.Bajas.Add(new DocumentoBaja
            {
                Correlativo = Obe.Correlativo,
                MotivoBaja = Obe.MotivoBaja,
                Id = Obe.Id,
                TipoDocumento = Obe.TipoDocumento,
                Serie = Obe.Serie,

            });

            obj.IdDocumento = string.Format("RA-{0}-{1}", DateTime.Now.ToString("yyyyMMdd"), 1);
            obj.FechaEmision = Convert.ToDateTime(Obe.FechaEmision).ToString("yyyy/MM/dd");
            obj.FechaReferencia = DateTime.Now.ToString("yyyy/MM/dd");

            obj.Emisor = new Contribuyente();
            obj.Emisor.NroDocumento = lstParametro.Ruc.Trim();
            obj.Emisor.TipoDocumento = "6";
            obj.Emisor.NombreLegal = lstParametro.pm_nombre_empresa.Trim();
            obj.Emisor.NombreComercial = lstParametro.pm_nombre_empresa.Trim();
            obj.Emisor.Direccion = lstParametro.pm_direccion_empresa.Trim();

            return obj;
        }

        public static ResumenDiarioNuevo DocumentoElectronicoResumenDocumentos(ESunatResumenDocumentosCab objBaja, List<ESunatResumenDocumentosDet> objBajaDet)
        {
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();

            ResumenDiarioNuevo obj_ = new ResumenDiarioNuevo();

            //---Resumen detalle

            obj_.Resumenes = new List<GrupoResumenNuevo>();
            int id = 1;
            foreach (var item in objBajaDet)
            {
                obj_.Resumenes.Add(new GrupoResumenNuevo
                {
                    Id = item.Id,
                    TipoDocumento = item.TipoDocumento.Trim(),
                    IdDocumento = item.IdDocumento.Trim(),
                    TipoDocumentoReceptor = item.TipoDocumentoReceptor.Trim(),
                    NroDocumentoReceptor = item.NroDocumentoReceptor.Trim(),
                    CodigoEstadoItem = item.CodigoEstadoItem,
                    DocumentoRelacionado = item.DocumentoRelacionado.Trim(),
                    TipoDocumentoRelacionado = item.TipoDocumentoRelacionado.Trim(),
                    Moneda = item.Moneda.Trim(),
                    TotalVenta = item.TotalVenta,
                    TotalDescuentos = item.TotalDescuentos,
                    TotalIgv = item.TotalIgv,
                    TotalIsc = item.TotalIsc,
                    TotalIvap = item.TotalIvap,
                    TotalOtrosImpuestos = item.TotalOtrosImpuestos,
                    Gravadas = item.Gravadas,
                    Exoneradas = item.Exoneradas,
                    Inafectas = item.Inafectas,
                    Exportacion = item.Exportacion,
                    Gratuitas = item.Gratuitas,
                    Serie = ""
                });
                id++;
            }

            obj_.IdDocumento = objBaja.IdDocumento.Trim();
            //obj_.FechaEmision = objBaja.FechaEmision;
            //obj_.FechaReferencia = objBaja.FechaGeneracion;

            obj_.FechaEmision = Convert.ToDateTime(objBaja.FechaEmision).ToString("yyyy-MM-dd");
            obj_.FechaReferencia = Convert.ToDateTime(objBaja.FechaGeneracion).ToString("yyyy-MM-dd");

            obj_.Emisor = new Contribuyente();
            obj_.Emisor.NroDocumento = objBaja.NroDocumento.Trim();
            obj_.Emisor.TipoDocumento = objBaja.TipoDocumento.Trim();
            obj_.Emisor.NombreLegal = objBaja.NombreLegal;
            obj_.Emisor.NombreComercial = objBaja.NombreComercial;
            obj_.Emisor.Ubigeo = objBaja.Ubigeo;
            obj_.Emisor.Direccion = objBaja.Direccion;
            obj_.Emisor.Urbanizacion = objBaja.Urbanizacion;
            obj_.Emisor.Departamento = objBaja.Departamento;
            obj_.Emisor.Provincia = objBaja.Provincia;
            obj_.Emisor.Distrito = objBaja.Distrito;

            return obj_;
        }

    }
}
