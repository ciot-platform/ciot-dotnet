using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNet.Serializer.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SizeAttribute : Attribute
    {
        public int Value { get; }

        public SizeAttribute(int length)
        {
            Value = length;
        }
    }
}
