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
        DetalhesAgendaUsuarioViewModel ObterPorId(string id);
        void Atualizar(AtualizarAgendaUsuarioViewModel agendaUsuarioViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
