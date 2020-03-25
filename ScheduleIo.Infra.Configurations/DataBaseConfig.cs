using ScheduleIo.Infra.Configurations.Enums;
using System;

namespace ScheduleIo.Infra.Configurations
{
    public interface IDataBaseConfig
    {
        EDataBaseType GetDataBaseType();
    }
}
