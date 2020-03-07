using System;
using System.Collections.Generic;
using Agenda.Application.ViewModels;

namespace Agenda.Application.Interfaces
{
    public interface IEventoAgendaAppService
    {
        void Registrar(CriarEventoAgendaViewModel eventoViewModel);
        IEnumerable<DetalhesEventoAgendaViewModel> ObterTodosEventosAgendaAtivos();
        DetalhesEventoAgendaViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarEventoAgendaViewModel eventoViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
