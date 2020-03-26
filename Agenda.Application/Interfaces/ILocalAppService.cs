using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Interfaces
{
    public interface ILocalAppService
    {
        void Registrar(CriarLocalViewModel localViewModel);
        IEnumerable<DetalhesLocalViewModel> ObterTodosLocaisAtivos();
        DetalhesLocalViewModel ObterPorId(string id);
        void Atualizar(AtualizarLocalViewModel localViewModel);
        void Remover(string id);
        //IList<CustomerHistoryData> GetAllHistory(string id);

        void Dispose();
    }
}
