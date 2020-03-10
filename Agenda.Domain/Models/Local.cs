using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Domain.Models
{
    public class Local : Entity, IAggregateRoot
    {
        public string IdentificadorExterno { get; private set; } 
        public string NomeLocal { get; private set; }
        public string Descricao { get; private set; }
        public bool ReservaLocal { get; private set; } 
        public int LotacaoMaxima { get; private set; }


        public Local(string nomeLocal)
        {
            this.NomeLocal = nomeLocal;

            var resultadoValidacao = this.NovoLocalEhValido();
            if (!resultadoValidacao.IsValid)
                throw new DomainException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirIdentificadorExterno(string identificadorExterno)
        {
            if (string.IsNullOrEmpty(identificadorExterno))
            {
                throw new DomainException("O Identificador do local não pode ser nulo ou vazio!");
            }

            this.IdentificadorExterno = identificadorExterno;
        }

        public void DefinirNomeLocal(string nomeLocal)
        {
            if (!nomeLocal.ValidarTamanho(2, 200))
            {
                throw new DomainException("O nome do local deve ter entre 2 e 200 caracteres.");
            }

            this.NomeLocal = nomeLocal;
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
            this.ReservaLocal = true;
        }

        public void RemoverReservaLocal()
        {
            this.ReservaLocal = false;
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
