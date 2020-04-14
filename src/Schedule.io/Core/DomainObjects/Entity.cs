using System;

namespace Schedule.io.Core.DomainObjects
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime CriadoAs { get; protected set; }
        public DateTime AtualizadoAs { get; protected set; }

        public Entity(string id)
        {
            Id = id;
        }

        protected internal void DefinirId(string id)
        {
            if (string.IsNullOrEmpty(Id))
                throw new ScheduleIoException("Id já existente!");

            if (string.IsNullOrEmpty(id) || Guid.Parse(id) == Guid.Empty)
                Id = id;
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
