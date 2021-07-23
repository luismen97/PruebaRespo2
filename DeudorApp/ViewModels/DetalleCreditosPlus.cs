using System;
using System.Collections.Generic;
using System.Text;

namespace DeudorApp.ViewModels
{
    public class DetalleCreditosPlus
    {
        public string NombreCliente { get; set; }
        public string NombreCredito { get; set; }
        public string idCliente { get; set; }
        public string idTienda { get; set; }
        public string TiendaOtros { get; set; }
        public string MontoCredito { get; set; }
        public string idtipo_credito { get; set; }
        public string IdCuenta { get; set; }
        public string status { get; set; }

    }
    public class RootCreditos
    {
        public List<DetalleCreditosPlus> CreditosPlus { get; set; }
    }
}
