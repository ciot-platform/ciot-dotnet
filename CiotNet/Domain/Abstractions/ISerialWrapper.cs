using System;

namespace CiotNetNS.Domain.Abstractions
{
    public interface ISerialWrapper
    {
        event EventHandler<byte[]> DataReceived;
        string Port { get; set; }
        bool IsOpen { get; }
        void Open();
        void Close();
        void Write(byte[] data);
        void Read(byte[] data);
        string[] GetPorts();
    }
}
