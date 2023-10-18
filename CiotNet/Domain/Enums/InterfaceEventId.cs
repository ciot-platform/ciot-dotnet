using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Enums
{
    public enum InterfaceEventId
    {
        Unknown = 0,
        Started,
        Stopped,
        Error,
        Data,
        Custom
    }
}
