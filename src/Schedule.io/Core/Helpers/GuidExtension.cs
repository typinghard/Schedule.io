using System;

namespace Schedule.io.Core.Helpers
{
    public static class GuidExtension
    {
        public static bool EhVazio(this Guid guid)
        {
            return guid == Guid.Empty;
        }

    }
}
