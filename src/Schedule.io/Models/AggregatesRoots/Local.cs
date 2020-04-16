using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using System.Linq;
using Schedule.io.Core.Helpers;
using Schedule.io.Validations.LocalValidations;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Local : Entity, IAggregateRoot
    {
        public string IdentificadorExterno { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Reserva { get; private set; }
        public int LotacaoMaxima { get; private set; }

        public Local(string nomeLocal)
        {
            this.Nome = nomeLocal;

            var resultadoValidacao = this.LocalEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        private Local()
        {

        }

        public void DefinirNomeLocal(string nomeLocal)
        {
            if (!nomeLocal.ValidarTamanho(2, 200))
                throw new ScheduleIoException("O nome do local deve ter entre 2 e 200 caracteres.");

            this.Nome = nomeLocal;
        }

        public void DefinirIdentificadorExterno(string identificadorExterno)
        {
            if (!string.IsNullOrEmpty(identificadorExterno) && !identificadorExterno.ValidarTamanho(2, 200))
                throw new ScheduleIoException("O Identificador Extorno deve ter entre 2 e 200 caracteres.");


            this.IdentificadorExterno = identificadorExterno;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao) && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException("A descrição do local deve ter entre 2 e 500 caracteres.");

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
                throw new ScheduleIoException("Por favor, certifique-se qua a lotação máxima de usuários para o local não é menor que 0.");

            this.LotacaoMaxima = lotacaoMaxima;
        }

        public ValidationResult LocalEhValido()
        {
            return new LocalValidation().Validate(this);
        }

    }
}
