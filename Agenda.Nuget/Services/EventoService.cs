using Agenda.Domain.Interfaces;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    internal class EventoService : IEventoService
    {
        private readonly IEventoAgendaRepository _eventoAgendaRepository;
        public EventoService(IEventoAgendaRepository eventoAgendaRepository) 
        {
            _eventoAgendaRepository = eventoAgendaRepository;
        }
        public Guid Criar(Evento evento)
        {
            throw new NotImplementedException();
        }

        public void Editar(Evento evento)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Evento evento)
        {
            throw new NotImplementedException();
        }

        public Evento Obter(Guid eventoId)
        {
            var eventoModel = _eventoAgendaRepository.ObterPorId(eventoId);
            var eventoVm = new Evento()
            {
                Id = eventoModel.Id
            };

            return eventoVm;
        }
    }
}
