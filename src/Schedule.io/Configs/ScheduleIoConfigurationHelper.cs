using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Configs
{
    public static class ScheduleIoConfigurationHelper
    {
        public static bool BloquearEventosOcupadosNoMesmoHorario { get; set; }
        public static bool BloquearEventosOcupadosNoMesmoHorarioParaConvidados { get; set; }
        public static bool BloquearEventosOcupadosNoMesmoLocal { get; set; }
        public static bool ValidarLimiteDeQuantidadePorLocal { get; set; }
    }
}
