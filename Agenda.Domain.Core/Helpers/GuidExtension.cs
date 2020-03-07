using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.Helpers
{
    public static class GuidExtension
    {
        public static bool EhVazio(this Guid guid)
        {
            return guid == Guid.Empty;
        }

    }
}
