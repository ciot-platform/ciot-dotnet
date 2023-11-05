using CiotNetNS.Application.DTOs;
using CiotNetNS.Application.DTOs.Interface;
using CiotNetNS.Domain.Interfaces;
using System;

namespace CiotNetNS.Infrastructure
{
    public abstract class InterfaceBase <ConfigType, StatusType, RequestType> : IInterfaceBase <ConfigType, StatusType, RequestType> 
        where ConfigType : IMessageConfig
        where StatusType : IMessageStatus
        where RequestType : IMessageRequest
    {
        protected InterfaceInfoDto Info {  get; set; }

        public abstract ConfigType Config { get; }

        public abstract StatusType Status { get; }

        public abstract event EventHandler<InterfaceEvent> InterfaceEvent;

        public ErrorCode ProcessMessage(MessageDto message)
        {
            if (message == null) return ErrorCode.NullArg;

            if (message.Interface.Type != Info.Type) return ErrorCode.InvalidType;

            switch (message.Type)
            {
                case MessageType.Unknown:
                    return ErrorCode.InvalidType;
                case MessageType.Start:
                    return Start(message.Data.Get<ConfigType>());
                case MessageType.Stop:
                    return Stop();
                case MessageType.GetConfig:
                    message.Data.Set(Config);
                    return ErrorCode.Ok;
                case MessageType.GetStatus:
                    message.Data.Set(Status);
                    return ErrorCode.Ok;
                case MessageType.Request:
                    return ProcessRequest(message.Data.Get<RequestType>());
                case MessageType.Event:
                    return ErrorCode.NotImplemented;
            }

            return ErrorCode.InvalidType;
        }

        public abstract ErrorCode Start(ConfigType config);

        public abstract ErrorCode Stop();

        public abstract ErrorCode ProcessRequest(RequestType request);

        public abstract ErrorCode SendData(byte[] data);

        public abstract ErrorCode SendMessage(MessageDto message);        
    }
}
