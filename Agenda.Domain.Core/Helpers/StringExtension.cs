using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.Helpers
{
    public static class StringExtension
    {
        public static bool ValidarTamanho(this string texto, int tamanhoMinimo, int tamanhoMaximo)
        {
            if (string.IsNullOrEmpty(texto))
                return false;

            if (texto.Length < tamanhoMinimo || texto.Length > tamanhoMaximo)
                return false;

            return true;
        }
    }
}
