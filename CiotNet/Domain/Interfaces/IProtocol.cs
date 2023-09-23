using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IProtocol <TProtocol>
    {
        TProtocol ProtocolType { get; }

        event EventHandler<MessageDto> DataReceived;

        Result Connect();

        Result Disconnect();

        Result SendData(MessageDto message);
    }

    public interface IUsbProtocol : IProtocol<ProtocolTypeUsb> { }

    public interface IBleProtocol : IProtocol<ProtocolTypeBle> { }

    public interface ITcpProtocol : IProtocol<ProtocolTypeTcp> { }
}
