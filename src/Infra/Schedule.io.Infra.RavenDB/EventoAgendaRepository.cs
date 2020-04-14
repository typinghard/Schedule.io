﻿using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class EventoAgendaRepository : Repository<Evento>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(IDocumentSession session) : base(session)
        {

        }

        public void AdicionarConvite(Convite convite)
        {
            var sessaoConvite = (IDocumentSession)convite;
            sessaoConvite.Store(convite);
            sessaoConvite.SaveChanges();
        }

        public void ExcluirConvite(Convite convite)
        {
            var sessaoConvite = (IDocumentSession)convite;
            sessaoConvite.Delete(convite);
            sessaoConvite.SaveChanges();
        }

        public IList<Convite> ListarConvites(string eventoId)
        {
            return Sessao
                 .Query<Convite>()
                 .Where(x => x.EventoId == eventoId)
                 .ToList();
        }

        public IList<Evento> ListarEventosDaAgenda(string agendaId)
        {
            return Sessao
                 .Query<Evento>()
                 .Where(x => x.AgendaId == agendaId)
                 .ToList();
        }

        public IList<Evento> ListarEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Sessao
                .Query<Evento>()
                .Where(x => x.AgendaId == agendaId
                       && x.DataInicio >= dataInicio
                       && (x.DataFinal == null || x.DataFinal <= dataFinal))
                .ToList();
        }

        public IList<Evento> ListarTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            return Sessao
                 .Query<Evento>()
                 .Where(x => x.AgendaId == agendaId
                        && x.UsuarioIdCriador == usuarioId)
                 .ToList();
        }
    }
}