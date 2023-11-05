using CiotNetNS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IInterfaceBase <ConfigType, StatusType, RequestType> : IInterface
    {
        ConfigType Config { get; }
        StatusType Status { get; }

        ErrorCode Start(ConfigType config);
        ErrorCode ProcessRequest(RequestType request);
    }
}
