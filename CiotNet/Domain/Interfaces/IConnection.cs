using System;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Shared;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IConnection <TProtocol> : IProtocol <TProtocol>
    {
        ConnectionType ConnectionType { get; }
    }
}
