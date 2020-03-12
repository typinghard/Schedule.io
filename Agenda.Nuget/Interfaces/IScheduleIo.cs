using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IScheduleIo
    {
        IEventoService Eventos();
        IUsuarioService Usuarios();
        ILocalService Locais();
        IAgendaService Agendas();
    }
}
