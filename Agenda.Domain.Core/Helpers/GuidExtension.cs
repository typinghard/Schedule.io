using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.Helpers
{
    public static class GuidExtension
    {
        public static bool EhVazio(this string guid)
        {
            return string.IsNullOrEmpty(guid);
        }

    }
}
