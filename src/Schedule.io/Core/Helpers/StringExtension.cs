using System.Text.RegularExpressions;

namespace Schedule.io.Core.Helpers
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
            if (texto.EhVazio())
                return false;

            if (texto.Length < tamanhoMinimo || texto.Length > tamanhoMaximo)
                return false;

            return true;
        }

        public static bool EhVazio(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Valida se o formato do e-mail está correto. Regex vindo do site da microsoft.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <see cref="https://docs.microsoft.com/pt-br/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format"/>
        public static bool EmailEhValido(this string email)
        {
            return Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        }
    }
}
