using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Services
{
    public interface IEventoService
    {
        void Gravar(Evento evento);
        Evento Obter(string eventoId);
        IEnumerable<Evento> Listar(string agendaId);
        IEnumerable<Evento> Listar(string agendaId, string usuarioId);
        IEnumerable<Evento> Listar(string agendaId, DateTime dataInicial, DateTime dataFinal);
        void Excluir(string eventoId);
    }
}

