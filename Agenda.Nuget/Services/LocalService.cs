using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    internal class LocalService : ILocalService
    {
        Guid ILocalService.Criar(Local local)
        {
            throw new NotImplementedException();
        }

        void ILocalService.Editar(Local local)
        {
            throw new NotImplementedException();
        }

        void ILocalService.Excluir(Local local)
        {
            throw new NotImplementedException();
        }

        void ILocalService.Obter(Guid localId)
        {
            throw new NotImplementedException();
        }
    }
}
