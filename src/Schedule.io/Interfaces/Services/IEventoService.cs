using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
{
    public interface IEventoService
    {
        string Gravar(Core.Models.EventoAgenda evento);
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