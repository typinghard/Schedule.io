using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Extensions
{
    public static class CustomDateTime
    {
        public static string FormataDataSql(this DateTime data, bool inicio = false)
        {
            var dataFormatada = DateTime.MinValue;

            if (inicio) dataFormatada = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
            else dataFormatada = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);

            return dataFormatada.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
