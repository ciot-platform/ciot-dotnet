using CiotNet.Serializer.Infrastructure;

namespace CiotNet.Serializer.Tests.Infrastructure
{
    public class BinarySerializerTests
    {
        public enum MessageState
        {
            Unknown,
            Received,
            Sended
        }

        public class Child
        {
            public byte Id { get; set; }
            public string Name { get; set; } = "";
        }
         
        public class Message
        {
            public MessageState State { get; set; }
            public byte Id { get; set; }
            public byte Data1 { get; set; }
            public byte Data2 { get; set; }
            public bool IsTrue { get; set; }
            public bool IsEnabled { get; set; }
            public short Number1 { get; set; }
            public ushort Number2 { get; set; }
            public int Number3 { get; set; }
            public uint Number4 { get; set; }
            public long Number5 { get; set; }
            public ulong Number6 { get; set; }
            public float FloatNum { get; set; }
            public double DoubleNum { get; set; }
            public string String1 { get; set; } = "";
            public string String2 { get; set; } = "";
            public Child Child { get; set; } = new Child();
            public int LastNumber { get; set; }
        }

        [Test]
        public void DeserializeTest()
        {
            var serializer = new BinarySerializer();
            byte[] data = {
                0x02, // State
                0x01, // Id
                0x02, // Data1
                0x03, // Data2
                0x01, // IsTrue
                0x00, // IsEnabled
                0x7f, 0x7f, // Number1
                0xab, 0xcd, // Number2
                0x11, 0x22, 0x33, 0x44, // Number3
                0xaa, 0xbb, 0xcc, 0xdd,  // Number4
                0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0x01, // Number5
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x01, // Number6
                0x25, 0x78, 0x32, 0x01, // FloatNum
                0x33, 0x42, 0x56, 0x22, 0x12, 0x13, 0x58, 0x22, // DoubleNum 
                (byte)'h', (byte)'e', (byte)'l', (byte)'l', (byte)'o', (byte)'\0', // String1
                (byte)'w', (byte)'o', (byte)'r', (byte)'l', (byte)'d', (byte)'\0', // String1
                0x01, // Child.Id
                (byte)'n', (byte)'a', (byte)'m', (byte)'e', (byte)'\0', // Child.Name
                0x34, 0x34, 0x23, 0x24 // LastNumber
            };

            var message = serializer.Deserialize<Message> (data);

            Assert.Multiple(() =>
            {
                Assert.That(message.State, Is.EqualTo((MessageState)data[0]));
                Assert.That(message.Id, Is.EqualTo(data[1]));
                Assert.That(message.Data1, Is.EqualTo(data[2]));
                Assert.That(message.Data2, Is.EqualTo(data[3]));
                Assert.That(message.IsTrue, Is.EqualTo((data[4] == 0x01)));
                Assert.That(message.IsEnabled, Is.EqualTo((data[5] == 0x01)));
                Assert.That(message.Number1, Is.EqualTo(0x7f7f));
                Assert.That(message.Number2, Is.EqualTo(0xcdab));
                Assert.That(message.Number3, Is.EqualTo(0x44332211));
                Assert.That(message.Number4, Is.EqualTo(0xddccbbaa));
                Assert.That(message.Number5, Is.EqualTo(0x01cdab8967452301));
                Assert.That(message.Number6, Is.EqualTo(0x0166554433221100));
                Assert.That(BitConverter.ToInt32(BitConverter.GetBytes(message.FloatNum)), Is.EqualTo(0x01327825));
                Assert.That(BitConverter.ToInt64(BitConverter.GetBytes(message.DoubleNum)), Is.EqualTo(0x2258131222564233));
                Assert.That(message.String1, Is.EqualTo("hello"));
                Assert.That(message.String2, Is.EqualTo("world"));
                Assert.That(message.Child.Id, Is.EqualTo(0x01));
                Assert.That(message.Child.Name, Is.EqualTo("name"));
                Assert.That(message.LastNumber, Is.EqualTo(0x24233434));
            });
        }
    }
}
