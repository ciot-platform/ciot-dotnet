using CiotNetNS.Application.DTOs;
using CiotNetNS.Shared;
using System;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IProtocol
    {
        event EventHandler <MessageDto> OnMessage;
        event EventHandler <Result> OnError;
        Result SendMessage(MessageDto message);
    }
}
