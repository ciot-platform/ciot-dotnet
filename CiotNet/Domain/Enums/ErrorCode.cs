using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Enums
{
    public enum ErrorCode
    {
        Fail = -1,
        Ok,
        NullArg,
        InvalidId,
        InvalidType,
        BufferOverflow,
        NotImplemented,
        NotSupported,
        Busy,
        InvalidState,
        SerializationError,
        DeserializationError,
        SendDataError,
        ReceiveDataError,
        InvalidSize,
        SerialPortClosed,
        NotFound,
        ValidationFailed,
        ConnectError,
        DisconnectError,
        Exception,
        TerminatorMissing
    }
}
