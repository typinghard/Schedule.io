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
                             SELECT a.Id, a.CriadoAs, a.AtualizadoAs, a.Inativo, 
				                    Titulo, Descricao, Publico
                             FROM {_table} a 
                             INNER JOIN AgendaUsuario au on a.Id = au.AgendaId
                             INNER JOIN Usuario u on au.UsuarioId = u.Id
                             WHERE AgendaId = '{agendaId}'
                             and UsuarioId = '{usuarioId}'
                             and a.{_inativoFalse} 
            ");


        }
    }
}
