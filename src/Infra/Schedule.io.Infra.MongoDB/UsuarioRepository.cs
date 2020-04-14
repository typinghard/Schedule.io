using MongoDB.Driver;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AgendaContext context) : base(context)
        {

        }

        public bool VerificaSeUsuarioExiste(string usuarioId)
        {
            return Db.Usuario.Find(x => x.Id == usuarioId).FirstOrDefault() == null ? false : true;
        }
    }
}
