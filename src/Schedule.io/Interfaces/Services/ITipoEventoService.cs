using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
{
    public interface ITipoEventoService
    {
        void Gravar(TipoEvento tipoEvento);

        TipoEvento Obter(string tipoEventoId);

        IEnumerable<TipoEvento> Listar();

        void Excluir(string tipoEventoId);
    }
}
