using System;

namespace Schedule.io.Core.DomainObjects
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime CriadoAs { get; protected set; }
        public DateTime AtualizadoAs { get; protected set; }


        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void DefinirDataCriacao()
        {
            if (CriadoAs != DateTime.MinValue)
                throw new ScheduleIoException("Não é possível atribuir uma nova data de criação!");

            CriadoAs = AtualizadoAs = DateTime.Now;
        }
        public void DefinirDataAtualizacao()
        {
            AtualizadoAs = DateTime.Now;
        }
    }
}
