
using Dapper;
using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            var query = @$"SELECT *, 
                                    Id as permissao_split, ModificaEvento, ConvidaUsuario, VeListaDeConvidados
                                    FROM Convite 
                                    WHERE
                                    EventoId = '{eventoId}'
                                    ";

            using (var con = new SqlConnection(_connectionString))
            {
                var convites = new List<Convite>();
                try
                {
                    con.Open();
                    con.Query<Convite, PermissoesConvite, Convite>(
                        query,
                        (convite, permissoesConvite) =>
                        {
                            convites.Add(convite);
                            convites.Last().AtribuirPermissao(permissoesConvite);
                            return convite;
                        },
                        splitOn: "permissao_split");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return convites;
            }
        }
    }
}
