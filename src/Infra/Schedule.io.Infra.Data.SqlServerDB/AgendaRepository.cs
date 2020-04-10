using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
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
    }
}
