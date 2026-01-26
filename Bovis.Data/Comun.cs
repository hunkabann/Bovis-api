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

            CultureInfo oCultura = CultureInfo.CreateSpecificCulture(sCultura);

            //DateTime fechaActual = DateTime.Now.Date;
            dttSalida = Convert.ToDateTime(cadena, oCultura);
            bool esHoy = false;

            if (DateTime.Now.Date == dttSalida)
            {
                esHoy = true;
            }

            return esHoy;
        }
    }
}
