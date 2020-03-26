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
        public RegistrarEventoAgendaCommand(Guid id, Guid agendaId, string identificadorExterno, string titulo,
                                            string descricao, List<Convite> convites, Guid local, DateTime dataInicio, DateTime? dataFinal,
                                            DateTime dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocuparUsuario, 
                                            bool eventoPublico, TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
            this.Id = id;
            this.AgendaId = agendaId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Convites = convites;
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
