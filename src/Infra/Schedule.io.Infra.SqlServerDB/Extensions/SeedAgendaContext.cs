namespace Schedule.io.Infra.SqlServerDB.Extensions
{
    public static class SeedAgendaContext
    {
        public static void CriarTabelas(AgendaContext agendaContext)
        {
            agendaContext.Database.EnsureCreated();
        }
    }
}
