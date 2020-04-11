using Schedule.io.Core.Core.DomainObjects;
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

        public void DefinirDataCriacao()
        {
            if (CriadoAs != DateTime.MinValue)
                throw new ScheduleIoException("PENSAAAAAAAAAR");

            CriadoAs = AtualizadoAs = DateTime.Now;
        }
        public void DefinirDataAtualizacao()
        {
            AtualizadoAs = DateTime.Now;
        }
    }
}
