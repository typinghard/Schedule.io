using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Linq;

namespace Agenda.Domain.Models
{
    public class Local : Entity, IAggregateRoot
    {
        public string IdentificadorExterno { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Reserva { get; private set; }
        public int LotacaoMaxima { get; private set; }

        public Local(string id, string nomeLocal) : base(id)
        {
            this.Nome = nomeLocal;

            var resultadoValidacao = this.NovoLocalEhValido();
            if (!resultadoValidacao.IsValid)
                throw new DomainException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirNomeLocal(string nomeLocal)
        {
            if (!nomeLocal.ValidarTamanho(2, 200))
            {
                throw new DomainException("O nome do local deve ter entre 2 e 200 caracteres.");
            }

            this.Nome = nomeLocal;
        }

        public void DefinirIdentificadorExterno(string identificadorExterno)
        {
            if (string.IsNullOrEmpty(identificadorExterno))
            {
                throw new DomainException("O Identificador do local não pode ser vazio!");
            }

            this.IdentificadorExterno = identificadorExterno;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.ValidarTamanho(2, 500))
            {
                throw new DomainException("A descrição do local deve ter entre 2 e 500 caracteres.");
            }
            this.Descricao = descricao;
        }

        public void ReservarLocal()
        {
            this.Reserva = true;
        }

        public void RemoverReservaLocal()
        {
            this.Reserva = false;
        }

        public void DefinirLotacaoMaxima(int lotacaoMaxima)
        {
            if (lotacaoMaxima < 0)
            {
                throw new DomainException("Por favor, certifique-se qua a lotação máxima de usuários para o local não é menor que 0.");
            }

            this.LotacaoMaxima = lotacaoMaxima;
        }

        public ValidationResult NovoLocalEhValido()
        {
            return new NovoLocalValidation().Validate(this);
        }

    }
}
