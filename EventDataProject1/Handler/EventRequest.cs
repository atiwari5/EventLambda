using System;
using System.Collections.Generic;
using System.Text;

namespace EventDataProject1.Handler
{
    public class EventRequest : IntegrationEvent
    {
        public string Event_Type { get; set; }

        public string Event_Time { get; set; }

        public string Policy { get; set; }

        public string Event_Eff_Date { get; set; }

        public string Api_Key { get; set; }

        public string Source_Req_Key { get; set; }

    }
}
