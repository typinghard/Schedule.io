using Schedule.io.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Schedule.io.Test.Testes_Unitários.Core.Helpers
{
    public class StringExtensionsTests
    {
        [Theory(DisplayName = "StringExtension - EhVazio - Deve Ser True")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void StringExtension_EhVazio_DeveSerTrue(string str)
        {
            var result = str.EhVazio();

            Assert.True(result);
        }

        [Theory(DisplayName = "StringExtension - EhVazio - Deve Ser False")]
        [InlineData(".")]
        [InlineData(" .")]
        [InlineData(". ")]
        public void StringExtension_EhVazio_DeveSerFalse(string str)
        {
            var result = str.EhVazio();

            Assert.False(result);
        }


        [Theory(DisplayName = "StringExtension - ValidarTamanho - Deve Ser True")]
        [InlineData("abcde", 1, 5)]
        [InlineData("a", 1, 1)]
        [InlineData("abcde", 5, 5)]
        [InlineData("a", -1, 1)]
        public void StringExtension_ValidarTamanho_DeveSerTrue(string str, int tamanhoMinimo, int tamanhoMaximo)
        {
            var result = str.ValidarTamanho(tamanhoMinimo, tamanhoMaximo);

            Assert.True(result);
        }

        [Theory(DisplayName = "StringExtension - ValidarTamanho - Deve Ser False")]
        [InlineData("", 1, 5)]
        [InlineData("abcde", 6, 5)]
        [InlineData("a", -1, 0)]
        public void StringExtension_ValidarTamanho_DeveSerFalse(string str, int tamanhoMinimo, int tamanhoMaximo)
        {
            var result = str.ValidarTamanho(tamanhoMinimo, tamanhoMaximo);

            Assert.False(result);
        }

        [Theory(DisplayName = "StringExtension - EmailEhValido - Deve Ser True")]
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
        public void StringExtension_EmailEhValido_DeveSerTrue(string str)
        {
            var result = str.EmailEhValido();

            Assert.True(result);
        }


        [Theory(DisplayName = "StringExtension - EmailEhValido - Deve Ser False")]
        [InlineData("j.@server1.proseware.com")]
        [InlineData("j..s@proseware.com")]
        [InlineData("js*@proseware.com")]
        [InlineData("js@proseware..com")]
        [InlineData("fdsa")]
        [InlineData("fdsa@")]
        [InlineData("fdsa@fdsa")]
        [InlineData("fdsa@fdsa.")]
        public void StringExtension_EmailEhValido_DeveSerFalse(string str)
        {
            var result = str.EmailEhValido();

            Assert.False(result);
        }
    }
}
