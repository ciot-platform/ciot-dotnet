using CiotNet.Serializer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.System
{
    public enum RequestType
    {
        Unknown,
        Restart,
    }

    public class SystemRequestDto
    {
        public RequestType Type { get; set; }
    }
}
