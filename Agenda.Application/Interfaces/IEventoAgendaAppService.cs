using System;
using System.Collections.Generic;
using Agenda.Application.ViewModels;

namespace Agenda.Application.Interfaces
{
    public interface IEventoAgendaAppService
    {
        void Registrar(CriarEventoAgendaViewModel eventoViewModel);
        IEnumerable<DetalhesEventoAgendaViewModel> ObterTodosEventosAgendaAtivos();
        DetalhesEventoAgendaViewModel ObterPorId(string id);
        void Atualizar(AtualizarEventoAgendaViewModel eventoViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
