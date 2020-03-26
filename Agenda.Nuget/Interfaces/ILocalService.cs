using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface ILocalService
    {
        string Criar(Local local);
        void Editar(Local local);
        void Excluir(Local local);
        void Obter(string localId);
    }
}
