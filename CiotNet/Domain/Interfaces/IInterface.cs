using CiotNetNS.Application.DTOs;
using CiotNetNS.Application.DTOs.Interface;
using CiotNetNS.Application.DTOs.System;
using System;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IInterface
    {
        event EventHandler<InterfaceEvent> InterfaceEvent;

        ErrorCode Stop();
        ErrorCode ProcessMessage(MessageDto message);
        ErrorCode SendData(byte[] data);
        ErrorCode SendMessage(MessageDto message);
    }
}
