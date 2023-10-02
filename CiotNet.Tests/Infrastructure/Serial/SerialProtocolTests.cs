using CiotNet.Serializer.Domain.Interfaces;
using CiotNet.Tests.Mocks;
using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Infrastructure.Serial;
using CiotNetNS.Shared;
using Moq;

namespace CiotNet.Tests.Infrastructure.Serial
{
    public class SerialProtocolTests
    {
        readonly Mock<IConnection> connectionMock = new();
        readonly Mock<ISerializer> serializerMock = new();
        SerialProtocol protocol;

        [SetUp]
        public void SetUp()
        {
            protocol = new SerialProtocol(connectionMock.Object, serializerMock.Object);
        }

        [Test]
        public void TestSendMessageSucess()
        {
            var bytesSended = new List<byte>();
            serializerMock.Setup(e => e.Serialize(MessageDtoMocks.Mock)).Returns(MessageDtoMocks.MockData);
            connectionMock.Setup(e => e.SendData(It.IsAny<byte[]>())).Callback<byte[]>(data =>
            {
                bytesSended.AddRange(data);
            });
            var result = protocol.SendMessage(MessageDtoMocks.Mock);
            Assert.Multiple(() =>
            {
                Assert.That(bytesSended[0], Is.EqualTo((byte)'{'));
                Assert.That(bytesSended[1], Is.EqualTo(0x02));
                Assert.That(bytesSended[2], Is.EqualTo(0x00));
                Assert.That(bytesSended[3], Is.EqualTo(0x01));
                Assert.That(bytesSended[4], Is.EqualTo(0x04));
                Assert.That(bytesSended[5], Is.EqualTo((byte)'}'));
                Assert.That(result.Ok, Is.EqualTo(true));
            });
        }

        [Test]
        public void TestSendMessageSerializationException()
        {
            var bytesSended = new List<byte>();
            serializerMock.Setup(e => e.Serialize(MessageDtoMocks.Mock)).Throws(new InvalidOperationException());
            var result = protocol.SendMessage(MessageDtoMocks.Mock);
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.SerializationError));
            });
        }

        [Test]
        public void TestSendMessageSendDataError()
        {
            var bytesSended = new List<byte>();
            serializerMock.Setup(e => e.Serialize(MessageDtoMocks.Mock)).Returns(MessageDtoMocks.MockData);
            connectionMock.Setup(e => e.SendData(It.IsAny<byte[]>())).Throws(new InvalidOperationException());
            var result = protocol.SendMessage(MessageDtoMocks.Mock);
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.SendDataError));
            });
        }

        [Test]
        public void TestOnErrorBufferOverflow()
        {
            Result? result = null;
            bool callbackExecuted = false;
            var callbackCompleted = new ManualResetEvent(false);
            var bigData = new byte[8192];
            var args = new object[] { connectionMock.Object, bigData };

            protocol.OnError += (s, e) =>
            {
                result = e;
                callbackExecuted = true;
                callbackCompleted.Set();
            };

            connectionMock.Raise(e => e.DataReceived += null, args);

            bool signaled = callbackCompleted.WaitOne(TimeSpan.FromSeconds(5));

            if (callbackExecuted)
            {
                Assert.That(result?.Failed, Is.True);
                Assert.That(result?.Error, Is.EqualTo(ErrorCode.BufferOverflow));
            }
            else
            {
                Assert.Fail("Event DataReceived not triggered");
            }
        }

        [Test]
        public void TestOnErrorIncorrectSize()
        {
            Result? result = null;
            bool callbackExecuted = false;
            var callbackCompleted = new ManualResetEvent(false);
            var data = new byte[] { (byte)'{', 0x01, 0x00, 0x01, 0x04, (byte)'}' };
            var args = new object[] { connectionMock.Object, data };

            protocol.OnError += (s, e) =>
            {
                result = e;
                callbackExecuted = true;
                callbackCompleted.Set();
            };

            connectionMock.Raise(e => e.DataReceived += null, args);

            bool signaled = callbackCompleted.WaitOne(TimeSpan.FromSeconds(5));

            if (callbackExecuted)
            {
                Assert.That(result?.Failed, Is.True);
                Assert.That(result?.Error, Is.EqualTo(ErrorCode.IncorrectSize));
            }
            else
            {
                Assert.Fail("Event DataReceived not triggered");
            }
        }

        [Test]
        public void TestOnErrorTerminatorMissing()
        {
            Result? result = null;
            bool callbackExecuted = false;
            var callbackCompleted = new ManualResetEvent(false);
            var data = new byte[] { (byte)'{', 0x02, 0x00, 0x01, 0x04, (byte)'_' };
            var args = new object[] { connectionMock.Object, data };

            protocol.OnError += (s, e) =>
            {
                result = e;
                callbackExecuted = true;
                callbackCompleted.Set();
            };

            connectionMock.Raise(e => e.DataReceived += null, args);

            bool signaled = callbackCompleted.WaitOne(TimeSpan.FromSeconds(5));

            if (callbackExecuted)
            {
                Assert.That(result?.Failed, Is.True);
                Assert.That(result?.Error, Is.EqualTo(ErrorCode.TerminatorMissing));
            }
            else
            {
                Assert.Fail("Event DataReceived not triggered");
            }
        }

        [Test]
        public void TestOnDataEventSuccess()
        {
            MessageDto? result = null;
            bool callbackExecuted = false;
            var callbackCompleted = new ManualResetEvent(false);
            var data = new byte[] { (byte)'{', 0x02, 0x00, 0x01, 0x04, (byte)'}' };
            var args = new object[] { connectionMock.Object, data };

            protocol.OnMessage += (s, e) =>
            {
                result = e;
                callbackExecuted = true;
                callbackCompleted.Set();
            };

            connectionMock.Raise(e => e.DataReceived += null, args);

            bool signaled = callbackCompleted.WaitOne(TimeSpan.FromSeconds(5));

            if (callbackExecuted)
            {
                Assert.That(result?.Interface, Is.EqualTo(InterfaceType.System));
                Assert.That(result?.Type, Is.EqualTo(MessageType.GetStatus));
            }
            else
            {
                Assert.Fail("Event DataReceived not triggered");
            }
        }
    }
}
