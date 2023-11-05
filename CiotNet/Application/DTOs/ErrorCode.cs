using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs
{
    public enum ErrorCode
    {
        Fail = -1,
        Ok,
        NullArg,
        InvalidId,
        InvalidType,
        Overflow,
        NotImplemented,
        NotSupported,
        Busy,
        InvalidState,
        SerializationError,
        DeserializationError,
        SendDataError,
        ReceiveDataError,
        InvalidSize,
        Closed,
        NotFound,
        ValidationFailed,
        ConnectError,
        DisconnectError,
        Exception,
        TerminatorMissing,
        InvalidArg,
        NoMemory
    }
}
