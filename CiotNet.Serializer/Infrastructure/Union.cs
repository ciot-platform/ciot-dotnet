using CiotNet.Serializer.Domain.Interfaces;

namespace CiotNet.Serializer.Infrastructure
{
    public class Union : IUnion
    {
        private ISerializer serializer;

        private byte[] data;

        public void SetSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public void SetData(byte[] data)
        {
            this.data = data;
        }

        public byte[] GetData()
        {
            return data;
        }

        public T Get<T>()
        {
            return serializer.Deserialize<T>(data);
        }

        public void Set<T>(T data)
        {
            this.data = serializer.Serialize(data);
        }
    }
}
