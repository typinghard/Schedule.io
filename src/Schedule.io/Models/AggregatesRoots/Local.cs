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
            Nome = nomeLocal;

            var resultadoValidacao = NovoLocalEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(resultadoValidacao.Errors.Select(x => x.ErrorMessage).ToList());
        }

        private Local()
        {

        }

        public void DefinirNomeLocal(string nomeLocal)
        {
            if (!nomeLocal.ValidarTamanho(2, 200))
                throw new ScheduleIoException("O nome do local deve ter entre 2 e 200 caracteres.");

            Nome = nomeLocal;
        }

        public void DefinirIdentificadorExterno(string identificadorExterno)
        {
            if (!identificadorExterno.EhVazio() && !identificadorExterno.ValidarTamanho(2, 200))
                throw new ScheduleIoException("O Identificador Extorno deve ter entre 2 e 200 caracteres.");

            IdentificadorExterno = identificadorExterno;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.EhVazio() && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException("A descrição do local deve ter entre 2 e 500 caracteres.");

            Descricao = descricao;
        }

        public void ReservarLocal()
        {
            Reserva = true;
        }

        public void RemoverReservaLocal()
        {
            Reserva = false;
        }

        public void DefinirLotacaoMaxima(int lotacaoMaxima)
        {
            if (lotacaoMaxima < 0)
                throw new ScheduleIoException("Por favor, certifique-se qua a lotação máxima de usuários para o local não é menor que 0.");

            LotacaoMaxima = lotacaoMaxima;
        }

        private ValidationResult NovoLocalEhValido()
        {
            return new LocalValidation().Validate(this);
        }

    }
}
