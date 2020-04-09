
using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class ConviteRepository : Repository<Convite>, IConviteRepository
    {

        public ConviteRepository(AgendaContext context) : base(context)
        {
        }

        public IList<Convite> ObterConvitesPorEventoId(string eventoId)
        {
            //var teste = ObterLista(@$"
            //                         SELECT Id, CriadoAs, AtualizadoAs, Inativo,
            //                                EventoId, UsuarioId, EmailConvidado, Status,
            //                                ModificaEvento, ConvidaUsuario, VeListaDeConvidados
            //                         FROM {_table} 
            //                         WHERE
            //                         EventoId = '{eventoId}'
            //                         and {_inativoFalse}
            //");

            var teste1 = Db.Convite
                     .AsNoTracking()
                     .Where(x => x.EventoId == eventoId
                           && !x.Inativo)
                     .ToList();


            return teste1;
        }
    }
}
