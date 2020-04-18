using Schedule.io.Interfaces.Services;

namespace Schedule.io.Services
{
    public class ScheduleIoService : IScheduleIo
    {
        private readonly IEventoService _eventoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILocalService _localService;
        private readonly IAgendaService _agendaService;
        private readonly ITipoEventoService _tipoEventoService;

        public ScheduleIoService(
            IEventoService eventoService,
            IUsuarioService usuarioService,
            ILocalService localService,
            IAgendaService agendaService,
            ITipoEventoService tipoEventoService
            )
        {
            _eventoService = eventoService;
            _usuarioService = usuarioService;
            _localService = localService;
            _agendaService = agendaService;
            _tipoEventoService = tipoEventoService;
        }

        public IAgendaService Agendas()
        {
            return _agendaService;
        }

        public IEventoService Eventos()
        {
            return _eventoService;
        }

        public ILocalService Locais()
        {
            return _localService;
        }

        public ITipoEventoService TiposDeEvento()
        {
            return _tipoEventoService;
        }

        public IUsuarioService Usuarios()
        {
            return _usuarioService;
        }
    }
}
