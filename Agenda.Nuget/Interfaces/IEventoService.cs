using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IEventoService
    {
        
        Guid Gravar(Evento evento);

        bool Excluir(Guid id);

        Evento Obter(Guid eventoId);

        //IEnumerable<Evento> ObterTodos();
    }
}
