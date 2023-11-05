using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNet.Serializer.Domain.Interfaces
{
    public interface IUnion
    {
        void SetSerializer(ISerializer serializer);

        void SetData(byte[] data);

        byte[] GetData();
    }
}
