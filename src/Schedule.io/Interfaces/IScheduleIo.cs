using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces
{
    public interface IScheduleIo
    {
        IEventoService Eventos();
        IUsuarioService Usuarios();
        ILocalService Locais();
        IAgendaService Agendas();
    }
}
