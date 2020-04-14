
using Schedule.io.Core.Data.Configurations.Enums;

namespace Schedule.io.Core.Data.Configurations
{
    public interface IDataBaseConfig
    {
        EDataBaseType GetDataBaseType();
    }
}
