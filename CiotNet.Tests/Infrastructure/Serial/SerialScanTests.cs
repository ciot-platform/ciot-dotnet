using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Abstractions;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Infrastructure.Serial;
using Moq;

namespace CiotNet.Tests.Infrastructure.Serial
{
    internal class SerialScanTests
    {
        private readonly Mock<IProtocol> protocolMock = new();
        private readonly Mock<ISerialWrapper> serialMock = new();
        private SerialScan scanner;

        [SetUp]
        public void SetUp()
        {
            scanner = new SerialScan(protocolMock.Object, serialMock.Object);
        }

        [Test]
        public async Task TestScanSuccessAsync()
        {
            var callbackExecuted = false;
            var callbackCompleted = new ManualResetEvent(false);
            var expectedMessage = new MessageDto(MessageType.GetStatus, InterfaceType.System);
            var args = new object[] { protocolMock.Object, expectedMessage }; 
            var port = "";

            serialMock.Setup(e => e.GetPorts()).Returns(new string[] { "COM1", "COM2", "COM3" });
            serialMock.Setup(e => e.Close()).Callback(() => { });
            serialMock.Setup(e => e.Open()).Callback(() => { });
            
            serialMock.SetupSet(e => e.Port = It.IsAny<string>()).Callback<string>(value => port = value);
            serialMock.SetupGet(e => e.Port).Returns(port);

            protocolMock.Setup(e => e.SendMessage(It.IsAny<MessageDto>())).Callback(() =>
            {
                if(port == "COM2")
                {
                    protocolMock.Raise(e => e.OnMessage += null, args);
                }
            });

            scanner.OnConnection += (s, e) =>
            {
                callbackExecuted = true;
                callbackCompleted.Set();
            };

            var resultAsync = scanner.Scan();

            bool signaled = callbackCompleted.WaitOne(TimeSpan.FromSeconds(5));

            if (callbackExecuted)
            {
                var result = await resultAsync;
                Assert.Multiple(() =>
                {
                    Assert.That(result.Ok, Is.True);
                    Assert.That(port, Is.EqualTo("COM2"));
                });
            }
            else
            {
                Assert.Fail("Event DataReceived not triggered");
            }
        }

        [Test]
        public async Task TestScanNotFoundAsync()
        {
            var callbackExecuted = false;
            var callbackCompleted = new ManualResetEvent(false);
            var expectedMessage = new MessageDto(MessageType.GetStatus, InterfaceType.System);
            var args = new object[] { protocolMock.Object, expectedMessage };
            var port = "";

            serialMock.Setup(e => e.GetPorts()).Returns(new string[] { "COM1", "COM2", "COM3" });
            serialMock.Setup(e => e.Close()).Callback(() => { });
            serialMock.Setup(e => e.Open()).Callback(() => { });

            serialMock.SetupSet(e => e.Port = It.IsAny<string>()).Callback<string>(value => port = value);
            serialMock.SetupGet(e => e.Port).Returns(port);

            protocolMock.Setup(e => e.SendMessage(It.IsAny<MessageDto>())).Callback(() =>
            {
                if (port == "COM4")
                {
                    protocolMock.Raise(e => e.OnMessage += null, args);
                }
            });

            scanner.OnConnection += (s, e) =>
            {
                callbackExecuted = true;
                callbackCompleted.Set();
            };

            var resultAsync = scanner.Scan();

            bool signaled = callbackCompleted.WaitOne(TimeSpan.FromSeconds(5));

            var result = await resultAsync;
            Assert.Multiple(() =>
            {
                Assert.That(callbackExecuted, Is.False);
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.NotFound));
            });
        }
    }
}
