using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IAgendaService
    {
        Guid Criar(Models.Agenda agenda);
        void Editar(Models.Agenda agenda);
        void Excluir(Models.Agenda agenda);
        /// <summary>
        /// Retorna a aeedansi doa  
        /// </summary>
        /// <param name="agendaId"></param>
        /// <returns></returns>
        Models.Agenda Obter(Guid agendaId);
        IEnumerable<Models.Agenda> ObterTodas();
    }
}
