using System;
using System.Threading.Tasks;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Shared;

namespace CiotNetNS.Domain.Interfaces
{
    public interface IConnection
    {
        ConnectionType ConnectionType { get; }

        event EventHandler<byte[]> DataReceived;

        Task<Result> Connect();

        Result Disconnect();

        Result SendData(byte[] data);
    }
}
