using Schedule.io.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Schedule.io.Test.Testes_Unitários.Core.Helpers
{
    public class StringExtensionsTests
    {
        [Theory(DisplayName = "StringExtension - EhVazio - Deve Ser Válido")]
        [InlineData("")]
        [InlineData(" ")]
        public void StringExtension_EhVazio_DeveSerValido(string str)
        {
            var result = str.EhVazio();

            Assert.True(result);
        }

        [Theory(DisplayName = "StringExtension - EhVazio - Deve Ser Inválido")]
        [InlineData(".")]
        [InlineData(" .")]
        [InlineData(". ")]
        public void StringExtension_EhVazio_DeveSerInvalido(string str)
        {
            var result = str.EhVazio();

            Assert.False(result);
        }


        [Theory(DisplayName = "StringExtension - ValidarTamanho - Deve Ser Válido")]
        [InlineData("abcde", 1, 5)]
        [InlineData("a", 1, 1)]
        [InlineData("abcde", 5, 5)]
        [InlineData("a", -1, 1)]
        public void StringExtension_ValidarTamanho_DeveSerValido(string str, int tamanhoMinimo, int tamanhoMaximo)
        {
            var result = str.ValidarTamanho(tamanhoMinimo, tamanhoMaximo);

            Assert.True(result);
        }

        [Theory(DisplayName = "StringExtension - ValidarTamanho - Deve Ser Inválido")]
        [InlineData("", 1, 5)]
        [InlineData("abcde", 6, 5)]
        [InlineData("a", -1, 0)]
        public void StringExtension_ValidarTamanho_DeveSerInvalido(string str, int tamanhoMinimo, int tamanhoMaximo)
        {
            var result = str.ValidarTamanho(tamanhoMinimo, tamanhoMaximo);

            Assert.False(result);
        }

        [Theory(DisplayName = "StringExtension - EmailEhValido - Deve Ser Válido")]
        [InlineData("david.jones@proseware.com")]
        [InlineData("d.j@server1.proseware.com")]
        [InlineData("jones@ms1.proseware.com")]
        [InlineData("j@proseware.com9")]
        [InlineData("js#internal@proseware.com")]
        [InlineData("j_9@[129.126.118.1]")]
        [InlineData("js@proseware.com9")]
        [InlineData("j.s@server1.proseware.com")]
        [InlineData("\"j\\\"s\\\"\"@proseware.com")] //"j\"s\""@proseware.com
        [InlineData("someone@somewhere.co.uk")]
        [InlineData("someone+tag@somewhere.net")]
        //[InlineData("futureTLD@somewhere.fooo")]
        //[InlineData("js@contoso.中国")]
        public void StringExtension_EmailEhValido_DeveSerValido(string str)
        {
            var result = str.EmailEhValido();

            Assert.True(result);
        }


        [Theory(DisplayName = "StringExtension - EmailEhValido - Deve Ser Inválido")]
        [InlineData("j.@server1.proseware.com")]
        [InlineData("j..s@proseware.com")]
        [InlineData("js*@proseware.com")]
        [InlineData("js@proseware..com")]
        [InlineData("fdsa")]
        [InlineData("fdsa@")]
        [InlineData("fdsa@fdsa")]
        [InlineData("fdsa@fdsa.")]
        public void StringExtension_EmailEhValido_DeveSerInvalido(string str)
        {
            var result = str.EmailEhValido();

            Assert.False(result);
        }
    }
}
