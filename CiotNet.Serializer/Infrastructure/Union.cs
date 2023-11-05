using CiotNet.Serializer.Domain.Interfaces;

namespace CiotNet.Serializer.Infrastructure
{
    public class Union<BaseType> : IUnion
    {
        private ISerializer serializer;

        protected byte[] data;

        public Union(ISerializer serializer = null) 
        {
            if(serializer == null)
            {
                this.serializer = DefaultSerializer.instance;
            }
            data = new byte[512];
        }

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

        public T Get<T>() where T : BaseType
        {
            return serializer.Deserialize<T>(data);
        }

        public void Set<T>(T data) where T : BaseType
        {
            if(data != null)
            {
                this.data = serializer.Serialize(data);
            }
        }
    }
}
