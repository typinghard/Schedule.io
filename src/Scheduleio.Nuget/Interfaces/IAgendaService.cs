using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces
{
    public interface IAgendaService
    {
        string Criar(Models.Agenda agenda);







        void Editar(Models.Agenda agenda);
        void Excluir(Models.Agenda agenda);
        /// <summary>
        /// Retorna a aeedansi doa  
        /// </summary>
        /// <param name="agendaId"></param>
        /// <returns></returns>
        Models.Agenda Obter(string agendaId);
        IEnumerable<Models.Agenda> ObterTodas();
    }
}

