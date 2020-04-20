namespace Schedule.io.Infra.SqlServerDB.Extensions
{
    public static class SeedAgendaContext
    {
        public static void CriarTabelas(this AgendaContext agendaContext)
        {
            agendaContext.Database.EnsureCreated();
        }
    }
}
