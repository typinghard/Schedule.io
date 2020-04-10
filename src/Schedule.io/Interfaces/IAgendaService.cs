using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces
{
    public interface IAgendaService
    {
        /// <summary>
        /// Grava os dados da Agenda.
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        string Gravar(Agenda agenda);

        /// <summary>
        /// Excluí a agenda de acordo com o id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Inativar(string id);

        /// <summary>
        /// Retorna a agenda pelo id.
        /// </summary>
        /// <param name="agendaId"></param>
        /// <returns></returns>
        Agenda Obter(string agendaId);

        /// <summary>
        /// Retorna todas as agendas ativas.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Agenda> ObterTodas();
    }
}
