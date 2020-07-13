namespace Schedule.io.Configs
{
    public class ScheduleIoConfigurations
    {
        /// <summary>
        /// Defina para true caso queira registrar os Events lançados
        /// </summary>
        public bool UseEventSourcing { get; set; }

        /// <summary>
        /// Caso seja definido para true e um evento seja lançado para um usuário
        /// em um horário que já existe um evento marcado como Ocupado, então
        /// uma exceção será lançada
        /// </summary>
        public bool BloquearEventosOcupadosNoMesmoHorario { get; set; }

        /// <summary>
        /// Caso seja definido para true e um evento seja lançado para um convidade
        /// em um horário que já existe um evento marcado como Ocupado, então
        /// uma exceção será lançada
        /// </summary>
        public bool BloquearEventosOcupadosNoMesmoHorarioParaConvidados { get; set; }

        /// <summary>
        /// Caso seja definido para true e um evento seja lançado para um local
        /// em um horário que já existe um evento marcado como Ocupado, então
        /// uma exceção será lançada
        /// </summary>
        public bool BloquearEventosOcupadosNoMesmoLocal { get; set; }

        /// <summary>
        /// Caso seja definido para true e um evento seja lançado em um
        /// local com uma quantidade de convidados maior, uma exceção será
        /// lançada
        /// </summary>
        public bool ValidarLimiteDeQuantidadePorLocal { get; set; }
    }
}
