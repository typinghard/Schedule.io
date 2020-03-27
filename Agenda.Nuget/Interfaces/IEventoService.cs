using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IEventoService
    {

        string Gravar(Evento evento);

        bool Excluir(string id);

        Evento Obter(string eventoId);

        //IEnumerable<Evento> ObterTodos();
    }
}
