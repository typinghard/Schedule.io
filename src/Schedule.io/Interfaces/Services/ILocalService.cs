using Schedule.io.Models.AggregatesRoots;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Services
{
    public interface ILocalService
    {
        void Gravar(Local local);
        Local Obter(string localId);
        IEnumerable<Local> Listar();
        void Excluir(string localId);
    }
}
