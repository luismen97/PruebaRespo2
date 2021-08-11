using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DeudorApp.Models
{
    public class Movimiento
    {
        /*NOTA: ES CONSULTA RELACIONAL, NO SOLO TIENE LOS CAMPOS DE MOVIMIENTO, TAMBIÉN LOS DE CREDITO QUE ESTÉN RELACIONADOS A ALGÚN CRÉDITO*/
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

        /*CREDITOS*/
        public string MontoCredito { get; set; }
        public string Nombre { get; set; }

        public string Saldo { get; set; }

        public string NombreCredito { get; set; }

    }
}
