using CiotNet.Serializer.Infrastructure;
using CiotNetNS.Application.DTOs.Interface;
using CiotNetNS.Application.DTOs.Uart;

namespace CiotNetNS.Application.DTOs
{
    public enum MessageType
    {
        Unknown,
        Start,
        Stop,
        GetConfig,
        GetStatus,
        Request,
        Event
    }

    public interface IMessageData { }

    public interface IMessageConfig : IMessageData{ }

    public interface IMessageStatus : IMessageData { }

    public interface IMessageRequest : IMessageData { }

    public class MessageData : Union<IMessageData>
    {
        public UartDataDto Uart { get => Get<UartDataDto>(); set => Set(value); }
    }

    public class MessageDto
    {
        public MessageType Type { get; set; }

        public InterfaceInfoDto Interface { get; set; }

        public ErrorCode Error { get; set; }

        public MessageData Data { get; set; }

        public MessageDto() 
        {
            Type = MessageType.Unknown;
            Interface = new InterfaceInfoDto();
            Error = ErrorCode.Ok;
            Data = new MessageData();
        }
    }
}
