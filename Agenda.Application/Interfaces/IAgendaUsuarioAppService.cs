using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaUsuarioAppService
    {
        void Registrar(CriarAgendaUsuarioViewModel agendaUsuarioViewModel);
        IEnumerable<DetalhesAgendaUsuarioViewModel> ObterTodasAtivos();
        DetalhesAgendaUsuarioViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarAgendaUsuarioViewModel agendaUsuarioViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
