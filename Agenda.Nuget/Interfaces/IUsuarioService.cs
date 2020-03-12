using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IUsuarioService
    {
        Guid Criar(Usuario usuario);
        void Editar(Usuario usuario);
        void Excluir(Usuario usuario);
        void Obter(Guid usuarioId);
    }
}
