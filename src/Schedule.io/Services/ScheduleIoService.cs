using Schedule.io.Interfaces;
using Schedule.io.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Services
{
    public class ScheduleIoService : IScheduleIo
    {
        private readonly IEventoService _eventoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILocalService _localService;
        private readonly IAgendaService _agendaService;

        public ScheduleIoService(
            IEventoService eventoService,
            IUsuarioService usuarioService,
            ILocalService localService,
            IAgendaService agendaService
            )
        {
            _eventoService = eventoService;
            _usuarioService = usuarioService;
            _localService = localService;
            _agendaService = agendaService;
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

        public IUsuarioService Usuarios()
        {
            return _usuarioService;
        }
    }
}
