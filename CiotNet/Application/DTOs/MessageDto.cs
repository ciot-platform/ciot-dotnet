
using CiotNet.Serializer.Domain.Interfaces;
using CiotNetNS.Application.DTOs.System;
using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Application.DTOs
{
    public class MessageDto
    {
        public InterfaceType Interface { get; set; }

        public MessageType Type { get; set; }

        public object Data { get; set; }

        public MessageDto(InterfaceType @interface = InterfaceType.Unknown, MessageType type = MessageType.Unknown, object data = null)
        {
            Interface = @interface;
            Type = type;
            Data = data;
        }

        public MessageDto(InterfaceType @interface, MessageType type, byte[] data, ISerializer serializer)
        {
            Interface = @interface;
            Type = type;

            switch (@interface)
            {
                case InterfaceType.Unknown:
                    Data = null;
                    break;
                case InterfaceType.System:
                    Data = SystemSerialize(type, data, serializer);
                    break;
            }
        }

        object SystemSerialize(MessageType type, byte[] data, ISerializer serializer)
        {
            switch (type)
            {
                case MessageType.Unknown:
                    return null;
                case MessageType.Start:
                    return null;
                case MessageType.Stop:
                    return null;
                case MessageType.GetConfig:
                    return null;
                case MessageType.GetStatus:
                    return serializer.Deserialize<SystemStatusDto>(data);
                case MessageType.Request:
                    return serializer.Deserialize<SystemRequestDto>(data);

            }
            return null;
        }
    }
}
