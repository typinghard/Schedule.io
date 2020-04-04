using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces
{
    public interface ILocalService
    {
        string Gravar(Local local);

        bool Excluir(string localId);

        Local Obter(string localId);

        
    }
}
