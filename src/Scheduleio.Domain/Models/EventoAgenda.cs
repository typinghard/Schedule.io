using FluentValidation.Results;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Helpers;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Validations.EventoAgendaValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Core.Models
{
    public class EventoAgenda : Entity, IAggregateRoot
    {
        public string AgendaId { get; private set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public IList<string> Pessoas { get; private set; }
        public string Local { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFinal { get; private set; } // Não obrigatório = padrão data final no mesmo dia.
        public DateTime? DataLimiteConfirmacao { get; private set; }
        public int QuantidadeMinimaDeUsuarios { get; private set; }
        public bool OcupaUsuario { get; private set; }
        public bool Publico { get; private set; }
        public TipoEvento Tipo { get; private set; }
        public EnumFrequencia Frequencia { get; private set; }

        public EventoAgenda(string agendaId, string titulo, DateTime dataInicio, TipoEvento tipoEvento)
        {
            this.AgendaId = agendaId;
            this.Titulo = titulo;
            this.DataInicio = dataInicio;
            this.Tipo = tipoEvento;
            this.Frequencia = EnumFrequencia.Nao_Repete;
            this.Pessoas = new List<string>();

            var resultadoValidacao = this.NovoEventoAgendaEhValido();
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
            if (!descricao.ValidarTamanho(2, 500))
            {
                throw new ScheduleIoException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }

        public void AdicionarPessoa(string pessoaId)
        {
            if (pessoaId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            Pessoas.Add(pessoaId);
        }

        public void DefinirLocal(string Local)
        {
            if (Local.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicionou um local.");
            }

            this.Local = Local;
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


        public void DefinirDataLimiteConfirmacao(DateTime dataLimiteConfirmacao)
        {
            if (dataLimiteConfirmacao == DateTime.MinValue)
            {
                throw new ScheduleIoException("Por favor, certifique-se que informou uma data limite.");
            }

            if (dataLimiteConfirmacao < this.DataInicio)
            {
                throw new ScheduleIoException("Por certifique-se de que a data limite é maior que a data inicio do evento.");
            }

            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
        }

        public void DefinirQuantidadeMinimaDeUsuarios(int quantidadeMinimaDeUsuarios)
        {
            if (quantidadeMinimaDeUsuarios < 0)
            {
                throw new ScheduleIoException("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.");
            }

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


        public ValidationResult NovoEventoAgendaEhValido()
        {
            return new NovoEventoAgendaValidation().Validate(this);
        }
    }


    public class TipoEvento : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public TipoEvento(string nome, string descricao)
        {
            this.Nome = nome;
            this.Descricao = descricao;
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ScheduleIoException("Por favor, certifique-se que digitou um nome para o tipo do evento.");
            }

            if (!nome.ValidarTamanho(2, 120))
            {
                throw new ScheduleIoException("O nome do tipo do evento deve ter entre 2 e 120 caracteres.");

            }

            this.Nome = nome;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.ValidarTamanho(2, 500))
            {
                throw new ScheduleIoException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }
    }

}


