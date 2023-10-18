using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;
using System;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IInterface <ConfigType, StatusType, RequestType>
    {
        event EventHandler<InterfaceEvent> InterfaceEvent;

        ConfigType Config { get; }
        StatusType Status { get; }

        ErrorCode Start(ConfigType config);
        ErrorCode Stop();
        ErrorCode ProcessRequest(RequestType request);
        ErrorCode ProcessMessage(MessageDto message);
        ErrorCode SendData(byte[] data);
    }
}
