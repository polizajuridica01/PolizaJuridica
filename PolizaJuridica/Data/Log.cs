using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string LogPantalla { get; set; }
        public string LogProceso { get; set; }
        public string LogObjetoIn { get; set; }
        public string LogObjetoOut { get; set; }
        public DateTime LogFecha { get; set; }
    }
}
