using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.BusinessLogic
{
    public class Parametros
    {
        public static string strPorcIGV = "";
        public static string strPorcRenta4taCat = "";

        public static int intEjercicio = 2013;
        /*ID´s de Tipos de Tabla--------------------------------------------------------------*/
        public static int intTipoTablaEstado = 1;
        public static int intTipoTablaSituacionVoucher = 2;
        public static int intTipoTablaOrigenVoucher = 3;
        public static int intTipoTablaMeses = 4;
        public static int intTipoTablaTipoMoneda = 5;
        public static int intTipoTablaTipoCuentaBanco = 10;
        public static int intTipoTablaSituacionDocumento = 21;
        public static int intTipoTablaTipoPersona = 22;
        public static int intTipoTablaTipoAnalitica = 24;
        public static int intTipoTablaTipoCuentaContable = 25;
        public static int intTipoTablaTipoProducto = 26;
        public static int intTipoTablaTipoRotacion = 27;            
        /*------------------------------------------------------------------------------------*/
        /*---Tabla Registro-------------------------------------------------------------*/
      

        /*Situación de Vouchers Contables-----------------------------------------------------*/
        public static int intSitVcoCuadrado = 1;
        public static int intSitVcoNoCuadrado = 2;
        public static int intSitVcoAnulado = 3;
        public static int intSitVcoSinDetalle = 4;
        /*------------------------------------------------------------------------------------*/
        /*Origenes de Vouchers Contables------------------------------------------------------*/
        public static int intOriVcoManual = 1;
        public static int intOriVcoOtroSistema = 2;
        public static int intOriVcoPorRedondeo = 3;
        public static int intOriVcoPorDiferenciaCambio = 4;
        public static int intOriVcoImporPlanilla = 5;
        /*------------------------------------------------------------------------------------*/
        /*Tipo Operaciones--------------------------------------------------------------------*/
        public static int intOperacionNuevo = 1;
        public static int intOperacionModificar = 2;
        public static int intOperacionEliminar = 3;
        /*------------------------------------------------------------------------------------*/
        /*Tipo Monedas------------------------------------------------------------------------*/
        public static int intTipoMonedaSoles = 3;
        public static int intTipoMonedaDolares = 4;
        public static int intTipoMonedaHistorico = 5;
        /*------------------------------------------------------------------------------------*/
        /*Módulos-----------------------------------------------------------------------------*/
        public static int intModuloAdministracionSitema = 1;
        public static int intModuloAlmacen = 2;
        public static int intModuloCompras = 3;
        public static int intModuloCtasPorCobrar = 4;
        public static int intModuloCtasPorPagar = 5;
        public static int intModuloTesoreria = 6;
        public static int intModuloVentas = 7;
        public static int intModuloContabilidad = 8;
        /*-----tipos de movimientos kardex-------------------------------------------------------*/
        public static int intKardexIn = 1;
        public static int intKardexOut = 0;
        /*Tipo de Documentos------------------------------------------------------------------*/
        public static int intTipoDocAperturaBanco = 12;
        public static int intTipoDocSaldoInicialBanco = 13;
        //---------- MOTIVOS DE MOVIMIENTOS DE BANCOS -----------------------
        public static int intMotivoApertura = 104;
        public static int intMotivoSaldoInicial = 105;
        public static int intMotivoCuentasPorPagar = 106;
        public static int intMotivoCuentasPorCobrar = 107;
        public static int intMotivoAdelantosProveedores = 108;
        public static int intMotivoAdelantosClientes = 109;
        public static int intMotivoVarios = 110;
        public static int intMotivoPagoAdelantadoProveedores = 111;
        public static int intMotivoPagoAdelantadoClientes = 112;        
        public static int intMotivoTransferenciaCuentas = 182;
        //-------------------------------------------------------------------
        public static string intTipoMovimientoCargo = "0";
        public static string intTipoMovimientoAbono = "1";
        //---------- TIPO DE DOCUMENTO --------------------------------------
        public static int intTipoDocAdelantoProveedor = 1;
        public static int intTipoDocAperturaKardex = 3;
        public static int intTipoDocLiquidacionCompra = 35;
        public static int intTipoDocNotaCreditoCliente = 36;
        public static int intTipoDocNotaDebitoCliente = 110;
        public static int intTipoDocNotaCreditoProveedor = 86;
        public static int intTipoDocPresupuestoNacional = 87;
        public static int intTipoDocTransferenciaAlmacen = 88;
        public static int intTipoDocPresupuestoImportacion = 46;
        public static int intTipoDocComprobanteRetencion = 50;
        public static int intTipoDocReciboPorHonorarios = 51;
        public static int intTipoDocAdelantoCliente = 83;
        public static int intTipoDocLetraRechazada = 33;
        public static int intTipoDocFacturaCompra = 24;
        public static int intTipoDocBoletaCompra = 84;
        public static int intTipoDocDeclaracionUnicaAduana = 23;
        public static int intTipoDocDeclaracionImportacionCourier = 22;
        public static int intTipoDocLiquidacionCobranzaAduana = 31;
        public static int intTipoDocReporteProduccion = 47;
        public static int intTipoDocGuiaRemision = 28;

        public static int intTipoDocRetencion = 50; 
        
        public static int intTipoDocNotaIngresoContable = 38;
        public static int intTipoDocNotaSalidaContable = 39;
        public static int intTipoDocNotaIngresoAlmacen = 27;
        public static int intTipoDocNotaSalidaAlmacen = 29;
        public static int intTipoDocLiquidacionCaja = 77;
        public static int intTipoDocAperturaCtaBanco = 90;        
        public static int intTipoDocTicket = 54;
        public static int intTipoDocLiquidacionCobranza = 32;
        public static int intTipoDocTicketCintaMaqReg = 54;
        public static int intTipoDocPercepcionCompra = 98;
        public static int intTipoDocLetraProveedor = 34;
        public static int intTipoDocLetraCliente = 85;
        public static int intTipoDocPlanillaVenta = 100;
        public static int intTipoDocFacturaVenta = 26;
        public static int intTipoDocBoletaVenta = 9;
        public static int intTipoDocTicketFactura = 95;
        public static int intTipoDocTicketBoleta = 96;
        public static int intTipoDocNotaCompra = 97;
        public static int intTipoDocGarantiaProveedor = 113;
        public static int intTipoDocGarantiaClientes = 112;
        //-------------------------------------------------------------------
        //---------- SITUACION ADELANTO PROVEEDOR -----------------------
        public static int intSitProveedorGenerado = 1;
        public static int intSitProveedorPagadoParcial = 2;
        public static int intSitProveedorCancelado = 3;
        public static int intSitProveedorAnulado = 4;
        //-------------------------------------------------------------------
        //---------- TIPO DE DOCUMENTO CLASE CUENTA -------------------------
        public static int intClaseTipoDocAdelantoProveedor = 27;
        public static int intClaseTipoDocAdelantoCliente = 30;
        public static int intClaseTipoDocLetraRechazada = 36;
        public static int intClaseTipoDocPercepcionCompra = 63;
        public static int intClaseTipoDocRetencion = 56; 

        public static int intClaseTipoDocLetraProveedor = 52;
        public static int intClaseTipoDocLetraCliente = 37;
        public static int intClaseTipoDocFacturaVentaServicios = 7;
        public static int intClaseTipoDocBoletaVentaServicios = 10;
        public static int intClaseTipoDocNotaCredClienteDevolucion = 21;

        public static int intClaseTipoDocTicketFactura = 67;
        public static int intClaseTipoDocTicketBoleta = 68;
        public static int intClaseTipoDocNotaCompra = 64;

        public static int intClaseTipoDocFacturaCompra = 32;
        public static int intClaseTipoDocBoletaCompra = 31; 

        //-------------------------------------------------------------------
        //---------- SITUACION DOCUMENTO X PAGAR ----------------------------
        public static int intSitDocGenerado = 8;
        public static int intSitDocPagadoParcial = 9;
        public static int intSitDocCancelado = 10;
        //-------------------------------------------------------------------
        //---------- SITUACION ADELANTO CLIENTE -----------------------------
        public static int intSitClienteGenerado = 1;
        public static int intSitClientePagadoParcial = 2;
        public static int intSitClienteCancelado = 3;
        public static int intSitClienteAnulado = 4;
        //-------------------------------------------------------------------
        //---------- SITUACION DOCUMENTO X COBRAR ---------------------------
        public static int intSitDocCobrarGenerado = 8;
        public static int intSitDocCobrarPagadoParcial = 9;
        public static int intSitDocCobrarCancelado = 10;
        public static int intSitDocCobrarAnulado = 11;
        //-------------------------------------------------------------------
        //---------- SITUACION LIBRO BANCOS ---------------------------------
        public static int intSitLibroBancosRegistrado = 114;
        public static int intSitLibroBancosAnulado = 115;
        //-------------------------------------------------------------------
        public static double dblPorRetencion = 0.00;       
        //-------------------------------------------------------------------
        //-----------ORIGEN EN DXP-----------------
        public static string origenManual = "2";
        public static string origenAlmacenCompra = "9";
        public static string origenComprasFac = "8";
        public static string origenComprasPercepcion = "4";
        public static string origenAlmacenNCP = "5";
        public static string origenLiquidacionCaja = "6"; 

        //-----------ORIGEN EN DXC-----------------
        public static string origenLetraPorCobrar = "3";
        public static string origenLetraPorPagar = "6";
        //----------PLANILLA TIPOS DE MOVIMIENTOS--------------------
        public static int intPlnFacturacion = 1;
        public static int intPlnPago = 2;
        public static int intPlnAnticipo = 3;
        //---------- MOTIVOS DE MOVIMIENTOS DE KARDEX -----------------------
        public static int intMotivoKrdDevolucionProdIn = 99;  
        public static int intMotivoKrdSaldoInicialIn = 101; 
        public static int intMotivoKrdVentasOut = 101;
        public static int intMotivoKrdComprasIn = 97;
        public static int intMotivoKrdTransferenciaIn = 98;
        public static int intMotivoKrdTransferenciaOut = 102;
        public static int intMotivoKrdAjusteInventIn = 193;
        public static int intMotivoKrdAjusteInventOut = 194;
        public static int intMovSalTransformacion = 661;
        public static int intMovIngTransformacion = 662;
        //---------- SITUACION PLANILLA DE VENTA -----------------------
        public static int intSitPlnFacturado = 4;

        //---------- TIPOS DE PAGO DE DOC DE VENTA -----------------------
        public static int intTipoPgoEfectivo = 1;
        public static int intTipoPgoTarjetaCredito = 2;
        public static int intTipoPgoNotaCredito = 3;
        public static int intTipoPgoCheque = 4;
        public static int intTipoPgoTransfBancaria = 5;
        public static int intTipoPgoCredito = 6;
        public static int intTipoPgoAnticipo = 7;

        //---------- CLIENTE -----------------------
        public static int intClientePortador = 1528;

        //---------- TIPOS DE PRODUCTO -----------------------
        public static int intTipoPrdServicio = 3;

        //---------- TIPOS MOV. LIQUIDACION CAJA -----------------------
        public static string strTipoLiqCajaCContable = "CCONTABLE";
        public static string strTipoLiqCajaPagoProvision = "PGOPROVIS";
        public static string strTipoLiqCajaGenProvision = "GENPROVIS";
        //---------- TIPOS ACTUALIZACIÓN DE COSTOS -----------------------
        public static int intTipoActualizacionCompras = 1;
        public static int intTipoActualizacionDevoluciones = 2;
        public static int intTipoActualizacionTransformaciones = 3;
        public static int intTipoActualizacionVentas = 4;
        public static int intTipoActualizacionImportacion = 5;
        //---------- TIPOS ACTUALIZACIÓN DE COSTOS -----------------------
        public static int intTipoAnaliticaBancos = 1;
        public static int intTipoAnaliticaClientes = 2;
        public static int intTipoAnaliticaProveedores = 5;



        //tipo tarjeta
        public static int tablc_tipo_tarjeta = 40;
        #region
        public static int Tabla_Tabla_registro_Visa = 1;
        public static int Tabla_Tabla_registro_Mastercard = 2;
        public static int Tabla_Tabla_registro_Diners = 3;
        #endregion
        //Baco tarjetas
        public static int bcoc_icod_banco_tarjeta = 17;
        #region
        public static int Cuenta_tarjeta_visa = 103003;
        public static int Cuenta_tarjeta_mastercard = 103003;
        public static int Cuenta_tarjeta_dinners = 103003;
        public static int Cuenta_tarjeta_American_express = 103003;

        #endregion
        //Banco tarjetas
        public static int bcoc_icod_banco_Caja_Efectivo = 16;
        #region
        public static int Cuenta_id_caja_efectivo_la_molina_MN = 101001;
        public static int Cuenta_id_caja_efectivo_la_molina_ME = 101002;
        public static int Cuenta_id_caja_pucusana = 101003;
        #endregion


        public static int intTipoFoma = 12493;
        public static int intTipoFinanciamiento = 12494;
    }
}
