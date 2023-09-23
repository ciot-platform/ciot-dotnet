using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Shared;
using System;

namespace ciot_net.Infrastructure
{
    public class UsbConnection : IConnection<ProtocolTypeUsb>
    {
        private readonly IProtocol<ProtocolTypeUsb> protocol;

        public event EventHandler<MessageDto> DataReceived;

        public ConnectionType ConnectionType => ConnectionType.Usb;

        public ProtocolTypeUsb ProtocolType => protocol.ProtocolType;

        public UsbConnection(IUsbProtocol protocol)
        {
            this.protocol = protocol;
        }

        public Result Connect()
        {
            return protocol.Connect();
        }

        public Result Disconnect()
        {
            return protocol.Disconnect();
        }

        public Result SendData(MessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
