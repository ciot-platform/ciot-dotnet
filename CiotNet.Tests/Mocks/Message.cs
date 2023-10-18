using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;

namespace CiotNet.Tests.Mocks
{
    public class MessageDtoMocks
    {
        public static readonly MessageDto Mock = new(MessageType.GetStatus, InterfaceType.System);

        public static readonly byte[] MockData = new byte[] {
            0x04, 0x01, 0x00
        };
    }
}
