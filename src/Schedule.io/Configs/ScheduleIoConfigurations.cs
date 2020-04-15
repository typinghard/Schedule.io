namespace Schedule.io.Configs
{
    public class ScheduleIoConfigurations
    {
        public ScheduleIoConfigurations(bool useEventSourcing = false)
        {
            UseEventSourcing = useEventSourcing;
        }
        public bool UseEventSourcing { get; }
    }
}
