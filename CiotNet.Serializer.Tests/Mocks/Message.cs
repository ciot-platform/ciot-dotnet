using CiotNet.Serializer.Infrastructure;

namespace CiotNet.Serializer.Tests.Mocks
{
    public enum MessageState
    {
        Unknown,
        Received,
        Sended
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
        public MessageChild Child { get; set; } = new MessageChild();
        public int LastNumber { get; set; }

        [Size(6)]
        public byte[] Bytes { get; set; } = { };

        public static Message Mock = new Message()
        {
            State = MessageState.Sended,
            Id = 0x01,
            Data1 = 0x02,
            Data2 = 0x03,
            IsTrue = true,
            IsEnabled = false,
            Number1 = 0x7f7f,
            Number2 = 0xabcd,
            Number3 = 0x11223344,
            Number4 = 0xaabbccdd,
            Number5 = 0x0123456789abcdef,
            Number6 = 0x0011223344556677,
            FloatNum = 3.55f,
            DoubleNum = 54.25,
            String1 = "hello",
            String2 = "world",
            Child = new MessageChild()
            {
                Id = 0x01,
                Name = "name",
            },
            LastNumber = 0x24233434,
            Bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 }
        };

        public static byte[] MockRaw = new byte[]
        {
            0x02, // State
            0x01, // Id
            0x02, // Data1
            0x03, // Data2
            0x01, // IsTrue
            0x00, // IsEnabled
            0x7f, 0x7f, // Number1
            0xcd, 0xab, // Number2
            0x44, 0x33, 0x22, 0x11, // Number3
            0xdd, 0xcc, 0xbb, 0xaa,  // Number4
            0xef, 0xcd, 0xab, 0x89, 0x67, 0x45, 0x23, 0x01, // Number5
            0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00, // Number6
            51, 51, 99, 64, // FloatNum
            0, 0, 0, 0, 0, 32, 75, 64, // DoubleNum 
            (byte)'h', (byte)'e', (byte)'l', (byte)'l', (byte)'o', (byte)'\0', // String1
            (byte)'w', (byte)'o', (byte)'r', (byte)'l', (byte)'d', (byte)'\0', // String1
            0x01, // Child.Id
            (byte)'n', (byte)'a', (byte)'m', (byte)'e', (byte)'\0', // Child.Name
            0x34, 0x34, 0x23, 0x24, // LastNumber
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, // Bytes
        };
    }
}
