using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Interfaces
{
    public interface IEventoUsuarioAppService
    {
        void Registrar(CriarEventoUsuarioViewModel eventoUsuarioViewModel);
        //IEnumerable<DetalhesEventoUsuarioViewModel> ObterTodosEventosUsuarioAtivos();
        //DetalhesEventoUsuarioViewModel ObterPorId(string id);
        //void Atualizar(AtualizarEventoUsuarioViewModel eventoUsuarioViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
