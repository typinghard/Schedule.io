using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    public class UsuarioService : IUsuarioService
    {
        Guid IUsuarioService.Criar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        void IUsuarioService.Editar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        void IUsuarioService.Excluir(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        void IUsuarioService.Obter(Guid usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
