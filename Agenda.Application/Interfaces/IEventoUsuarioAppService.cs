using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Interfaces
{
    public interface IEventoUsuarioAppService
    {
        void Registrar(CriarEventoUsuarioViewModel eventoUsuarioViewModel);
        IEnumerable<DetalhesEventoUsuarioViewModel> ObterTodosEventosUsuarioAtivos();
        DetalhesEventoUsuarioViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarEventoUsuarioViewModel eventoUsuarioViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
