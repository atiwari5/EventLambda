using System;
using System.Collections.Generic;
using System.Text;

namespace EventDataProject1
{
    public class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
