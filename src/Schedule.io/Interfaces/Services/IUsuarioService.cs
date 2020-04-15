using Schedule.io.Models;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario Gravar(string email);
        void Gravar(Usuario usuario);
        IEnumerable<Usuario> Gravar(List<string> emails);
        void AtualizarEmail(string usuarioId, string novoEmail);
        Usuario Obter(string usuarioId);
        IEnumerable<Usuario> Listar();
        void Excluir(string usuarioId);
    }
}
