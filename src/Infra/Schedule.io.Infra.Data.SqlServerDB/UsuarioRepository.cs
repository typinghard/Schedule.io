﻿using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AgendaContext context) : base(context)
        {
        }

        public bool VerificaSeUsuarioExiste(string usuarioId)
        {
            throw new System.NotImplementedException();
        }

        public override Usuario Obter(string usuarioId)
        {
            var query = $@"SELECT * 
                           FROM Usuario
                           WHERE Id = '{usuarioId}'";

            return base.Obter(query);
        }
    }
}
