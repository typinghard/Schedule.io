using System;
using System.Collections.Generic;
using Agenda.Application.ViewModels;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaAppService
    {
        void Registrar(CriarAgendaViewModel agendaViewModel);
        IEnumerable<DetalhesAgendaViewModel> ObterTodasAgendasAtivas();
        DetalhesAgendaViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarAgendaViewModel agendaViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
