using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
{
    public interface IUsuarioService
    {
        string Gravar(Core.Models.Usuario usuario);
        void Excluir(string usuarioId);
        Core.Models.Usuario Obter(string usuarioId);
    }
}
