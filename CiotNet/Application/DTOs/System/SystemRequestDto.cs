using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.System
{
    public enum RequestType
    {
        Unknown,
        Restart,
        FactoryReset,
    }

    public class SystemRequestDto
    {
        public RequestType Type { get; set; }
    }
}
