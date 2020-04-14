﻿using Schedule.io.Core.Messages;
using System;


namespace Schedule.io.Events.LocalEvents
{
    public class LocalRemovidoEvent : Event
    {
        public string Id { get; set; }

        public LocalRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}