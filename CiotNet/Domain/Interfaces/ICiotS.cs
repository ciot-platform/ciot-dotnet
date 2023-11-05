using CiotNetNS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Interfaces
{
    public interface ICiotS
    {
        event EventHandler<byte[]> OnMessage;
        ErrorCode Send(byte[] data);
        ErrorCode ProcessByte(byte @byte);
    }
}
