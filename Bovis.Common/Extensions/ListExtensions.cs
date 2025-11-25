using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Bovis.Common.Extensions
{
    public static class ListExtensions
    {
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            DataTable table = new DataTable(typeof(T).Name);
            if (list == null || list.Count == 0)
                return table;

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Crear columnas
            foreach (var prop in props)
            {
                Type colType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, colType);
            }

            // Llenar filas
            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                foreach (var prop in props)
                {
                    object value = prop.GetValue(item) ?? DBNull.Value;
                    row[prop.Name] = value;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
