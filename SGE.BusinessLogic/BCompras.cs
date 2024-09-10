﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGE.Entity;
using SGE.DataAccess;
using System.Transactions;
using System.Data;

namespace SGE.BusinessLogic
{
    public class BCompras
    {
        AlmacenData objAlmacenData = new AlmacenData();
        ComprasData objComprasData = new ComprasData();
        VentasData objVentas = new VentasData();
        public enum Operacion
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4
        }
        #region proveedor
        public List<EProveedor> ListarProveedor()
        {
            List<EProveedor> lista = null;
            try
            {
                lista = (new ComprasData()).ListarProveedor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarProveedor(EAnaliticaDetalle objEAnaliticaDetalle, EProveedor obj)
        {
            try
            {
                int idProveedor = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertar Analitica Detalle
                    int IdAnaliticaDetalle = new ContabilidadData().insertarAnaliticaDetalle(objEAnaliticaDetalle);
                    //Insertar Proveedor
                    obj.anac_icod_analitica = IdAnaliticaDetalle;
                    idProveedor = new ComprasData().ProveedorInsertar(obj);
                    tx.Complete();
                }
                return idProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarProveedor(EAnaliticaDetalle objEAnaliticaDetalle, EProveedor obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Analitica Detalle
                    new ContabilidadData().modificarAnaliticaDetalle(objEAnaliticaDetalle);
                    //Actualizar Proveedor
                    objComprasData.ActualizarProveedor(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarProveedorSolo(EProveedor obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Analitica Detalle
                    //new ContabilidadData().modificarAnaliticaDetalle(objEAnaliticaDetalle);
                    //Actualizar Proveedor
                    objComprasData.ActualizarProveedorSolo(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarProveedor(EProveedor obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Elimina Proveedor
                    objComprasData.EliminarProveedor(obj.iid_icod_proveedor);

                    //Elimina Analitica Detalle
                    EAnaliticaDetalle oBe = new EAnaliticaDetalle() { anad_icod_analitica = obj.anac_icod_analitica };
                    new ContabilidadData().eliminarAnaliticaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Doc. Compra Cab.
        
        public List<EDocCompra> listarDocCompra(int intEjercicio,DateTime FechaDesde,DateTime FechaHasta)
        {
            List<EDocCompra> lista = null;
            try
            {
                lista = objComprasData.listarDocCompra(intEjercicio, FechaDesde, FechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EDocCompraDet> listarConsultaDocCompra(int intEjercicio, DateTime dt1, DateTime dt2)
        {
            List<EDocCompraDet> lista = null;
            try
            {
                lista = objComprasData.listarConsultaDocCompra(intEjercicio, dt1, dt2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EDocCompra> listarDocCompraSinIGV(int intEjercicio, DateTime dt1, DateTime dt2)
        {
            List<EDocCompra> lista = null;
            try
            {
                lista = objComprasData.listarDocCompraSinIGV(intEjercicio, dt1, dt2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public int insertarDocCompra(EDocCompra oBe, List<EDocCompraDet> lstDetalle)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    #region Extraer descripcion del Producto
                    int i = 1;
                    string vdescripcion_pro = "";
                     lstDetalle.ForEach(x =>
                    {
                         if (i == 1)
                        {
                            vdescripcion_pro = x.facd_vdescripcion_item;
                            i++;
                        }
                    });
                    #endregion

                    #region Generación de Doc Por Pagar
                     oBe.facc_vobservaciones = vdescripcion_pro;
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarData.insertarDocPorPagar(objDXP);
                    #endregion

                    oBe.facc_icod_doc = objComprasData.insertarDocCompra(oBe);                     
                    //----------------------------
                    #region Detalle del Doc. Compra
                   
                    lstDetalle.ForEach(x =>
                    {
                        
                        #region Ingreso a Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.facc_anio;
                            obKardex.kardc_fecha_movimiento = oBe.facc_sfecha_doc;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.facd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                            obKardex.kardc_numero_doc = oBe.facc_num_doc;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                            obKardex.kardc_beneficiario = oBe.facc_vobservaciones;
                            obKardex.kardc_observaciones = oBe.facc_vobservaciones;
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.facd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                        #region Actualización de Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.facc_sfecha_doc.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stck.stocc_stock_producto = Convert.ToInt32(x.facd_ncantidad);
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                        x.facc_icod_doc = oBe.facc_icod_doc;
                        insertarDocCompraDet(x);

                        List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                        lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(x.prd_icod_producto), Parametros.intEjercicio);
                        if (lstProdPrecioDet.Count()>0)
                        {
                            new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(x.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                        }  

                    });
                    #endregion
                   

                    //#region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    //var lstDetCuentas = objComprasData.listarDocCompraDetCuentas(oBe.facc_icod_doc);
                    //lstDetCuentas.ForEach(x =>
                    //{
                    //    EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                    //    oBeDet.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                    //    if (x.intCtaContable == 0 && x.intClasificacion != 3)
                    //        throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                    //    if (x.intCtaContable == 0 && x.intClasificacion == 3)
                    //        throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                    //    oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                    //    if (x.intTipoAnalitica > 0)
                    //        oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                    //    oBeDet.cdxpc_nmonto_cuenta = x.facd_nmonto_total;
                    //    oBeDet.cdxpc_vglosa = x.facd_vdescripcion_item;
                    //    oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                    //    oBeDet.intUsuario = oBe.intUsuario;
                    //    oBeDet.strPc = oBe.strPc;
                    //    oBeDet.cdxpc_flag_estado = true; //estado del detalle

                    //    objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    //});
                    //#endregion

                    //obtener el ultimo precio costo del documento compra
                    tx.Complete();
                }
                return oBe.facc_icod_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarDocCompra(EDocCompra oBe, List<EDocCompraDet> lstDetalle, List<EDocCompraDet> lstDelete)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarDocCompra(oBe);
                   
                    #region Borrar los datos del det. del Doc. de Compra
                    lstDelete.ForEach(x =>
                    {
                        if (x.intClasificacion != Parametros.intTipoPrdServicio)
                        {
                            #region Eliminar Kardex
                            EKardex obKardexAnt = new EKardex();
                            obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.facd_icod_kardex);
                            obKardexAnt.intUsuario = oBe.intUsuario;
                            obKardexAnt.strPc = oBe.strPc;
                            new AlmacenData().eliminarKardex(obKardexAnt);
                            #endregion  
                            #region Actualización de stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.facc_sfecha_doc.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stck.stocc_stock_producto = Convert.ToInt32(x.facd_ncantidad);
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                        }
                        eliminarDocCompraDet(x);
                        List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                        lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(x.prd_icod_producto), Parametros.intEjercicio);
                        if (lstProdPrecioDet.Count() > 0)
                        {
                            new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(x.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                        }
                    });
                    #endregion
                    #region Modificar y/o ingresar el det. del Doc. de Compra
                    int i = 1;
                    string vdescripcion_pro = "";
                    lstDetalle.ForEach(x=>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)
                            {
                                #region Ingreso a Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = oBe.facc_sfecha_doc.Year;
                                obKardex.kardc_fecha_movimiento = oBe.facc_sfecha_doc;
                                obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.facd_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                                obKardex.kardc_numero_doc = oBe.facc_num_doc;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                                obKardex.kardc_beneficiario = oBe.facc_vobservaciones;
                                obKardex.kardc_observaciones = oBe.facc_vobservaciones;
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.facd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Actualización de Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.facc_sfecha_doc.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                stck.stocc_stock_producto = Convert.ToInt32(x.facd_ncantidad);
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                            x.facc_icod_doc = oBe.facc_icod_doc;
                            insertarDocCompraDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)
                            {
                                #region Ingreso a Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_icod_correlativo=Convert.ToInt32(x.facd_icod_kardex);
                                obKardex.kardc_ianio = oBe.facc_sfecha_doc.Year;
                                obKardex.kardc_fecha_movimiento = oBe.facc_sfecha_doc;
                                obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.facd_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                                obKardex.kardc_numero_doc = oBe.facc_num_doc;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                                obKardex.kardc_beneficiario = oBe.facc_vobservaciones;
                                obKardex.kardc_observaciones = oBe.facc_vobservaciones;
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                objAlmacenData.modificarKardex(obKardex);
                                #endregion
                                #region Actualización de Kardex
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.facc_sfecha_doc.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                stck.stocc_stock_producto = Convert.ToInt32(x.facd_ncantidad);
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                            modificarDocCompraDet(x);
                        }
                        List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                        lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(x.prd_icod_producto), Parametros.intEjercicio);
                        if (lstProdPrecioDet.Count() > 0)
                        {
                            new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(x.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                        }
                        if (i == 1)
                        {
                            vdescripcion_pro = x.facd_vdescripcion_item;
                            i++;
                        }
                    });
                    #endregion
                    #region Modificación de Doc Por Pagar
                    oBe.facc_vobservaciones = vdescripcion_pro;
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    new CuentasPorPagarData().modificarDocPorPagar(objDXP);
                    #endregion
                    //#region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    //var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    //objCuentasPorPagarData.eliminarDXPDetCtaContable(lstDetDXP);                        
                    //#endregion
                    //#region Reingresar el det. del DXP (Cuentas Contables)
                    //var lstDetCuentas = objComprasData.listarDocCompraDetCuentas(oBe.facc_icod_doc);
                    //lstDetCuentas.ForEach(x =>
                    //{
                    //    EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                    //    oBeDet.doxpc_icod_correlativo = objDXP.doxpc_icod_correlativo;
                    //    if (x.intCtaContable == 0 && x.intClasificacion != 2)
                    //        throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                    //    if (x.intCtaContable == 0 && x.intClasificacion == 2)
                    //        throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                    //    oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                    //    if (x.intTipoAnalitica > 0)
                    //        oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                    //    oBeDet.cdxpc_nmonto_cuenta = x.facd_nmonto_total;
                    //    oBeDet.cdxpc_vglosa = x.facd_vdescripcion_item;
                    //    oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                    //    oBeDet.intUsuario = oBe.intUsuario;
                    //    oBeDet.strPc = oBe.strPc;
                    //    oBeDet.cdxpc_flag_estado = true; //estado del detalle

                    //    objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    //});
                    //#endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarPrecioStockValorizado(List<EDocCompra> Mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var obj in Mlist)
                    {
                        int i=1;
                        string vdescripcionProd = "";
                        List<EDocCompraDet> lstDetalle = new List<EDocCompraDet>();
                        lstDetalle = new BCompras().listarDocCompraDet(obj.facc_icod_doc);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.facd_vdescripcion_item;
                                i++;
                            }
                            ////List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                            ////lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(_be.prd_icod_producto), Parametros.intEjercicio);
                            ////if (lstProdPrecioDet.Count() > 0)
                            ////{
                            ////    new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(_be.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                            ////}
                        }
                        new CuentasPorCobrarData().modificarDocumentoXPagarDescripcion(obj.intDXP, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
          
        }
        public void ActualizarDescripcionDXPNCP(List<ENotaCreditoProvedor> Mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var obj in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<ENotaCreditoProveedorDet> lstDetalle = new List<ENotaCreditoProveedorDet>();
                        lstDetalle = new BCompras().listarNCProveedorDet(obj.ncpc_icod_nota_cred);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.ncpd_vdescripcion_item;
                                i++;
                            }
                            ////List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                            ////lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(_be.prd_icod_producto), Parametros.intEjercicio);
                            ////if (lstProdPrecioDet.Count() > 0)
                            ////{
                            ////    new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(_be.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                            ////}
                        }
                        new CuentasPorCobrarData().modificarDocumentoXPagarDescripcion(obj.intDXP, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int i = 1;
        public void ActualizarCuentasDCompra(List<EDocCompra> Mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var obj in Mlist)
                    {

                        //if (obj.facc_icod_doc == 2147)
                        //{
                        i = obj.facc_icod_doc;
                        List<EDocCompraDet> lstDetalle = new List<EDocCompraDet>();
                        List<EDocPorPagarDetalleCuentaContable> Lista = new List<EDocPorPagarDetalleCuentaContable>();
                        //listar cuentas
                        Lista = new BCuentasPorPagar().listarDXPDetCtaContable(obj.intDXP);
                        //cuenta cuentas 
                        new CuentasPorPagarData().eliminarDXPDetCtaContable(Lista, null);

                            #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                        var lstDetCuentas = objComprasData.listarDocCompraDetCuentas(obj.facc_icod_doc);
                            lstDetCuentas.ForEach(x =>
                            {
                                EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                                oBeDet.doxpc_icod_correlativo = obj.intDXP;
                                if (x.intCtaContable == 0 && x.intClasificacion != 2)
                                    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                                if (x.intCtaContable == 0 && x.intClasificacion == 2)
                                    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                                oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                                if (x.intTipoAnalitica > 0)
                                    oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                                oBeDet.cdxpc_nmonto_cuenta = x.facd_nmonto_total;
                                oBeDet.cdxpc_vglosa = x.facd_vdescripcion_item;
                                oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                                oBeDet.intUsuario = obj.intUsuario;
                                oBeDet.strPc = obj.strPc;
                                oBeDet.cdxpc_flag_estado = true; //estado del detalle

                                new CuentasPorPagarData().insertarDXPDetCtaContable(oBeDet);
                            });
                            #endregion
                        //}

                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                i = i;
                throw ex;
            }



        }
        public void eliminarDocCompra(EDocCompra oBe)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    #region Eliminación del det. del Doc. Compra
                    var lst = listarDocCompraDet(oBe.facc_icod_doc);
                    lst.ForEach(x =>
                    {
                        if (x.intClasificacion != Parametros.intTipoPrdServicio)
                        {
                            #region Eliminar Kardex y Stock
                            EKardex obKardexAnt = new EKardex();
                            obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.facd_icod_kardex);
                            obKardexAnt.intUsuario = oBe.intUsuario;
                            obKardexAnt.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexAnt);
                            #endregion                            
                        }
                        List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                        lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(x.prd_icod_producto), Parametros.intEjercicio);
                        if (lstProdPrecioDet.Count() > 0)
                        {
                            new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(x.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                        }
                    });
                    #endregion
                    #region Eliminación del Doc. Compra
                    objComprasData.eliminarDocCompra(oBe);

                    #endregion
                    #region Eliminación del Doc. Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    objCuentasPorPagarData.eliminarDocPorPagar(objDXP);
                    #endregion
                    //#region Eliminación del det. del DXP (Cuentas Contables)
                    //var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    //objCuentasPorPagarData.eliminarDXPDetCtaContable(lstDetDXP);       
                    //#endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public EDocPorPagar crearDocPorPagar(EDocCompra obj)
        {            
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                objDXP.doxpc_icod_correlativo = obj.intDXP;
                objDXP.anio = obj.facc_anio;
                objDXP.mesec_iid_mes = obj.facc_mes;
                objDXP.tdocc_icod_tipo_doc = obj.tdocc_icod_tipo_doc;

                if (objDXP.tdocc_icod_tipo_doc == Parametros.intTipoDocTicketFactura)
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocTicketFactura;

                if (objDXP.tdocc_icod_tipo_doc == Parametros.intTipoDocTicketBoleta)
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocTicketBoleta;

                if (objDXP.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCompra)
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCompra;

                if (objDXP.tdocc_icod_tipo_doc == Parametros.intTipoDocFacturaCompra)
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaCompra;

                if (objDXP.tdocc_icod_tipo_doc == Parametros.intTipoDocBoletaCompra)
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaCompra;
                                
                objDXP.doxpc_iid_correlativo = 0;
                objDXP.doxpc_vnumero_doc = obj.facc_num_doc;
                objDXP.doxpc_sfecha_doc = obj.facc_sfecha_doc;
                objDXP.doxpc_sfecha_vencimiento_doc = obj.facc_svencimiento;
                objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;
                objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;//TEMPORAL
                objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.facc_sfecha_doc);
                if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXP.doxpc_vdescrip_transaccion = obj.facc_vobservaciones;                
                objDXP.doxpc_nmonto_nogravado = (obj.tdocc_icod_tipo_doc == 84 || obj.tdocc_icod_tipo_doc == 96 || obj.tdocc_icod_tipo_doc == 97) ? Convert.ToDecimal(obj.facc_nmonto_total_doc) : 0;                
                objDXP.doxpc_nmonto_destino_gravado = (obj.tdocc_icod_tipo_doc == 24 || obj.tdocc_icod_tipo_doc == 95) ? obj.facc_nmonto_neto_doc : 0;
                objDXP.doxpc_nmonto_imp_destino_gravado = (obj.tdocc_icod_tipo_doc == 24 || obj.tdocc_icod_tipo_doc == 95) ? Convert.ToDecimal(obj.facc_nmonto_imp) : 0;
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.facc_nmonto_total_doc);
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.facc_nmonto_total_doc);
                objDXP.doxpc_nporcentaje_igv = Convert.ToDecimal(obj.facc_nporcent_imp_doc);
                objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;

                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";
                objDXP.doxpc_sfecha_emision_referencia = null;

                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;
                objDXP.doxpc_nmonto_destino_nogravado = 0;
                objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                objDXP.doxpc_nmonto_imp_destino_nogravado = 0;

                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.doxpc_sfec_deposito_detraccion = null;
                objDXP.intUsuario = obj.intUsuario;
                objDXP.strPc = obj.strPc;
                objDXP.doxpc_origen = Parametros.origenAlmacenCompra;
                objDXP.doxpc_flag_estado = true;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Doc. Compra Det.
        public List<EDocCompraDet> listarDocCompraDet(int intDocCompra)
        {
            List<EDocCompraDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarDocCompraDet(intDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarDocCompraDet(EDocCompraDet obj)
        {
            try
            {                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarDocCompraDet(obj);
                    tx.Complete();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarDocCompraDet(EDocCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarDocCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarDocCompraDet(EDocCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarDocCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #region Fac Compra Nacional Cab.
        public List<EFacturaCompra> listarFacCompra(int intEjercicio)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                lista = (new ComprasData()).listarFacCompra(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFacturaCompra> listarFacCompraXID(int intEjercicio, int fcoc_icod_doc)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                lista = (new ComprasData()).listarFacCompraXID(intEjercicio, fcoc_icod_doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarFacCompraEstadoRecepcion(EFacturaCompra obj)
        {

            try
            {
                (new ComprasData()).modificarFacCompraEstadoRecepcion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EFacturaCompra> listarFacCompraRelacionPresupuesto(int intEjercicio, int prep_cod_presupuesto)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                lista = (new ComprasData()).listarFacCompraRelacionPresupuesto(intEjercicio, prep_cod_presupuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFacCompraNacional(EFacturaCompra oBe, List<EFacturaCompraDet> lstDetalle)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarData.insertarDocPorPagar(objDXP);
                    #endregion

                    #region Insertamos Factura de Compra
                    oBe.fcoc_icod_doc = new ComprasData().insertarFacCompraNacional(oBe);

                    #region Detalle de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        if (oBe.tipo_doc_ref_compras == 0)
                        {
                            #region Ingreso a Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.fcoc_sfecha_doc.Year;
                            obKardex.kardc_fecha_movimiento = oBe.fcoc_sfecha_doc;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.fcod_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = 24;//TIPO DE DOCUMENTO 24 FACTURA DE COMPRA
                            obKardex.kardc_numero_doc = oBe.fcoc_num_doc;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                            obKardex.kardc_beneficiario = oBe.strProveedor;
                            obKardex.kardc_observaciones = oBe.fcoc_vreferencia;
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.fcod_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualización de Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.fcoc_sfecha_doc.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stck.stocc_stock_producto = Convert.ToInt32(x.fcod_ncantidad);
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                        }
                        x.fcoc_icod_doc = oBe.fcoc_icod_doc;
                        insertarFacCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc,x.prd_icod_producto);
                    });
                    #endregion
                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarFacCompraDetCuentas(oBe.fcoc_icod_doc);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                        //if (x.intCtaContable == 0 && x.intClasificacion != 2)
                        //    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //if (x.intCtaContable == 0 && x.intClasificacion == 2)
                        //    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intCtaContable == 0)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada"));
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.fcod_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.fcod_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle
                        objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion
                    #endregion



                    tx.Complete();
                }
                return oBe.fcoc_icod_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFacCompraNacional(EFacturaCompra oBe, List<EFacturaCompraDet> lstDetalle,
         List<EFacturaCompraDet> lstDelete)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarFacCompraNacional(oBe);
                    #region Modificación del Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    objCuentasPorPagarData.modificarDocPorPagar(objDXP);
                    #endregion
                    #region eliminación del det. de la fac. de compra
                    lstDelete.ForEach(x =>
                    {
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.fcod_icod_kardex);
                        obKardexAnt.intUsuario = oBe.intUsuario;
                        obKardexAnt.strPc = oBe.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        eliminarFacCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                    });
                    #endregion
                    #region ingreso o modificación del det. de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            if (oBe.tipo_doc_ref_compras == 0)
                            {
                                #region Ingreso a Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = oBe.fcoc_sfecha_doc.Year;
                                obKardex.kardc_fecha_movimiento = oBe.fcoc_sfecha_doc;
                                obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.fcod_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = 24;//24 TIPO DOC FACTURA DE COMPRA FAC
                                obKardex.kardc_numero_doc = oBe.fcoc_num_doc;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                                obKardex.kardc_beneficiario = oBe.strProveedor;
                                obKardex.kardc_observaciones = oBe.fcoc_vreferencia;
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.fcod_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Actualización de Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.fcoc_sfecha_doc.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                stck.stocc_stock_producto = Convert.ToInt32(x.fcod_ncantidad);
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                            x.fcoc_icod_doc = oBe.fcoc_icod_doc;
                            insertarFacCompraNacionalDet(x);
                            ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (oBe.tipo_doc_ref_compras == 0)
                            {
                                #region Ingreso a Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_icod_correlativo = Convert.ToInt32(x.fcod_icod_kardex);
                                obKardex.kardc_ianio = oBe.fcoc_sfecha_doc.Year;
                                obKardex.kardc_fecha_movimiento = oBe.fcoc_sfecha_doc;
                                obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.fcod_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = 24;//TIPO DE DOCUMENTO 24 DE FACTURA DE COMPRA
                                obKardex.kardc_numero_doc = oBe.fcoc_num_doc;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                                obKardex.kardc_beneficiario = oBe.strProveedor;
                                obKardex.kardc_observaciones = oBe.fcoc_vreferencia;
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                objAlmacenData.modificarKardex(obKardex);
                                #endregion
                                #region Actualización de Kardex
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.fcoc_sfecha_doc.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                stck.stocc_stock_producto = Convert.ToInt32(x.fcod_ncantidad);
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                            modificarFacCompraNacionalDet(x);
                            ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                        }
                        modificarFacCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                    });
                
                    #endregion
                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    objCuentasPorPagarData.eliminarDXPDetCtaContable(lstDetDXP, oBe);
                    #endregion
                    #region Reingresar el det. del DXP (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarFacCompraDetCuentas(oBe.fcoc_icod_doc);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = objDXP.doxpc_icod_correlativo;
                        //if (x.intCtaContable == 0 && x.intClasificacion != 2)
                        //    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //if (x.intCtaContable == 0 && x.intClasificacion == 2)
                        //    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.fcod_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.fcod_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarFacCompraNacional(EFacturaCompra obj)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación del det. de la fac. de compra
                    var lst = listarFacCompraDet(obj.fcoc_icod_doc);
                    lst.ForEach(x =>
                    {

                        objComprasData.eliminarFacCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.fcod_icod_kardex);
                        obKardexAnt.intUsuario = obj.intUsuario;
                        obKardexAnt.strPc = obj.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        /*----------------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = obj.fcoc_sfecha_doc.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(obj.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stck.stocc_stock_producto = x.fcod_ncantidad;
                        stck.intTipoMovimiento = 1;
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                    });
                    #endregion
                    #region Eliminación de la Fac. Compra
                    objComprasData.eliminarFacCompraNacional(obj);
                    #endregion
                    #region Eliminación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(obj);
                    objCuentasPorPagarData.eliminarDocPorPagar(objDXP);
                    #endregion
                    #region Eliminación del det. del DXP (Cuentas Contables)
                    var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    objCuentasPorPagarData.eliminarDXPDetCtaContable(lstDetDXP, obj);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EDocPorPagar crearDocPorPagar(EFacturaCompra obj)
        {
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                /*gerson*/
                objDXP.doxpc_icod_correlativo = obj.intDXP;
                objDXP.anio = Parametros.intEjercicio;
                objDXP.mesec_iid_mes = obj.fcoc_imes_iid_proceso;
                if (obj.fcoc_flag_importacion == false)
                {
                    objDXP.tdocc_icod_tipo_doc = 24;//TIPO DOCUMENTO 24 DE FACTURA DE COMPRA
                    objDXP.tdodc_iid_correlativo = 3; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                }
                else
                {
                    objDXP.tdocc_icod_tipo_doc = 109;//TIPO DOCUMENTO 24 DE FACTURA DE COMPRA
                    objDXP.tdodc_iid_correlativo = 77; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                }               
                objDXP.doxpc_iid_correlativo = 0;
                //objDXP.doxpc_vnumero_serio = obj.fcoc_num_doc.Substring(0, 4);
                //objDXP.doxpc_vnumero_correlativo = obj.fcoc_num_doc.Substring(4, 8);
                if (obj.fcoc_num_doc.Length == 12)
                {
                    objDXP.doxpc_vnumero_serio = obj.fcoc_num_doc.Substring(0, 4);
                    objDXP.doxpc_vnumero_correlativo = obj.fcoc_num_doc.Substring(4, 8);
                    objDXP.doxpc_numdoc_tipo = 1;
                }
                else
                {
                    objDXP.doxpc_vnumero_doc = obj.fcoc_num_doc;
                    objDXP.doxpc_numdoc_tipo = 2;
                }
                    
                objDXP.doxpc_vnumero_doc = obj.fcoc_num_doc;
                objDXP.doxpc_sfecha_doc = obj.fcoc_sfecha_doc;
                objDXP.doxpc_sfecha_vencimiento_doc = obj.fcoc_svencimiento;
                objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;

                objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;

                objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.fcoc_sfecha_doc);
                if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXP.doxpc_vdescrip_transaccion = obj.fcoc_vreferencia;
                objDXP.doxpc_nmonto_destino_gravado = obj.fcoc_nmonto_destino_gravado;
                objDXP.doxpc_nmonto_destino_mixto = obj.fcoc_nmonto_destino_mixto;
                objDXP.doxpc_nmonto_destino_nogravado = obj.fcoc_nmonto_destino_nogravado;
                objDXP.doxpc_nmonto_nogravado = obj.fcoc_nmonto_nogravado;
                objDXP.doxpc_nmonto_imp_destino_gravado = obj.fcoc_nmonto_imp_destino_gravado;
                objDXP.doxpc_nmonto_imp_destino_mixto = obj.fcoc_nmonto_imp_destino_mixto;
                objDXP.doxpc_nmonto_imp_destino_nogravado = obj.fcoc_nmonto_imp_destino_nogravado;
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.fcoc_nmonto_total_detalle);
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.fcoc_nmonto_total_detalle);
                objDXP.doxpc_nporcentaje_igv = Convert.ToDecimal(obj.fcoc_nporcent_imp_doc);
                objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";
                objDXP.doxpc_sfecha_emision_referencia = null;
                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;
                objDXP.doxpc_vnro_deposito_detraccion = obj.fcoc_vnro_depo_detraccion;
                objDXP.doxpc_sfec_deposito_detraccion = obj.fcoc_sfecha_depo_detraccion;
                objDXP.intUsuario = obj.intUsuario;
                objDXP.strPc = obj.strPc;
                objDXP.doxpc_origen = Parametros.origenComprasFac;
                objDXP.doxpc_itipo_adquisicion = 339;
                objDXP.doxpc_flag_estado = true;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Fac. Compra Nacional Det.
        public List<EFacturaCompraDet> listarFacCompraDet(int intDocCompra)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarFacCompraDet(intDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFacturaCompraDet> listarFacCompraImportacionDet(int intDocCompra)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarFacCompraImportacionDet(intDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarFacCompraNacionalDet(EFacturaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarFacCompraNacionalDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFacCompraNacionalDet(EFacturaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarFacCompraNacionalDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarFacCompraNacionalDet(EFacturaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarFacCompraNacionalDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Boleta Compra Cab
        public List<EBoletaCompra> listarBoletaCompraNacional(int intEjercicio)
        {
            List<EBoletaCompra> lista = null;
            try
            {
                lista = (new ComprasData()).listarBoletaCompraNacional(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarBoletaCompraNacional(EBoletaCompra oBe, List<EBoletaCompraDet> lstDetalle)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagarBC(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarData.insertarDocPorPagar(objDXP);
                    #endregion

                    #region Insertamos Factura de Compra
                    oBe.bcoc_icod_doc = new ComprasData().insertarBoletaCompraNacional(oBe);

                    #region Detalle de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        #region Ingreso a Kardex
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.bcoc_sfecha_doc.Year;
                        obKardex.kardc_fecha_movimiento = oBe.bcoc_sfecha_doc;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bcod_ncantidad);
                        obKardex.tdocc_icod_tipo_doc = 84;//TIPO DE DOCUMENTO 24 FACTURA DE COMPRA
                        obKardex.kardc_numero_doc = oBe.bcoc_num_doc;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                        obKardex.kardc_beneficiario = oBe.strProveedor;
                        obKardex.kardc_observaciones = oBe.bcoc_vreferencia;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        x.bcod_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                        #endregion
                        #region Actualización de Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.bcoc_sfecha_doc.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stck.stocc_stock_producto = Convert.ToInt32(x.bcod_ncantidad);
                        stck.intTipoMovimiento = 1;
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        x.bcoc_icod_doc = oBe.bcoc_icod_doc;
                        insertarBoletaCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                    });
                    #endregion
                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarFacCompraDetCuentas(oBe.bcoc_icod_doc);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                        //if (x.intCtaContable == 0 && x.intClasificacion != 2)
                        //    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //if (x.intCtaContable == 0 && x.intClasificacion == 2)
                        //    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intCtaContable == 0)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada"));
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.fcod_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.fcod_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle
                        objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion
                    #endregion



                    tx.Complete();
                }
                return oBe.bcoc_icod_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarBoletaCompraNacional(EBoletaCompra oBe, List<EBoletaCompraDet> lstDetalle,
         List<EBoletaCompraDet> lstDelete)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarBoletaCompraNacional(oBe);
                    #region Modificación del Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagarBC(oBe);
                    objCuentasPorPagarData.modificarDocPorPagar(objDXP);
                    #endregion
                    #region eliminación del det. de la fac. de compra
                    lstDelete.ForEach(x =>
                    {
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.bcod_icod_kardex);
                        obKardexAnt.intUsuario = oBe.intUsuario;
                        obKardexAnt.strPc = oBe.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        eliminarBoletaCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                    });
                    #endregion
                    #region ingreso o modificación del det. de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            #region Ingreso a Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.bcoc_sfecha_doc.Year;
                            obKardex.kardc_fecha_movimiento = oBe.bcoc_sfecha_doc;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bcod_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = 84;//24 TIPO DOC FACTURA DE COMPRA FAC
                            obKardex.kardc_numero_doc = oBe.bcoc_num_doc;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                            obKardex.kardc_beneficiario = oBe.strProveedor;
                            obKardex.kardc_observaciones = oBe.bcoc_vreferencia;
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.bcod_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualización de Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.bcoc_sfecha_doc.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stck.stocc_stock_producto = Convert.ToInt32(x.bcod_ncantidad);
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            x.bcoc_icod_doc = oBe.bcoc_icod_doc;
                            insertarBoletaCompraNacionalDet(x);
                            ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            #region Ingreso a Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.bcod_icod_kardex);
                            obKardex.kardc_ianio = oBe.bcoc_sfecha_doc.Year;
                            obKardex.kardc_fecha_movimiento = oBe.bcoc_sfecha_doc;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bcod_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = 84;//TIPO DE DOCUMENTO 24 DE FACTURA DE COMPRA
                            obKardex.kardc_numero_doc = oBe.bcoc_num_doc;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = 97;//INGRESO A KARDEX POR COMPRAS ES 97
                            obKardex.kardc_beneficiario = oBe.strProveedor;
                            obKardex.kardc_observaciones = oBe.bcoc_vreferencia;
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            objAlmacenData.modificarKardex(obKardex);
                            #endregion
                            #region Actualización de Kardex
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.bcoc_sfecha_doc.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stck.stocc_stock_producto = Convert.ToInt32(x.bcod_ncantidad);
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            modificarBoletaCompraNacionalDet(x);
                            ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                        }
                        modificarBoletaCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                    });

                    #endregion
                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    objCuentasPorPagarData.eliminarDXPDetCtaContableBC(lstDetDXP, oBe);
                    #endregion
                    #region Reingresar el det. del DXP (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarFacCompraDetCuentas(oBe.bcoc_icod_doc);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = objDXP.doxpc_icod_correlativo;
                        //if (x.intCtaContable == 0 && x.intClasificacion != 2)
                        //    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //if (x.intCtaContable == 0 && x.intClasificacion == 2)
                        //    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.fcod_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.fcod_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarBoletaCompraNacional(EBoletaCompra obj)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación del det. de la fac. de compra
                    var lst = listarBoletaCompraNacionalDet(obj.bcoc_icod_doc);
                    lst.ForEach(x =>
                    {

                        objComprasData.eliminarBoletaCompraNacionalDet(x);
                        ActualizarCantidadFacturada(x.ocod_icod_detalle_oc, x.prd_icod_producto);
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.bcod_icod_kardex);
                        obKardexAnt.intUsuario = obj.intUsuario;
                        obKardexAnt.strPc = obj.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        /*----------------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = obj.bcoc_sfecha_doc.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(obj.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stck.stocc_stock_producto = x.bcod_ncantidad;
                        stck.intTipoMovimiento = 1;
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                    });
                    #endregion
                    #region Eliminación de la Fac. Compra
                    objComprasData.eliminarBoletaCompraNacional(obj);
                    #endregion
                    #region Eliminación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagarBC(obj);
                    objCuentasPorPagarData.eliminarDocPorPagar(objDXP);
                    #endregion
                    #region Eliminación del det. del DXP (Cuentas Contables)
                    var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    objCuentasPorPagarData.eliminarDXPDetCtaContableBC(lstDetDXP, obj);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Boleta Compra Det
        public List<EBoletaCompraDet> listarBoletaCompraNacionalDet(int intDocCompra)
        {
            List<EBoletaCompraDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarBoletaCompraNacionalDet(intDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarBoletaCompraNacionalDet(EBoletaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarBoletaCompraNacionalDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarBoletaCompraNacionalDet(EBoletaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarBoletaCompraNacionalDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarBoletaCompraNacionalDet(EBoletaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarBoletaCompraNacionalDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public EDocPorPagar crearDocPorPagarBC(EBoletaCompra obj)
        {
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                /*gerson*/
                objDXP.doxpc_icod_correlativo = obj.intDXP;
                objDXP.anio = Parametros.intEjercicio;
                objDXP.mesec_iid_mes = obj.bcoc_imes_iid_proceso;
                if (obj.bcoc_flag_importacion == false)
                {
                    objDXP.tdocc_icod_tipo_doc = 84;//TIPO DOCUMENTO 84 DE BOLETA DE COMPRA
                    objDXP.tdodc_iid_correlativo = 31; //CLASE DE DOCUMENTO DE BOLETA DE COMPRA
                }
                else
                {
                    objDXP.tdocc_icod_tipo_doc = 109;//TIPO DOCUMENTO 24 DE FACTURA DE COMPRA
                    objDXP.tdodc_iid_correlativo = 77; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                }
                objDXP.doxpc_iid_correlativo = 0;
                //objDXP.doxpc_vnumero_serio = obj.fcoc_num_doc.Substring(0, 4);
                //objDXP.doxpc_vnumero_correlativo = obj.fcoc_num_doc.Substring(4, 8);
                if (obj.bcoc_num_doc.Length == 12)
                {
                    objDXP.doxpc_vnumero_serio = obj.bcoc_num_doc.Substring(0, 4);
                    objDXP.doxpc_vnumero_correlativo = obj.bcoc_num_doc.Substring(4, 8);
                    objDXP.doxpc_numdoc_tipo = 1;
                }
                else
                {
                    objDXP.doxpc_vnumero_doc = obj.bcoc_num_doc;
                    objDXP.doxpc_numdoc_tipo = 2;
                }

                objDXP.doxpc_vnumero_doc = obj.bcoc_num_doc;
                objDXP.doxpc_sfecha_doc = obj.bcoc_sfecha_doc;
                objDXP.doxpc_sfecha_vencimiento_doc = obj.bcoc_svencimiento;
                objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;

                objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;

                objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.bcoc_sfecha_doc);
                if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXP.doxpc_vdescrip_transaccion = obj.bcoc_vreferencia;
                objDXP.doxpc_nmonto_destino_gravado = obj.bcoc_nmonto_destino_gravado;
                objDXP.doxpc_nmonto_destino_mixto = obj.bcoc_nmonto_destino_mixto;
                objDXP.doxpc_nmonto_destino_nogravado = obj.bcoc_nmonto_destino_nogravado;
                objDXP.doxpc_nmonto_nogravado = obj.bcoc_nmonto_nogravado;
                objDXP.doxpc_nmonto_imp_destino_gravado = obj.bcoc_nmonto_imp_destino_gravado;
                objDXP.doxpc_nmonto_imp_destino_mixto = obj.bcoc_nmonto_imp_destino_mixto;
                objDXP.doxpc_nmonto_imp_destino_nogravado = obj.bcoc_nmonto_imp_destino_nogravado;
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.bcoc_nmonto_total_detalle);
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.bcoc_nmonto_total_detalle);
                objDXP.doxpc_nporcentaje_igv = Convert.ToDecimal(obj.bcoc_nporcent_imp_doc);
                objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";
                objDXP.doxpc_sfecha_emision_referencia = null;
                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;
                objDXP.doxpc_vnro_deposito_detraccion = obj.bcoc_vnro_depo_detraccion;
                objDXP.doxpc_sfec_deposito_detraccion = obj.bcoc_sfecha_depo_detraccion;
                objDXP.intUsuario = obj.intUsuario;
                objDXP.strPc = obj.strPc;
                objDXP.doxpc_origen = Parametros.origenComprasFac;
                objDXP.doxpc_itipo_adquisicion = 339;
                objDXP.doxpc_flag_estado = true;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Fac.Compra Importacion Cab
        public int insertarFacCompraImportacion(EFacturaCompra oBe, List<EFacturaCompraDet> lstDetalle)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarData.insertarDocPorPagar(objDXP);
                    #endregion

                    #region Insertamos Factura de Compra
                    oBe.fcoc_icod_doc = new ComprasData().insertarFacCompraImportacion(oBe);

                    #region Detalle de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        x.fcoc_icod_doc = oBe.fcoc_icod_doc;
                        insertarFacCompraImportacionDet(x);
                    });
                    #endregion
                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarFacCompraDetCuentas(oBe.fcoc_icod_doc);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                        //if (x.intCtaContable == 0 && x.intClasificacion != 2)
                        //    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //if (x.intCtaContable == 0 && x.intClasificacion == 2)
                        //    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intCtaContable == 0)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada"));
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.fcod_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.fcod_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle
                        objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion
                    #endregion


                    tx.Complete();
                }
                return oBe.fcoc_icod_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFacCompraImportacion(EFacturaCompra oBe, List<EFacturaCompraDet> lstDetalle,
        List<EFacturaCompraDet> lstDelete)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarFacCompraImportacion(oBe);
                    #region Modificación del Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    objCuentasPorPagarData.modificarDocPorPagar(objDXP);
                    #endregion
                    #region eliminación del det. de la fac. de compra
                    lstDelete.ForEach(x =>
                    {
                        eliminarFacCompraImportacionDet(x);
                    });
                    #endregion
                    #region ingreso o modificación del det. de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {

                           
                            x.fcoc_icod_doc = oBe.fcoc_icod_doc;
                            insertarFacCompraImportacionDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                           
                            modificarFacCompraImportacionDet(x);
                        }
                    });
                    #endregion
                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    objCuentasPorPagarData.eliminarDXPDetCtaContable(lstDetDXP,oBe);
                    #endregion
                    #region Reingresar el det. del DXP (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarFacCompraDetCuentas(oBe.fcoc_icod_doc);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = objDXP.doxpc_icod_correlativo;
                        //if (x.intCtaContable == 0 && x.intClasificacion != 2)
                        //    throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //if (x.intCtaContable == 0 && x.intClasificacion == 2)
                        //    throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.fcod_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.fcod_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        objCuentasPorPagarData.insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarFacCompraImporatcion(EFacturaCompra obj)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación del det. de la fac. de compra
                    var lst = listarFacCompraDet(obj.fcoc_icod_doc);
                    lst.ForEach(x =>
                    {

                        objComprasData.eliminarFacCompraImportacionDet(x);
                        #region Eliminar Kardex y Stock
                        //EKardex obKardexAnt = new EKardex();
                        //obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.fcod_icod_kardex);
                        //obKardexAnt.intUsuario = obj.intUsuario;
                        //obKardexAnt.strPc = obj.strPc;
                        //new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion                        
                    });
                    #endregion
                    #region Eliminación de la Fac. Compra
                    objComprasData.eliminarFacCompraImportacion(obj);
                    #endregion
                    #region Eliminación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(obj);
                    objCuentasPorPagarData.eliminarDocPorPagar(objDXP);
                    #endregion
                    #region Eliminación del det. del DXP (Cuentas Contables)
                    //var lstDetDXP = objCuentasPorPagarData.listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    //objCuentasPorPagarData.eliminarDXPDetCtaContable(lstDetDXP);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Fac.Compra Importacion Det
        public void insertarFacCompraImportacionDet(EFacturaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarFacCompraImportacionDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFacCompraImportacionDet(EFacturaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarFacCompraImportacionDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarFacCompraImportacionDet(EFacturaCompraDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarFacCompraImportacionDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Percepción
        public List<EPercepcion> listarPercepcionCab(int intEjercicio) 
        {
            List<EPercepcion> lista = null;
            try
            {
                lista = (new ComprasData()).listarPercepcionCab(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPercepcionCab(EPercepcion obj, List<EPercepcionDet> lstDetalle)
        {
            try
            {                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    obj.percc_icod_percepcion = new ComprasData().insertarPercepcionCab(obj);
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(obj);
                    obj.percc_icod_dxp = new CuentasPorPagarData().insertarDocPorPagar(objDXP);
                    objComprasData.modificarPercepcionCab(obj);//se realiza para actualizar el icod del dxp en tabla percep.
                    #endregion                                   
                    lstDetalle.ForEach(x =>
                    {
                        x.percc_icod_percepcion = obj.percc_icod_percepcion;                            
                        new ComprasData().insertarPercepcionDet(x);
                    });
                    tx.Complete();
                }
                return obj.percc_icod_percepcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPercepcionCab(EPercepcion obj, List<EPercepcionDet> lstDetalle, List<EPercepcionDet> lstDelete)
        {
            try
            {                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarPercepcionCab(obj);
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(obj);
                    new CuentasPorPagarData().modificarDocPorPagar(objDXP);                    
                    #endregion
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.percc_icod_percepcion = obj.percc_icod_percepcion;
                            new ComprasData().insertarPercepcionDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                            new ComprasData().modificarPercepcionDet(x);                        
                    });

                    lstDelete.ForEach(x => 
                    {
                        new ComprasData().eliminarPercepcionDet(x);
                    });
                    tx.Complete();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPercepcionCab(EPercepcion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarPercepcionCab(obj);//esta eliminac. tamb. elimina el detalle
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(obj);
                    new CuentasPorPagarData().eliminarDocPorPagar(objDXP);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EDocPorPagar crearDocPorPagar(EPercepcion obj)
        {
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                objDXP.doxpc_icod_correlativo = obj.percc_icod_dxp;
                objDXP.anio = obj.percc_sfecha_percepcion.Year;
                objDXP.mesec_iid_mes = obj.percc_sfecha_percepcion.Month;
                objDXP.tdocc_icod_tipo_doc = Parametros.intTipoDocPercepcionCompra;
                objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocPercepcionCompra;
                objDXP.doxpc_iid_correlativo = 0;
                objDXP.doxpc_vnumero_doc = obj.percc_vnro_percepcion;
                objDXP.doxpc_sfecha_doc = obj.percc_sfecha_percepcion;
                objDXP.doxpc_sfecha_vencimiento_doc = obj.percc_sfecha_percepcion;
                objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;
                objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;
                objDXP.doxpc_nmonto_tipo_cambio = obj.percc_tipo_cambio;

                objDXP.doxpc_vdescrip_transaccion = String.Format("PERCEPCION NRO. {0}", obj.percc_vnro_percepcion);
                objDXP.doxpc_nmonto_destino_gravado = 0;
                objDXP.doxpc_nmonto_destino_mixto = 0;
                objDXP.doxpc_nmonto_destino_nogravado = 0;
                objDXP.doxpc_nmonto_nogravado = obj.percc_nmonto_percibido;
                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nmonto_imp_destino_gravado = 0;
                objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.percc_nmonto_percibido);
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.percc_nmonto_percibido);
                objDXP.doxpc_nporcentaje_igv = 0;
                objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;

                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";

                objDXP.doxpc_sfecha_emision_referencia = null;
                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;
                objDXP.doxpc_icod_documento = obj.percc_icod_percepcion;
                objDXP.intUsuario = obj.intUsuario;
                objDXP.strPc = obj.strPc;
                objDXP.doxpc_origen = Parametros.origenComprasPercepcion;
                objDXP.doxpc_flag_estado = true;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Percepción Detalle
        public List<EPercepcionDet> listarPercepcionDet(int intPercepcion) 
        {
            List<EPercepcionDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarPercepcionDet(intPercepcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Nota Crédito
        public List<ENotaCreditoProvedor> listarNotaCreditoProveedor(int intEjercicio)
        {
            List<ENotaCreditoProvedor> lista = null;
            try
            {
                lista = (new ComprasData()).listarNotaCreditoProveedor(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ENotaCreditoProveedorDet> listarNCProveedorDet(int intNotaCredito)
        {
            List<ENotaCreditoProveedorDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarNotaCreditoProveedorDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNCProveedor(ENotaCreditoProvedor oBe, List<ENotaCreditoProveedorDet> lstDetalle)
        {
            try
            {
                ComprasData objComprasData = new ComprasData();                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    oBe.doxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagar(objDXP);
                    #endregion
                    oBe.ncpc_icod_nota_cred = objComprasData.insertarNotaCreditoProveedor(oBe);

                    lstDetalle.ForEach(x =>
                    {
                        #region Salida de Kardex
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                        obKardex.kardc_fecha_movimiento = oBe.ncpc_fecha_nota_cred;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = x.prd_icod_producto;
                        obKardex.kardc_icantidad_prod = x.ncpd_ncantidad;
                        obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                        obKardex.kardc_numero_doc = oBe.ncpc_nro_nota_cred;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = 103;//SALIDA DE KARDEX POR DEVOLUCIONES ES 103
                        obKardex.kardc_beneficiario = "";
                        obKardex.kardc_observaciones = "";
                        obKardex.intUsuario = x.intUsuario;
                        obKardex.strPc = x.strPc;
                        x.ncpd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                        stck.almac_icod_almacen = oBe.almac_icod_almacen;
                        stck.prdc_icod_producto = x.prd_icod_producto;
                        stck.stocc_stock_producto = x.ncpd_ncantidad;
                        stck.intTipoMovimiento = Parametros.intKardexOut;
                        new AlmacenData().actualizarStock(stck);
                        #endregion
                        x.ncpc_icod_nota_cred = oBe.ncpc_icod_nota_cred;
                        objComprasData.insertarNotaCreditoProveedorDet(x);
                    });

                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarNotaCreditoDetCuentas(oBe.ncpc_icod_nota_cred);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod != 2)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod == 2)
                            throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.ncpd_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.ncpd_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        new CuentasPorPagarData().insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion

                    tx.Complete();
                }
                return oBe.ncpc_icod_nota_cred;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCuentasNCP(List<ENotaCreditoProvedor> Mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var obj in Mlist)
                    {

                       
                        List<EDocCompraDet> lstDetalle = new List<EDocCompraDet>();
                        List<EDocPorPagarDetalleCuentaContable> Lista = new List<EDocPorPagarDetalleCuentaContable>();
                        //listar cuentas
                        Lista = new BCuentasPorPagar().listarDXPDetCtaContable(obj.intDXP);
                        //cuenta cuentas 
                        new CuentasPorPagarData().eliminarDXPDetCtaContable(Lista, null);

                        //#region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                        //var lstDetCuentas = objComprasData.listarNotaCreditoDetCuentas(obj.ncpc_icod_nota_cred);
                        //lstDetCuentas.ForEach(x =>
                        //{
                        //    EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        //    oBeDet.doxpc_icod_correlativo = obj.intDXP;
                        //    if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod != 2)
                        //        throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        //    if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod == 2)
                        //        throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        //    oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        //    if (x.intTipoAnalitica > 0)
                        //        oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        //    oBeDet.cdxpc_nmonto_cuenta = x.ncpd_nmonto_total;
                        //    oBeDet.cdxpc_vglosa = x.ncpd_vdescripcion_item;
                        //    oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        //    oBeDet.intUsuario = obj.intUsuario;
                        //    oBeDet.strPc = obj.strPc;
                        //    oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        //    new CuentasPorPagarData().insertarDXPDetCtaContable(oBeDet);
                        //});
                        //#endregion
              

                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNCProveedor(ENotaCreditoProvedor oBe, List<ENotaCreditoProveedorDet> lstDetalle,
            List<ENotaCreditoProveedorDet> lstDelete) 
        {
            try
            {
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    new CuentasPorPagarData().modificarDocPorPagar(objDXP);
                    #endregion
                    objComprasData.modificarNotaCreditoProveedor(oBe);
                    /**/
                    lstDelete.ForEach(x =>
                    {
                        #region Eliminar Kardex
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.ncpd_icod_kardex);
                        obKardexAnt.intUsuario = oBe.intUsuario;
                        obKardexAnt.strPc = oBe.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion   
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                        stck.almac_icod_almacen = oBe.almac_icod_almacen;
                        stck.prdc_icod_producto = x.prd_icod_producto;
                        stck.stocc_stock_producto = x.ncpd_ncantidad;
                        stck.intTipoMovimiento = Parametros.intKardexOut;
                        new AlmacenData().actualizarStock(stck);
                        #endregion
                        objComprasData.eliminarNotaCreditoProveedorDet(x);
                    });
                    /**/
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                            obKardex.kardc_fecha_movimiento = oBe.ncpc_fecha_nota_cred;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prd_icod_producto;
                            obKardex.kardc_icantidad_prod = x.ncpd_ncantidad;
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                            obKardex.kardc_numero_doc = oBe.ncpc_nro_nota_cred;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = 103;//SALIDA DE KARDEX POR DEVOLUCIONES ES 103
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = x.intUsuario;
                            obKardex.strPc = x.strPc;
                            x.ncpd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                            stck.almac_icod_almacen = oBe.almac_icod_almacen;
                            stck.prdc_icod_producto = x.prd_icod_producto;
                            stck.stocc_stock_producto = x.ncpd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            new AlmacenData().actualizarStock(stck);
                            #endregion
                            x.ncpc_icod_nota_cred = oBe.ncpc_icod_nota_cred;
                            objComprasData.insertarNotaCreditoProveedorDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            #region Salida a Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.ncpd_icod_kardex);
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.ncpc_fecha_nota_cred;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.ncpd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                            obKardex.kardc_numero_doc = oBe.ncpc_nro_nota_cred;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = 103;//SALIDA DE KARDEX POR DEVOLUCIONES ES 103
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = x.intUsuario;
                            obKardex.strPc = x.strPc;
                            objAlmacenData.modificarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = oBe.almac_icod_almacen;
                            stck.prdc_icod_producto = x.prd_icod_producto;
                            stck.stocc_stock_producto = x.ncpd_ncantidad;
                            stck.intTipoMovimiento = Parametros.intKardexOut;
                            new AlmacenData().actualizarStock(stck);

                            #endregion

                            objComprasData.modificarNotaCreditoProveedorDet(x);
                        }
                    });

                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = new CuentasPorPagarData().listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    new CuentasPorPagarData().eliminarDXPDetCtaContable(lstDetDXP, null);
                    #endregion
                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarNotaCreditoDetCuentas(oBe.ncpc_icod_nota_cred);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = objDXP.doxpc_icod_correlativo;
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod != 2)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod == 2)
                            throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.ncpd_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.ncpd_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        new CuentasPorPagarData().insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion

                    tx.Complete();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNCProveedor(ENotaCreditoProvedor oBe)
        {
            try
            {
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lst = objComprasData.listarNotaCreditoProveedorDet(oBe.ncpc_icod_nota_cred);

                    lst.ForEach(x =>
                    {
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.ncpd_icod_kardex);
                        obKardexAnt.intUsuario = oBe.intUsuario;
                        obKardexAnt.strPc = oBe.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = Parametros.intEjercicio;
                        stck.almac_icod_almacen = oBe.almac_icod_almacen;
                        stck.prdc_icod_producto = x.prd_icod_producto;
                        stck.stocc_stock_producto = x.ncpd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        new AlmacenData().actualizarStock(stck);

                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objComprasData.eliminarNotaCreditoProveedorDet(x);
                    });
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    new CuentasPorPagarData().eliminarDocPorPagar(objDXP);

                    objComprasData.eliminarNotaCreditoProveedor(oBe);
                    #endregion
                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = new CuentasPorPagarData().listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    new CuentasPorPagarData().eliminarDXPDetCtaContable(lstDetDXP, null);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EDocPorPagar crearDocPorPagar(ENotaCreditoProvedor oBe)
        {
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                objDXP.doxpc_icod_correlativo = oBe.intDXP;
                objDXP.anio = oBe.ncpc_fecha_nota_cred.Year;
                objDXP.mesec_iid_mes = oBe.ncpc_fecha_nota_cred.Month;
                if (oBe.ncpc_flag_importacion == false)
                {
                    objDXP.tdocc_icod_tipo_doc = 86;//TIPO DOCUMENTO 86 NCP
                    objDXP.tdodc_iid_correlativo = oBe.tdodc_iid_clase_nota_cred; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                }
                else if (true)
                {
                    objDXP.tdocc_icod_tipo_doc = 119;//TIPO DOCUMENTO 119 NCP Importacion
                    objDXP.tdodc_iid_correlativo = oBe.tdodc_iid_clase_nota_cred; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                }

                objDXP.doxpc_iid_correlativo = 0;
                if (oBe.ncpc_nro_nota_cred.Length == 12)
                {
                    objDXP.doxpc_vnumero_serio = oBe.ncpc_nro_nota_cred.Substring(0, 4);
                    objDXP.doxpc_vnumero_correlativo = oBe.ncpc_nro_nota_cred.Substring(4, 8);
                    objDXP.doxpc_numdoc_tipo = 1;
                }
                else
                {
                    objDXP.doxpc_vnumero_doc = oBe.ncpc_nro_nota_cred;
                    objDXP.doxpc_numdoc_tipo = 2;
                }

                objDXP.doxpc_vnumero_doc = oBe.ncpc_nro_nota_cred;
                objDXP.doxpc_sfecha_doc = oBe.ncpc_fecha_nota_cred;
                objDXP.proc_icod_proveedor = oBe.proc_icod_proveedor;
                objDXP.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;//provisional
                objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncpc_fecha_nota_cred);
                if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXP.doxpc_vdescrip_transaccion = oBe.ncpc_nro_doc_ref_doc;
                objDXP.doxpc_nmonto_destino_gravado = oBe.ncpc_nmonto_destino_gravado;
                objDXP.doxpc_nmonto_destino_mixto = oBe.ncpc_nmonto_destino_mixto;
                objDXP.doxpc_nmonto_destino_nogravado = oBe.ncpc_nmonto_destino_nogravado;
                objDXP.doxpc_nmonto_nogravado = oBe.ncpc_nmonto_adq_nogravado;
                objDXP.doxpc_nmonto_imp_destino_gravado = oBe.ncpc_nmonto_imp_destino_gravado;
                objDXP.doxpc_nmonto_imp_destino_mixto = oBe.ncpc_nmonto_imp_destino_mixto;
                objDXP.doxpc_nmonto_imp_destino_nogravado = oBe.ncpc_nmonto_imp_destino_nogravado;
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(oBe.ncpc_nmonto_total_doc);
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(oBe.ncpc_nmonto_total_doc);
                objDXP.doxpc_nporcentaje_igv = Convert.ToDecimal(oBe.ncpc_nporcent_imp_doc);
                objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;

                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";
                objDXP.doxpc_sfecha_emision_referencia = null;

                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;

                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.intUsuario = oBe.intUsuario;
                objDXP.strPc = oBe.strPc;
                objDXP.doxpc_origen = Parametros.origenAlmacenNCP;
                objDXP.doxpc_flag_estado = true;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Nota Credito Importacion
        public int insertarNCProveedorImportacion(ENotaCreditoProvedor oBe, List<ENotaCreditoProveedorDet> lstDetalle)
        {
            try
            {
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    oBe.doxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagar(objDXP);
                    #endregion
                    oBe.ncpc_icod_nota_cred = objComprasData.insertarNotaCreditoProveedorImportacion(oBe);

                    lstDetalle.ForEach(x =>
                    {
                        #region Salida de Kardex
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                        obKardex.kardc_fecha_movimiento = oBe.ncpc_fecha_nota_cred;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = x.prd_icod_producto;
                        obKardex.kardc_icantidad_prod = x.ncpd_ncantidad;
                        obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                        obKardex.kardc_numero_doc = oBe.ncpc_nro_nota_cred;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = 103;//SALIDA DE KARDEX POR DEVOLUCIONES ES 103
                        obKardex.kardc_beneficiario = "";
                        obKardex.kardc_observaciones = "";
                        obKardex.intUsuario = x.intUsuario;
                        obKardex.strPc = x.strPc;
                        x.ncpd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                        stck.almac_icod_almacen = oBe.almac_icod_almacen;
                        stck.prdc_icod_producto = x.prd_icod_producto;
                        stck.stocc_stock_producto = x.ncpd_ncantidad;
                        stck.intTipoMovimiento = Parametros.intKardexOut;
                        new AlmacenData().actualizarStock(stck);
                        #endregion
                        x.ncpc_icod_nota_cred = oBe.ncpc_icod_nota_cred;
                        objComprasData.insertarNotaCreditoProveedorDetImportacion(x);
                    });

                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarNotaCreditoDetCuentas(oBe.ncpc_icod_nota_cred);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod != 2)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod == 2)
                            throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.ncpd_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.ncpd_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        new CuentasPorPagarData().insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion

                    tx.Complete();
                }
                return oBe.ncpc_icod_nota_cred;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNCProveedorImportacion(ENotaCreditoProvedor oBe, List<ENotaCreditoProveedorDet> lstDetalle,
            List<ENotaCreditoProveedorDet> lstDelete)
        {
            try
            {
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    new CuentasPorPagarData().modificarDocPorPagar(objDXP);
                    #endregion
                    objComprasData.modificarNotaCreditoProveedorImportacion(oBe);
                    /**/
                    lstDelete.ForEach(x =>
                    {
                        #region Eliminar Kardex
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.ncpd_icod_kardex);
                        obKardexAnt.intUsuario = oBe.intUsuario;
                        obKardexAnt.strPc = oBe.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                        stck.almac_icod_almacen = oBe.almac_icod_almacen;
                        stck.prdc_icod_producto = x.prd_icod_producto;
                        stck.stocc_stock_producto = x.ncpd_ncantidad;
                        stck.intTipoMovimiento = Parametros.intKardexOut;
                        new AlmacenData().actualizarStock(stck);
                        #endregion
                        objComprasData.eliminarNotaCreditoProveedorDetImportacion(x);
                    });
                    /**/
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                            obKardex.kardc_fecha_movimiento = oBe.ncpc_fecha_nota_cred;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prd_icod_producto;
                            obKardex.kardc_icantidad_prod = x.ncpd_ncantidad;
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                            obKardex.kardc_numero_doc = oBe.ncpc_nro_nota_cred;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = 103;//SALIDA DE KARDEX POR DEVOLUCIONES ES 103
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = x.intUsuario;
                            obKardex.strPc = x.strPc;
                            x.ncpd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.ncpc_fecha_nota_cred.Year;
                            stck.almac_icod_almacen = oBe.almac_icod_almacen;
                            stck.prdc_icod_producto = x.prd_icod_producto;
                            stck.stocc_stock_producto = x.ncpd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            new AlmacenData().actualizarStock(stck);
                            #endregion
                            x.ncpc_icod_nota_cred = oBe.ncpc_icod_nota_cred;
                            objComprasData.insertarNotaCreditoProveedorDetImportacion(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            #region Salida a Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.ncpd_icod_kardex);
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.ncpc_fecha_nota_cred;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.ncpd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                            obKardex.kardc_numero_doc = oBe.ncpc_nro_nota_cred;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = 103;//SALIDA DE KARDEX POR DEVOLUCIONES ES 103
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = x.intUsuario;
                            obKardex.strPc = x.strPc;
                            objAlmacenData.modificarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = oBe.almac_icod_almacen;
                            stck.prdc_icod_producto = x.prd_icod_producto;
                            stck.stocc_stock_producto = x.ncpd_ncantidad;
                            stck.intTipoMovimiento = Parametros.intKardexOut;
                            new AlmacenData().actualizarStock(stck);

                            #endregion

                            objComprasData.modificarNotaCreditoProveedorDetImportacion(x);
                        }
                    });

                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = new CuentasPorPagarData().listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    new CuentasPorPagarData().eliminarDXPDetCtaContable(lstDetDXP, null);
                    #endregion
                    #region Ingreso del Detalle del Doc Por Pagar (Cuentas Contables)
                    var lstDetCuentas = objComprasData.listarNotaCreditoDetCuentas(oBe.ncpc_icod_nota_cred);
                    lstDetCuentas.ForEach(x =>
                    {
                        EDocPorPagarDetalleCuentaContable oBeDet = new EDocPorPagarDetalleCuentaContable();
                        oBeDet.doxpc_icod_correlativo = objDXP.doxpc_icod_correlativo;
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod != 2)
                            throw new ArgumentException(String.Format("No existe Clasificación Contable registrada para la LÍNEA de Producto <<{0}>> <<{1}>>.\nDebe registrar la Clasificación Contable correspondiente en el REGISTRO DE LÍNEAS DE PRODUCTOS (M. Almacenes)", x.strCodLinea, x.strLinea));
                        if (x.intCtaContable == 0 && x.prd_iid_clasificacion_prod == 2)
                            throw new ArgumentException("No existe Cuenta Contable registrada para Servicios de Terceros.\nDebe registrar la Cuenta Contable correspondiente en el REGISTRO DE CLASIFICACIÓN DE SERVICIOS (M. Contabilidad)");
                        oBeDet.ctacc_iid_cuenta_contable = x.intCtaContable;
                        if (x.intTipoAnalitica > 0)
                            oBeDet.anac_icod_analitica = x.intAnaliticaProveedor;
                        oBeDet.cdxpc_nmonto_cuenta = x.ncpd_nmonto_total;
                        oBeDet.cdxpc_vglosa = x.ncpd_vdescripcion_item;
                        oBeDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                        oBeDet.intUsuario = oBe.intUsuario;
                        oBeDet.strPc = oBe.strPc;
                        oBeDet.cdxpc_flag_estado = true; //estado del detalle

                        new CuentasPorPagarData().insertarDXPDetCtaContable(oBeDet);
                    });
                    #endregion

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNCProveedorImportacion(ENotaCreditoProvedor oBe)
        {
            try
            {
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lst = objComprasData.listarNotaCreditoProveedorDet(oBe.ncpc_icod_nota_cred);

                    lst.ForEach(x =>
                    {
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.ncpd_icod_kardex);
                        obKardexAnt.intUsuario = oBe.intUsuario;
                        obKardexAnt.strPc = oBe.strPc;
                        new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = Parametros.intEjercicio;
                        stck.almac_icod_almacen = oBe.almac_icod_almacen;
                        stck.prdc_icod_producto = x.prd_icod_producto;
                        stck.stocc_stock_producto = x.ncpd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        new AlmacenData().actualizarStock(stck);

                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objComprasData.eliminarNotaCreditoProveedorDetImportacion(x);
                    });
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagar(oBe);
                    new CuentasPorPagarData().eliminarDocPorPagar(objDXP);

                    objComprasData.eliminarNotaCreditoProveedorImportacion(oBe);
                    #endregion
                    #region Borrar el det. del DXP (Cuentas Contables) con el fin de reingresar los datos
                    var lstDetDXP = new CuentasPorPagarData().listarDXPDetCtaContable(objDXP.doxpc_icod_correlativo);
                    new CuentasPorPagarData().eliminarDXPDetCtaContable(lstDetDXP, null);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Concepto Presupuesto
        public DataTable ConceptoPresupuestoNacionalReporte()
        {
            DataTable dtResultado = new DataTable();
            dtResultado = objComprasData.ConceptoPresupuestoNacionalReporte();
            return dtResultado;
        }
        public List<EConceptoPresupuestoNacional> ListarConceptoPresupuestoNacional()
        {
            List<EConceptoPresupuestoNacional> lista = null;
            try
            {
                lista = (new ComprasData()).ListarConceptoPresupuestoNacional();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarConceptoPresupuestoNacional(EConceptoPresupuestoNacional objBConceptoPresupuestoNacional)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.InsertarConceptoPresupuestoNacional(objBConceptoPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarConceptoPresupuestoNacional(EConceptoPresupuestoNacional objEBConceptoPresupuestoNacional)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarConceptoPresupuestoNacional(objEBConceptoPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarConceptoPresupuestoNacional(EConceptoPresupuestoNacional objEBConceptoPresupuestoNacional)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarConceptoPresupuestoNacional(objEBConceptoPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Concepto Presupuesto Detalle
        public List<EConceptoPresupuestoNacionalDetalle> ListarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacional obj)
        {
            List<EConceptoPresupuestoNacionalDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).ListarConceptoPresupuestoNacionalDetalle(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacionalDetalle objConceptoPresupuestoNacionalDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.InsertarConceptoPresupuestoNacionalDetalle(objConceptoPresupuestoNacionalDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacionalDetalle objEConceptoPresupuestoNacionalDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarConceptoPresupuestoNacionalDetalle(objEConceptoPresupuestoNacionalDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacionalDetalle objEConceptoPresupuestoNacionalDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarConceptoPresupuestoNacionalDetalle(objEConceptoPresupuestoNacionalDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Presupuesto Nacional
        public List<EPresupuestoNacional> ListarPresupuestoNacional(int Periodo, int Idtipo)
        {
            List<EPresupuestoNacional> lista = null;
            try
            {
                lista = objComprasData.ListarPresupuestoNacional(Periodo, Idtipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarPresupuestoNacional(EPresupuestoNacional objPresupuestoNacional, List<EPresupuestoNacionalDetalle> ListaPresupuestoNacionalDetalle,List<EFacturaCompra> MlistFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int IdPresupuestoNacional = 0;
                    //Insertar Presupuesto Nacional
                    IdPresupuestoNacional = objComprasData.InsertarPresupuestoNacional(objPresupuestoNacional);

                    //Insertar Presupuesto Nacional Detalle
                    foreach (EPresupuestoNacionalDetalle item in ListaPresupuestoNacionalDetalle)
                    {
                        item.prep_icod_presupuesto = IdPresupuestoNacional;
                        objComprasData.InsertarPresupuestoNacionalDetalle(item);
                    }
                    //Insertar Relacion de la Factura con Presupuesto
                    foreach (var _be in MlistFactura.Where(ob=>ob.sflag_relacion_presupuesto==true).ToList())
                    {
                        new ComprasData().modificarFacCompraRelacionPresupuesto(_be.fcoc_icod_doc, IdPresupuestoNacional);
                    }
                    //

                    //Actualizamos el correlativo Numeracion Documento
                    new TesoreriaData().ActualizarNumero(objPresupuestoNacional.prep_iid_anio, Parametros.intTipoDocPresupuestoNacional);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarPresupuestoNacional(EPresupuestoNacional objEPresupuestoNacional, List<EPresupuestoNacionalDetalle> ListaPresupuestoNacionalDetalle,List<EFacturaCompra> mlistPres,List<EFacturaCompra> MlistEliminadosPres)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EPresupuestoNacionalDetalle item in ListaPresupuestoNacionalDetalle)
                    {
                        if (item.TipOper == Convert.ToInt32(Operacion.Nuevo))
                        {
                            item.prep_icod_presupuesto = objEPresupuestoNacional.prep_icod_presupuesto;
                            objComprasData.InsertarPresupuestoNacionalDetalle(item);
                        }
                        else
                        {
                            objComprasData.ActualizarPresupuestoNacionalDetalle(item);
                        }

                    }
                    //eliminamos
                    foreach (var _be in MlistEliminadosPres.ToList())
                    {
                        new ComprasData().modificarFacCompraRelacionPresupuesto(_be.fcoc_icod_doc, 0);
                    }
                    //
                    foreach (var _be in mlistPres.Where(ob => ob.sflag_relacion_presupuesto == true).ToList())
                    {
                        new ComprasData().modificarFacCompraRelacionPresupuesto(_be.fcoc_icod_doc, objEPresupuestoNacional.prep_icod_presupuesto);
                    }
                    //
                    objComprasData.ActualizarPresupuestoNacional(objEPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPresupuestoNacional(EPresupuestoNacional objEPresupuestoNacional)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarPresupuestoNacional(objEPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AnularPresupuestoNacional(int IdPresupuestoNacional, long IdKardex)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EFacturaCompra> MlistFac = new List<EFacturaCompra>();
                    MlistFac = new BCompras().listarFacCompraRelacionPresupuesto(Parametros.intEjercicio, IdPresupuestoNacional);
                    foreach (var _BE in MlistFac)
                    {
                        new ComprasData().modificarFacCompraRelacionPresupuesto(_BE.fcoc_icod_doc, 0);
                    }
                    objComprasData.AnularPresupuestoNacional(IdPresupuestoNacional, IdKardex);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPresupuestoNacionalkardex(EKardex EKardex, EPresupuestoNacional EpresuNacio)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                ////objAlmacenData.EliminarKardex(EKardex.kardc_iid_correlativo);
                //ACTUALIZAR STOCK
                ////objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intPeriodo, Convert.ToInt32(EKardex.kardc_icod_almacen), Convert.ToInt32(EKardex.kardc_iid_producto_especifico));

                EpresuNacio.krdx_icod_kardex = 0;
                EpresuNacio.krdx_sfecha_kardex = null;
                EpresuNacio.prep_isituacion = 303;//generado
                objComprasData.ActualizarPresupuestoNacionalKardex(EpresuNacio);
                tx.Complete();
            }
        }

        public void ActualizarPresupuestoNacionalKardex(EPresupuestoNacional objEPresupuestoNacional, EKardex objEKardex)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    long IdKardex = 0;
                    if (objEKardex.kardc_icod_correlativo == 0)
                    {
                        //Insertar Kardex
                        ////IdKardex = objAlmacenData.InsertarKardex(objEKardex);
                    }
                    else
                    {
                        IdKardex = objEKardex.kardc_icod_correlativo;
                        ////objAlmacenData.ActualizarKardex(objEKardex);
                    }

                    //ACTUALIZAR STOCK
                   //// objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intPeriodo, Convert.ToInt32(objEKardex.kardc_icod_almacen), Convert.ToInt32(objEKardex.kardc_iid_producto_especifico));



                    //Actualiza el presupuesto nacional con el kardex generado
                    objEPresupuestoNacional.krdx_icod_kardex = IdKardex;
                    objComprasData.ActualizarPresupuestoNacionalKardex(objEPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCierrePresupuestoNacional(int IdPresupuestoNacional, DateTime FechaCierre)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarCierrePresupuestoNacional(IdPresupuestoNacional, FechaCierre);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EPresupuestoNacionalDetalle> ListarNacionalPlantilla()
        {
            List<EPresupuestoNacionalDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).ListarNacionalPlantilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EPresupuestoNacionalDetalle> ListarPresupuestoNacionalDetalle(int Code)
        {
            List<EPresupuestoNacionalDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).ListarPresupuestoNacionalDetalle(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarPresupuestoNacionalDetalle(EPresupuestoNacionalDetalle objPresupuestoNacionalDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.InsertarPresupuestoNacionalDetalle(objPresupuestoNacionalDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPresupuestoNacionalDetalle(EPresupuestoNacionalDetalle objEPresupuestoNacionalDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarPresupuestoNacionalDetalle(objEPresupuestoNacionalDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPresupuestoNacionalDetalle(EPresupuestoNacionalDetalle objEPresupuestoNacionalDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarPresupuestoNacionalDetalle(objEPresupuestoNacionalDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPresupuestoNacionalKardex(EPresupuestoNacional objEPresupuestoNacional)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarPresupuestoNacionalKardex(objEPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarMontoNacional(EPresupuestoNacional objEPresupuestoNacional)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarMontoNacional(objEPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int NumeroCorrelativoPresupuestoNacional(int anio)
        {
            try
            {
                return objComprasData.NumeroCorrelativoPresupuestoNacional(anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MOTONAVE
        public void InsertarMotonaves(EMotonaves obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.MotonavesInsertar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarMotonaves(EMotonaves obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarMotonaves(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarMotonaves(EMotonaves obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarMotonaves(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EMotonaves> ListarMotonaves()
        {
            List<EMotonaves> lista = null;
            try
            {
                lista = (new ComprasData()).ListarMotonaves();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Agente Maritimo
        public void InsertarAgenteMaritimo(EAgenteMaritimo obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.AgenteMaritimoInsertar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ModificarAgenteMaritimo(EAgenteMaritimo obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarAgenteMaritimo(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EliminarAgenteMaritimo(EAgenteMaritimo obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarAgenteMaritimo(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EAgenteMaritimo> ListarAgenteMaritimo()
        {
            List<EAgenteMaritimo> lista = null;
            try
            {
                lista = (new ComprasData()).ListarAgenteMaritimo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Agente Aduana
        public void InsertarAgenciaAduana(EAgenciaAduana obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.AgenciaAduanaInsertar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarAgenciaAduana(EAgenciaAduana obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarAgenciaAduana(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarAgenciaAduana(EAgenciaAduana obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarAgenciaAduana(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EAgenciaAduana> ListarAgenciaAduana()
        {
            List<EAgenciaAduana> lista = null;
            try
            {
                lista = (new ComprasData()).ListarAgenciaAduana();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion


        #region Lista de Precio Compra
        public List<EListaPrecioCab> listarPrecioCompra()
        {
            List<EListaPrecioCab> lista = null;
            try
            {
                lista = (new ComprasData()).listarPrecioCompra();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarPrecioCompra(EListaPrecioCab oBe, List<EListaPreciosDetalle> lstDetalle)
        {
            int lprec_icod_proveedor = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                   lprec_icod_proveedor=new ComprasData().insertarPrecioCompra(oBe);

                    #region Detalle 
                    lstDetalle.ForEach(x =>
                    {
                        x.lprec_icod_proveedor = lprec_icod_proveedor;
                        new ComprasData().insertarPrecioCompraDet(x);
                    });
                    #endregion
                    tx.Complete();
                }
                return oBe.lprec_icod_proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPrecioCompra(EListaPrecioCab oBe, List<EListaPreciosDetalle> lstDetalle,
            List<EListaPreciosDetalle> lstDelete)
        {
           
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarPrecioCompra(oBe);
                   
                    #region Eliminar
                    lstDelete.ForEach(x =>
                    {
                        new ComprasData().eliminarPrecioCompraDet(x);
                    });
                    #endregion
                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.lprec_icod_proveedor = oBe.lprec_icod_proveedor;
                            new ComprasData().insertarPrecioCompraDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            new ComprasData().modificarPrecioCompraDet(x);
                        }
                    });
                    #endregion
                 
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPrecioCompra(EListaPrecioCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación 
                    var lst = listarPrecioCompraDet(obj.lprec_icod_proveedor,0,Parametros.intEjercicio);
                    lst.ForEach(x =>
                    {
                        new ComprasData().eliminarPrecioCompraDet(x);
                    });
                    #endregion
                    new ComprasData().eliminarPrecioCompra(obj);

                   
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #endregion

        #region Lista de Precio Compra Det
        public List<EListaPreciosDetalle> listarPrecioCompraDet(int lprec_icod_proveedor, int proc_icod_proveedor,int anio)
        {
            List<EListaPreciosDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).listarPrecioCompraDet(lprec_icod_proveedor, proc_icod_proveedor, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void insertarPrecioCompraDet(EListaPreciosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarPrecioCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPrecioCompraDet(EListaPreciosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarPrecioCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPrecioCompraDet(EListaPreciosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarPrecioCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #region Lista de Precio Compra
        public List<EPedidoProvCab> listarPedidoCompra()
        {
            List<EPedidoProvCab> lista = null;
            try
            {
                lista = (new ComprasData()).listarPedidoCompra();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarPedidoCompra(EPedidoProvCab oBe, List<EPedidoProvDet> lstDetalle)
        {
            int lpedi_icod_proveedor = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lpedi_icod_proveedor = new ComprasData().insertarPedidoCompra(oBe);

                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        x.lpedi_icod_proveedor = lpedi_icod_proveedor;
                        new ComprasData().insertarPedidoCompraDet(x);
                    });
                    #endregion
                    tx.Complete();
                }
                return oBe.lprec_icod_proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void CambiarSituacionPedidoCompra(EPedidoProvCab oBe)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.CambiarSituacionPedidoCompra(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPedidoCompra(EPedidoProvCab oBe, List<EPedidoProvDet> lstDetalle,
            List<EPedidoProvDet> lstDelete)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarPedidoCompra(oBe);

                    #region Eliminar
                    lstDelete.ForEach(x =>
                    {
                        new ComprasData().eliminarPedidoCompraDet(x);
                    });
                    #endregion
                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.lpedi_icod_proveedor = oBe.lpedi_icod_proveedor;
                            new ComprasData().insertarPedidoCompraDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            new ComprasData().modificarPedidoCompraDet(x);
                        }
                    });
                    #endregion

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPrecioCompra(EPedidoProvCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación
                    var lst = listarPedidoCompraDet(obj.lprec_icod_proveedor, Parametros.intEjercicio);
                    lst.ForEach(x =>
                    {
                        new ComprasData().eliminarPedidoCompraDet(x);
                    });
                    #endregion
                    new ComprasData().eliminarPedidoCompra(obj);


                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Lista de Pedido Compra Det
        public List<EPedidoProvDet> listarPedidoCompraDet(int lpedi_icod_proveedor,int Anio)
        {
            List<EPedidoProvDet> lista = null;
            try
            {
                lista = (new ComprasData()).listarPedidoCompraDet(lpedi_icod_proveedor, Anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void insertarPedidoCompraDet(EPedidoProvDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarPedidoCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPedidoCompraDet(EPedidoProvDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarPedidoCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPedidoCompraDet(EPedidoProvDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarPedidoCompraDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Orden Compra
        public int InsertarOrdenCompra(EOrdenCompra obj, List<EOrdenCompra> mlistOrdenCompra, List<EArchivos> lstArchivos)
        {
            try
            {
                int intIcod = 0;
                int intIcod2 = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertar Proveedor
                    intIcod = new ComprasData().InsertarOrdenCompra(obj);
                    if (intIcod == 0)
                    {
                        throw new Exception("El número de la Orden de Compra ya fué utilizado, intente con un número de Orden de Compra superior");
                    }

                    foreach (var ob in mlistOrdenCompra)
                    {
                       ob.ococ_icod_orden_compra = intIcod;
                       intIcod2 = InsertarOrdenCompraDetalle(ob);
                    }
                    foreach (var obd in lstArchivos)
                    {
                        obd.arch_iid_orden_compra_local = intIcod2;
                        objVentas.insertarArchivos(obd);
                    }



                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarOrdenCompra(EOrdenCompra obj, List<EOrdenCompra> mlistOrdenCompra, List<EOrdenCompra> mlistOrdenEliminados,List<EArchivos> lstArchivos)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Proveedor
                    new ComprasData().ActualizarOrdenCompra(obj);

                    foreach (var objdetalle in mlistOrdenCompra)
                    {
                        if (objdetalle.operacion == 1)
                        {
                            
                            objdetalle.ococ_icod_orden_compra = obj.ococ_icod_orden_compra;
                            InsertarOrdenCompraDetalle(objdetalle);

                        }
                        if (objdetalle.operacion == 2)
                        {
                            
                            new ComprasData().ActualizarOrdenCompraDetalle(objdetalle);
                        }
                    }
                    foreach (var objElim in mlistOrdenEliminados)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        EliminarOrdenCompraDetalle(objElim);
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenCompra(EOrdenCompra obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EOrdenCompra> mlist = new List<EOrdenCompra>();
                    mlist = ListarOrdenCompraDetalle(obj.ococ_icod_orden_compra);
                    //Elimina Proveedor
                    foreach (var objElim in mlist)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }

                        EliminarOrdenCompraDetalle(objElim);
                    }
                    if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    {
                        EPedidoProvCab _bee = new EPedidoProvCab();
                        _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                        _bee.lpedi_isituacion_prov = 249;//con pedido
                        //_bee.intUsuario = 1;
                        CambiarSituacionPedidoCompra(_bee);
                    }
                    new ComprasData().EliminarOrdenCompra(obj.ococ_icod_orden_compra);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularOrdenCompra(EOrdenCompra obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EOrdenCompra> mlist = new List<EOrdenCompra>();
                    mlist = ListarOrdenCompraDetalle(obj.ococ_icod_orden_compra);
                    //Elimina Proveedor
                    foreach (var objElim in mlist)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        EliminarOrdenCompraDetalle(objElim);
                    }

                    if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    {
                        EPedidoProvCab _bee = new EPedidoProvCab();
                        _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                        _bee.lpedi_isituacion_prov = 249;//con pedido
                        //_bee.intUsuario = 1;
                        CambiarSituacionPedidoCompra(_bee);
                    }


                    new ComprasData().AnularOrdenCompra(obj.ococ_icod_orden_compra, 284);//Parametros.intSituacOCAnulado);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CambiarSituacionOrdenCompra(EOrdenCompra obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    new ComprasData().AnularOrdenCompra(obj.ococ_icod_orden_compra, obj.tablc_iid_situacion_oc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EOrdenCompra> ListarOrdenCompra()
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompra();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompra> ListarOrdenCompraReporte()
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraReporte();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompra> ListarOrdenCompraRPTConsultar(int Generar,int Entregado_Parcial, int Entregado_Total)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraRPTConsultar(Generar,Entregado_Parcial,Entregado_Total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
       
        #endregion
        #region Orden Compra Detalle
        public int InsertarOrdenCompraDetalle(EOrdenCompra obj)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertar Proveedor
                    intIcod = new ComprasData().InsertarOrdenCompraDetalle(obj);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarOrdenCompraDetalle(EOrdenCompra obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Proveedor
                    new ComprasData().ActualizarOrdenCompraDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenCompraDetalle(EOrdenCompra obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Elimina Proveedor
                    new ComprasData().EliminarOrdenCompraDetalle(obj.ocod_icod_detalle_oc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EOrdenCompra> ListarOrdenCompraDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraDetalle(ococ_icod_orden_compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompra> ListarOrdenCompraDetalleFC(int ococ_icod_orden_compra)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraDetalleFC(ococ_icod_orden_compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Orden Compra Serivicio
        public List<EOrdenCompraServicio> ListarOrdenCompraServicio()
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraServicio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarOrdenCompraServicio(EOrdenCompraServicio obj, List<EOrdenCompraServicio> mlistOrdenCompra, List<EArchivos> lstArchivos)
        {
            try
            {
                int intIcod = 0;
                int intIcod2 = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().InsertarOrdenCompraServicio(obj);
                    if (intIcod == 0)
                    {
                        throw new Exception("El número de la Orden de Compra ya fué utilizado, intente con un número de Orden de Compra superior");
                    }
                    foreach (var ob in mlistOrdenCompra)
                    {
                        ob.ocsc_icod_ocs = intIcod;
                        intIcod2 = InsertarOrdenCompraServicioDetalle(ob);
                    }

                    foreach (var obd in lstArchivos)
                    {
                        obd.arch_iid_orden_compra_local = intIcod2;
                        objVentas.insertarArchivos(obd);
                    }

                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarOrdenCompraServicio(EOrdenCompraServicio obj, List<EOrdenCompraServicio> mlistOrdenCompra, List<EOrdenCompraServicio> mlistOrdenEliminados, List<EArchivos> lstArchivos)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Proveedor
                    new ComprasData().ActualizarOrdenCompraServicio(obj);

                    foreach (var objdetalle in mlistOrdenCompra)
                    {
                        if (objdetalle.operacion == 1)
                        {

                            objdetalle.ocsc_icod_ocs = obj.ocsc_icod_ocs;
                            InsertarOrdenCompraServicioDetalle(objdetalle);

                        }
                        if (objdetalle.operacion == 2)
                        {

                            new ComprasData().ActualizarOrdenCompraServicioDetalle(objdetalle);
                        }
                    }
                    foreach (var objElim in mlistOrdenEliminados)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        EliminarOrdenCompraServicioDetalle(objElim);
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenCompraServicio(EOrdenCompraServicio obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EOrdenCompraServicio> mlist = new List<EOrdenCompraServicio>();
                    mlist = ListarOrdenCompraServicioDetalle(obj.ocsc_icod_ocs);
                    //Elimina Proveedor
                    foreach (var objElim in mlist)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }

                        EliminarOrdenCompraServicioDetalle(objElim);
                    }
                    if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    {
                        EPedidoProvCab _bee = new EPedidoProvCab();
                        _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                        _bee.lpedi_isituacion_prov = 249;//con pedido
                        //_bee.intUsuario = 1;
                        CambiarSituacionPedidoCompra(_bee);
                    }
                    new ComprasData().EliminarOrdenCompraServicio(obj.ocsc_icod_ocs);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularOrdenCompraServicio(EOrdenCompraServicio obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //List<EOrdenCompraServicio> mlist = new List<EOrdenCompraServicio>();
                    //mlist = ListarOrdenCompraServicioDetalle(obj.ocsc_icod_ocs);
                    ////Elimina Proveedor
                    //foreach (var objElim in mlist)
                    //{
                    //    if (objElim.prdc_icod_producto != null)
                    //    {
                    //        //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                    //        //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                    //    }
                    //    EliminarOrdenCompraServicioDetalle(objElim);
                    //}

                    if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    {
                        EPedidoProvCab _bee = new EPedidoProvCab();
                        _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                        _bee.lpedi_isituacion_prov = 249;//con pedido
                        //_bee.intUsuario = 1;
                        CambiarSituacionPedidoCompra(_bee);
                    }


                    new ComprasData().AnularOrdenCompraServicio(obj.ocsc_icod_ocs, 291);//Parametros.intSituacOCAnulado);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
   
        #endregion
        #region Orden Compra Servicio Detalle
        public List<EOrdenCompraServicio> ListarOrdenCompraServicioDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraServicioDetalle(ococ_icod_orden_compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarOrdenCompraServicioDetalle(EOrdenCompraServicio obj)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertar Proveedor
                    intIcod = new ComprasData().InsertarOrdenCompraServicioDetalle(obj);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarOrdenCompraServicioDetalle(EOrdenCompraServicio obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Proveedor
                    new ComprasData().ActualizarOrdenCompraServicioDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenCompraServicioDetalle(EOrdenCompraServicio obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Elimina Proveedor
                    new ComprasData().EliminarOrdenCompraServicioDetalle(obj.ocsd_icod_detalle_ocs);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Orden Compra Importacion
        public List<EOrdenCompraImportacion> ListarOrdenCompraImportacion()
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraImportacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarOrdenCompraImportacion(EOrdenCompraImportacion obj, List<EOrdenCompraImportacion> mlistOrdenCompra, List<EArchivos> lstArchivos)
        {
            try
            {
                int intIcod = 0;
                int intIcod2 = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertar Proveedor
                    intIcod = new ComprasData().InsertarOrdenCompraImportacion(obj);
                    if (intIcod == 0)
                    {
                        throw new Exception("El número de la Orden de Compra ya fué utilizado, intente con un número de Orden de Compra superior");
                    }

                    foreach (var ob in mlistOrdenCompra)
                    {
                        ob.ocic_icod_oci = intIcod;
                        intIcod2 = InsertarOrdenCompraImportacionDetalle(ob);
                    }
                    foreach (var obd in lstArchivos)
                    {
                        obd.arch_iid_orden_compra_local = intIcod2;
                        objVentas.insertarArchivos(obd);
                    }

                    //if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    //{
                    //    EPedidoProvCab _bee = new EPedidoProvCab();
                    //    _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                    //    _bee.lpedi_isituacion_prov = 250;//con pedido
                    //    //_bee.intUsuario = 1;
                    //    CambiarSituacionPedidoCompra(_bee);
                    //}

                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarOrdenCompraImportacion(EOrdenCompraImportacion obj, List<EOrdenCompraImportacion> mlistOrdenCompra, List<EOrdenCompraImportacion> mlistOrdenEliminados, List<EArchivos> lstArchivos)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Proveedor
                    new ComprasData().ActualizarOrdenCompraImportacion(obj);

                    foreach (var objdetalle in mlistOrdenCompra)
                    {
                        if (objdetalle.operacion == 1)
                        {

                            objdetalle.ocic_icod_oci= obj.ocic_icod_oci;
                            InsertarOrdenCompraImportacionDetalle(objdetalle);

                        }
                        if (objdetalle.operacion == 2)
                        {

                            new ComprasData().ActualizarOrdenCompraImportacionDetalle(objdetalle);
                        }
                    }
                    foreach (var objElim in mlistOrdenEliminados)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        EliminarOrdenCompraImportacionDetalle(objElim);
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenCompraImportacion(EOrdenCompraImportacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EOrdenCompraImportacion> mlist = new List<EOrdenCompraImportacion>();
                    mlist = ListarOrdenCompraImportacionDetalle(obj.ocic_icod_oci);
                    //Elimina Proveedor
                    foreach (var objElim in mlist)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }

                        EliminarOrdenCompraImportacionDetalle(objElim);
                    }
                    if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    {
                        EPedidoProvCab _bee = new EPedidoProvCab();
                        _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                        _bee.lpedi_isituacion_prov = 249;//con pedido
                        //_bee.intUsuario = 1;
                        CambiarSituacionPedidoCompra(_bee);
                    }
                    new ComprasData().EliminarOrdenCompraImportacion(obj.ocic_icod_oci);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularOrdenCompraImportacion(EOrdenCompraImportacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EOrdenCompraImportacion> mlist = new List<EOrdenCompraImportacion>();
                    mlist = ListarOrdenCompraImportacionDetalle(obj.ocic_icod_oci);
                    //Elimina Proveedor
                    foreach (var objElim in mlist)
                    {
                        if (objElim.prdc_icod_producto != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        EliminarOrdenCompraImportacionDetalle(objElim);
                    }

                    if (Convert.ToInt32(obj.lpedi_icod_proveedor) != 0)
                    {
                        EPedidoProvCab _bee = new EPedidoProvCab();
                        _bee.lpedi_icod_proveedor = Convert.ToInt32(obj.lpedi_icod_proveedor);
                        _bee.lpedi_isituacion_prov = 249;//con pedido
                        //_bee.intUsuario = 1;
                        CambiarSituacionPedidoCompra(_bee);
                    }


                    new ComprasData().AnularOrdenCompraImportacion(obj.ocic_icod_oci, 298);//Parametros.intSituacOCAnulado);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Orden Compra Importacion Detalle
        public List<EOrdenCompraImportacion> ListarOrdenCompraImportacionDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                lista = (new ComprasData()).ListarOrdenCompraImportacionDetalle(ococ_icod_orden_compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarOrdenCompraImportacionDetalle(EOrdenCompraImportacion obj)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertar Proveedor
                    intIcod = new ComprasData().InsertarOrdenCompraImportacionDetalle(obj);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarOrdenCompraImportacionDetalle(EOrdenCompraImportacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizar Proveedor
                    new ComprasData().ActualizarOrdenCompraImportacionDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenCompraImportacionDetalle(EOrdenCompraImportacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Elimina Proveedor
                    new ComprasData().EliminarOrdenCompraImportacionDetalle(obj.ocid_icod_detalle_oci);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Guia Remision Compras
        public List<EGuiaRemisionCompras> listarGuiaRemisionCompras()
        {
            List<EGuiaRemisionCompras> lista = new List<EGuiaRemisionCompras>();
            try
            {
                lista = objComprasData.listarGuiaRemisionCompras();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGuiaRemisionCompras(EGuiaRemisionCompras oBe, List<EGuiaRemisionComprasDetalle> lstDetalle)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().insertarGuiaRemisionCompras(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        if (x.grcd_ncantidad > 0)
                        {
                            decimal Cantidad_Saldo = new ComprasData().listarCantidadSaldoOC(x.ocod_icod_detalle_oc);
                            if (Cantidad_Saldo < x.grcd_ncantidad)
                            {
                                throw new Exception("La Cantidad Recibida, Sobrepasa al Saldo de la O/C");
                            }
                            /*----------------Ingreso Kardex-----------------*/
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.grcc_sfecha_ingreso;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;                          
                            obKardex.kardc_icantidad_prod = x.grcd_ncantidad;                          
                            obKardex.tdocc_icod_tipo_doc = 103;
                            obKardex.kardc_numero_doc = oBe.grcc_vnumero_grc;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones ="RECEPCION DE PRODUCTOS";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            x.grcc_icod_grcc = intIcod;
                            insertarGuiaRemisionComprasDetalle(x);
                            objComprasData.CambiarSituacionOC(oBe.ococ_icod_orden_compra);
                            /*---------------------------Stock-----------------------*/
                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = oBe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,

                                stocc_stock_producto = obKardex.kardc_icantidad_prod,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                            ActualizarCantidadRequeridaOC(x.ocod_icod_detalle_oc);
                          
                        }

                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionCompras(EGuiaRemisionCompras oBe, List<EGuiaRemisionComprasDetalle> lstDetalle, List<EGuiaRemisionComprasDetalle> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarGuiaRemisionCompras(oBe);
                    lstDetalle.ForEach(x=>
                    {
                        if (x.grcd_ncantidad != x.grcd_ncantidad2)
                        {
                             if (x.grcd_ncantidad == 0)
                                {
                                 EKardex obKardex = new EKardex();
                                 obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                                 obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                 obKardex.intUsuario = oBe.intUsuario;
                                 obKardex.strPc = oBe.strPc;                                        
                                   if (obKardex.kardc_icod_correlativo > 0)
                                       {
                                       /*--eliminar el kardex y stock--------------------------------*/
                                       objAlmacenData.eliminarKardex(obKardex);
                                       eliminarGuiaRemisionComprasDetalle(x);
                                       ActualizarCantidadRequeridaOC(x.ocod_icod_detalle_oc);
                                       }
                                }
                             else if (x.grcd_ncantidad2 == 0)
                             {
                                 if (x.grcd_ncantidad > 0)
                                {
                                    decimal Cantidad_Saldo = new ComprasData().listarCantidadSaldoOC(x.ocod_icod_detalle_oc);
                                    if (Cantidad_Saldo < x.grcd_ncantidad)
                                    {
                                        throw new Exception("La Cantidad Recibida, Sobrepasa al Saldo de la O/C");
                                    }
                                    /*----------------Ingreso Kardex-----------------*/
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = Parametros.intEjercicio;
                                    obKardex.kardc_fecha_movimiento = oBe.grcc_sfecha_ingreso;
                                    obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                    obKardex.prdc_icod_producto = x.prdc_icod_producto;
                                    obKardex.kardc_icantidad_prod = x.grcd_ncantidad;
                                    obKardex.tdocc_icod_tipo_doc = 103;
                                    obKardex.kardc_numero_doc = oBe.grcc_vnumero_grc;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "RECEPCION DE PRODUCTOS";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                    x.grcc_icod_grcc = oBe.grcc_icod_grc;
                                    insertarGuiaRemisionComprasDetalle(x);
                                    /*---------------------------Stock-----------------------*/
                                    EStock objStock = new EStock()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        almac_icod_almacen = oBe.almac_icod_almacen,
                                        prdc_icod_producto = x.prdc_icod_producto,

                                        stocc_stock_producto = obKardex.kardc_icantidad_prod,
                                        intTipoMovimiento = obKardex.kardc_tipo_movimiento
                                    };
                                    objAlmacenData.actualizarStock(objStock);
                                    ActualizarCantidadRequeridaOC(x.ocod_icod_detalle_oc);
                                }
                             }
                        }
                        else
                        {
                            if (x.grcd_ncantidad > x.grcd_ncantidad2)
                            {
                                if (x.grcd_ncantidad > 0)
                                {
                                    decimal Cantidad_Saldo = new ComprasData().listarCantidadSaldoOC(x.ocod_icod_detalle_oc);
                                    if (Cantidad_Saldo < x.grcd_ncantidad)
                                    {
                                        throw new Exception("La Cantidad Recibida, Sobrepasa al Saldo de la O/C");
                                    }
                                    /*----------------Ingreso Kardex-----------------*/
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = Parametros.intEjercicio;
                                    obKardex.kardc_fecha_movimiento = oBe.grcc_sfecha_ingreso;
                                    obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                    obKardex.prdc_icod_producto = x.prdc_icod_producto;
                                    obKardex.kardc_icantidad_prod = x.grcd_ncantidad;
                                    obKardex.tdocc_icod_tipo_doc = 103;
                                    obKardex.kardc_numero_doc = oBe.grcc_vnumero_grc;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "RECEPCION DE PRODUCTOS";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                    x.grcc_icod_grcc = oBe.grcc_icod_grc;
                                    insertarGuiaRemisionComprasDetalle(x);
                                    /*---------------------------Stock-----------------------*/
                                    EStock objStock = new EStock()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        almac_icod_almacen = oBe.almac_icod_almacen,
                                        prdc_icod_producto = x.prdc_icod_producto,

                                        stocc_stock_producto = obKardex.kardc_icantidad_prod,
                                        intTipoMovimiento = obKardex.kardc_tipo_movimiento
                                    };
                                    objAlmacenData.actualizarStock(objStock);
                                    ActualizarCantidadRequeridaOC(x.ocod_icod_detalle_oc);
                                }
                            }
                            else
                            {
                                if (x.grcd_ncantidad > 0)
                                {
                                    decimal Cantidad_Saldo = new ComprasData().listarCantidadSaldoOC(x.ocod_icod_detalle_oc);
                                    if (Cantidad_Saldo < x.grcd_ncantidad)
                                    {
                                        throw new Exception("La Cantidad Recibida, Sobrepasa al Saldo de la O/C");
                                    }
                                    /*----------------Ingreso Kardex-----------------*/
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = Parametros.intEjercicio;
                                    obKardex.kardc_fecha_movimiento = oBe.grcc_sfecha_ingreso;
                                    obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                                    obKardex.prdc_icod_producto = x.prdc_icod_producto;
                                    obKardex.kardc_icantidad_prod = x.grcd_ncantidad;
                                    obKardex.tdocc_icod_tipo_doc = 103;
                                    obKardex.kardc_numero_doc = oBe.grcc_vnumero_grc;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "RECEPCION DE PRODUCTOS";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.grcc_icod_grcc = oBe.grcc_icod_grc;
                                    objAlmacenData.modificarKardex(obKardex);
                                    modificarGuiaRemisionDetalle(x);
                                    /*---------------------------Stock-----------------------*/
                                    EStock objStock = new EStock()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        almac_icod_almacen = oBe.almac_icod_almacen,
                                        prdc_icod_producto = x.prdc_icod_producto,
                                        stocc_stock_producto = obKardex.kardc_icantidad_prod,
                                        intTipoMovimiento = obKardex.kardc_tipo_movimiento
                                    };
                                    objAlmacenData.actualizarStock(objStock);
                                    ActualizarCantidadRequeridaOC(x.ocod_icod_detalle_oc);
                                }
                            }
                        }
                        objComprasData.CambiarSituacionOC(oBe.ococ_icod_orden_compra);                                             
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionCompras(EGuiaRemisionCompras oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarGuiaRemisionCompras(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        #endregion
        #region Guia Remision Compras Detalle
        public List<EGuiaRemisionComprasDetalle> listarGuiaRemisionComprasDetalle(int GuiRemision)
        {
            List<EGuiaRemisionComprasDetalle> lista = new List<EGuiaRemisionComprasDetalle>();
            try
            {
                lista = objComprasData.listarGuiaRemisionComprasDetalle(GuiRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGuiaRemisionComprasDetalle(EGuiaRemisionComprasDetalle oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().insertarGuiaRemisionComprasDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionDetalle(EGuiaRemisionComprasDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarGuiaRemisionDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionComprasDetalle(EGuiaRemisionComprasDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarGuiaRemisionComprasDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        #endregion

        #region Suma Cantidad Requerida
        public void ActualizarCantidadRequeridaOC(int ocod_icod_detalle_oc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarCantidadRequeridaOC(ocod_icod_detalle_oc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Garantia Proveedores
        public List<EGarantiaProveedores> listarGarantiaProveedores()
        {
            List<EGarantiaProveedores> lista = null;
            try
            {
                lista = new ComprasData().listarGarantiaProveedores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGarantiaProveedores(EGarantiaProveedores oBe)
        {
            CuentasPorPagarData objCuentasPorPagarDataGP = new CuentasPorPagarData();
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().insertarGarantiaProveedores(oBe);
                    oBe.garp_icod_garantia = intIcod;
                    #region Generación de Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagarGarantiaProveedores(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarDataGP.insertarDocPorPagar(objDXP);
                    #endregion
                    #region Pago de DXC
                    EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                    obj_DXP_pago.doxpc_icod_correlativo = oBe.fcoc_icod_doc;
                    obj_DXP_pago.tdocc_icod_tipo_doc = 113;
                    obj_DXP_pago.pdxpc_vnumero_doc = oBe.garap_vnumero_garantia;
                    obj_DXP_pago.pdxpc_sfecha_pago = oBe.garp_sfecha_garantia;
                    obj_DXP_pago.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj_DXP_pago.pdxpc_nmonto_pago = oBe.garp_nmonto;
                    obj_DXP_pago.pdxpc_nmonto_tipo_cambio = objDXP.doxpc_nmonto_tipo_cambio;
                    obj_DXP_pago.pdxpc_vobservacion = String.Format("GP N° : {0}", oBe.garap_vnumero_garantia);
                    //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                    obj_DXP_pago.pdxpc_vorigen = "L";
                    obj_DXP_pago.doxcc_icod_correlativo = null;
                    //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                    //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                    //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                    obj_DXP_pago.intUsuario = oBe.intUsuario;
                    obj_DXP_pago.strPc = oBe.strPc;
                    obj_DXP_pago.pdxpc_mes = oBe.garp_sfecha_garantia.Month;
                    obj_DXP_pago.anio = oBe.garp_sfecha_garantia.Year;
                    obj_DXP_pago.pdxpc_flag_estado = true;
                    oBe.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(obj_DXP_pago);
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(oBe.fcoc_icod_doc, obj_DXP_pago.tablc_iid_tipo_moneda);
                    #endregion
                    new ComprasData().modificarGarantiaProveedores(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGarantiaProveedores(EGarantiaProveedores oBe)
        {
            CuentasPorPagarData objCuentasPorPagarDataGP = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.doxpc_icod_correlativo = oBe.intDXP;
                    new ComprasData().modificarGarantiaProveedores(oBe);

                    #region Modificación del Doc Por Pagar
                    EDocPorPagar objDXP = crearDocPorPagarGarantiaProveedores(oBe);
                    objCuentasPorPagarDataGP.modificarDocPorPagar(objDXP);
                    #endregion
                    #region Pago DXP
                    EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                    obj_DXP_pago.doxpc_icod_correlativo = oBe.fcoc_icod_doc;
                    obj_DXP_pago.pdxpc_icod_correlativo = oBe.pdxpc_icod_correlativo;
                    obj_DXP_pago.tdocc_icod_tipo_doc = 113;
                    obj_DXP_pago.pdxpc_vnumero_doc = oBe.garap_vnumero_garantia;
                    obj_DXP_pago.pdxpc_sfecha_pago = oBe.garp_sfecha_garantia;
                    obj_DXP_pago.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj_DXP_pago.pdxpc_nmonto_pago = oBe.garp_nmonto;
                    obj_DXP_pago.pdxpc_nmonto_tipo_cambio = objDXP.doxpc_nmonto_tipo_cambio;
                    obj_DXP_pago.pdxpc_vobservacion = String.Format("GP N° : {0}", oBe.garap_vnumero_garantia);
                    //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                    obj_DXP_pago.pdxpc_vorigen = "L";
                    obj_DXP_pago.doxcc_icod_correlativo = null;
                    //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                    //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                    //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                    obj_DXP_pago.intUsuario = oBe.intUsuario;
                    obj_DXP_pago.strPc = oBe.strPc;
                    obj_DXP_pago.pdxpc_mes = oBe.garp_sfecha_garantia.Month;
                    obj_DXP_pago.anio = oBe.garp_sfecha_garantia.Year;
                    obj_DXP_pago.pdxpc_flag_estado = true;

                    new CuentasPorPagarData().modificarDocPorPagarPago(obj_DXP_pago);
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(oBe.fcoc_icod_doc, obj_DXP_pago.tablc_iid_tipo_moneda);
                    #endregion

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGarantiaProveedores(EGarantiaProveedores oBe)
        {
            CuentasPorPagarData objCuentasPorPagarDataGP = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarGarantiaProveedores(oBe);

                    #region Eliminación de Doc Por Pagar
                    //EDocPorPagar objDXP = crearDocPorPagarGarantiaProveedores(oBe);
                    //objCuentasPorPagarDataGP.eliminarDocPorPagar(objDXP);
                    EDocPorPagar oBeDXC = new EDocPorPagar();
                    oBeDXC.doxpc_icod_correlativo = oBe.intDXP;
                    oBeDXC.intUsuario = oBe.intUsuario;
                    oBeDXC.strPc = oBe.strPc;
                    new CuentasPorPagarData().eliminarDocPorPagar(oBeDXC);
                    #endregion
                    #region Eliminar Pago DXP
                    EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                    oBeDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(oBe.pdxpc_icod_correlativo);
                    oBeDXPPago.intUsuario = oBe.intUsuario;
                    oBeDXPPago.strPc = oBe.strPc;
                    new CuentasPorPagarData().eliminarDocPorPagarPago(oBeDXPPago);
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(oBe.fcoc_icod_doc), oBe.tablc_iid_tipo_moneda);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EDocPorPagar crearDocPorPagarGarantiaProveedores(EGarantiaProveedores obj)
        {
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                objDXP.doxpc_icod_correlativo = obj.intDXP;
                objDXP.anio = obj.garp_sfecha_garantia.Year;
                objDXP.mesec_iid_mes = obj.garp_sfecha_garantia.Month;
                objDXP.tdocc_icod_tipo_doc = 113;//TIPO DOCUMENTO 113 DE GARANTIA PROVEEDORES
                objDXP.tdodc_iid_correlativo = 84; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                objDXP.doxpc_iid_correlativo = 0;
                objDXP.doxpc_vnumero_doc = obj.garap_vnumero_garantia;
                objDXP.doxpc_sfecha_doc = obj.garp_sfecha_garantia;
                objDXP.doxpc_sfecha_vencimiento_doc = obj.garp_sfecha_garantia;
                objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;

                objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;

                objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.garp_sfecha_garantia);
                if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXP.doxpc_vdescrip_transaccion = "GARANTIA POR SERVICIO";
                objDXP.doxpc_nmonto_destino_gravado = 0;
                objDXP.doxpc_nmonto_destino_mixto = 0;
                objDXP.doxpc_nmonto_destino_nogravado = 0;
                objDXP.doxpc_nmonto_nogravado = obj.garp_nmonto;
                objDXP.doxpc_nmonto_imp_destino_gravado = 0;
                objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.garp_nmonto);
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.garp_nmonto);
                objDXP.doxpc_nporcentaje_igv = 0;
                objDXP.tablc_iid_situacion_documento = 8;
                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";
                objDXP.doxpc_sfecha_emision_referencia = null;
                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;
                objDXP.doxpc_vnro_deposito_detraccion = null;
                objDXP.doxpc_sfec_deposito_detraccion = null;
                objDXP.intUsuario = obj.intUsuario;
                objDXP.doxpc_icod_documento = obj.garp_icod_garantia;
                objDXP.strPc = obj.strPc;
                objDXP.doxpc_origen = "9";
                objDXP.doxpc_flag_estado = true;
                objDXP.doxpc_numdoc_tipo = 2;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        public void InsertarimportacionyConceptos(EImportacion objImpotacion, List<EImportacionConceptos> ListaPresupuestoNacionalDetalle, List<EFacturaCompra> MlistFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int IdPresupuestoNacional = 0;
                    //Insertar Importacion
                    IdPresupuestoNacional = objComprasData.InsertarImportacion(objImpotacion);

                    //Insertar Presupuesto Nacional Detalle
                    foreach (EImportacionConceptos item in ListaPresupuestoNacionalDetalle)
                    {
                        item.impc_icod_importacion = IdPresupuestoNacional;
                        objComprasData.InsertarImportacionConceptos(item);
                    }
                    //Insertar Relacion de la Factura con Presupuesto
                    //foreach (var _be in MlistFactura.Where(ob => ob.sflag_relacion_presupuesto == true).ToList())
                    //{
                    //    new ComprasData().modificarFacCompraRelacionPresupuesto(_be.fcoc_icod_doc, IdPresupuestoNacional);
                    //}
                    //

                    //Actualizamos el correlativo Numeracion Documento
                    //new TesoreriaData().ActualizarNumero( Parametros.intTipoDocPresupuestoNacional);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarImportacionyConceptos(EImportacion objEPresupuestoNacional, List<EImportacionConceptos> ListaPresupuestoNacionalDetalle, List<EFacturaCompra> mlistPres, List<EFacturaCompra> MlistEliminadosPres)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EImportacionConceptos item in ListaPresupuestoNacionalDetalle)
                    {
                        //if (item.TipOper == Convert.ToInt32(Operacion.Nuevo))
                        //{
                        //    //item.prep_icod_presupuesto = objEPresupuestoNacional.prep_icod_presupuesto;
                        //    objComprasData.InsertarImportacionConceptos(item);
                        //}
                        //else
                        //{
                        objComprasData.modificarImportacionConceptos(item);
                        //}

                    }
                    ////eliminamos
                    //foreach (var _be in MlistEliminadosPres.ToList())
                    //{
                    //    new ComprasData().modificarFacCompraRelacionPresupuesto(_be.fcoc_icod_doc, 0);
                    //}
                    ////
                    //foreach (var _be in mlistPres.Where(ob => ob.sflag_relacion_presupuesto == true).ToList())
                    //{
                    //    new ComprasData().modificarFacCompraRelacionPresupuesto(_be.fcoc_icod_doc, objEPresupuestoNacional.prep_icod_presupuesto);
                    //}
                    ////
                    objComprasData.modificarImportacion(objEPresupuestoNacional);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Importacion
        public List<EImportacion> ListarImportacion()
        {
            List<EImportacion> lista = null;
            try
            {
                lista = new ComprasData().ListarImportacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarImportacion(EImportacion oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().InsertarImportacion(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarImportacion(EImportacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarImportacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarImportacion(EImportacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarImportacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region importacion_Conceptos

        public List<EImportacionConceptos> ListarImportacionConceptos(int codImportacion)
        {
            List<EImportacionConceptos> lista = null;
            try
            {
                lista = new ComprasData().ListarImportacionConceptos(codImportacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarTablaPlanillaDetalle(EImportacionConceptos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().InsertarImportacionConceptos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptos(EImportacionConceptos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarImportacionConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarImportacionConceptos(int impd_icod_importacion, int intUsuario, string strPc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarImportacionConceptos(impd_icod_importacion,
                        intUsuario,
                        strPc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptosMontoSoles(int impc_icod_importacion, decimal impd_nmonto_concepto_sol)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarImportacionConceptosMontoSoles(impc_icod_importacion, impd_nmonto_concepto_sol);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCostos(int fcoc_icod_doc, decimal fcod_nmonto_unit_costo, decimal fcod_nmonto_total_costo)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().ActualizarCostos(fcoc_icod_doc, fcod_nmonto_unit_costo, fcod_nmonto_total_costo);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptosMontoDolares(int impc_icod_importacion, decimal impd_nmonto_concepto_dol)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarImportacionConceptosMontoDolares(impc_icod_importacion, impd_nmonto_concepto_dol);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptosDXPMontoSoles(int impc_icod_importacion, decimal impd_nmonto_concepto_sol)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarImportacionConceptosDXPMontoSoles(impc_icod_importacion, impd_nmonto_concepto_sol);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptosDXPMontoDolares(int impc_icod_importacion, decimal impd_nmonto_concepto_dol)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarImportacionConceptosDXPMontoDolares(impc_icod_importacion, impd_nmonto_concepto_dol);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Importacion factura

        public List<EImportacionFactura> ListarImportacionFactura(int codImp)
        {
            List<EImportacionFactura> lista = null;
            try
            {
                lista = new ComprasData().ListarImportacionFactura(codImp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarImportacionFactura(EImportacionFactura oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().InsertarImportacionFactura(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ACTUALIZAR_FAC_COMPRA_IMP_FACT(int fcoc_icod_doc, int impd1_icod_import_factura, int impc_icod_importacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().ACTUALIZAR_FAC_COMPRA_IMP_FACT(fcoc_icod_doc,
                       impd1_icod_import_factura, impc_icod_importacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ACTUALIZAR_IMPORTACION_MONTOS(int impc_icod_importacion, decimal impc_nmonto_total_soles, decimal impc_nmonto_total_dolares)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().ACTUALIZAR_IMPORTACION_MONTOS(impc_icod_importacion,
                       impc_nmonto_total_soles, impc_nmonto_total_dolares);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarImportacionFactura(EImportacionFactura oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarImportacionFactura(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EFacturaCompra> FILTAR_FAC_IMP_X_PROVEE(int CodProvee)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                lista = new ComprasData().FILTAR_FAC_IMP_X_PROVEE(CodProvee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion

        #region Suma Cantidad Facturada
        public void ActualizarCantidadFacturada(int ocod_icod_detalle_oc, int prdc_icod_producto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarCantidadFacturada(ocod_icod_detalle_oc, prdc_icod_producto);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DXP Importacion
        public List<EDXPImportacion> ListarDXPImportacion(long doxpc_icod_correlativo)
        {
            List<EDXPImportacion> lista = null;
            try
            {
                lista = new ComprasData().ListarDXPImportacion(doxpc_icod_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarDXPImportacion(EDXPImportacion oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().InsertarDXPImportacion(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDXPImportacion(EDXPImportacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarDXPImportacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDXPImportacion(EDXPImportacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarDXPImportacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void eliminarDXPImportacion(List<EDXPImportacion> ListaEliminadosDXPI)
        //{
        //    try
        //    {
        //        using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            if (ListaEliminadosDXPI != null && ListaEliminadosDXPI.Count > 0)
        //            {
        //                objComprasData.eliminarDXPDetCtaContable(ListaEliminadosDXPI);
        //            }
        //            tx.Complete();
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}    
        #endregion

        #region Correlativo Letra
        public string ObtenerCorrelativoLetra(int anio)
        {
            string NumDocumento = null;
            try
            {
                NumDocumento = (new ComprasData()).ObtenerCorrelativoLetra(anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        #endregion

        #region DXP Importacion Detalle
        public List<EDXPImportacion> listarDXPImpDet(int IcodImpoDet)
        {
            List<EDXPImportacion> lista = null;
            try
            {
                lista = new ComprasData().listarDXPImpDet(IcodImpoDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region DXP Importacion Detalle Todo
        public List<EDXPImportacion> listarDXPImpDetTodo()
        {
            List<EDXPImportacion> lista = null;
            try
            {
                lista = new ComprasData().listarDXPImpDetTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Verificar Numero Factura
        public List<EFacturaCompra> getVerificarNumero(string vnumero)
        {
            List<EFacturaCompra> lista = new List<EFacturaCompra>();
            try
            {
                lista = objComprasData.getVerificarNumero(vnumero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Importacion Factura
        public List<EFacturaCompraDet> ListarFacturaImpor(int idimporta)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                lista = (new ComprasData()).ListarFacturaImpor(idimporta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        public int insertarKardexFactImportacion(EFacturaCompra oBe, List<EFacturaCompraDet> lstDetalle)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Detalle de la fac. de compra
                    lstDetalle.ForEach(x =>
                    {
                        #region Ingreso a Kardex
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.fcoc_sfecha_doc.Year;
                        obKardex.kardc_fecha_movimiento = oBe.fcoc_sfecha_doc;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.fcod_ncantidad);
                        obKardex.tdocc_icod_tipo_doc = 24;//TIPO DE DOCUMENTO 24 FACTURA DE COMPRA
                        obKardex.kardc_numero_doc = oBe.fcoc_num_doc;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = 352;//INGRESO A KARDEX POR COMPRAS ES 352
                        obKardex.kardc_beneficiario = oBe.strProveedor;
                        obKardex.kardc_observaciones = oBe.impc_vnumero_importacion;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        x.fcod_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                        #endregion
                        #region Actualización de Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.fcoc_sfecha_doc.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stck.stocc_stock_producto = Convert.ToInt32(x.fcod_ncantidad);
                        stck.intTipoMovimiento = 1;
                        objAlmacenData.actualizarStock(stck);
                        x.fcoc_icod_doc = oBe.fcoc_icod_doc;
                        modificarFacCompraNacionalDet(x);
                        #endregion
                        #region Modificar Importacion
                        ComprasData objComprasData = new ComprasData();
                        var lstImportacion = new BCompras().ListarImportacion().Where(xi=> xi.impc_icod_importacion == oBe.impc_icod_importacion);
                        EImportacion ObeI = new EImportacion();
                        foreach (var item in lstImportacion)
                        {
                            item.almac_icod_almacen = oBe.almac_icod_almacen;
                            item.fcoc_sfecha_doc = oBe.fcoc_sfecha_doc;
                            objComprasData.modificarImportacion(item);
                        }                       
                        #endregion
                        new ComprasData().SituacionFacturaImportacion(oBe.impc_icod_importacion, 353);
                    });
                    #endregion
                    tx.Complete();
                }
                return oBe.fcoc_icod_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarIngresoKardexImportacion(EImportacion obj)
        {
            CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación del det. de la fac. de compra
                    List<EImportacionFactura> lstImpFactura = new List<EImportacionFactura>();
                    lstImpFactura = new BCompras().ListarImportacionFactura(obj.impc_icod_importacion);
                    lstImpFactura.ForEach(xf=>
                    {
                    
                            var lst = listarFacCompraDet(Convert.ToInt32(xf.fcoc_icod_doc));
                            lst.ForEach(x =>
                            {

                                #region Eliminar Kardex y Stock
                                EKardex obKardexAnt = new EKardex();
                                obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(x.fcod_icod_kardex);
                                obKardexAnt.intUsuario = obj.intUsuario;
                                obKardexAnt.strPc = obj.strPc;
                                new AlmacenData().eliminarKardex(obKardexAnt);
                                #endregion
                                /*----------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(obj.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                                stck.stocc_stock_producto = x.fcod_ncantidad;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #region Modificar Importacion
                                ComprasData objComprasData = new ComprasData();
                                var lstImportacion = new BCompras().ListarImportacion().Where(xi => xi.impc_icod_importacion == obj.impc_icod_importacion);
                                EImportacion ObeI = new EImportacion();
                                foreach (var item in lstImportacion)
                                {
                                    item.almac_icod_almacen = 0;
                                    item.fcoc_sfecha_doc = null;
                                    objComprasData.modificarImportacion(item);
                                }
                                #endregion
                                new ComprasData().SituacionFacturaImportacion(obj.impc_icod_importacion, 333);
                            });
                    });
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EDetalleFacOC> ListarDetalleFacOC(int ococ_icod_orden_compra, int prd_icod_producto)
        {
            List<EDetalleFacOC> lista = null;
            try
            {
                lista = (new ComprasData()).ListarDetalleFacOC(ococ_icod_orden_compra,prd_icod_producto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #region Cantidad Recibida OC
        public void ActualizarCantidadRecibidaOC(int ococ_icod_orden_compra, int prdc_icod_producto, int ocod_icod_detalle_oc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarCantidadRecibidaOC(ococ_icod_orden_compra, prdc_icod_producto, ocod_icod_detalle_oc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ComprasDaot

        public List<EComprasDaot> ListarComprasDaot(decimal monto, int anio)
        {
            List<EComprasDaot> lista = null;
            try
            {
                lista = (new ComprasData()).ListarComprasDaot(monto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EComprasDaotDetalle> ListarComprasDaotDetallexProveedor(long proc_icod_proveedor, int anio)
        {
            List<EComprasDaotDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).ListarComprasDaotDetallexProveedor(proc_icod_proveedor, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EComprasDaotDetalle> ListarComprasDaotDetalle(decimal monto, int anio)
        {
            List<EComprasDaotDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).ListarComprasDaotDetalle(monto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion

        #region Registro firmas
        public List<ERegistroFirmas> listarRegistroFirmas()
        {
            List<ERegistroFirmas> lista = null;
            try
            {
                lista = new ComprasData().listarRegistroFirmas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarRegistroFirmas(ERegistroFirmas oBe)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().insertarRegistroFirmas(oBe);
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRegistroFirmas(ERegistroFirmas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().modificarRegistroFirmas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reporte Produccion
        public List<EReporteProduccion> ListarReporteProduccion(int Periodo)
        {
            List<EReporteProduccion> lista = null;
            try
            {
                lista = (new ComprasData()).ListarReporteProduccion(Periodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarReporteProduccion(EReporteProduccion objReporteProduccion, List<EReporteProduccionDetalle> ListaReporteProduccionDetalle, List<ECostoReporteProduccion> ListaCostoReporteProduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    int IdReporteProduccion = 0;                  

                    ////----------------------------------------------------------------------------------------------------------
                    // objReporteProduccion.rp_id_kardex_producto_ingreso = IdKardexIngreso;
                    objReporteProduccion.rp_sfecha_ing_kardex = null;
                    //Insertar Reporte Producción
                    IdReporteProduccion = objComprasData.InsertarReporteProduccion(objReporteProduccion);

                    foreach (EReporteProduccionDetalle item in ListaReporteProduccionDetalle)
                    {
                        //Datos del Kardex
                        EKardex objE_KardexSalida = new EKardex();
                        objE_KardexSalida.kardc_fecha_movimiento = objReporteProduccion.rp_sfecha_produccion;
                        objE_KardexSalida.almac_icod_almacen = item.almac_icod_almacen;
                        objE_KardexSalida.kardc_ianio = Parametros.intEjercicio;
                        objE_KardexSalida.prdc_icod_producto = item.prdc_icod_producto;
                        objE_KardexSalida.kardc_icantidad_prod = item.rpd_ncant_pro;
                        objE_KardexSalida.tdocc_icod_tipo_doc = Parametros.intTipoDocReporteProduccion;
                        objE_KardexSalida.kardc_numero_doc = objReporteProduccion.rp_num_produccion;
                        objE_KardexSalida.kardc_tipo_movimiento = Parametros.intKardexOut;
                        objE_KardexSalida.kardc_iid_motivo = Parametros.intMovSalTransformacion;
                        objE_KardexSalida.kardc_beneficiario = objReporteProduccion.almac_vdescripcion;
                        objE_KardexSalida.kardc_observaciones = "REPORTE DE PRODUCCIÓN";
                        objE_KardexSalida.intUsuario = 0;
                        //objE_KardexSalida.kardc_flag_estado = true;

                        long IdKardexSalida = 0;
                        //Insertar Kardex
                        IdKardexSalida = objAlmacenData.insertarKardex(objE_KardexSalida);

                        //ACTUALIZAR STOCK
                        objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(item.almac_icod_almacen), Convert.ToInt32(item.prdc_icod_producto));

                        //Insertar Reporte Producción Detalle
                        item.rp_icod_produccion = IdReporteProduccion;
                        item.rpd_id_kardex_salida = IdKardexSalida;
                        objComprasData.InsertarReporteProduccionDetalle(item);
                    }

                    foreach (ECostoReporteProduccion item in ListaCostoReporteProduccion)
                    {
                        //Insertar Costo Reporte Producción
                        item.rp_icod_produccion = IdReporteProduccion;
                        objComprasData.InsertarCostoReporteProduccion(item);
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarReporteProduccion(EReporteProduccion objReporteProduccion, List<EReporteProduccionDetalle> ListaReporteProduccionDetalle, List<ECostoReporteProduccion> ListaCostoReporteProduccion, List<EReporteProduccionDetalle> ListaReporteProduccionDetalleElim, List<ECostoReporteProduccion> ListaCostoReporteProduccionElim)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    foreach (EReporteProduccionDetalle item in ListaReporteProduccionDetalle)
                    {
                        if (item.TipOper == Convert.ToInt32(Operacion.Nuevo))
                        {
                            //Datos del Kardex
                            EKardex objE_KardexSalida = new EKardex();
                            objE_KardexSalida.kardc_fecha_movimiento = objReporteProduccion.rp_sfecha_produccion;
                            objE_KardexSalida.almac_icod_almacen = item.almac_icod_almacen;
                            objE_KardexSalida.kardc_ianio = Parametros.intEjercicio;
                            objE_KardexSalida.prdc_icod_producto = item.prdc_icod_producto;
                            objE_KardexSalida.kardc_icantidad_prod = item.rpd_ncant_pro;
                            objE_KardexSalida.tdocc_icod_tipo_doc = Parametros.intTipoDocReporteProduccion;
                            objE_KardexSalida.kardc_numero_doc = objReporteProduccion.rp_num_produccion;
                            objE_KardexSalida.kardc_tipo_movimiento = Parametros.intKardexOut;
                            objE_KardexSalida.kardc_iid_motivo = Parametros.intMovSalTransformacion;
                            objE_KardexSalida.kardc_beneficiario = objReporteProduccion.almac_vdescripcion;
                            objE_KardexSalida.kardc_observaciones = "REPORTE DE PRODUCCIÓN";
                            objE_KardexSalida.intUsuario = objReporteProduccion.rp_iid_usuario_crea;
                            //objE_KardexSalida.kardc_flag_estado = true;

                            long IdKardexSalida = 0;
                            //Insertar Kardex
                            IdKardexSalida = objAlmacenData.insertarKardex(objE_KardexSalida);

                            //ACTUALIZAR STOCK
                            objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(item.almac_icod_almacen), Convert.ToInt32(item.prdc_icod_producto));

                            //Insertar Reporte Producción Detalle
                            item.rpd_id_kardex_salida = IdKardexSalida;
                            item.rp_icod_produccion = objReporteProduccion.rp_icod_produccion;
                            objComprasData.InsertarReporteProduccionDetalle(item);
                        }
                        else if (item.TipOper == Convert.ToInt32(Operacion.Modificar))
                        {
                            //Datos del Kardex
                            EKardex objE_KardexSalida = new EKardex();
                            objE_KardexSalida.kardc_icod_correlativo = Convert.ToInt32(item.rpd_id_kardex_salida);
                            objE_KardexSalida.kardc_fecha_movimiento = objReporteProduccion.rp_sfecha_produccion;
                            objE_KardexSalida.almac_icod_almacen = item.almac_icod_almacen;
                            objE_KardexSalida.kardc_ianio = Parametros.intEjercicio;
                            objE_KardexSalida.prdc_icod_producto = item.prdc_icod_producto;
                            objE_KardexSalida.kardc_icantidad_prod = item.rpd_ncant_pro;
                            objE_KardexSalida.tdocc_icod_tipo_doc = Parametros.intTipoDocReporteProduccion;
                            objE_KardexSalida.kardc_numero_doc = objReporteProduccion.rp_num_produccion;
                            objE_KardexSalida.kardc_tipo_movimiento = Parametros.intKardexOut;
                            objE_KardexSalida.kardc_iid_motivo = Parametros.intMovSalTransformacion; 
                            objE_KardexSalida.kardc_beneficiario = objReporteProduccion.almac_vdescripcion;
                            objE_KardexSalida.kardc_observaciones = "REPORTE DE PRODUCCIÓN";
                            objE_KardexSalida.intUsuario = 0;
                            //objE_KardexSalida.kardc_flag_estado = true;

                            //Actualizar Kardex
                            objAlmacenData.modificarKardex(objE_KardexSalida);
                            //ACTUALIZAR STOCK
                            objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(item.almac_icod_almacen), Convert.ToInt32(item.prdc_icod_producto));
                          
                            //Actualizar Reporte Producción Detalle
                            objComprasData.ActualizarReporteProduccionDetalle(item);
                        }
                    }

                    foreach (EReporteProduccionDetalle item in ListaReporteProduccionDetalleElim)
                    {
                        //Eliminar Kardex
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(item.rpd_id_kardex_salida);
                        obKardexAnt.intUsuario = item.intUsuario;
                        obKardexAnt.strPc = item.strPc;
                        objAlmacenData.eliminarKardex(obKardexAnt);
                        //new AlmacenData().eliminarKardex(obKardexAnt);
                        #endregion
                      

                        //ACTUALIZAR STOCK
                        objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(item.almac_icod_almacen), Convert.ToInt32(item.prdc_icod_producto));

                        //Eliminar Reporte Produccion Detalle
                        objComprasData.EliminarReporteProduccionDetalle(item);
                    }

                    foreach (ECostoReporteProduccion item in ListaCostoReporteProduccion)
                    {
                        if (item.TipOper == Convert.ToInt32(Operacion.Nuevo))
                        {
                            //Insertar Costo Reporte Producción
                            item.rp_icod_produccion = objReporteProduccion.rp_icod_produccion;
                            objComprasData.InsertarCostoReporteProduccion(item);
                        }
                        else if (item.TipOper == Convert.ToInt32(Operacion.Modificar))
                        {
                            //Actualizar Costo Reporte Producción
                            objComprasData.ActualizarCostoReporteProduccion(item);
                        }
                    }

                    foreach (ECostoReporteProduccion item in ListaCostoReporteProduccionElim)
                    {
                        objComprasData.EliminarCostoReporteProduccion(item);
                    }

                    //Actulizar Reporte de Producción
                    objComprasData.ActualizarReporteProduccion(objReporteProduccion);

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarReporteProduccionMontoTotal(EReporteProduccion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().ActualizarReporteProduccionMontoTotal(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarReporteProduccion(EReporteProduccion objEReporteProduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Eliminar Reporte de Produccion
                    objComprasData.EliminarReporteProduccion(objEReporteProduccion);
                    if (objEReporteProduccion.rp_id_kardex_producto_ingreso != 0)
                    {
                        //Eliminar Kardex
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(objEReporteProduccion.rp_id_kardex_producto_ingreso);
                        obKardexAnt.intUsuario = objEReporteProduccion.intUsuario;
                        obKardexAnt.strPc = objEReporteProduccion.strPc;
                        objAlmacenData.eliminarKardex(obKardexAnt);
                        //objAlmacenData.EliminarKardex(Convert.ToInt64(objEReporteProduccion.rp_id_kardex_producto_ingreso));
                        #endregion
                        

                        //ACTUALIZAR STOCK
                        objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(objEReporteProduccion.almac_icod_almacen), Convert.ToInt32(objEReporteProduccion.prdc_icod_producto));
                    }


                    List<EReporteProduccionDetalle> lstTmpReporteProduccionDetalle = new List<EReporteProduccionDetalle>();
                    lstTmpReporteProduccionDetalle = ListarReporteProduccionDetalle(objEReporteProduccion.rp_icod_produccion);

                    foreach (EReporteProduccionDetalle item in lstTmpReporteProduccionDetalle)
                    {
                        //Eliminar Kardex
                        #region Eliminar Kardex y Stock
                        EKardex obKardexAnt = new EKardex();
                        obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(item.rpd_id_kardex_salida);
                        obKardexAnt.intUsuario = item.intUsuario;
                        obKardexAnt.strPc = item.strPc;
                        objAlmacenData.eliminarKardex(obKardexAnt);
                        //objAlmacenData.EliminarKardex(item.rpd_id_kardex_salida);
                        #endregion
                        

                        //ACTUALIZAR STOCK
                        objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(item.almac_icod_almacen), Convert.ToInt32(item.prdc_icod_producto));


                        //Eliminar Reporte Produccion Detalle
                        objComprasData.EliminarReporteProduccionDetalle(item);
                    }

                    List<ECostoReporteProduccion> lstTmpCostoReporteProduccion = new List<ECostoReporteProduccion>();
                    lstTmpCostoReporteProduccion = ListarCostoReporteProduccion(objEReporteProduccion.rp_icod_produccion);

                    foreach (ECostoReporteProduccion item in lstTmpCostoReporteProduccion)
                    {
                        //Eliminar Costo Reporte Produccion
                        objComprasData.EliminarCostoReporteProduccion(item);
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularReporteProduccion(EReporteProduccion objEReporteProduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Anular Reporte de Produccion
                    objComprasData.AnularReporteProduccion(objEReporteProduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarReporteProduccionKardex(EReporteProduccion objEReporteProduccion, EKardex objEKardex)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    long IdKardex = 0;
                    //Insertar Kardex
                    IdKardex = objAlmacenData.insertarKardex(objEKardex);
                    //ACTUALIZAR STOCK ***falta 
                    objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(objEKardex.almac_icod_almacen), Convert.ToInt32(objEKardex.prdc_icod_producto));



                    //Actualiza el reporte de produccion con el kardex generado
                    objEReporteProduccion.rp_id_kardex_producto_ingreso = IdKardex;
                    objEReporteProduccion.rp_sfecha_ing_kardex = objEKardex.kardc_fecha_movimiento;
                    objComprasData.ActualizarReporteProduccionKardex(objEReporteProduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int NumeroCorrelativoReporteProduccion(int anio)
        {
            try
            {
                return objComprasData.NumeroCorrelativoReporteProduccion(anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPTKardex(EReporteProduccion objEReporteProduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    long IdKardex = 0;
                    //Insertar Kardex
                    #region Eliminar Kardex y Stock
                    EKardex obKardexAnt = new EKardex();
                    obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(objEReporteProduccion.rp_id_kardex_producto_ingreso);
                    obKardexAnt.intUsuario = objEReporteProduccion.intUsuario;
                    obKardexAnt.strPc = objEReporteProduccion.strPc;
                    objAlmacenData.eliminarKardex(obKardexAnt);
                    //new AlmacenData().EliminarKardex(Convert.ToInt64(objEReporteProduccion.rp_id_kardex_producto_ingreso));
                    #endregion
                    
                    //ACTUALIZAR STOCK
                    objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(objEReporteProduccion.almac_icod_almacen), Convert.ToInt32(objEReporteProduccion.prdc_icod_producto));

                    //Actualiza el reporte de produccion con el kardex generado
                    objEReporteProduccion.rp_id_kardex_producto_ingreso = 0;
                    objEReporteProduccion.rp_sfecha_ing_kardex = null;
                    objComprasData.ActualizarReporteProduccionKardex(objEReporteProduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Reporte Produccion Detalle
        public List<EReporteProduccionDetalle> ListarReporteProduccionDetalle(int IdReporteProduccion)
        {
            List<EReporteProduccionDetalle> lista = null;
            try
            {
                lista = (new ComprasData()).ListarReporteProduccionDetalle(IdReporteProduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarReporteProduccionDetalle(EReporteProduccionDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.InsertarReporteProduccionDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarReporteProduccionDetalle(EReporteProduccionDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarReporteProduccionDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarReporteProduccionDetalle(EReporteProduccionDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Eliminar Kardex
                    #region Eliminar Kardex y Stock
                    EKardex obKardexAnt = new EKardex();
                    obKardexAnt.kardc_icod_correlativo = Convert.ToInt32(obj.rpd_id_kardex_salida);
                    obKardexAnt.intUsuario = obj.intUsuario;
                    obKardexAnt.strPc = obj.strPc;
                    objAlmacenData.eliminarKardex(obKardexAnt);
                    //objAlmacenData.eliminarKardex(obj.rpd_id_kardex_salida);
                    #endregion
                    

                    //ACTUALIZAR STOCK
                    objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(obj.prdc_icod_producto));


                    //Eliminar Reporte Produccion Detalle
                    objComprasData.EliminarReporteProduccionDetalle(obj);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarReporteProduccionDetalleCostos(List<EReporteProduccionDetalle> ListaReporteProduccionDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EReporteProduccionDetalle item in ListaReporteProduccionDetalle)
                    {
                        objComprasData.ActualizarReporteProduccionDetalle(item);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Costo Reporte Produccion
        public List<ECostoReporteProduccion> ListarCostoReporteProduccion(int IdReporteProduccion)
        {
            List<ECostoReporteProduccion> lista = null;
            try
            {
                lista = (new ComprasData()).ListarCostoReporteProduccion(IdReporteProduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarCostoReporteProduccion(ECostoReporteProduccion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.InsertarCostoReporteProduccion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCostoReporteProduccion(ECostoReporteProduccion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.ActualizarCostoReporteProduccion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarCostoReporteProduccion(ECostoReporteProduccion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.EliminarCostoReporteProduccion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public void listarVoucherRepProduccion(List<EVoucherContableCab> lstCabeceras, List<EVoucherContableDet> lstDetalleGeneral, int mes, int anio, int idUsuario, string pcCrea)
        {
            try
            {
                var lstParamentros = (new ContabilidadData()).listarParametroContable();
                //int nroVoucher = (new ContabilidadData()).getNroVoucherPorSubdiario(Parametros.intEjercicio, mes, lstParamentros[0].parac_id_sd_cosvta);
                int nroVoucher = new ContabilidadData().UltimoCorrelativoVoucherContableCab();
                List<ECuentaContable> mlistCuenta = (new ContabilidadData()).listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                List<ECuentaContable> listCuentaLocal;

                List<EReporteProduccion> listRepProdCab = objComprasData.ListarReporteProduccionXMes(mes, anio);
                List<EReporteProduccionDetalle> listRepProdDet = objComprasData.ListarReporteProduccionDetalleXMes(mes, anio);

                int id = 0;
                int strNroVco = 1;
                listRepProdCab.ForEach(x =>
                {
                    /***CREANDO VOUCHER CABECERA***/
                    nroVoucher += 1;
                    EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                    #region Cabecera de Comprobante
                    obj_CompCab.anioc_iid_anio = anio;
                    obj_CompCab.mesec_iid_mes = mes;
                    obj_CompCab.vcocc_icod_vcontable = ++id;
                    /*Falta Completar*/
                    obj_CompCab.subdi_icod_subdiario = 22;
                    obj_CompCab.vcocc_fecha_vcontable = x.rp_sfecha_produccion;
                    obj_CompCab.vcocc_glosa = x.rp_voservaciones_produccion;
                    obj_CompCab.vcocc_observacion = x.rp_voservaciones_produccion;
                    /*Falta completar*/
                    //obj_CompCab.vnumero_voucher_contable = String.Format("{0:00}", lstParamentros[0].subdi_iid_costo_venta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                    obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                    obj_CompCab.tablc_iid_moneda = 3;// NUEVOS SOLES
                    obj_CompCab.strTipoMoneda = "S/.";
                    obj_CompCab.intUsuario = idUsuario;
                    obj_CompCab.strPc = pcCrea;
                    //obj_CompCab.cestado = 'A';
                    obj_CompCab.vcocc_tipo_cambio = x.rp_ntipo_cambio;
                    obj_CompCab.tbl_origen = "REPOR-PRODUC";
                    obj_CompCab.tbl_origen_icod = x.rp_icod_produccion;
                    if (Convert.ToDecimal(x.rp_ntipo_cambio) <= 0)
                    {
                        throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                    }
                    #endregion

                    decimal mto;
                    decimal mto_soles;
                    decimal mto_dolares;

                    /***CREANDO VOUCHER DETALLE***/

                    /*** DETALLE HABER ***/
                    #region Haber

                    int cont = 0;
                    List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                    listRepProdDet.Where(obe => obe.rp_icod_produccion == x.rp_icod_produccion).ToList().ForEach(y =>
                    {
                        EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                        obj_CompDet_01.vcocc_icod_vcontable = id;
                        obj_CompDet_01.vcocd_nro_item_det = ++cont;
                        obj_CompDet_01.tdocc_icod_tipo_doc = 47; // Reporte Producción;
                        obj_CompDet_01.vcocd_numero_doc = x.rp_num_produccion;
                        obj_CompDet_01.intUsuario = idUsuario;
                        obj_CompDet_01.strPc = pcCrea;
                        //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                        //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                        //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                        obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(y.clasc_vcuenta_contable_producto);
                        listCuentaLocal = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                        obj_CompDet_01.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                        listCuentaLocal.ForEach(Obe =>
                        {
                            obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        obj_CompDet_01.vcocd_vglosa_linea = y.prdc_vdescripcion_larga;
                        //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                        //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                        mto = Math.Round(Convert.ToDecimal(y.pcp * y.rpd_ncant_pro), 2);

                        obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                        obj_CompDet_01.vcocd_nmto_tot_haber_sol = mto;
                        obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                        obj_CompDet_01.vcocd_nmto_tot_haber_dol = Math.Round(mto / Convert.ToDecimal(x.rp_ntipo_cambio), 2);
                        mto_soles = Convert.ToDecimal(obj_CompDet_01.vcocd_nmto_tot_haber_sol);
                        mto_dolares = Convert.ToDecimal(obj_CompDet_01.vcocd_nmto_tot_haber_dol);

                        obj_CompDet_01.intTipoOperacion = 1;
                        //obj_CompDet_01.cestado = 'A';
                        obj_CompDet_01.vcocd_tipo_cambio = x.rp_ntipo_cambio;
                        lstCompDetalle.Add(obj_CompDet_01);

                        //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                        //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                        //    mto_soles,
                        //    mto_dolares, mlistCuenta);

                    });

                    #endregion

                    /*** DETALLE DEBE ***/
                    #region Debe

                    // SUBPRODUCTO
                    #region Sub Producto
                    if (x.prdc_icod_sub_producto != 0)
                    {
                        EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                        obj_CompDet_03.vcocc_icod_vcontable = id;
                        obj_CompDet_03.vcocd_nro_item_det = ++cont;
                        obj_CompDet_03.tdocc_icod_tipo_doc = 47; // Reporte Producción;
                        obj_CompDet_03.vcocd_numero_doc = x.rp_num_produccion;
                        obj_CompDet_03.intUsuario = idUsuario;
                        obj_CompDet_03.strPc = pcCrea;
                        //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                        //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                        //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                        obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(x.clasc_vcuenta_contable_producto_sub_prod);
                        listCuentaLocal = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                        obj_CompDet_03.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                        listCuentaLocal.ForEach(Obe =>
                        {
                            obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        obj_CompDet_03.vcocd_vglosa_linea = x.SubProductoEspecifico;
                        //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                        //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                        mto = x.rp_nmonto_costo_subprod;

                        obj_CompDet_03.vcocd_nmto_tot_debe_sol = mto;
                        obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                        obj_CompDet_03.vcocd_nmto_tot_debe_dol = Math.Round(mto / Convert.ToDecimal(x.rp_ntipo_cambio), 2);
                        obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                        mto_soles = Convert.ToDecimal(obj_CompDet_03.vcocd_nmto_tot_debe_sol);
                        mto_dolares = Convert.ToDecimal(obj_CompDet_03.vcocd_nmto_tot_debe_dol);

                        obj_CompDet_03.intTipoOperacion = 1;
                        //obj_CompDet_03.cestado = 'A';
                        obj_CompDet_03.vcocd_tipo_cambio = x.rp_ntipo_cambio;
                        lstCompDetalle.Add(obj_CompDet_03);

                        //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                        //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                        //    mto_soles,
                        //    mto_dolares, mlistCuenta);
                    }

                    #endregion

                    // PRODUCTO
                    #region Producto
                    EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                    obj_CompDet_02.vcocc_icod_vcontable = id;
                    obj_CompDet_02.vcocd_nro_item_det = ++cont;
                    obj_CompDet_02.tdocc_icod_tipo_doc = 47; // Reporte Producción;
                    obj_CompDet_02.vcocd_numero_doc = x.rp_num_produccion;
                    obj_CompDet_02.intUsuario = idUsuario;
                    obj_CompDet_02.strPc = pcCrea;
                    //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                    //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                    //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                    obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(x.clasc_vcuenta_contable_producto);
                    listCuentaLocal = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                    obj_CompDet_02.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                    listCuentaLocal.ForEach(Obe =>
                    {
                        obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    obj_CompDet_02.vcocd_vglosa_linea = x.prdc_vdescripcion_larga;
                    //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                    //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                    mto = Convert.ToDecimal(lstCompDetalle.Sum(a => a.vcocd_nmto_tot_haber_sol)) - x.rp_nmonto_costo_subprod;

                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = mto;
                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_sol) / x.rp_ntipo_cambio, 2)) - x.rp_nmonto_costo_subprod;
                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                    mto_soles = Convert.ToDecimal(obj_CompDet_02.vcocd_nmto_tot_haber_sol);
                    mto_dolares = Convert.ToDecimal(obj_CompDet_02.vcocd_nmto_tot_haber_dol);

                    obj_CompDet_02.intTipoOperacion = 1;
                    //obj_CompDet_02.cestado = 'A';
                    obj_CompDet_02.vcocd_tipo_cambio = x.rp_ntipo_cambio;
                    lstCompDetalle.Add(obj_CompDet_02);

                    //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                    //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                    //    mto_soles,
                    //    mto_dolares, mlistCuenta);
                    #endregion

                    #endregion

                    /***TOTALES DEL COMPROBANTE***/

                    if (cont > 0)
                    {
                        obj_CompCab.vcocc_nmto_tot_debe_sol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_debe_sol), 2));
                        obj_CompCab.vcocc_nmto_tot_haber_sol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_sol), 2));
                        obj_CompCab.vcocc_nmto_tot_debe_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_debe_dol), 2));
                        obj_CompCab.vcocc_nmto_tot_haber_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_dol), 2));

                        if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_debe_sol &&
                            obj_CompCab.vcocc_nmto_tot_haber_sol == obj_CompCab.vcocc_nmto_tot_haber_sol)
                        {
                            obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                            obj_CompCab.strVcoSituacion = "Cuadrado";
                        }
                        else
                        {
                            obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                            obj_CompCab.strVcoSituacion = "No Cuadrado";
                        }
                        lstDetalleGeneral.AddRange(lstCompDetalle);
                    }
                    else
                    {
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;
                        obj_CompCab.strVcoSituacion = "Sin Detalle";
                    }

                    //obj_CompCab.Movimiento = cont;
                    obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL   
                    lstCabeceras.Add(obj_CompCab);
                    strNroVco++;
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Documento Recepcion Compras
        public List<EDocRecepcionCompra> listarDocRecepcionCompras()
        {
            List<EDocRecepcionCompra> lista = new List<EDocRecepcionCompra>();
            try
            {
                lista = objComprasData.listarDocRecepcionCompras();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarDocRecepcionCompras(EDocRecepcionCompra oBe, List<EDocRecepcionCompraDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objComprasData.insertarDocRecepcionCompras(oBe);


                    #region Factura Det. Insertar
                    lstDetalle.ForEach(x =>
                    {

                        #region Salida de Kardex
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = Parametros.intEjercicio;
                        obKardex.kardc_fecha_movimiento = Convert.ToDateTime(oBe.drcc_sfecha);
                        obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.drcd_ncantidad);
                        obKardex.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                        obKardex.kardc_numero_doc = oBe.drcc_vnumero_doc_recepcion_compra;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                        obKardex.kardc_beneficiario = "";
                        obKardex.kardc_observaciones = "";
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        x.dcrd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                        /*--------------------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stckS.stocc_stock_producto = x.drcd_ncantidad;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        #region Transferencia
                        //if (oBe.tablc_iid_motivo == 226)
                        //{
                        //    #region Ingreso de Kardex
                        //    EKardex obKardexIngreso = new EKardex();
                        //    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                        //    obKardexIngreso.kardc_fecha_movimiento = Convert.ToDateTime(oBe.drcc_sfecha);
                        //    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                        //    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        //    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                        //    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                        //    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                        //    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                        //    obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                        //    obKardexIngreso.kardc_beneficiario = "";
                        //    obKardexIngreso.kardc_observaciones = "";
                        //    obKardexIngreso.intUsuario = oBe.intUsuario;
                        //    obKardexIngreso.strPc = oBe.strPc;
                        //    x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                        //    /*-----------------------------------------------------------------------------*/
                        //    #region Actualizando Stock
                        //    EStock stck = new EStock();
                        //    stck.stocc_ianio = Parametros.intEjercicio;
                        //    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);
                        //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        //    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                        //    stck.intTipoMovimiento = 1;
                        //    objAlmacenData.actualizarStock(stck);
                        //    #endregion
                        //    #endregion
                        //}
                        #endregion
                        x.drcc_icod_doc_recepcion_compra = intIcod;
                        objComprasData.insertarDocRecepcionComprasDetalle(x);
                    });
                    #endregion

                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocRecepcionCompras(EDocRecepcionCompra oBe, List<EDocRecepcionCompraDet> lstDetalle, List<EDocRecepcionCompraDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarDocRecepcionCompras(oBe);
                    lstDelete.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.dcrd_icod_kardex);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*--------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stckS.stocc_stock_producto = x.drcd_ncantidad;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        #region Transferencia
                        //if (oBe.tablc_iid_motivo == 226)
                        //{
                        //    #region Eliminar Kardex Ingreso
                        //    EKardex obKardexDelIngreso = new EKardex();
                        //    obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                        //    obKardexDelIngreso.intUsuario = oBe.intUsuario;
                        //    obKardexDelIngreso.strPc = oBe.strPc;
                        //    objAlmacenData.eliminarKardex(obKardexDelIngreso);
                        //    /*----------------------------------------------------------------------------------*/
                        //    #region Actualizando Stock
                        //    EStock stck = new EStock();
                        //    stck.stocc_ianio = Parametros.intEjercicio;
                        //    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        //    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                        //    stck.intTipoMovimiento = 1;
                        //    objAlmacenData.actualizarStock(stck);
                        //    #endregion
                        //    #endregion
                        //}
                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objComprasData.eliminarDocRecepcionComprasDetalle(x);
                    });

                    lstDetalle.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {

                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.dcrd_icod_kardex);
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.drcc_sfecha;
                            obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.drcd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                            obKardex.kardc_numero_doc = oBe.drcc_vnumero_doc_recepcion_compra;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.dcrd_icod_kardex = objAlmacenData.insertarKardex(obKardex);
                            /*------------------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stckS.stocc_stock_producto = x.drcd_ncantidad;
                            stckS.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            //verificar stock del producto
                            //decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, Convert.ToInt32(oBe.almac_icod_almacen), x.prdc_icod_producto);
                            //if (Stock_Producto < Convert.ToDecimal(x.dremc_ncantidad_producto))
                            //{
                            //    throw new Exception("Stock insuficiente para el producto: " + x.strDesProducto + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            //}
                            #region Transferencia
                            //if (oBe.tablc_iid_motivo == 226)
                            //{
                            //    #region Ingreso de Kardex
                            //    EKardex obKardexIngreso = new EKardex();
                            //    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                            //    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_remision;
                            //    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                            //    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            //    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            //    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                            //    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                            //    obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                            //    obKardexIngreso.kardc_beneficiario = "";
                            //    obKardexIngreso.kardc_observaciones = "";
                            //    obKardexIngreso.intUsuario = oBe.intUsuario;
                            //    obKardexIngreso.strPc = oBe.strPc;
                            //    x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                            //    /*-------------------------------------------------------------------------*/
                            //    #region Actualizando Stock
                            //    EStock stck = new EStock();
                            //    stck.stocc_ianio = Parametros.intEjercicio;
                            //    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            //    stck.intTipoMovimiento = 1;
                            //    objAlmacenData.actualizarStock(stck);
                            //    #endregion

                            //    #endregion

                            //}
                            #endregion

                            x.drcc_icod_doc_recepcion_compra = oBe.drcc_icod_doc_recepcion_compra;
                            objComprasData.insertarDocRecepcionComprasDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            #region Tranferencia
                            //if (oBe.tablc_iid_motivo == 226)
                            //{
                            //    #region Eliminar Kardex Ingreso
                            //    EKardex obKardexDelIngreso = new EKardex();
                            //    obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            //    obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            //    obKardexDelIngreso.strPc = oBe.strPc;
                            //    objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            //    /*---------------------------------------------------------------*/
                            //    EKardex obKardexIngreso = new EKardex();
                            //    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                            //    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_remision;
                            //    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                            //    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            //    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            //    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                            //    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                            //    obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                            //    obKardexIngreso.kardc_beneficiario = "";
                            //    obKardexIngreso.kardc_observaciones = "";
                            //    obKardexIngreso.intUsuario = oBe.intUsuario;
                            //    obKardexIngreso.strPc = oBe.strPc;
                            //    x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                            //    #region Actualizando Stock
                            //    EStock stck = new EStock();
                            //    stck.stocc_ianio = Parametros.intEjercicio;
                            //    stck.almac_icod_almacen = Convert.ToInt32(obKardexIngreso.almac_icod_almacen);
                            //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            //    stck.intTipoMovimiento = 1;
                            //    objAlmacenData.actualizarStock(stck);
                            //    #endregion
                            //    #endregion
                            //}
                            #endregion
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.dcrd_icod_kardex);
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.drcc_sfecha;
                            obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.drcd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                            obKardex.kardc_numero_doc = oBe.drcc_vnumero_doc_recepcion_compra;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            objAlmacenData.modificarKardex(obKardex);
                            /*----------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(obKardex.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                            stckS.stocc_stock_producto = x.drcd_ncantidad;
                            stckS.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objComprasData.modificarDocRecepcionDetalle(x);
                        }

                    });

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDocRecepcionCompras(EDocRecepcionCompra oBe, List<EDocRecepcionCompraDet> lstDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.eliminarDocRecepcionCompras(oBe);

                    lstDetalle.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.dcrd_icod_kardex);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prd_icod_producto);
                        stckS.stocc_stock_producto = x.drcd_ncantidad;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        #region Trnaqferencia
                        //if (oBe.tablc_iid_motivo == 226)
                        //{
                        //    #region Eliminar Kardex Ingreso
                        //    EKardex obKardexDelIngreso = new EKardex();
                        //    obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                        //    obKardexDelIngreso.intUsuario = oBe.intUsuario;
                        //    obKardexDelIngreso.strPc = oBe.strPc;
                        //    objAlmacenData.eliminarKardex(obKardexDelIngreso);
                        //    /*--------------------------------------------------------------*/
                        //    #region Actualizando Stock
                        //    EStock stck = new EStock();
                        //    stck.stocc_ianio = Parametros.intEjercicio;
                        //    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        //    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                        //    stck.intTipoMovimiento = 1;
                        //    objAlmacenData.actualizarStock(stck);
                        //    #endregion
                        //    #endregion
                        //}
                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objComprasData.eliminarDocRecepcionComprasDetalle(x);
                    });

                    //lstDelete.ForEach(x =>
                    //{
                    //    x.intUsuario = oBe.intUsuario;
                    //    x.strPc = oBe.strPc;
                    //    objVentasData.eliminarGuiaRemisionDet(x);
                    //});

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Documento Recepcion Compras Detalle
        public List<EDocRecepcionCompraDet> listarDocRecepcionComprasDetalle(int GuiRemision)
        {
            List<EDocRecepcionCompraDet> lista = new List<EDocRecepcionCompraDet>();
            try
            {
                lista = objComprasData.listarDocRecepcionComprasDetalle(GuiRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarDocRecepcionComprasDetalle(EDocRecepcionCompraDet oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ComprasData().insertarDocRecepcionComprasDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocRecepcionDetalle(EDocRecepcionCompraDet oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objComprasData.modificarDocRecepcionDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDocRecepcionComprasDetalle(EDocRecepcionCompraDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ComprasData().eliminarDocRecepcionComprasDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
