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
        DetalhesConviteViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarConviteViewModel conviteViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
