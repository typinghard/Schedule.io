using System;
using MediatR;

namespace Schedule.io.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public string DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.Now;
            DomainNotificationId = Guid.NewGuid().ToString();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
