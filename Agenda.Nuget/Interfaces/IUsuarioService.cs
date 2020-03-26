using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IUsuarioService
    {
        Guid Gravar(Usuario usuario);

        bool Excluir(Guid id);

        Usuario Obter(Guid usuarioId);
    }
}
