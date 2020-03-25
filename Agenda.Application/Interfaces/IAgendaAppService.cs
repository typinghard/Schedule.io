using System;
using System.Collections.Generic;
using Agenda.Application.ViewModels;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaAppService
    {
        void Registrar(CriarAgendaViewModel agendaViewModel);
        IEnumerable<DetalhesAgendaViewModel> ObterTodasAgendasAtivas();
        DetalhesAgendaViewModel ObterPorId(string id);
        void Atualizar(AtualizarAgendaViewModel agendaViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
