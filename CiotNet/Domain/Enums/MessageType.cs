using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Enums
{
    public enum MessageType
    {
        Unknown,
        Start,
        Stop,
        GetConfig,
        GetStatus,
        Request,
        Event
    }
}
