using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DeudorApp.Models
{
    public class Movimiento
    {
        public string _colorNuevo;

        public string IdMovimiento { get; set; }
        public string Fecha { get; set; }
        public string FechaFormato { get; set; }
        public string IdCuenta { get; set; }
        public string Tipo { get; set; }
        public string Tipo2 { get; set; }
        public string Nota { get; set; }
        public string Cantidad { get; set; }
        public string TotalViejo { get; set; }
        public string TotalNuevo { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaFormC {
            get
            {
                return DateTime.ParseExact(Fecha, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
         }

        public string colorNuevo {
            get {
                if (Tipo2 == "INGRESO")
                {
                    return "#428655";
                }
                else
                {
                    return "#9A1C1C";
                }
                
            } 
        }

    }
}
