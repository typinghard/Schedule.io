using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.Helpers
{
    public static class StringExtension
    {
        /// <summary>
        /// Retorna Verdadeiro se o valor estiver entre o tamanho mínimo e o tamanho máximo.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tamanhoMinimo"></param>
        /// <param name="tamanhoMaximo"></param>
        /// <returns></returns>
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
