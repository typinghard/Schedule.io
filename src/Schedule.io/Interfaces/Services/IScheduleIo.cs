namespace Schedule.io.Interfaces.Services
{
    public interface IScheduleIo
    {
        IEventoService Eventos();
        IUsuarioService Usuarios();
        ILocalService Locais();
        IAgendaService Agendas();
        ITipoEventoService TiposDeEvento();
    }
}
