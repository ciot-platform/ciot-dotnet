using CiotNet.Serializer.Infrastructure;
using CiotNet.Serializer.Tests.Mocks;

namespace CiotNet.Serializer.Tests.Infrastructure
{
    public class BinarySerializerTests
    {   
        [Test]
        public void DeserializeTest()
        {
            var serializer = new BinarySerializer();
            var message = serializer.Deserialize<Message>(Message.MockRaw);

            Assert.Multiple(() =>
            {
                Assert.That(message.State, Is.EqualTo(Message.Mock.State));
                Assert.That(message.Id, Is.EqualTo(Message.Mock.Id));
                Assert.That(message.Data1, Is.EqualTo(Message.Mock.Data1));
                Assert.That(message.Data2, Is.EqualTo(Message.Mock.Data2));
                Assert.That(message.IsTrue, Is.EqualTo(Message.Mock.IsTrue));
                Assert.That(message.IsEnabled, Is.EqualTo(Message.Mock.IsEnabled));
                Assert.That(message.Number1, Is.EqualTo(Message.Mock.Number1));
                Assert.That(message.Number2, Is.EqualTo(Message.Mock.Number2));
                Assert.That(message.Number3, Is.EqualTo(Message.Mock.Number3));
                Assert.That(message.Number4, Is.EqualTo(Message.Mock.Number4));
                Assert.That(message.Number5, Is.EqualTo(Message.Mock.Number5));
                Assert.That(message.Number6, Is.EqualTo(Message.Mock.Number6));
                Assert.That(message.FloatNum, Is.EqualTo(Message.Mock.FloatNum));
                Assert.That(message.DoubleNum, Is.EqualTo(Message.Mock.DoubleNum));
                Assert.That(message.String1, Is.EqualTo(Message.Mock.String1));
                Assert.That(message.String2, Is.EqualTo(Message.Mock.String2));
                Assert.That(message.Child.Id, Is.EqualTo(Message.Mock.Child.Id));
                Assert.That(message.Child.Name, Is.EqualTo(Message.Mock.Child.Name));
                Assert.That(message.LastNumber, Is.EqualTo(Message.Mock.LastNumber));
                Assert.IsTrue(message.Bytes.SequenceEqual(Message.Mock.Bytes));
            });
        }

        [Test]
        public void SerializeTest()
        {
            var serializer = new BinarySerializer();
            var data = serializer.Serialize(Message.Mock);
            for (int i = 0; i < data.Length; i++)
            {
                Assert.That(data[i], Is.EqualTo(Message.MockRaw[i]));
            }
        }
    }
}
