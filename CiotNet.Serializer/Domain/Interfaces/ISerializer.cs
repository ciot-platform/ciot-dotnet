using CiotNet.Serializer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNet.Serializer.Domain.Interfaces
{
    public interface ISerializer <TSerializationType>
    {
        TSerializationType Serialize(object data);
        T Deserialize<T>(TSerializationType data, int offset = 0);
    }
}
