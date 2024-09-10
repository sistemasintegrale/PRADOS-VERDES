﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using SGE.Entity;
using SGE.DataAccess;

namespace SGE.BusinessLogic
{
    public class BDocXCobrarDocxPagarCanje
    {
        CuentasPorCobrarData objDocXCobrarData = new CuentasPorCobrarData();

        public void InsertarCanjeDXCconDXP(EDocPorPagarPago canje, int opcionVCO)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.InsertarCanjeDXCconDXP(canje, opcionVCO);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCanjeDXCconDXP(EDocPorPagarPago canje, int opcionVCO)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.ActualizarCanjeDXCconDXP(canje, opcionVCO);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCanjeDXCconDXP(EDocXCobrarDocxPagarCanje canje)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.EliminarCanjeDXCconDXP(canje);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
