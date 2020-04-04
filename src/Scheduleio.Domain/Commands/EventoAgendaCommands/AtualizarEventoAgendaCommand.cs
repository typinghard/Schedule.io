﻿using Schedule.io.Core.Enums;
using Schedule.io.Core.Models;
using Schedule.io.Core.Validations.EventoAgendaValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.EventoAgendaCommands
{
    public class AtualizarEventoAgendaCommand : EventoAgendaCommand
    {
        public AtualizarEventoAgendaCommand(string id, string agendaId, string usuarioId, string identificadorExterno, string titulo, string descricao, IList<Convite> convites, string localId, DateTime dataInicio, DateTime? dataFinal,
            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocuparUsuario, bool eventoPublico, TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
            this.Id = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Convites = convites;
            this.LocalId = localId;
            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
            this.QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
            this.OcupaUsuario = ocuparUsuario;
            this.Publico = eventoPublico;
            this.Tipo = tipoEvento;
            this.Frequencia = enumFrequencia;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarEventoAgendaCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}