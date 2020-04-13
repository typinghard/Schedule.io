using Schedule.io.Models;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Services
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
        /// Retorna a agenda pelo id.
        /// </summary>
        /// <param name="agendaId"></param>
        /// <returns></returns>
        Agenda Obter(string agendaId);

        /// <summary>
        /// Lista todas agendas ativas.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Agenda> Listar();

        /// <summary>
        /// Lista todas agendas do usuário.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        IEnumerable<Agenda> Listar(string usuarioId);

        /// <summary>
        /// Remove permanentemente a agenda!
        /// </summary>
        /// <param name="agendaId"></param>
        void Excluir(string agendaId);
    }
}
