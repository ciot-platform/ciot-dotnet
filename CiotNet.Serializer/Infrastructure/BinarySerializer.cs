using CiotNet.Serializer.Domain.Interfaces;
using System;
using System.Reflection;

namespace CiotNet.Serializer.Infrastructure
{
    public class BinarySerializer : IBinarySerializer
    {
        public int endIdx = 0;

        public T Deserialize<T>(byte[] data, int offset = 0)
        {
            int idx = offset;
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                var val = DeserializeValue(prop.PropertyType, data, ref idx);
                prop.SetValue(obj, val);
            }

            endIdx = idx;

            return (T)obj;
        }

        public byte[] Serialize(object data)
        {
            return new byte[] { (byte)data };
        }

        private dynamic DeserializeValue(Type type, byte[] data, ref int idx)
        {
            if(type.IsEnum)
            {
                return Enum.GetValues(type).GetValue(data[idx++]);
            }

            if(type == typeof(byte) || type == typeof(bool))
            {
                return Convert.ChangeType(data[idx++], type);
            }

            if (type == typeof(short))
            {
                var number = BitConverter.ToInt16(data, idx);
                idx += 2;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(ushort))
            {
                var number = BitConverter.ToUInt16(data, idx);
                idx += 2;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(uint))
            {
                var number = BitConverter.ToUInt32(data, idx);
                idx += 4;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(int))
            {
                var number = BitConverter.ToInt32(data, idx);
                idx += 4;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(ulong))
            {
                var number = BitConverter.ToUInt64(data, idx);
                idx += 8;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(long))
            {
                var number = BitConverter.ToInt64(data, idx);
                idx += 8;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(float))
            {
                var number = BitConverter.ToSingle(data, idx);
                idx += 4;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(double))
            {
                var number = BitConverter.ToDouble(data, idx);
                idx += 8;
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(string))
            {
                string text = "";
                while (data[idx] != '\0')
                {
                    text += (char)data[idx++];
                }
                idx++;
                return text;
            }

            if (type.IsClass)
            {
                MethodInfo method = typeof(BinarySerializer).GetMethod("Deserialize").MakeGenericMethod(type);
                var obj = method.Invoke(this, new object[] { data, idx });
                idx = endIdx;
                return obj;
            }

            return 0;
        }

    }
}
