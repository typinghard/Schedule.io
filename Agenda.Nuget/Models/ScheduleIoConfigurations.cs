using ScheduleIo.Infra.Configurations;
using ScheduleIo.Infra.Configurations.Enums;
using ScheduleIo.Infra.MongoDB.Configs;
using ScheduleIo.Infra.RavenDB.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class ScheduleIoConfigurations
    {
        public IDataBaseConfig DataBaseConfig { get; }
        public bool UseEventSourcing { get; }
        public ScheduleIoConfigurations(MongoDBConfig mongoDBConfig, bool useEventSourcing = false)
        {
            UseEventSourcing = useEventSourcing;
            DataBaseConfig = mongoDBConfig;
        }
        public ScheduleIoConfigurations(RavenDBConfig ravenDBConfig, bool useEventSourcing = false)
        {
            UseEventSourcing = useEventSourcing;
            DataBaseConfig = ravenDBConfig;
        }

        //public EDataBaseType GetDataBaseType()
        //{
        //    if(DataBaseConfig.GetType(DataBaseConfig) == )
        //}
    }
}
