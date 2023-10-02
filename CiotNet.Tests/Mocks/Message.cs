using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;

namespace CiotNet.Tests.Mocks
{
    public class MessageDtoMocks
    {
        public static MessageDto Mock = new MessageDto(InterfaceType.System, MessageType.GetStatus);

        public static byte[] MockData = new byte[] {
            0x01, 0x04
        };
    }
}
