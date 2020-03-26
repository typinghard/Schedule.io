using System;
using System.Collections.Generic;
using System.Text;

namespace EventSoursing
{
    public static class EventSourcingConfigurationHelper
    {
        public static bool Use { get; private set; }
        public static void SetUse(bool use)
        {
            Use = use;
        }
    }
}
