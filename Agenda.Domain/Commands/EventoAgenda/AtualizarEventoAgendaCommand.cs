using Agenda.Domain.Models;
using System;
using Agenda.Domain.Validations;
using System.Collections.Generic;
using System.Text;
using Agenda.Domain.Enums;

namespace Agenda.Domain.Commands
{
    public class AtualizarEventoAgendaCommand : EventoAgendaCommand
    {
        public AtualizarEventoAgendaCommand(string id, string agendaId, string identificadorExterno, string titulo, string descricao, IList<string> pessoas, string local, DateTime dataInicio, DateTime? dataFinal,
            DateTime dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocuparUsuario, bool eventoPublico, TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
            this.Id = id;
            this.AgendaId = agendaId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Pessoas = pessoas;
            this.Local = local;
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
