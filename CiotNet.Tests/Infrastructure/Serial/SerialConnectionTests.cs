using CiotNetNS.Domain.Abstractions;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Infrastructure.Serial;
using CiotNetNS.Shared;
using Moq;

namespace CiotNet.Tests.Infrastructure.Serial
{
    internal class SerialConnectionTests
    {
        readonly Mock<ISerialScanner> scannerMock = new();
        readonly Mock<ISerialWrapper> serialMock = new();
        SerialConnection connection;

        [SetUp]
        public void SetUp()
        {
            connection = new SerialConnection(serialMock.Object, scannerMock.Object);
        }

        [Test]
        public async Task ConnectTestSuccessAsync()
        {
            scannerMock.Setup(e => e.Scan()).Returns(Task.FromResult(Result.Success()));
            var result = await connection.Connect();
            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public async Task ConnectTestExceptionAsync()
        {
            scannerMock.Setup(e => e.Scan()).Throws<InvalidOperationException>();
            var result = await connection.Connect();
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.Exception));
            });
        }

        [Test]
        public async Task ConnectTestErrorAsync()
        {
            scannerMock.Setup(e => e.Scan()).Returns(Task.FromResult(new Result(ErrorCode.NotFound, "NotFound")));
            var result = await connection.Connect();
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.NotFound));
            });
        }

        [Test]
        public void DisconnectTestSuccess()
        {
            serialMock.Setup(e => e.Close());
            serialMock.Setup(e => e.IsOpen).Returns(false);
            var result = connection.Disconnect();
            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void DisconnectTestException()
        {
            serialMock.Setup(e => e.Close()).Throws<InvalidOperationException>();
            var result = connection.Disconnect();
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.Exception));
            });
        }

        [Test]
        public void DisconnectTestError()
        {
            serialMock.Setup(e => e.IsOpen).Returns(true);
            var result = connection.Disconnect();
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.ConnectError));
            });
        }

        [Test]
        public void SendDataTestSuccess()
        {
            serialMock.Setup(e => e.Write(It.IsAny<byte[]>()));
            serialMock.Setup(e => e.IsOpen).Returns(true);
            var result = connection.SendData(new byte[] { 0x01 });
            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void SendDataTestException()
        {
            serialMock.Setup(e => e.Write(It.IsAny<byte[]>())).Throws<InvalidOperationException>();
            serialMock.Setup(e => e.IsOpen).Returns(true);
            var result = connection.SendData(new byte[] { 0x01 });
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.Exception));
            });
        }

        [Test]
        public void SendDataTestPortClosed()
        {
            serialMock.Setup(e => e.Write(It.IsAny<byte[]>())).Throws<InvalidOperationException>();
            serialMock.Setup(e => e.IsOpen).Returns(false);
            var result = connection.SendData(new byte[] { 0x01 });
            Assert.Multiple(() =>
            {
                Assert.That(result.Failed, Is.True);
                Assert.That(result.Error, Is.EqualTo(ErrorCode.SerialPortClosed));
            });
        }
    }
}
