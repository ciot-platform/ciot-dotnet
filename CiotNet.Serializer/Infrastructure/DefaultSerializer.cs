using CiotNet.Serializer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNet.Serializer.Infrastructure
{
    public static class DefaultSerializer
    {
        public static readonly ISerializer instance = new BinarySerializer();
    }
}
