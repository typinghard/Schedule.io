using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IEventoService
    {
        string Criar(Evento evento);





        void Editar(Evento evento);
        void Excluir(Evento evento);
        Evento Obter(string eventoId);
    }
}
