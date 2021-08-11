using System;
using System.Collections.Generic;
using System.Text;

namespace DeudorApp.Models
{
    public class Creditos
    {
        public string idcredito { get; set; }
        public string NombreCredito { get; set; }

        public string CURP { get; set; }
       

        public string FormatoCred
        {
            get
            {
                return NombreCredito+"-"+CURP;
            }
        }

        public string tipoCredito { get; set; }
        public string Nombre { get; set; }
        public string MontoCredito { get; set; }
    }
}
