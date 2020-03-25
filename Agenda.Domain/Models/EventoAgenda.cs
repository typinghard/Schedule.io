using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Models
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
        public DateTime? DataFinal { get; private set; }
        public DateTime? DataLimiteConfirmacao { get; private set; }
        public int QuantidadeMinimaDeUsuarios { get; private set; }
        public bool OcupaUsuario { get; private set; }
        public bool EventoPublico { get; private set; }
        public TipoEvento TipoEvento { get; private set; }
        public EnumFrequencia EnumFrequencia { get; private set; }

        public EventoAgenda(string agendaId, string identificadorExterno, string titulo, string descricao, IList<string> pessoas, string local,
                            DateTime dataInicio, DateTime? dataFinal,
                            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocuparUsuario, bool eventoPublico,
                            TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
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
            this.EventoPublico = eventoPublico;
            this.TipoEvento = tipoEvento;
            this.EnumFrequencia = enumFrequencia;
        }

        public void DefinirAgenda(string agenda)
        {
            if (agenda.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que escolheu uma agenda.");
            }

            this.AgendaId = agenda;
        }

        public void DefinirIdentificadorExterno(string idExterno)
        {
            //if (string.IsNullOrEmpty(idExterno))
            //{
            //    throw new DomainException("Por favor, certifique-se que digitou um título.");
            //}

            this.IdentificadorExterno = idExterno;
        }

        public void DefinirTitulo(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
            {
                throw new DomainException("Por favor, certifique-se que digitou um título.");
            }

            if (titulo.ValidarTamanho(2, 150))
            {
                throw new DomainException("O título deve ter entre 2 e 150 caracteres.");

            }

            this.Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (descricao.ValidarTamanho(2, 500))
            {
                throw new DomainException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }

        public void AdicionarPessoa(string pessoa)
        {
            if (pessoa.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            Pessoas.Add(pessoa);
        }

        public void DefinirLocal(string Local)
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

        public void DefinirEventoPublicoOuPrivado(bool eventoPublicOuPrivado)
        {
            if (eventoPublicOuPrivado)
                this.TornarEventoPublico();
            else
                this.TornarEventoPrivado();
        }

        private void TornarEventoPublico()
        {
            this.EventoPublico = true;
        }

        private void TornarEventoPrivado()
        {
            this.EventoPublico = false;
        }

        public void DefinirTipoEvento(TipoEvento tipoEvento)
        {
            tipoEvento.DefinirNome(tipoEvento.Nome);
            tipoEvento.DefinirDescricao(tipoEvento.Descricao);

            this.TipoEvento = TipoEvento;
            ///instancia uma nova tipo evento antes de atribuir? 
        }

        public void DefinirFrequencia(EnumFrequencia enumFrequencia)
        {
            this.EnumFrequencia = enumFrequencia;
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
                throw new DomainException("Por favor, certifique-se que digitou um nome para o tipo do evento.");
            }

            if (nome.ValidarTamanho(2, 150))
            {
                throw new DomainException("O nome do tipo do evento deve ter entre 2 e 150 caracteres.");

            }

            this.Nome = nome;
        }

        public void DefinirDescricao(string descricao)
        {
            if (descricao.ValidarTamanho(2, 500))
            {
                throw new DomainException("A descrição deve ter entre 2 e 500 caracteres.");
            }

            this.Descricao = descricao;
        }
    }

}


