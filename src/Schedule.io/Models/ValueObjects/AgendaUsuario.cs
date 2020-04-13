﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Models.ValueObjects
{
    public class AgendaUsuario
    {
        public string AgendaId { get; protected set; }
        public string UsuarioId { get; protected set; }
        public PermissoesAgenda Permissoes { get; protected set; }

        public AgendaUsuario(string agendaId, string usuarioId) 
        {
            AgendaId = agendaId;
            UsuarioId = usuarioId;
            Permissoes = new PermissoesAgenda();

            //var resultadoValidacao = this.NovaAgendaUsuarioEhValido();
            //if (!resultadoValidacao.IsValid)
            //    throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirUsuarioId(string usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }


        public void DefinirAgendaId(string agendaId)
        {
            if (agendaId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou uma agenda.");
            }

            AgendaId = agendaId;
        }

        public bool NovaAgendaUsuarioEhValido()
        {
            return true;
        }
    }
}
