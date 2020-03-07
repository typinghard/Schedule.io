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
        DetalhesLocalViewModel ObterPorId(Guid id);
        void Atualizar(AtualizarLocalViewModel localViewModel);
        void Remover(Guid id);
        //IList<CustomerHistoryData> GetAllHistory(Guid id);

        void Dispose();
    }
}
