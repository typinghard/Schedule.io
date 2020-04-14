using Schedule.io.Core.Data;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        bool VerificaSeUsuarioExiste(string usuarioId);
    }
}
