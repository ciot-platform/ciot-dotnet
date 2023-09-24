using CiotNet.Serializer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CiotNet.Serializer.Infrastructure
{
    public class BinarySerializer : ISerializer
    {
        public int endIdx = 0;

        public T Deserialize<T>(byte[] data, int offset = 0)
        {
            int idx = offset;
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                var val = DeserializeValue(prop.PropertyType, data, ref idx, prop);
                prop.SetValue(obj, val);
            }

            endIdx = idx;

            return (T)obj;
        }

        public byte[] Serialize<T>(T data)
        {
            var bytes = new List<byte>();
            var type = data.GetType();

            foreach (var prop in type.GetProperties())
            {
                dynamic value = prop.GetValue(data);
                var result = SerializeValue(value.GetType(), value, prop);
                bytes.AddRange(result);
            }

            return bytes.ToArray();
        }

        private byte[] SerializeValue(Type type, dynamic value, PropertyInfo prop = null)
        {
            if (type.IsEnum || type == typeof(byte))
            {
                byte @byte = (byte)value;
                return new byte[] { @byte };
            }
            else if (type == typeof(string))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(value + '\0');
                return bytes;
            }
            else if (type.IsArray)
            {
                var size = (SizeAttribute)Attribute.GetCustomAttribute(prop, typeof(SizeAttribute));
                Array array = (Array)value;
                var bytes = new List<byte>();
                for (int i = 0; i < size.Value; i++)
                {
                    var item = array.GetValue(i);
                    bytes.AddRange(SerializeValue(item.GetType(), item));
                }
                return bytes.ToArray();
            }
            else if (type.IsClass)
            {
                return Serialize(value);
            }
            else
            {
                return BitConverter.GetBytes(value);
            }
        }

        private dynamic DeserializeValue(Type type, byte[] data, ref int idx, PropertyInfo prop = null)
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

            if (type.IsArray)
            {
                var size = (SizeAttribute)Attribute.GetCustomAttribute(prop, typeof(SizeAttribute));
                dynamic arr = Array.CreateInstance(type.GetElementType(), size.Value);
                for (int i = 0; i < size.Value; i++)
                {
                    arr[i] = DeserializeValue(type.GetElementType(), data, ref idx);
                }
                return arr;
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
