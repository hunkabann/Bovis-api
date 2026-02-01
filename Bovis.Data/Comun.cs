using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public static class Comun
    {
        public static bool EsFechaActual(this string cadena, string sCultura, out DateTime dttSalida)
        {
            dttSalida = new DateTime();

            bool esHoy = false;
            CultureInfo oCultura = CultureInfo.CreateSpecificCulture(sCultura);

            
            if (cadena == null || cadena == "")
            {
                dttSalida = DateTime.Now.Date;
                esHoy = true;
                return esHoy;
            }

            dttSalida = Convert.ToDateTime(cadena, oCultura);
            if (DateTime.Now.Date == dttSalida)
            {
                esHoy = true;
            }

            return esHoy;
        }

        public static string ObtieneCultura()
        {
            return "es-Mx";
        }
    }
}
