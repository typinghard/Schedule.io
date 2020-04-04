using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces
{
    public interface IUsuarioService
    {
        string Criar(Usuario usuario);
        void Editar(Usuario usuario);
        void Excluir(Usuario usuario);
        void Obter(string usuarioId);
    }
}
