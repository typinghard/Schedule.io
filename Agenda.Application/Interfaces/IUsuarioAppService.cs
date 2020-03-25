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
        DetalhesUsuarioViewModel ObterPorId(string id);
        void Atualizar(AtualizarUsuarioViewModel usuarioViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
