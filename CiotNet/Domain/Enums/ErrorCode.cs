using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Enums
{
    public enum ErrorCode
    {
        Fail = -1,
        Success,
        IncorrectState,
        SerializationError,
        DeserializationError,
        SendDataError,
        ReceiveDataError,
        BufferOverflow,
        IncorrectSize,
        SerialPortClosed,
        NotFound,
        ValidationFailed,
        ConnectError,
        DisconnectError,
        Exception,
        TerminatorMissing
    }
}
