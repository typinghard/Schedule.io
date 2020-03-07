using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        void Registrar(CriarUsuarioViewModel usuarioViewModel);
        IEnumerable<DetalhesUsuarioViewModel> ObterTodosUsuariosAtivos();
        DetalhesUsuarioViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarUsuarioViewModel usuarioViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
