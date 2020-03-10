using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarEventoAgendaCommand : EventoAgendaCommand
    {
        public RegistrarEventoAgendaCommand(Guid agendaId, string identificadorExterno, string titulo, string descricao, IList<Guid> usuarios, Guid local, DateTime dataInicio, DateTime? dataFinal,
            DateTime dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocuparUsuario, bool eventoPublico, TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
            this.AgendaId = agendaId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Usuarios = usuarios;
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
            ValidationResult = new RegistrarEventoAgendaCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
