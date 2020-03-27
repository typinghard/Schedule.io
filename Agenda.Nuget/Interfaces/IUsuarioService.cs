using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IUsuarioService
    {
        string Gravar(Usuario usuario);

        bool Excluir(string usuarioId);

        Usuario Obter(string usuarioId);
    }
}
