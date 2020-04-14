
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(AgendaContext context) : base(context)
        {
        }

        public void Gravar(AgendaUsuario agendaUsuario)
        {
            throw new NotImplementedException();
        }

        public IList<Agenda> ListarAgendasPorUsuarioId(string usuarioId)
        {
            throw new NotImplementedException();
        }

        public Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            return Obter(@$"
                             SELECT a.*
                             FROM {_table} a 
                             INNER JOIN AgendaUsuario au on a.Id = au.AgendaId
                             WHERE AgendaId = '{agendaId}'
                             and au.UsuarioId = '{usuarioId}'
                             and a.{_inativoFalse} 
            ");


        }

        public bool VerificaSeAgendaExiste(string agendaId)
        {
            throw new NotImplementedException();
        }
    }
}
