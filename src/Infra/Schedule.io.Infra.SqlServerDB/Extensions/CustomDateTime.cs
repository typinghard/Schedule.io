using System;

namespace Schedule.io.Infra.SqlServerDB.Extensions
{
    public static class CustomDateTime
    {
        public static string FormataDataSql(this DateTime data, bool inicio = false)
        {
            DateTime dataFormatada;
            if (inicio) dataFormatada = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
            else dataFormatada = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);

            return dataFormatada.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
