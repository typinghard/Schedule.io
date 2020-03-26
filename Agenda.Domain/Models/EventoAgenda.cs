using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Enums;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Domain.Models
{
    public class EventoAgenda : Entity, IAggregateRoot
    {
        public Guid AgendaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string IdentificadorExterno { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public IReadOnlyCollection<Convite> Convites { get { return _convites; } }
        private List<Convite> _convites { get; set; }
        public Guid? Local { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFinal { get; private set; }
        public DateTime? DataLimiteConfirmacao { get; private set; }
        public int QuantidadeMinimaDeUsuarios { get; private set; }
        public bool OcupaUsuario { get; private set; }
        public bool Publico { get; private set; }
        public TipoEvento Tipo { get; private set; }
        public EnumFrequencia Frequencia { get; private set; }

        public EventoAgenda(Guid id, Guid agendaId, string titulo, DateTime dataInicio, TipoEvento tipoEvento) : base(id)
        {
            this.AgendaId = agendaId;
            this.Titulo = titulo;
            this.DataInicio = dataInicio;
            this.Tipo = tipoEvento;
            this.Frequencia = EnumFrequencia.Nao_Repete;

            this._convites = new List<Convite>();

            var resultadoValidacao = this.NovoEventoAgendaEhValido();
            if (!resultadoValidacao.IsValid)
                throw new DomainException(string.Join("## ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirAgenda(Guid agendaId)
        {
            if (agendaId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que escolheu uma agenda.");
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
                throw new DomainException("Por favor, certifique-se que digitou um título.");
            }

            if (!titulo.ValidarTamanho(2, 150))
            {
                throw new DomainException("O título deve ter entre 2 e 150 caracteres.");

            }

            this.Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.ValidarTamanho(2, 500))
            {
                throw new DomainException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }

        //public void AdicionarPessoa(Guid pessoa)
        //{
        //    if (pessoa.EhVazio())
        //    {
        //        throw new DomainException("Por favor, certifique-se que adicinou uma pessoa.");
        //    }

        //    Usuarios.Add(pessoa);
        //}

        public void AdicionarConvite(Convite convite)
        {
            convite.NovoConviteEhValido();
            _convites.Add(convite);
        }

        public void DefinirLocal(Guid Local)
        {
            if (Local.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicionou um local.");
            }

            this.Local = Local;
        }

        public void DefinirDatas(DateTime dataInicio, DateTime? dataFinal = null)
        {
            if (dataInicio == DateTime.MinValue)
            {
                throw new DomainException("Por favor, escolha a data e hora inicial do evento.");
            }

            if (dataFinal.HasValue && dataFinal < dataInicio)
            {
                throw new DomainException("Por certifique-se de que a data inicial é maior que a data final do evento.");
            }

            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
        }

        public void DefinirDataInicial(DateTime dataInicio)
        {
            if (dataInicio == DateTime.MinValue)
            {
                throw new DomainException("Por favor, escolha a data e hora inicial do evento.");
            }

            this.DataInicio = dataInicio;
            this.DataFinal = DateTime.MinValue;
        }


        public void DefinirDataLimiteConfirmacao(DateTime dataLimiteConfirmacao)
        {
            if (dataLimiteConfirmacao == DateTime.MinValue)
            {
                throw new DomainException("Por favor, certifique-se que informou uma data limite.");
            }

            if (dataLimiteConfirmacao < this.DataInicio)
            {
                throw new DomainException("Por certifique-se de que a data limite é maior que a data inicio do evento.");
            }

            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
        }

        public void DefinirQuantidadeMinimaDeUsuarios(int quantidadeMinimaDeUsuarios)
        {
            if (quantidadeMinimaDeUsuarios < 0)
            {
                throw new DomainException("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.");
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

        public TipoEvento(Guid id, string nome, string descricao) : base(id)
        {
            this.Nome = nome;
            this.Descricao = descricao;
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Por favor, certifique-se que digitou um nome para o tipo do evento.");
            }

            if (!nome.ValidarTamanho(2, 120))
            {
                throw new DomainException("O nome do tipo do evento deve ter entre 2 e 120 caracteres.");

            }

            this.Nome = nome;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.ValidarTamanho(2, 500))
            {
                throw new DomainException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }
    }

}


