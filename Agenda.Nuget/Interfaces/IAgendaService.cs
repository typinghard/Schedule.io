using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IAgendaService
    {
        Guid Criar(Models.Agenda agenda);
        void Editar(Models.Agenda agenda);
        void Excluir(Models.Agenda agenda);
        Models.Agenda Obter(Guid agendaId);
        IEnumerable<Models.Agenda> ObterTodas();
    }
}
