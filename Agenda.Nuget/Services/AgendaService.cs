using Agenda.Domain.Interfaces;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        public AgendaService(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }
        public Guid Criar(Models.Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public void Editar(Models.Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Models.Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Models.Agenda Obter(Guid agendaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Agenda> ObterTodas()
        {
            var agendas = _agendaRepository.ObterTodosAtivos();
            foreach(var agenda in agendas) 
            {
                yield return new Models.Agenda()
                {
                    Id = agenda.Id
                };
            }
        }
    }
}
