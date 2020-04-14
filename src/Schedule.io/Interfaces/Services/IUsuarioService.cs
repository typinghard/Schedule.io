using Schedule.io.Models;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
{
    public interface IUsuarioService
    {
        void Gravar(Usuario usuario);
        Usuario Obter(string usuarioId);
        IEnumerable<Usuario> Listar();
        void Excluir(string usuarioId);
    }
}
