using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNet.Serializer.Domain.Interfaces
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T data);
        T Deserialize<T>(byte[] data, int offset = 0);
        int SizeOf<T>(T data);
    }
}
