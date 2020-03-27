using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Interfaces
{
    public interface IConviteAppService
    {
        void Registrar(CriarConviteViewModel conviteViewModel);
        IEnumerable<DetalhesConviteViewModel> ObterTodosConvitesAtivos();
        DetalhesConviteViewModel ObterPorId(string id);
        void Atualizar(AtualizarConviteViewModel conviteViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
