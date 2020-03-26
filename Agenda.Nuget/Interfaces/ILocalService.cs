using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface ILocalService
    {
        Guid Gravar(Local local);

        bool Excluir(Guid localId);

        Local Obter(Guid localId);

        
    }
}
