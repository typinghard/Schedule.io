using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Services
{
    public interface IEventoService
    {
        string Gravar(Evento evento);
        void Excluir(string eventoId);
        Evento Obter(string eventoId);
        IEnumerable<Evento> Listar(string agendaId);
        IEnumerable<Evento> Listar(string agendaId, DateTime dataInicial, DateTime dataFinal);
    }
}


/*
 Obter -> é um
 Listar -> Vários (slcccc)

     */