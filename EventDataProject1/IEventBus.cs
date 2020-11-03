using EventDataProject1.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventDataProject1
{
    interface IEventBus
    {
        void Publish<T>(T e) where T : EventRequest;
    }
}
