using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class InvestigacionListasNegras
    {

        private readonly PolizaJuridicaDbContext _context;

        public InvestigacionListasNegras(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        public Boolean ListasNegras(string nombres, string ApellidoPaterno, string ApellidoMaterno, string RFC, string RazonSocial)
        {
            bool isMatch = false;

            List<Listanegra> ls = new List<Listanegra>();

            if (nombres != null && ApellidoPaterno != null && ApellidoMaterno != null && RFC == null && RazonSocial == null)            
                ls = _context.Listanegra.Where(l => nombres.Contains(l.Nombres) && ApellidoPaterno.Contains(l.ApellidoPaterno) && ApellidoMaterno.Contains(l.ApellidoPaterno)).ToList();            

            if (nombres != null && ApellidoPaterno != null && ApellidoMaterno == null && RFC == null && RazonSocial == null)           
                ls = _context.Listanegra.Where(l => nombres.Contains(l.Nombres) && ApellidoPaterno.Contains(l.ApellidoPaterno)).ToList();

            if (nombres != null && ApellidoPaterno != null && ApellidoMaterno == null && RFC != null && RazonSocial == null)
                ls = _context.Listanegra.Where(l => nombres.Contains(l.Nombres) && ApellidoPaterno.Contains(l.ApellidoPaterno)).ToList();

            if (nombres != null && ApellidoPaterno != null && ApellidoMaterno != null && RFC != null && RazonSocial == null)
                ls = _context.Listanegra.Where(l => nombres.Contains(l.Nombres) && ApellidoPaterno.Contains(l.ApellidoPaterno)).ToList();

            if (nombres != null && ApellidoPaterno != null && ApellidoMaterno != null && RFC != null && RazonSocial != null)
                ls = _context.Listanegra.Where(l => nombres.Contains(l.Nombres) && ApellidoPaterno.Contains(l.ApellidoPaterno)).ToList();

            if (nombres != null && ApellidoPaterno != null && ApellidoMaterno == null && RFC != null && RazonSocial != null)
                ls = _context.Listanegra.Where(l => nombres.Contains(l.Nombres) && ApellidoPaterno.Contains(l.ApellidoPaterno)).ToList();

            if (ls.Count >= 1)
                isMatch = true;

            return isMatch;
        }
    }
}
