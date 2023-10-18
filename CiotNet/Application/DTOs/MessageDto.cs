using CiotNet.Serializer.Domain.Interfaces;
using CiotNet.Serializer.Infrastructure;
using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Application.DTOs
{
    public class MessageDto
    {
        public MessageType Type { get; set; }

        public InterfaceInfoDto Interface { get; set; }

        public ErrorCode Error { get; set; }

        public Union Data { get; set; }

        public MessageDto(MessageType messageType = MessageType.Unknown, InterfaceType interfaceType = InterfaceType.Unknown, byte interfaceId = 0, Union data = null)
        {
            Type = messageType;
            Interface = new InterfaceInfoDto(interfaceType, interfaceId);
            Error = ErrorCode.Ok;
            Data = data;
        }

        public static MessageDto Deserialize(ISerializer serializer, byte[] data, int offset = 0)
        {
            return serializer.Deserialize<MessageDto>(data, offset);
        }
    }
}
