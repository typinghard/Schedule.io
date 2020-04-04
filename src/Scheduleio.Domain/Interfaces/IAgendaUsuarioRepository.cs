﻿using Schedule.io.Core.Core.Data;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Interfaces
{
    public interface IAgendaUsuarioRepository : IRepository<AgendaUsuario>
    {
        AgendaUsuario ObterPorAgendaIdEUsuarioId(string agendaId, string usuarioId);
    }
}
