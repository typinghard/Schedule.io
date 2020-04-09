using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class EventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }

        public IList<EventoAgenda> ObterEventosDaAgenda(string agendaId)
        {
            //return ObterLista(@$"
            //                     SELECT {_atributosBase},
            //                            AgendaId, UsuarioId, IdentificadorExterno, Titulo, Descricao,
            //                            LocalId, DataInicio, DataFinal, DataLimiteConfirmacao, QuantidadeMinimaDeUsuarios,
            //                            OcupaUsuario, Publico,  Frequencia,
            //                            Nome as 'Tipo_Nome', Descricao as 'Tipo_Descricao'
            //                     FROM {_table}  
            //                     WHERE
            //                     AgendaId = '{agendaId}'
            //                     and {_inativoFalse}
            //");

            return Db.EventoAgenda
                .AsNoTracking()
                .Where(x => x.AgendaId == agendaId
                      && !x.Inativo)
                .ToList();
        }

        public IList<EventoAgenda> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            //return ObterLista(@$"
            //                     SELECT {_atributosBase},
            //                            AgendaId, UsuarioId, IdentificadorExterno, Titulo, Descricao,
            //                            LocalId, DataInicio, DataFinal, DataLimiteConfirmacao, QuantidadeMinimaDeUsuarios,
            //                            OcupaUsuario, Publico,  Frequencia,
            //                            Nome as 'Tipo_Nome', Descricao as 'Tipo_Descricao'
            //                     FROM {_table}  
            //                     WHERE
            //                     AgendaId = '{agendaId}'
            //                     and DataInicio between '{FormataDataSql(dataInicio, true)}'and '{FormataDataSql(dataFinal)}'
            //                     and {_inativoFalse}
            //");

            return Db.EventoAgenda
                .AsNoTracking()
                .Where(x => x.AgendaId == agendaId
                      && x.DataInicio >= dataInicio
                      && (x.DataFinal == null || x.DataFinal <= dataFinal)
                      && !x.Inativo)
                .ToList();

        }

        public IList<EventoAgenda> ObterTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            //return ObterLista(@$"
            //                     SELECT {_atributosBase},
            //                            AgendaId, UsuarioId, IdentificadorExterno, Titulo, Descricao,
            //                            LocalId, DataInicio, DataFinal, DataLimiteConfirmacao, QuantidadeMinimaDeUsuarios,
            //                            OcupaUsuario, Publico,  Frequencia,
            //                            Nome as 'Tipo_Nome', Descricao as 'Tipo_Descricao'
            //                     FROM {_table}  
            //                     WHERE
            //                     AgendaId = '{agendaId}'
            //                     and UsuarioId = '{usuarioId}'
            //                     and {_inativoFalse}
            //");
            return Db.EventoAgenda
                .AsNoTracking()
                .Where(x => x.AgendaId == agendaId
                      && x.UsuarioId == usuarioId
                      && !x.Inativo)
                .ToList();

        }
    }
}
