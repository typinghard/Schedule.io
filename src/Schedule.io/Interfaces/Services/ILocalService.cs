using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
{
    public interface ILocalService
    {
        string Gravar(Core.Models.Local local);
        void Excluir(string localId);
        Evento Obter(string localId);
    }
}
