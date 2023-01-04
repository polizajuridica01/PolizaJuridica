using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolizaJuridica.Utilerias
{
    public class CalculoPolizaRegalias
    {
        private readonly PolizaJuridicaDbContext _context;

        public CalculoPolizaRegalias(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        public void Calcular(int id)
        {
            List<int> categoriasList = new List<int>();
            categoriasList.Add(2);
            categoriasList.Add(3);
            //2: Polizas
            //3: Regalias
            var poliza = _context.Poliza.Include(pol => pol.FisicaMoral.Solicitud.Representante.Representacion).AsNoTracking().SingleOrDefault(pol => pol.FisicaMoralId == id);
            if (poliza != null)
            {
                var dpPoliza = _context.DetallePoliza.Where(dps => dps.CategoriaEsid == 2 && dps.PolizaId == poliza.PolizaId).AsNoTracking().SingleOrDefault();
                var dpregalias = _context.DetallePoliza.Where(dps => dps.CategoriaEsid == 3 && dps.PolizaId == poliza.PolizaId).AsNoTracking().SingleOrDefault();
                int idPoliza = dpPoliza != null ? dpPoliza.DetallePolizaId : 0;
                int idregalias = dpregalias != null ? dpregalias.DetallePolizaId : 0;
                CreamosActualizamosPoliza(poliza, idPoliza);
                CreamosActualizamosRegalias(poliza, idregalias);
            }
        }
        public void CreamosActualizamosPoliza(Poliza poliza,int id)
        {
            try
            {

                //se agrega el detalle de la poliza
                decimal cantidad = 0;
                var rp = poliza.FisicaMoral.Solicitud.Representante.Representacion;
                DetallePoliza dpPoliza = new DetallePoliza()
                {
                    CategoriaEsid = 2, // esta es la categoria de la poliza
                    Importe = poliza.FisicaMoral.Solicitud.CentroCostosId > 0
                    ? _context.CentroCostos.Find(poliza.FisicaMoral.Solicitud.CentroCostosId).CentroCostosMonto
                    : (decimal)poliza.FisicaMoral.Solicitud.CostoPoliza,
                    PolizaId = poliza.PolizaId,
                    DetallePolizaId = id,

                };
                if (id > 0)
                {
                    _context.DetallePoliza.Update(dpPoliza);
                }
                else
                {
                    _context.DetallePoliza.Add(dpPoliza);
                }
                //este primer bloque registra el monto de la poliza como entrada
                
                
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log log = new Log()
                {
                    LogObjetoIn = JsonConvert.SerializeObject(poliza),
                    LogFecha = DateTime.Now,
                    LogPantalla = "CreamosActualizamosPoliza",
                    LogProceso = "CreamosActualizamosPoliza",
                    LogObjetoOut = ex.Message

                };
                _context.Add(log);
                _context.SaveChanges();
            }
        }
            public void CreamosActualizamosRegalias(Poliza poliza,int id)
        {
            DetallePoliza dp = new DetallePoliza();
            try
            {
                double IVA = 1.16;
                decimal porcentaje = 0;
                decimal precioPoliza = 0;
                decimal importe = 0;
                List<int> listRepresentaciones = new List<int>();
                listRepresentaciones.Add(4);//Francisco Castro
                listRepresentaciones.Add(29);//Cimatario

                porcentaje = poliza.FisicaMoral.Solicitud.Representante != null ? (decimal)poliza.FisicaMoral.Solicitud.Representante.Representacion.Porcentaje : 1;
                precioPoliza = poliza.FisicaMoral.Solicitud.CentroCostosId > 0
                    ? _context.CentroCostos.Find(poliza.FisicaMoral.Solicitud.CentroCostosId).CentroCostosMonto
                    : (decimal)poliza.FisicaMoral.Solicitud.CostoPoliza;
                
                dp.PolizaId = poliza.PolizaId;
                dp.CategoriaEsid = 3;
                /*calculamos la regalia*/
                if (listRepresentaciones.Contains(poliza.FisicaMoral.Solicitud.Representante.RepresentacionId))
                {
                    precioPoliza = precioPoliza / Convert.ToDecimal(IVA);
                }
                
                importe = (porcentaje * precioPoliza) / 100;
                dp.Importe = importe;
                dp.DetallePolizaId = id;
                if (id > 0)
                {
                    _context.DetallePoliza.Update(dp);
                }
                else
                {
                    _context.DetallePoliza.Add(dp);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log log = new Log()
                {
                    LogObjetoIn = JsonConvert.SerializeObject(dp),
                    LogFecha = DateTime.Now,
                    LogPantalla = "CreamosActualizamosRegalias",
                    LogProceso = "CreamosActualizamosRegalias",
                    LogObjetoOut = ex.Message

                };
                _context.Add(log);
                _context.SaveChanges();
            }
        }
    }
}
