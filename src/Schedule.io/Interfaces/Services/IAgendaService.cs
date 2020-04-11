using Schedule.io.Models;
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
        string Gravar(Core.Models.Agenda agenda);
        /// <summary>
        /// Retorna a agenda pelo id.
        /// </summary>
        /// <param name="agendaId"></param>
        /// <returns></returns>
        Core.Models.Agenda Obter(string agendaId);
        /// <summary>
        /// Retorna todas as agendas ativas.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Core.Models.Agenda> Listar();
        IEnumerable<Core.Models.Agenda> Listar(string usuarioId);
        void Excluir(string agendaId);
    }
}
