using CiotNetNS.Domain.Abstractions;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Infrastructure.Serial;
using Moq;

namespace CiotNet.Tests.Infrastructure.Serial
{
    internal class SerialScanTests
    {
        Mock<IProtocol> protocolMock = new();
        Mock<ISerialWrapper> serialMock = new();
        SerialScan serialScan;

        [SetUp]
        public void SetUp()
        {
            serialScan = new SerialScan(protocolMock.Object, serialMock.Object);
        }

        [Test]
        public void TestScanSuccess()
        {

        }

        [Test]
        public void TestScanNotFound()
        {

        }
    }
}
