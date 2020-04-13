using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Enums;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Validations.EventoAgendaValidations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Evento : Entity, IAggregateRoot
    {
        public string AgendaId { get; private set; }
        public string UsuarioIdCriador { get; private set; }
        public string IdTipoEvento { get; private set; }
        public string IdentificadorExterno { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public IReadOnlyCollection<Convite> Convites { get { return _convites; } }
        private List<Convite> _convites { get; set; }
        public string LocalId { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFinal { get; private set; }
        public DateTime? DataLimiteConfirmacao { get; private set; }
        public int QuantidadeMinimaDeUsuarios { get; private set; }
        public bool OcupaUsuario { get; private set; }
        public bool Publico { get; private set; }
        public EnumFrequencia Frequencia { get; private set; }

        public Evento(string id, string agendaId, string usuarioIdCriador, string titulo, DateTime dataInicio, string idTipoEvento) : base(id)
        {
            this.AgendaId = agendaId;
            this.UsuarioIdCriador = usuarioIdCriador;
            this.Titulo = titulo;
            this.DataInicio = dataInicio;
            this.IdTipoEvento = idTipoEvento;
            this.Frequencia = EnumFrequencia.Nao_Repete;

            this._convites = new List<Convite>();

            var resultadoValidacao = this.EventoAgendaEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join("## ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirAgenda(string agendaId)
        {
            if (agendaId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que escolheu uma agenda.");
            }

            this.AgendaId = agendaId;
        }

        public void DefinirIdentificadorExterno(string idExterno)
        {
            if (string.IsNullOrEmpty(idExterno))
                idExterno = null;

            this.IdentificadorExterno = idExterno;
        }

        public void DefinirTitulo(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
            {
                throw new ScheduleIoException("Por favor, certifique-se que digitou um título.");
            }

            if (!titulo.ValidarTamanho(2, 150))
            {
                throw new ScheduleIoException("O título deve ter entre 2 e 150 caracteres.");

            }

            this.Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao) && !descricao.ValidarTamanho(2, 500))
            {
                throw new ScheduleIoException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }

        public void AdicionarConvite(Convite convite)
        {
            convite.NovoConviteEhValido();
            _convites.Add(convite);
        }

        public void LimparConvites()
        {
            _convites.Clear();
        }

        public void DefinirLocal(string localId)
        {
            if (string.IsNullOrEmpty(localId))
                localId = null;

            this.LocalId = localId;
        }

        public void DefinirTipoEvento(string tipoEventoId)
        {
            if (string.IsNullOrEmpty(tipoEventoId))
                IdTipoEvento = null;

            this.IdTipoEvento = tipoEventoId;
        }

        public void DefinirDatas(DateTime dataInicio, DateTime? dataFinal = null)
        {
            if (dataInicio == DateTime.MinValue)
            {
                throw new ScheduleIoException("Por favor, escolha a data e hora inicial do evento.");
            }

            if (dataFinal.HasValue && dataFinal < dataInicio)
            {
                throw new ScheduleIoException("Por certifique-se de que a data inicial é maior que a data final do evento.");
            }

            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
        }

        public void DefinirDataInicial(DateTime dataInicio)
        {
            if (dataInicio == DateTime.MinValue)
            {
                throw new ScheduleIoException("Por favor, escolha a data e hora inicial do evento.");
            }

            this.DataInicio = dataInicio;
            this.DataFinal = DateTime.MinValue;
        }


        public void DefinirDataLimiteConfirmacao(DateTime? dataLimiteConfirmacao)
        {
            //if (dataLimiteConfirmacao == DateTime.MinValue)
            //{
            //    throw new ScheduleIoException("Por favor, certifique-se que informou uma data limite.");
            //}

            if (dataLimiteConfirmacao.HasValue && dataLimiteConfirmacao < this.DataInicio)
            {
                throw new ScheduleIoException("Por certifique-se de que a data limite é maior que a data inicio do evento.");
            }

            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
        }

        public void DefinirQuantidadeMinimaDeUsuarios(int quantidadeMinimaDeUsuarios)
        {
            if (quantidadeMinimaDeUsuarios < 0)
                throw new ScheduleIoException("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.");

            this.QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
        }

        public void OcuparUsuario()
        {
            this.OcupaUsuario = true;
        }

        public void DesocuparUsuario()
        {
            this.OcupaUsuario = false;
        }

        public void TornarEventoPublico()
        {
            this.Publico = true;
        }

        public void TornarEventoPrivado()
        {
            this.Publico = false;
        }

        public void DefinirFrequencia(EnumFrequencia frequencia)
        {
            this.Frequencia = frequencia;
        }


        public ValidationResult EventoAgendaEhValido()
        {
            return new EventoAgendaValidation().Validate(this);
        }
    }


}


